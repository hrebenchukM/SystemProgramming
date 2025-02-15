using System;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using System.Diagnostics;


namespace MutexSemaphore
{
    /*
        мьютекс - примитив синхронизации, который также может использоваться в межпроцессорной синхронизации. 
      public Mutex(
            bool initiallyOwned, - Значение true для предоставления вызывающему потоку начального владения мьютексом; в противном случае — false.
            string name - Имя объекта Mutex. Если значение равно null, то у объекта Mutex нет имени.
            out bool createdNew - При возврате из данного метода содержит логическое значение, равное true, 
                     если был создан новый мьютекс; false, если была получена ссылка на существующий мютекс. 
                     Этот параметр передается неинициализированным. 
              )

             public virtual bool WaitOne(); - ожидает переход мьютекса в сигнальное состояние. 
             public void ReleaseMutex(); - переводит мьютекс из несигнального в сигнальное состояние.
             public static Mutex OpenExisting(string name); - возвращает ссылку на существующий мьютекс. 
             Если его нет, то появляется WaitHandleCannotBeOpenedException
            * public virtual void Close(); - закрывает дескриптор открытых мьютексов.
        */
    public partial class Form1 : Form
    {
        Random rnd = new Random();
        public SynchronizationContext uiContext;
        //Task[] arraytasks = new Task[15];
        public Form1()
        {
            InitializeComponent();
            uiContext = SynchronizationContext.Current;
        }

     
    

        void ThreadFunction1()
        {
            bool CreatedNew;
            // Создаём мьютекс 
            Mutex mutex = new Mutex(false, "DB744E26-72C1-4F2A-8BF8-5C31980953C7", out CreatedNew);
            mutex.WaitOne(); // Захватываем мьютекс
            uiContext.Send(d => label1.Text = "Поток захватил мьютекс! Будем генерировать числа", null);
            // Сразу запускаем второй 
            Task tsk2 = Task.Factory.StartNew(() => ThreadFunction2(mutex));
            GeneratorOfNumbers();
            mutex.ReleaseMutex(); // Освобождаем мьютекс
        }

        void ThreadFunction2(Mutex mutex)
        {
          
                // Ожидаем переход мьютекса в сигнальное состояние
                uiContext.Send(d => label2.Text = "Ожидаем переход мьютекса в сигнальное состояние", null);
                mutex.WaitOne();// Захватываем мьютекс
                uiContext.Send(d => label2.Text = "Мьютекс свободен! Будем искать простые элементы.", null);
            
                // Сразу запускаем третий 
                Task tsk3 = Task.Factory.StartNew(() => ThreadFunction3(mutex));
                PrimeOfNumbers();
                //Переводим мьютекс в сигнальное состояние
                mutex.ReleaseMutex(); // Освобождаем мьютекс
        }
        void ThreadFunction3(Mutex mutex)
        {
          
                // Ожидаем переход мьютекса в сигнальное состояние
                uiContext.Send(d => label3.Text = "Ожидаем переход мьютекса в сигнальное состояние", null);
                mutex.WaitOne();// Захватываем мьютекс
                uiContext.Send(d => label3.Text = "Мьютекс свободен! Будет искать простые элементы заканчивающиеся на 7.", null);
                PrimeOfNumbersEndSeven();
                //Переводим мьютекс в сигнальное состояние
                mutex.ReleaseMutex(); // Освобождаем мьютекс

        }

        private void Start_Click(object sender, EventArgs e)
        {
            try
            {
                label1.Text = "";
                label2.Text = "";
                label3.Text = "";
                Task  tsk1 = Task.Factory.StartNew(ThreadFunction1);
                //arraytasks[1] = Task.Factory.StartNew(ThreadFunction4);
                //arraytasks[2] = arraytasks[1].ContinueWith(ThreadFunction5);
                //arraytasks[3] = arraytasks[2].ContinueWith(ThreadFunction6);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        void GeneratorOfNumbers()
        {
            try
            {
                FileStream file2 = new FileStream("../../array.dat", FileMode.Create, FileAccess.Write);
                BinaryWriter writer = new BinaryWriter(file2);
                int range = rnd.Next(1000);
                for (int i = 0; i < 200000000; i++)
                {
                    int n = rnd.Next(range);
                    writer.Write(n);
                }
                writer.Close();
                file2.Close();
                uiContext.Send(d => label1.Text = "Файл с числовыми данными создан!", null);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }


        void PrimeOfNumbers()
        {
            try
            {
                FileStream file = new FileStream("../../array.dat", FileMode.Open, FileAccess.Read);
                BinaryReader reader = new BinaryReader(file, Encoding.UTF8);

            
                int[] ar = new int[file.Length / sizeof(int)];

                for (int i = 0; i < ar.Length; i++)
                {
                    ar[i] = reader.ReadInt32();
                }

                reader.Close();
                file.Close();


                FileStream file2 = new FileStream("../../prime.dat", FileMode.Create, FileAccess.Write);
                BinaryWriter writer = new BinaryWriter(file2, Encoding.UTF8);

                for (int i = 0; i < ar.Length; i++)
                {
                    int num = ar[i];

                    if (num > 1)
                    {
                        bool prime = true;
                        for (int j = 2; j <= Math.Sqrt(num); j++)
                        {
                            if (num % j == 0)
                            {
                                prime = false;
                                break;
                            }
                        }
                        if (prime)
                        {
                            writer.Write(num);
                        }
                    }
                }
                writer.Close();
                file2.Close();

                uiContext.Send( d => label2.Text = "Файл с простыми числами создан!" , null);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        void PrimeOfNumbersEndSeven()
        {
            try
            {
                FileStream file = new FileStream("../../prime.dat", FileMode.Open, FileAccess.Read);
                BinaryReader reader = new BinaryReader(file, Encoding.UTF8);
                FileStream file2 = new FileStream("../../primeendseven.dat", FileMode.Create, FileAccess.Write);
                BinaryWriter writer = new BinaryWriter(file2, Encoding.UTF8);

                try
                {
                    while (true)
                    {
                        int num = reader.ReadInt32();
                        if (num % 10 == 7) 
                        {
                            writer.Write(num);
                        }
                    }
                }
                catch (EndOfStreamException)
                {
                    //Достигнут конец файла
                }
                uiContext.Send(d => label3.Text = "Файл с простыми числами, заканчивающимися на 7, создан!", null);
                reader.Close();
                file.Close();
                writer.Close();
                file2.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

    }
}
