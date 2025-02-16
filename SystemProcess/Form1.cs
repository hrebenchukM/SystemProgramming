using System.Diagnostics;

namespace SystemProcess
{
    public partial class Form1 : Form
    {
        public SynchronizationContext uiContext;

        public Form1()
        {
            InitializeComponent();
            // Получим контекст синхронизации для текущего потока 
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
                    foreach (Process p in lp) // список всех процессов, запущенных в системе
                    {
                        MyProcess myProcess = new MyProcess(p);

                        // uiContext.Send отправляет синхронное сообщение в контекст синхронизации
                        // SendOrPostCallback - делегат указывает метод, вызываемый при отправке сообщения в контекст синхронизации. 
                        uiContext.Send(d => listBox1.Items.Add(myProcess) /* Вызываемый делегат SendOrPostCallback */,
                            null /* Объект, переданный делегату */);// получим имя очередного процесса
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
                    //// Получаем коллекцию процессов Notepad
                    //Process[] procs = Process.GetProcessesByName("Notepad");
                    //List<MyProcess> myProcesses = new List<MyProcess>();

                    //foreach (Process p in procs)
                    //{
                    //    MyProcess myProcess = new MyProcess(p);
                    //    myProcesses.Add(myProcess);
                    //}
                    //MessageBox.Show("Всего : " + myProcesses.Count.ToString());


                    //foreach (MyProcess myProcess in myProcesses)
                    //{
                    //    myProcess.ProcessRef.Kill(); // останавливаем процесс
                    //}


                    MyProcess myProcess = (MyProcess)listBox1.SelectedItem;
                    myProcess.ProcessRef.Kill(); // останавливаем процесс
                

                }
                else
                {
                    MessageBox.Show("Выберите процесс для завершения.");
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
                //// создаем новый процесс
                //Process proc = new Process();
                //// Запускаем Блокнот
                //proc.StartInfo.FileName = "Notepad.exe";
                //proc.StartInfo.Arguments = Application.ExecutablePath;
                //MyProcess myProcess = new MyProcess(proc);
                //myProcess.ProcessRef.Start();



                string fileName = textBox1.Text;
                // создаем новый процесс
                if (!string.IsNullOrWhiteSpace(fileName))
                {
                    Process proc = new Process();
                    proc.StartInfo.FileName = fileName;
                    proc.StartInfo.Arguments = Application.ExecutablePath;
                    proc.Start();
                }
                else
                {
                    MessageBox.Show("Введите процесс для создания,например: calc.exe");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }




    }
}
