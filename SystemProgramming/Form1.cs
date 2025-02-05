using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;


namespace SystemProgramming
{
    public partial class Form1 : Form
    {
        ManualResetEvent event_for_suspend1 = new ManualResetEvent(true);//управляет паузой в потоке (true означает, что поток может работать)
        ManualResetEvent event_for_stop1 = new ManualResetEvent(false);// сигнализирует о завершении (false - поток работает, true - нужно остановить)
        ManualResetEvent event_for_suspend2 = new ManualResetEvent(true);//управляет паузой в потоке (true означает, что поток может работать)
        ManualResetEvent event_for_stop2 = new ManualResetEvent(false);// сигнализирует о завершении (false - поток работает, true - нужно остановить)
        ManualResetEvent event_for_suspend3 = new ManualResetEvent(true);//управляет паузой в потоке (true означает, что поток может работать)
        ManualResetEvent event_for_stop3 = new ManualResetEvent(false);// сигнализирует о завершении (false - поток работает, true - нужно остановить)

        public SynchronizationContext uiContext;//используется для взаимодействия потока с UI (чтобы обновлять интерфейс из потока)
        public Form1()
        {
            InitializeComponent();
            // Получим контекст синхронизации для текущего потока 
            uiContext = SynchronizationContext.Current;
        }
        private void ThreadFunk1()
        {

            try
            {
                uiContext.Send(d => progressBar1.Minimum = 0, null);
                uiContext.Send(d => progressBar1.Maximum = (int)d, 230);
                uiContext.Send(d => progressBar1.Value = 0, null);

                while (!event_for_stop1.WaitOne(0)) //будет выполняться, пока не будет установлен сигнал для завершения работы потока
                {
                    event_for_suspend1.WaitOne(); //Проверяем, нужно ли приостановить выполнение потока

                    for (int i = 0; i < 230; i++)
                    {
                        Thread.Sleep(50); 

                        if (event_for_stop1.WaitOne(0)) // Проверяем, не был ли дан сигнал о завершении работы потока
                            break;

                        // uiContext.Send отправляет синхронное сообщение в контекст синхронизации
                        // SendOrPostCallback - делегат указывает метод, вызываемый при отправке сообщения в контекст синхронизации. 
                        uiContext.Send(d => progressBar1.Value = (int)d /* Вызываемый делегат SendOrPostCallback */,
                            i /* Объект, переданный делегату */); // добавляем в список имя клиента

                        event_for_suspend1.WaitOne(); //Проверяем, нужно ли приостановить выполнение потока
                    }
                }

                uiContext.Send(d => progressBar1.Value = 0, null); 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }



        private void ThreadFunk2()
        {

            try
            {
                uiContext.Send(d => progressBar2.Minimum = 0, null);
                uiContext.Send(d => progressBar2.Maximum = (int)d, 230);
                uiContext.Send(d => progressBar2.Value = 0, null);

                while (!event_for_stop2.WaitOne(0))
                {
                    event_for_suspend2.WaitOne();

                    for (int i = 0; i < 230; i++)
                    {
                        Thread.Sleep(50);

                        if (event_for_stop2.WaitOne(0))
                            break;

                        // uiContext.Send отправляет синхронное сообщение в контекст синхронизации
                        // SendOrPostCallback - делегат указывает метод, вызываемый при отправке сообщения в контекст синхронизации. 
                        uiContext.Send(d => progressBar2.Value = (int)d /* Вызываемый делегат SendOrPostCallback */,
                            i /* Объект, переданный делегату */); // добавляем в список имя клиента

                        event_for_suspend2.WaitOne();
                    }
                }

                uiContext.Send(d => progressBar2.Value = 0, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void ThreadFunk3()
        {

            try
            {
                uiContext.Send(d => progressBar3.Minimum = 0, null);
                uiContext.Send(d => progressBar3.Maximum = (int)d, 230);
                uiContext.Send(d => progressBar3.Value = 0, null);

                while (!event_for_stop3.WaitOne(0))
                {
                    event_for_suspend3.WaitOne();

                    for (int i = 0; i < 230; i++)
                    {
                        Thread.Sleep(50);

                        if (event_for_stop3.WaitOne(0))
                            break;

                        // uiContext.Send отправляет синхронное сообщение в контекст синхронизации
                        // SendOrPostCallback - делегат указывает метод, вызываемый при отправке сообщения в контекст синхронизации. 
                        uiContext.Send(d => progressBar3.Value = (int)d /* Вызываемый делегат SendOrPostCallback */,
                            i /* Объект, переданный делегату */); // добавляем в список имя клиента

                        event_for_suspend3.WaitOne();
                    }
                }

                uiContext.Send(d => progressBar3.Value = 0, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
          
       
            if (checkBox1.Checked)
            {
                checkBox1.Text = "Остановить 1-й поток";
                event_for_stop1.Reset();
                // Создание делегата функции, в которой будет работать новый поток
                ThreadStart MethodThread = new ThreadStart(ThreadFunk1);
                // Создание объекта потока
                Thread th1 = new Thread(MethodThread);
                th1.IsBackground = true;
                // Старт потока
                th1.Start();
            

            }
            else
            {
                checkBox1.Text = "Запустить 1-й поток";
                event_for_stop1.Set();

            }
        }


        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
         
            if (checkBox3.Checked)
            {
                checkBox3.Text = "Остановить 2-й поток";
                event_for_stop2.Reset();
                // Создание делегата функции, в которой будет работать новый поток
                ThreadStart MethodThread = new ThreadStart(ThreadFunk2);
                // Создание объекта потока
                Thread th2 = new Thread(MethodThread);
                th2.IsBackground = true;
                // Старт потока
                th2.Start();

            }
            else
            {
                checkBox3.Text = "Запустить 2-й поток";
                event_for_stop2.Set();
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
    

            if (checkBox5.Checked)
            {
                checkBox5.Text = "Остановить 3-й поток";
                event_for_stop3.Reset();
                // Создание делегата функции, в которой будет работать новый поток
                ThreadStart MethodThread = new ThreadStart(ThreadFunk3);
                // Создание объекта потока
                Thread th3 = new Thread(MethodThread);
                th3.IsBackground = true;
                // Старт потока
                th3.Start();
               

            }
            else
            {
                checkBox5.Text = "Запустить 3-й поток";
                event_for_stop3.Set();
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
         
            if (checkBox2.Checked)
            {
                checkBox2.Text = "Возобновить 1-й поток";
                event_for_suspend1.Reset();
            }
            else
            { 
                checkBox2.Text = "Приостановить 1-й поток";
                event_for_suspend1.Set();
            }
        }

        

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
           
            if (checkBox4.Checked)
            {
                checkBox4.Text = "Возобновить 2-й поток";
                event_for_suspend2.Reset();
            }
            else 
            {
                checkBox4.Text = "Приостановить 2-й поток";
                event_for_suspend2.Set();
            }
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {

          
            if (checkBox6.Checked)
            {
                checkBox6.Text = "Возобновить 3-й поток";
                event_for_suspend3.Reset();
            }
            else
            {
                checkBox6.Text = "Приостановить 3-й поток";
                event_for_suspend3.Set();
            }
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            event_for_stop1.Set();
            event_for_stop2.Set();
            event_for_stop3.Set();
        }

    }
}
