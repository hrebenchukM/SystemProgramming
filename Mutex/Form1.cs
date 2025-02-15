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
        ������� - �������� �������������, ������� ����� ����� �������������� � ��������������� �������������. 
      public Mutex(
            bool initiallyOwned, - �������� true ��� �������������� ����������� ������ ���������� �������� ���������; � ��������� ������ � false.
            string name - ��� ������� Mutex. ���� �������� ����� null, �� � ������� Mutex ��� �����.
            out bool createdNew - ��� �������� �� ������� ������ �������� ���������� ��������, ������ true, 
                     ���� ��� ������ ����� �������; false, ���� ���� �������� ������ �� ������������ ������. 
                     ���� �������� ���������� ��������������������. 
              )

             public virtual bool WaitOne(); - ������� ������� �������� � ���������� ���������. 
             public void ReleaseMutex(); - ��������� ������� �� ������������� � ���������� ���������.
             public static Mutex OpenExisting(string name); - ���������� ������ �� ������������ �������. 
             ���� ��� ���, �� ���������� WaitHandleCannotBeOpenedException
            * public virtual void Close(); - ��������� ���������� �������� ���������.
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
            // ������ ������� 
            Mutex mutex = new Mutex(false, "DB744E26-72C1-4F2A-8BF8-5C31980953C7", out CreatedNew);
            mutex.WaitOne(); // ����������� �������
            uiContext.Send(d => label1.Text = "����� �������� �������! ����� ������������ �����", null);
            // ����� ��������� ������ 
            Task tsk2 = Task.Factory.StartNew(() => ThreadFunction2(mutex));
            GeneratorOfNumbers();
            mutex.ReleaseMutex(); // ����������� �������
        }

        void ThreadFunction2(Mutex mutex)
        {
          
                // ������� ������� �������� � ���������� ���������
                uiContext.Send(d => label2.Text = "������� ������� �������� � ���������� ���������", null);
                mutex.WaitOne();// ����������� �������
                uiContext.Send(d => label2.Text = "������� ��������! ����� ������ ������� ��������.", null);
            
                // ����� ��������� ������ 
                Task tsk3 = Task.Factory.StartNew(() => ThreadFunction3(mutex));
                PrimeOfNumbers();
                //��������� ������� � ���������� ���������
                mutex.ReleaseMutex(); // ����������� �������
        }
        void ThreadFunction3(Mutex mutex)
        {
          
                // ������� ������� �������� � ���������� ���������
                uiContext.Send(d => label3.Text = "������� ������� �������� � ���������� ���������", null);
                mutex.WaitOne();// ����������� �������
                uiContext.Send(d => label3.Text = "������� ��������! ����� ������ ������� �������� ��������������� �� 7.", null);
                PrimeOfNumbersEndSeven();
                //��������� ������� � ���������� ���������
                mutex.ReleaseMutex(); // ����������� �������

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
                uiContext.Send(d => label1.Text = "���� � ��������� ������� ������!", null);
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

                uiContext.Send( d => label2.Text = "���� � �������� ������� ������!" , null);
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
                    //��������� ����� �����
                }
                uiContext.Send(d => label3.Text = "���� � �������� �������, ���������������� �� 7, ������!", null);
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
