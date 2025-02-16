using System.Diagnostics;

namespace SystemProcess
{
    public partial class Form1 : Form
    {
        public SynchronizationContext uiContext;

        public Form1()
        {
            InitializeComponent();
            // ������� �������� ������������� ��� �������� ������ 
            uiContext = SynchronizationContext.Current;

            UpdateList();

        }

        private async void UpdateButton_Click(object sender, EventArgs e)
        {
            await UpdateList();
        }


        private async Task UpdateList()
        {
            await Task.Run(() =>
            {
                try
                {
                    uiContext.Send(d => listBox1.Items.Clear(), null);
                    Process[] lp = Process.GetProcesses();
                    foreach (Process p in lp) // ������ ���� ���������, ���������� � �������
                    {
                        MyProcess myProcess = new MyProcess(p);

                        // uiContext.Send ���������� ���������� ��������� � �������� �������������
                        // SendOrPostCallback - ������� ��������� �����, ���������� ��� �������� ��������� � �������� �������������. 
                        uiContext.Send(d => listBox1.Items.Add(myProcess) /* ���������� ������� SendOrPostCallback */,
                            null /* ������, ���������� �������� */);// ������� ��� ���������� ��������
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });
        }


        private void EndButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBox1.SelectedItem != null)
                {
                    //// �������� ��������� ��������� Notepad
                    //Process[] procs = Process.GetProcessesByName("Notepad");
                    //List<MyProcess> myProcesses = new List<MyProcess>();

                    //foreach (Process p in procs)
                    //{
                    //    MyProcess myProcess = new MyProcess(p);
                    //    myProcesses.Add(myProcess);
                    //}
                    //MessageBox.Show("����� : " + myProcesses.Count.ToString());


                    //foreach (MyProcess myProcess in myProcesses)
                    //{
                    //    myProcess.ProcessRef.Kill(); // ������������� �������
                    //}


                    MyProcess myProcess = (MyProcess)listBox1.SelectedItem;
                    myProcess.ProcessRef.Kill(); // ������������� �������
                

                }
                else
                {
                    MessageBox.Show("�������� ������� ��� ����������.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

       

        private void CreateButton_Click(object sender, EventArgs e)
        {
            try
            {
                //// ������� ����� �������
                //Process proc = new Process();
                //// ��������� �������
                //proc.StartInfo.FileName = "Notepad.exe";
                //proc.StartInfo.Arguments = Application.ExecutablePath;
                //MyProcess myProcess = new MyProcess(proc);
                //myProcess.ProcessRef.Start();



                string fileName = textBox1.Text;
                // ������� ����� �������
                if (!string.IsNullOrWhiteSpace(fileName))
                {
                    Process proc = new Process();
                    proc.StartInfo.FileName = fileName;
                    proc.StartInfo.Arguments = Application.ExecutablePath;
                    proc.Start();
                }
                else
                {
                    MessageBox.Show("������� ������� ��� ��������,��������: calc.exe");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }




    }
}
