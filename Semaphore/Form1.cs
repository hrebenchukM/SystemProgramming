using System;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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
        public SynchronizationContext uiContext;
        Task[] arraytasks = new Task[15];
        public Form1()
        {
            InitializeComponent();
            uiContext = SynchronizationContext.Current;

        }




        void ThreadAct(Object obj)
        {
            try
            {
                ListBox txt = (ListBox)obj;
                Semaphore sem = Semaphore.OpenExisting("1A9191BF-AA26-46E1-BB85-BDA396BC6469");
                sem.WaitOne();
                int i = 0;
                while (i++ < 500)
                {
                    uiContext.Send(d => txt.Text = i.ToString(), null);
                    System.Threading.Thread.Sleep(10);
                }
                sem.Release();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // пример на семафор
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                arraytasks[1] = Task.Factory.StartNew(ThreadAct, listBox1);
                arraytasks[2] = Task.Factory.StartNew(ThreadAct, listBox2);
                arraytasks[3] = Task.Factory.StartNew(ThreadAct, listBox3);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

