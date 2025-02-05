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
        ManualResetEvent event_for_suspend1 = new ManualResetEvent(true);//��������� ������ � ������ (true ��������, ��� ����� ����� ��������)
        ManualResetEvent event_for_stop1 = new ManualResetEvent(false);// ������������� � ���������� (false - ����� ��������, true - ����� ����������)
        ManualResetEvent event_for_suspend2 = new ManualResetEvent(true);//��������� ������ � ������ (true ��������, ��� ����� ����� ��������)
        ManualResetEvent event_for_stop2 = new ManualResetEvent(false);// ������������� � ���������� (false - ����� ��������, true - ����� ����������)
        ManualResetEvent event_for_suspend3 = new ManualResetEvent(true);//��������� ������ � ������ (true ��������, ��� ����� ����� ��������)
        ManualResetEvent event_for_stop3 = new ManualResetEvent(false);// ������������� � ���������� (false - ����� ��������, true - ����� ����������)

        public SynchronizationContext uiContext;//������������ ��� �������������� ������ � UI (����� ��������� ��������� �� ������)
        public Form1()
        {
            InitializeComponent();
            // ������� �������� ������������� ��� �������� ������ 
            uiContext = SynchronizationContext.Current;
        }
        private void ThreadFunk1()
        {

            try
            {
                uiContext.Send(d => progressBar1.Minimum = 0, null);
                uiContext.Send(d => progressBar1.Maximum = (int)d, 230);
                uiContext.Send(d => progressBar1.Value = 0, null);

                while (!event_for_stop1.WaitOne(0)) //����� �����������, ���� �� ����� ���������� ������ ��� ���������� ������ ������
                {
                    event_for_suspend1.WaitOne(); //���������, ����� �� ������������� ���������� ������

                    for (int i = 0; i < 230; i++)
                    {
                        Thread.Sleep(50); 

                        if (event_for_stop1.WaitOne(0)) // ���������, �� ��� �� ��� ������ � ���������� ������ ������
                            break;

                        // uiContext.Send ���������� ���������� ��������� � �������� �������������
                        // SendOrPostCallback - ������� ��������� �����, ���������� ��� �������� ��������� � �������� �������������. 
                        uiContext.Send(d => progressBar1.Value = (int)d /* ���������� ������� SendOrPostCallback */,
                            i /* ������, ���������� �������� */); // ��������� � ������ ��� �������

                        event_for_suspend1.WaitOne(); //���������, ����� �� ������������� ���������� ������
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

                        // uiContext.Send ���������� ���������� ��������� � �������� �������������
                        // SendOrPostCallback - ������� ��������� �����, ���������� ��� �������� ��������� � �������� �������������. 
                        uiContext.Send(d => progressBar2.Value = (int)d /* ���������� ������� SendOrPostCallback */,
                            i /* ������, ���������� �������� */); // ��������� � ������ ��� �������

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

                        // uiContext.Send ���������� ���������� ��������� � �������� �������������
                        // SendOrPostCallback - ������� ��������� �����, ���������� ��� �������� ��������� � �������� �������������. 
                        uiContext.Send(d => progressBar3.Value = (int)d /* ���������� ������� SendOrPostCallback */,
                            i /* ������, ���������� �������� */); // ��������� � ������ ��� �������

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
                checkBox1.Text = "���������� 1-� �����";
                event_for_stop1.Reset();
                // �������� �������� �������, � ������� ����� �������� ����� �����
                ThreadStart MethodThread = new ThreadStart(ThreadFunk1);
                // �������� ������� ������
                Thread th1 = new Thread(MethodThread);
                th1.IsBackground = true;
                // ����� ������
                th1.Start();
            

            }
            else
            {
                checkBox1.Text = "��������� 1-� �����";
                event_for_stop1.Set();

            }
        }


        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
         
            if (checkBox3.Checked)
            {
                checkBox3.Text = "���������� 2-� �����";
                event_for_stop2.Reset();
                // �������� �������� �������, � ������� ����� �������� ����� �����
                ThreadStart MethodThread = new ThreadStart(ThreadFunk2);
                // �������� ������� ������
                Thread th2 = new Thread(MethodThread);
                th2.IsBackground = true;
                // ����� ������
                th2.Start();

            }
            else
            {
                checkBox3.Text = "��������� 2-� �����";
                event_for_stop2.Set();
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
    

            if (checkBox5.Checked)
            {
                checkBox5.Text = "���������� 3-� �����";
                event_for_stop3.Reset();
                // �������� �������� �������, � ������� ����� �������� ����� �����
                ThreadStart MethodThread = new ThreadStart(ThreadFunk3);
                // �������� ������� ������
                Thread th3 = new Thread(MethodThread);
                th3.IsBackground = true;
                // ����� ������
                th3.Start();
               

            }
            else
            {
                checkBox5.Text = "��������� 3-� �����";
                event_for_stop3.Set();
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
         
            if (checkBox2.Checked)
            {
                checkBox2.Text = "����������� 1-� �����";
                event_for_suspend1.Reset();
            }
            else
            { 
                checkBox2.Text = "������������� 1-� �����";
                event_for_suspend1.Set();
            }
        }

        

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
           
            if (checkBox4.Checked)
            {
                checkBox4.Text = "����������� 2-� �����";
                event_for_suspend2.Reset();
            }
            else 
            {
                checkBox4.Text = "������������� 2-� �����";
                event_for_suspend2.Set();
            }
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {

          
            if (checkBox6.Checked)
            {
                checkBox6.Text = "����������� 3-� �����";
                event_for_suspend3.Reset();
            }
            else
            {
                checkBox6.Text = "������������� 3-� �����";
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
