using System;

namespace Asynk_Await
{
    public partial class Form1 : Form
    {
        public SynchronizationContext uiContext;//используется для взаимодействия потока с UI (чтобы обновлять интерфейс из потока)
        CancellationTokenSource cts;

        public Form1()
        {
            InitializeComponent();
            // Получим контекст синхронизации для текущего потока 
            uiContext = SynchronizationContext.Current;

        }

        private void FileButtton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFileDialog.FileName;
            }
        }

        private async void StartButton_Click(object sender, EventArgs e)
        {

            string source = textBox1.Text;
            string key = textBox2.Text;
            if (source == null || key == null)
            {
                MessageBox.Show("Источник или пароль пуст", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            if (!radioButtonEncrypt.Checked && !radioButtonDecrypt.Checked)
            {
                MessageBox.Show("Не выбран режим: Зашифровка или Расшифровка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            progressBar1.Value = 0;

         
            cts = new CancellationTokenSource();
            try
            {
                Task t1;
                if (radioButtonEncrypt.Checked)
                {
                    // Зашифровать
                    t1 = EncryptDecrypt(source, key, cts.Token, true);
                }
                else
                {
                    // Расшифровать
                    t1 = EncryptDecrypt(source, key, cts.Token, false);
                }

                await t1;
            }
            catch (OperationCanceledException ex)
            {
                MessageBox.Show("Задача отменена.", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);

                progressBar1.Value = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
            finally
            {
                cts.Dispose();
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            cts.Cancel(); // отмена асинхронной операции
        }


         async Task EncryptDecrypt(string source,string key, CancellationToken token,bool encdec)
         {
            byte[] keyBytes = System.Text.Encoding.UTF8.GetBytes(key);
            byte[] fileBytes;

            FileStream source_file = null;
            FileStream receiver_file = null;
            BinaryReader reader = null;
            BinaryWriter writer = null;
          
            try
            {

                source_file = new FileStream(source, FileMode.Open, FileAccess.Read);
                reader = new BinaryReader(source_file);
                fileBytes = reader.ReadBytes((int)source_file.Length);

                source_file.Close();
                reader.Close();



              


                long allBytes = fileBytes.Length;  // общий размер файла
                long bytesProc = 0;  // Кол-во обработтанных байт

                uiContext.Send(d => progressBar1.Minimum = 0, null);
                uiContext.Send(d => progressBar1.Maximum = 100, null);
                uiContext.Send(d => progressBar1.Value = 0, null);

                await Task.Run( () =>
                {
                     for (int i = 0; i < fileBytes.Length; i++)
                     {
                        token.ThrowIfCancellationRequested();
 
                        if (encdec)
                        {
                          // Зашифровка: XOR с ключом
                          fileBytes[i] ^= keyBytes[i % keyBytes.Length];
                        }
                        else
                        {
                           // Расшифровка: XOR с тем же ключом
                           fileBytes[i] ^= keyBytes[i % keyBytes.Length];
                        }
                            bytesProc++;

                            // uiContext.Send отправляет синхронное сообщение в контекст синхронизации
                            // SendOrPostCallback - делегат указывает метод, вызываемый при отправке сообщения в контекст синхронизации. 
                            uiContext.Send(d => progressBar1.Value = (int)((bytesProc * 100) / allBytes) /* Вызываемый делегат SendOrPostCallback */, null);
                            Thread.Sleep(10);
                     }
                }, token);



                receiver_file = new FileStream(source, FileMode.Open, FileAccess.Write);
                writer = new BinaryWriter(receiver_file);


                writer.Write(fileBytes);
                receiver_file.Close();
                writer.Close();




                MessageBox.Show("Файл успешно обработан!", "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (OperationCanceledException ex)
            {
                writer?.Close();
                reader?.Close();
                source_file?.Close();
                receiver_file?.Close();

               
                MessageBox.Show("Задача отменена.", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
            finally
            {

                writer?.Close();
                reader?.Close();
                source_file?.Close();
                receiver_file?.Close();
            }


         }


    }
}
