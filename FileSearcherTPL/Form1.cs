using System.Text.RegularExpressions;
using System.Text;
using System.Threading.Tasks;

namespace FileSearcherTPL
{
    public partial class Form1 : Form
    {
        CancellationTokenSource cancelTokSrc;
        public SynchronizationContext uiContext;//������������ ��� �������������� ������ � UI (����� ��������� ��������� �� ������)


        // ������� ������ ������ ����������� ��� ������� � ��������� �������
        private ImageList imageListSmall = new ImageList();
        private ImageList imageListLarge = new ImageList();
        public Form1()
        {
            InitializeComponent();

            string[] disks =
            {
            "C:\\",
            "D:\\",
            };

            foreach (var disk in disks)
            {
                comboBoxPath.Items.Add(disk);
            }

            if (comboBoxPath.Items.Count > 0)
            {
                comboBoxPath.SelectedIndex = 0;
            }



            imageListSmall.ImageSize = new Size(32, 32);
            imageListLarge.ImageSize = new Size(48, 48);

            // ����������� ������ ����������� � ListView
            listView1.LargeImageList = imageListLarge;
            listView1.SmallImageList = imageListSmall;


            // ������� �������� ������������� ��� �������� ������ 
            uiContext = SynchronizationContext.Current;
        }


        private void MyTask(Object ct)
        {
            // ��������� CancellationToken �������������� ����������� � ���, ��� �������� ������� ��������.
            CancellationToken cancelTok = (CancellationToken)ct;

            // �������� ������, ���� ��� ���� �������� ��� �� �������
            cancelTok.ThrowIfCancellationRequested();
            // ThrowIfCancellationRequested ������� ���������� OperationCanceledException, 
            // ���� ��� ������� �������� ���� ������ �� ������.

            ulong Count = 0;

            try
            {


                // uiContext.Send ���������� ���������� ��������� � �������� �������������
                // SendOrPostCallback - ������� ��������� �����, ���������� ��� �������� ��������� � �������� �������������. 
                uiContext.Send(d => listView1.Items.Clear()/* ���������� ������� SendOrPostCallback */,
                null/* ������, ���������� �������� */);


                string Path = string.Empty;
                uiContext.Send(d =>
                {
                    Path = comboBoxPath.Text;
                }, null);



                string Mask = string.Empty;
                uiContext.Send(d =>
                {
                    Mask = textBoxMask.Text;
                }, null);



                string Text = string.Empty;
                uiContext.Send(d =>
                {
                    Text = textBoxText.Text;
                }, null);







                // ���������� ���� (� ������ ��� ����������)
                if (Path[Path.Length - 1] != '\\')
                    Path += '\\';

                // �������� ������� �� ������ ���������� ����
                DirectoryInfo di = new DirectoryInfo(Path);
                // ���� ���� �� ����������
                if (!di.Exists)
                {
                    //Console.WriteLine("������������ ����!!!");
                    return;
                }

                // ����������� ��������� ����� ��� ������ 
                // � ���������� ���������

                // �������� . �� \.
                Mask = Mask.Replace(".", @"\.");
                // �������� ? �� .
                Mask = Mask.Replace("?", "."); // ????.txt  ....\.txt
                                               // �������� * �� .*
                Mask = Mask.Replace("*", ".*");// *.txt   .*\.txt
                                               // ���������, ��� ��������� ����� ������ ������������ �����
                Mask = "^" + Mask + "$";

                // �������� ������� ����������� ���������
                // �� ������ �����
                Regex regMask = new Regex(Mask, RegexOptions.IgnoreCase);

                // ���������� ����������� �� ��������� ������
                Text = Regex.Escape(Text);
                // �������� ������� ����������� ���������
                // �� ������ ������
                Regex regText = Text.Length == 0 ? null : new Regex(Text, RegexOptions.IgnoreCase);




                // IsCancellationRequested �������� ��������, �����������, 
                // ���� �� ��� ������� ������� CancellationTokenSource ������ �� ������.
                if (cancelTok.IsCancellationRequested)
                {
                    MessageBox.Show("������� ������ �� ������ ������!");
                    // ����������� ����������, ���� ���������� ������� ������ ������
                    cancelTok.ThrowIfCancellationRequested();
                }




                // �������� ������� ������
                Count = FindTextInFiles(regText, di, regMask, cancelTok);





            }
            catch (OperationCanceledException)
            {
                MessageBox.Show("����� ������.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("������ ��� ������: " + ex.Message);
            }
            finally
            {
                // uiContext.Send ���������� ���������� ��������� � �������� �������������
                // SendOrPostCallback - ������� ��������� �����, ���������� ��� �������� ��������� � �������� �������������. 
                uiContext.Send(d => {
                    labelRes.Text = "���������� ������: ������ ������� " + Count + ".";
                }/* ���������� ������� SendOrPostCallback */,
                null/* ������, ���������� �������� */);

            }

        }





        private void SearchButton_Click(object sender, EventArgs e)
        {
            cancelTokSrc = new CancellationTokenSource();// �������� ������ ��������� ��������� ������   
            // ������� ������� ������ �� ��������� � ��������� ��� ������ � ��������
            Task tsk = Task.Factory.StartNew(MyTask,
                cancelTokSrc.Token, /* ������� ������ CancellationToken, ������������ � ������ */
                cancelTokSrc.Token /* ������� ������ CancellationToken, ������� ����� �������� ����� ������ Task */ );

        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            //������� ������, ��������� ������� ������
            cancelTokSrc.Cancel();
            //Application.Exit();

        }



        // ������� ������
        private ulong FindTextInFiles(Regex regText, DirectoryInfo di, Regex regMask, CancellationToken ct)
        {

            // ����� ��� ������ �� �����
            StreamReader sr = null;
            // ������ ��������� ����������
            MatchCollection mc = null;

            // ���������� ������������ ������
            ulong CountOfMatchFiles = 0;

            FileInfo[] fi = null;
            try
            {
                // �������� ������ ������
                fi = di.GetFiles();
            }
            catch
            {
                return CountOfMatchFiles;
            }

            // ���������� ������ ������
            foreach (FileInfo f in fi)
            {

                if (ct.IsCancellationRequested)
                    return CountOfMatchFiles;


                // ���� ���� ������������� �����
                if (regMask.IsMatch(f.Name))
                {
                    if (regText != null)
                    {
                        try
                        {
                            // ��������� ����
                            sr = new StreamReader(di.FullName + @"\" + f.Name,
                                Encoding.Default);
                            // ��������� �������
                            string Content = sr.ReadToEnd();
                            // ��������� ����
                            sr.Close();
                            // ���� �������� �����
                            mc = regText.Matches(Content);

                            if (mc.Count > 0)
                            {
                                AddToListView(f);
                                // ����������� �������
                                ++CountOfMatchFiles;

                                Thread.Sleep(100);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    else
                    {

                        AddToListView(f);
                        // ����������� �������
                        ++CountOfMatchFiles;

                        Thread.Sleep(100);

                    }
                }
            }





            if (SubdirectoriesCheckBox.Checked)
            {
                // �������� ������ ������������
                DirectoryInfo[] diSub = di.GetDirectories();
                // ��� ������� �� ��� �������� (����������)
                // ��� �� ������� ������
                foreach (DirectoryInfo diSubDir in diSub)
                    CountOfMatchFiles += FindTextInFiles(regText, diSubDir, regMask,ct);
            }
            // ������� ���������� ������������ ������
            return CountOfMatchFiles;
        }


        private void AddToListView(FileInfo file)
        {

            // uiContext.Send ���������� ���������� ��������� � �������� �������������
            // SendOrPostCallback - ������� ��������� �����, ���������� ��� �������� ��������� � �������� �������������. 
            uiContext.Send(d =>
            {

                foreach (ListViewItem i in listView1.Items)
                {
                    if (i.Text == file.Name && i.SubItems[1].Text == file.DirectoryName)
                    {
                        return;
                    }
                }


                // �������� ������� ������ � ��� ����������� 
                ListViewItem item = new ListViewItem(file.Name);
                item.SubItems.Add(file.DirectoryName);
                item.SubItems.Add(file.Length.ToString() + " ����");
                item.SubItems.Add(file.LastWriteTime.ToString());


                Icon icon = Icon.ExtractAssociatedIcon(file.FullName);

                // �������������� ������ ����������� ����������
                imageListSmall.Images.Add(icon);
                imageListLarge.Images.Add(icon);

                item.ImageIndex = imageListSmall.Images.Count - 1;

                listView1.Items.Add(item);



            }/* ���������� ������� SendOrPostCallback */,
            null/* ������, ���������� �������� */);
        }




        private void button3_Click(object sender, EventArgs e)// ����� ������
        {

            listView1.View = View.SmallIcon;

        }

        private void button4_Click(object sender, EventArgs e)// ������
        {
            listView1.View = View.Tile;
        }

        private void button5_Click(object sender, EventArgs e)// ������� ������
        {
            listView1.View = View.LargeIcon;

        }

        private void button6_Click(object sender, EventArgs e) //  �������
        {
            // ��������� ��������� ����� �����������
            listView1.View = View.Details;

            // ��� ������ �������� ������ ����� ���������� ��� ������
            listView1.FullRowSelect = true;

            // ��������� ����� ����� � ��������� ������
            listView1.GridLines = true;

            // ��������� ���������� ��������� � ������� �����������
            listView1.Sorting = SortOrder.Ascending;

            if (listView1.Columns.Count == 0)
            {
                // �������� ������� (1 �������� - �������� �������, 2 �������� - ������ �������, ������������ ��������)
                listView1.Columns.Add("��� ", 200);
                listView1.Columns.Add("�����", 300);
                listView1.Columns.Add("������", 100);
                listView1.Columns.Add("���� �����������", 100);
            }
        }



        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //������� ������, ��������� ������� ������
            cancelTokSrc.Cancel();
        }


    }
}
