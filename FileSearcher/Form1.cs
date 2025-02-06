using System.Text;
using System.Text.RegularExpressions;
        /*
        class Directory https://msdn.microsoft.com/ru-ru/library/system.io.directory(v=vs.110).aspx
        class DirectoryInfo https://msdn.microsoft.com/ru-ru/library/system.io.directoryinfo(v=vs.110).aspx
        class File https://learn.microsoft.com/ru-ru/dotnet/api/system.io.file?view=net-6.0
        class FileInfo https://msdn.microsoft.com/ru-ru/library/system.io.fileinfo(v=vs.110).aspx
        */
namespace FileSearcher
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            string[] drives = new string[]
            {
            "C:\\",  
            "D:\\",  
            };

            foreach (var drive in drives)
            {
                comboBoxPath.Items.Add(drive);
            }

            if (comboBoxPath.Items.Count > 0)
            {
                comboBoxPath.SelectedIndex = 0;
            }
        }


       


        private void SearchButton_Click(object sender, EventArgs e)
        {


            string Path = comboBoxPath.Text;
            string Mask = textBoxMask.Text; 
            string Text = textBoxText.Text; 
           



           

            // �������� ������� �� ������ ���������� ����
            DirectoryInfo di = new DirectoryInfo(Path);
            // ���� ���� �� ����������
            if (!di.Exists)
            {
                MessageBox.Show("������������ ����!!!", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            try
            {
                int fIndex = 0;
                // �������� ������� ������
                ulong Count = FindFiles(regText, di, regMask, ref fIndex);
                labelRes.Text = "����� ���������� ������: {0}." + Count.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("������ ��� ������: {0}", ex.Message);
            }



        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("����� ����������.");
           
        }

        private void SubdirectoriesCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }


        private void AddToListView(string fileName)
        {
            // ������� ����� ������� ������
            ListViewItem item = new ListViewItem(fileName);
            // ��������� ��� � ListView
            listView1.Items.Add(item);
        }





        // ������� ������ ������ �� ����� (�����) � ������ ������ ������
        private ulong FindFiles(Regex regText, DirectoryInfo di, Regex regMask, ref int fIndex)
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

                // ���� ���� ������������� �����
                if (regMask.IsMatch(f.Name))
                {
                    ++fIndex;
                    // ����������� �������
                    ++CountOfMatchFiles;
                  
                    AddToListView(f.FullName);

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
                         
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("������ ��� ������ ����� : {0}", ex.Message);

                        }
                    }


                }
            }

            // �������� ������ ������������
            DirectoryInfo[] diSub = di.GetDirectories();
            // ��� ������� �� ��� �������� (����������)
            // ��� �� ������� ������
            foreach (DirectoryInfo diSubDir in diSub)
                CountOfMatchFiles += FindFiles(regText, diSubDir, regMask, ref fIndex);

            // ������� ���������� ������������ ������
            return CountOfMatchFiles;
        }


    }
}
