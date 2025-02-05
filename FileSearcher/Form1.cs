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





            // Дописываем слэш (в случае его отсутствия)
            if (Path[Path.Length - 1] != '\\')
                Path += '\\';

            // Создание объекта на основе введенного пути
            DirectoryInfo di = new DirectoryInfo(Path);
            // Если путь не существует
            if (!di.Exists)
            {
                Console.WriteLine("Некорректный путь!!!");
                return;
            }

            // Преобразуем введенную маску для файлов 
            // в регулярное выражение

            // Заменяем . на \.
            Mask = Mask.Replace(".", @"\.");
            // Заменяем ? на .
            Mask = Mask.Replace("?", "."); // ????.txt  ....\.txt
            // Заменяем * на .*
            Mask = Mask.Replace("*", ".*");// *.txt   .*\.txt
            // Указываем, что требуется найти точное соответствие маске
            Mask = "^" + Mask + "$";

            // Создание объекта регулярного выражения
            // на основе маски
            Regex regMask = new Regex(Mask, RegexOptions.IgnoreCase);

            // Экранируем спецсимволы во введенном тексте
            Text = Regex.Escape(Text);
            // Создание объекта регулярного выражения
            // на основе текста
            Regex regText = Text.Length == 0 ? null : new Regex(Text, RegexOptions.IgnoreCase);
            try
            {
                int fIndex = 0;
                // Вызываем функцию поиска
                ulong Count = FindFiles(regText, di, regMask, ref fIndex);
                labelRes.Text = "Всего обработано файлов: {0}." + Count.ToString();
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
