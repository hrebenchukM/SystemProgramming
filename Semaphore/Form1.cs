using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SemaphoreHW
{
    /*
     Семафор ограничивает число потоков, которые могут одновременно получать доступ к ресурсу или пулу ресурсов. 
      public Semaphore(
         int initialCount,  - Начальное количество запросов семафора, которое может быть удовлетворено одновременно.
         int maximumCount,  - Максимальное количество запросов семафора, которое может быть удовлетворено одновременно.
         string name – имя  - Имя объекта именованного системного семафора.
     );

     public int Release( - Выходит из семафора указанное число раз и возвращает последнее значение счетчика.
         int releaseCount – Количество требуемых выходов из семафора.
     );

     public virtual bool WaitOne();  ожидает переход семафора в сигнальное состояние

     public static Semaphore OpenExisting( - Открытие существующего именованного семафора.
         string name
     );
     */
    public partial class Form1 : Form
    {
        private SynchronizationContext uiContext;
        private Semaphore semaphore;
        private int threadCount = 0;
        private int maxSlots = 0;

        private CancellationTokenSource[] ctsArr = new CancellationTokenSource[50];
        private Task[] tasksArr = new Task[50];

        public Form1()
        {
            InitializeComponent();
            uiContext = SynchronizationContext.Current;
            semaphore = new Semaphore(maxSlots, 50, "1A9191BF-AA26-46E1-BB85-BDA396BC6469");
        }

        private void ThreadAct(int tN, CancellationToken token)
        {
            string tName = "Поток " + tN;
            int sec = 0;
            try
            {
                semaphore.WaitOne();
                uiContext.Send(d =>
                {
                    listBox2.Items.Remove(tName);
                    listBox1.Items.Add(tName + " -> 0");
                }, null);


                while (!token.IsCancellationRequested)
                {
                    sec++;
                    uiContext.Send(d =>
                    {
                        for (int i = 0; i < listBox1.Items.Count; i++)
                        {
                            if (listBox1.Items[i].ToString().StartsWith(tName + " ->"))
                            {
                                listBox1.Items[i] = tName + " -> " + sec;
                                break;
                            }
                        }
                    }, null);
                    Thread.Sleep(1000);
                }
            }
            catch (OperationCanceledException)
            {
                MessageBox.Show("Поток отменён.");
            }
          
       
        }

       
        private void button1_Click(object sender, EventArgs e)
        {
            threadCount++;
            string tName = "Поток " + threadCount;
            ctsArr[threadCount - 1] = new CancellationTokenSource();
            listBox3.Items.Add(tName);
        }

   
        private void listBox3_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBox3.SelectedItem == null)
                return;
            string tName = listBox3.SelectedItem.ToString();
            listBox3.Items.Remove(tName);
            listBox2.Items.Add(tName);

            string[] parts = tName.Split(' ');
            if (int.TryParse(parts[1], out int tNum))
            {
               
                Task tsk = Task.Run(() => ThreadAct(tNum, ctsArr[tNum - 1].Token), ctsArr[tNum - 1].Token);
                tasksArr[tNum - 1] = tsk;
            }
        }

      
        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBox1.SelectedItem == null)
                return;
            string tName = listBox1.SelectedItem.ToString();
            string[] parts = listBox1.SelectedItem.ToString().Split(' ');
            if (int.TryParse(parts[1], out int tNum))
            {
               
                ctsArr[tNum - 1].Cancel();
                semaphore.Release();
            }
            listBox1.Items.Remove(tName);
        }

        
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            int newMaxSlots = (int)numericUpDown1.Value;

            if (newMaxSlots > maxSlots)
            {
                semaphore.Release(newMaxSlots - maxSlots);
            }
            else if (newMaxSlots < maxSlots)
            {
                for (int i = 0; i < maxSlots - newMaxSlots; i++)
                {
                    if (listBox1.Items.Count > 0)
                    {
                        string[] parts = listBox1.Items[0].ToString().Split(' ');
                        if (int.TryParse(parts[1], out int tNum))
                        {
                            ctsArr[tNum - 1].Cancel();
                        }
                        listBox1.Items.RemoveAt(0);
                    }
                }
            }
            maxSlots = newMaxSlots;
        }
    }
}
