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
           



           

            // Создание объекта на основе введенного пути
            DirectoryInfo di = new DirectoryInfo(Path);
            // Если путь не существует
            if (!di.Exists)
            {
                MessageBox.Show("Некорректный путь!!!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("Ошибка при поиске: {0}", ex.Message);
            }



        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Поиск остановлен.");
           
        }

        private void SubdirectoriesCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }


        private void AddToListView(string fileName)
        {
            // Создаем новый элемент списка
            ListViewItem item = new ListViewItem(fileName);
            // Добавляем его в ListView
            listView1.Items.Add(item);
        }





        // Функция поиска файлов по имени (маске) и текста внутри файлов
        private ulong FindFiles(Regex regText, DirectoryInfo di, Regex regMask, ref int fIndex)
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
                  
                    AddToListView(f.FullName);

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
                         
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Ошибка при чтении файла : {0}", ex.Message);

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
