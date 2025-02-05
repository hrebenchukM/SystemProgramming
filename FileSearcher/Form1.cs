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

        }

        private void StopButton_Click(object sender, EventArgs e)
        {

        }

        private void SubdirectoriesCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }






        // Функция поиска файлов по имени (маске) и текста внутри файлов
        static ulong FindFiles(Regex regText, DirectoryInfo di, Regex regMask, ref int fIndex)
        {
            // Поток для чтения из файла
            StreamReader sr = null;
            // Список найденных совпадений
            MatchCollection mc = null;

            // Количество обработанных файлов
            ulong CountOfMatchFiles = 0;

            FileInfo[] fi = null;

            try
            {
                // Получаем список файлов
                fi = di.GetFiles();
            }
            catch
            {
                return CountOfMatchFiles;
            }

            // Перебираем список файлов
            foreach (FileInfo f in fi)
            {

                // Если файл соответствует маске
                if (regMask.IsMatch(f.Name))
                {
                    ++fIndex;
                    // Увеличиваем счетчик
                    ++CountOfMatchFiles;
                    string res = fIndex + ") File: " + f.Name;
                    Console.WriteLine(res);

                    if (regText != null)
                    {
                        try
                        {
                            // Открываем файл
                            sr = new StreamReader(di.FullName + @"\" + f.Name,
                                Encoding.Default);
                            // Считываем целиком
                            string Content = sr.ReadToEnd();
                            // Закрываем файл
                            sr.Close();
                            // Ищем заданный текст
                            mc = regText.Matches(Content);
                            // Перебираем список вхождений
                            foreach (Match m in mc)
                            {
                                Console.WriteLine("Текст найден в позиции {0}.", m.Index);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }


                }
            }

            // Получаем список подкаталогов
            DirectoryInfo[] diSub = di.GetDirectories();
            // Для каждого из них вызываем (рекурсивно)
            // эту же функцию поиска
            foreach (DirectoryInfo diSubDir in diSub)
                CountOfMatchFiles += FindFiles(regText, diSubDir, regMask, ref fIndex);

            // Возврат количества обработанных файлов
            return CountOfMatchFiles;
        }


    }
}
