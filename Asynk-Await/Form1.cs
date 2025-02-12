using System;

namespace Asynk_Await
{
    public partial class Form1 : Form
    {
        public SynchronizationContext uiContext;//������������ ��� �������������� ������ � UI (����� ��������� ��������� �� ������)
        CancellationTokenSource cts;

        public Form1()
        {
            InitializeComponent();
            // ������� �������� ������������� ��� �������� ������ 
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
                MessageBox.Show("�������� ��� ������ ����", "������", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            if (!radioButtonEncrypt.Checked && !radioButtonDecrypt.Checked)
            {
                MessageBox.Show("�� ������ �����: ���������� ��� �����������", "������", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            progressBar1.Value = 0;

         
            cts = new CancellationTokenSource();
            try
            {
                Task t1;
                if (radioButtonEncrypt.Checked)
                {
                    // �����������
                    t1 = EncryptDecrypt(source, key, cts.Token, true);
                }
                else
                {
                    // ������������
                    t1 = EncryptDecrypt(source, key, cts.Token, false);
                }

                await t1;
            }
            catch (OperationCanceledException ex)
            {
                MessageBox.Show("������ ��������.", "������", MessageBoxButtons.OK, MessageBoxIcon.Information);

                progressBar1.Value = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show("������: " + ex.Message);
            }
            finally
            {
                cts.Dispose();
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            cts.Cancel(); // ������ ����������� ��������
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



              


                long allBytes = fileBytes.Length;  // ����� ������ �����
                long bytesProc = 0;  // ���-�� ������������� ����

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
                          // ����������: XOR � ������
                          fileBytes[i] ^= keyBytes[i % keyBytes.Length];
                        }
                        else
                        {
                           // �����������: XOR � ��� �� ������
                           fileBytes[i] ^= keyBytes[i % keyBytes.Length];
                        }
                            bytesProc++;

                            // uiContext.Send ���������� ���������� ��������� � �������� �������������
                            // SendOrPostCallback - ������� ��������� �����, ���������� ��� �������� ��������� � �������� �������������. 
                            uiContext.Send(d => progressBar1.Value = (int)((bytesProc * 100) / allBytes) /* ���������� ������� SendOrPostCallback */, null);
                            Thread.Sleep(10);
                     }
                }, token);



                receiver_file = new FileStream(source, FileMode.Open, FileAccess.Write);
                writer = new BinaryWriter(receiver_file);


                writer.Write(fileBytes);
                receiver_file.Close();
                writer.Close();




                MessageBox.Show("���� ������� ���������!", "������", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (OperationCanceledException ex)
            {
                writer?.Close();
                reader?.Close();
                source_file?.Close();
                receiver_file?.Close();

               
                MessageBox.Show("������ ��������.", "������", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("������: " + ex.Message);
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
