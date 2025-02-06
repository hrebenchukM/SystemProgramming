using System.Threading.Tasks;

namespace TPL_copy_file
{
    public partial class Form1 : Form
    {
        public SynchronizationContext uiContext;//используется для взаимодействия потока с UI (чтобы обновлять интерфейс из потока)

        public Form1()
        {
            InitializeComponent();
            // Получим контекст синхронизации для текущего потока 
            uiContext = SynchronizationContext.Current;

        }


        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFileDialog.FileName;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = folderDialog.SelectedPath; 
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string source = textBox1.Text;
            string receiver = textBox2.Text;
            if (source == null || receiver == null)
            {
                MessageBox.Show("Источник или Приемник пуст", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            progressBar1.Value = 0;

            //Task - асинхронная операция - элементарная единица исполнения
            //Task tsk1 = new Task(Task1);
            Task tsk1 = new Task(() => Task1());


            try
            {
                //Start запускает Task.
                tsk1.Start();
                //Wait ожидает завершения выполнения объекта Task.
                //tsk1.Wait();
                MessageBox.Show("Задача выполнена!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

                // Dispose - освобождение ресурсов, используемых задачами
                //tsk1.Dispose();

            }


        }


        void Task1()
        {
            CopyBinaryFile(textBox1.Text, Path.Combine(textBox2.Text, Path.GetFileName(textBox1.Text)));
        }

        void CopyBinaryFile(string source, string receiver)
        {
            try
            {
                uiContext.Send(d => progressBar1.Minimum = 0, null);
                uiContext.Send(d => progressBar1.Maximum = 100, null);
                uiContext.Send(d => progressBar1.Value = 0, null);

                byte[] buff = new byte[4096];
                long allBytes = new FileInfo(source).Length;  // общий размер файла
                long bytesCopy = 0;  // Кол-во скопированных байт


                FileStream source_file = new FileStream(source, FileMode.Open, FileAccess.Read);
                FileStream receiver_file = new FileStream(receiver, FileMode.Create, FileAccess.Write);
                BinaryReader reader = new BinaryReader(source_file);
                BinaryWriter writer = new BinaryWriter(receiver_file);

                int nIterations = (int)(allBytes / 4096);
                if (allBytes % 4096 != 0)
                {
                    nIterations++;
                }


                for (int i = 0; i < nIterations; i++)
                {
                 
                    int bytesRead = reader.Read(buff, 0, 4096);
                    writer.Write(buff, 0, bytesRead);  // Записываем прочитанные данные в файл 
                    bytesCopy += bytesRead;

                    // uiContext.Send отправляет синхронное сообщение в контекст синхронизации
                    // SendOrPostCallback - делегат указывает метод, вызываемый при отправке сообщения в контекст синхронизации. 
                    uiContext.Send(d => progressBar1.Value = (int)((bytesCopy * 100) / allBytes) /* Вызываемый делегат SendOrPostCallback */, null);
                   
                }

                writer.Close();
                reader.Close();
                source_file.Close();
                receiver_file.Close();

                MessageBox.Show("Файл успешно скопирован!");

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

    }
}