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
            string receiver = encdec ? source + ".xor" : source + ".dec";
            byte[] keyBytes = System.Text.Encoding.UTF8.GetBytes(key);

            FileStream source_file = null;
            FileStream receiver_file = null;
            BinaryReader reader = null;
            BinaryWriter writer = null;
          
            try
            {

                source_file = new FileStream(source, FileMode.Open, FileAccess.Read);
                receiver_file = new FileStream(receiver, FileMode.Create, FileAccess.Write);
                reader = new BinaryReader(source_file);
                writer = new BinaryWriter(receiver_file);


                byte[] buff = new byte[256];
                long allBytes = new FileInfo(source).Length;  // ����� ������ �����
                long bytesProc = 0;  // ���-�� ������������� ����

                uiContext.Send(d => progressBar1.Minimum = 0, null);
                uiContext.Send(d => progressBar1.Maximum = 100, null);
                uiContext.Send(d => progressBar1.Value = 0, null);

                await Task.Run(async () =>
                {



                    while (bytesProc < allBytes)
                    {


                        token.ThrowIfCancellationRequested();
                       
                        int bytesRead = reader.Read(buff, 0, buff.Length);
                        if (bytesRead == 0) break;
                        for (int i = 0; i < bytesRead; i++)
                        {
                            if (encdec)
                            {
                                // ����������: XOR � ������
                                buff[i] ^= keyBytes[i % keyBytes.Length];
                            }
                            else
                            {
                                // �����������: XOR � ��� �� ������
                                buff[i] ^= keyBytes[i % keyBytes.Length];
                            }
                        }
                        writer.Write(buff, 0, bytesRead);
                        bytesProc += bytesRead;

                        // uiContext.Send ���������� ���������� ��������� � �������� �������������
                        // SendOrPostCallback - ������� ��������� �����, ���������� ��� �������� ��������� � �������� �������������. 
                        uiContext.Send(d => progressBar1.Value = (int)((bytesProc * 100) / allBytes) /* ���������� ������� SendOrPostCallback */, null);
                        await Task.Delay(2000, token);
                    }

                }, token);
                MessageBox.Show("���� ������� ���������!", "������", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (OperationCanceledException ex)
            {
                writer.Close();
                reader.Close();
                source_file.Close();
                receiver_file.Close();

                if (File.Exists(receiver))
                {
                    File.Delete(receiver);
                }
                MessageBox.Show("������ ��������.", "������", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("������: " + ex.Message);
            }
            finally
            {

                writer.Close();
                reader.Close();
                source_file.Close();
                receiver_file.Close();
            }


         }


    }
}
