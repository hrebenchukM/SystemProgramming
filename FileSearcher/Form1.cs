using System.Text;
using System.Text.RegularExpressions;

namespace FileSearcher
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


       


        private void SearchButton_Click(object sender, EventArgs e)
        {
     

            string Path = comboBoxPath.Text;
            string Mask = textBoxMask.Text;
            string Text = textBoxText.Text;
            bool SubDir = SubdirectoriesCheckBox.Checked;





            // ���������� ���� (� ������ ��� ����������)
            if (Path[Path.Length - 1] != '\\')
                Path += '\\';

            // �������� ������� �� ������ ���������� ����
            DirectoryInfo di = new DirectoryInfo(Path);
            // ���� ���� �� ����������
            if (!di.Exists)
            {
                Console.WriteLine("������������ ����!!!");
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
                Console.WriteLine(ex.Message);
            }



        }

        private void StopButton_Click(object sender, EventArgs e)
        {

        }

        private void SubdirectoriesCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }






        // ������� ������ ������ �� ����� (�����) � ������ ������ ������
        static ulong FindFiles(Regex regText, DirectoryInfo di, Regex regMask, ref int fIndex)
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
                    string res = fIndex + ") File: " + f.Name;
                    Console.WriteLine(res);

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
                            // ���������� ������ ���������
                            foreach (Match m in mc)
                            {
                                Console.WriteLine("����� ������ � ������� {0}.", m.Index);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
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
