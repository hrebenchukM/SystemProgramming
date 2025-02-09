using System.Drawing;
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
        // Создаем пустой список изображений для больших и маленьких значков
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

            // ассоциируем списки изображений с ListView
            listView1.LargeImageList = imageListLarge;
            listView1.SmallImageList = imageListSmall;
        }





        private void SearchButton_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();

            string Path = comboBoxPath.Text;
            string Mask = textBoxMask.Text;
            string Text = textBoxText.Text;






            // Дописываем слэш (в случае его отсутствия)
            if (Path[Path.Length - 1] != '\\')
                Path += '\\';

            // Создание объекта на основе введенного пути
            DirectoryInfo di = new DirectoryInfo(Path);
            // Если путь не существует
            if (!di.Exists)
            {
                //Console.WriteLine("Некорректный путь!!!");
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
                // Вызываем функцию поиска
                ulong Count = FindTextInFiles(regText, di, regMask);
                labelRes.Text = "Результаты поиска: файлов найдено " + Count + ".";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при поиске: " + ex.Message);
            }


        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Поиск остановлен.");
            Application.Exit();
        }

        private void SubdirectoriesCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }


       

        // Функция поиска
        private ulong FindTextInFiles(Regex regText, DirectoryInfo di, Regex regMask)
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

                            if (mc.Count > 0)
                            {
                                AddToListView(f);
                                // Увеличиваем счетчик
                                ++CountOfMatchFiles;
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
                        // Увеличиваем счетчик
                        ++CountOfMatchFiles;

                    }
                }
            }





            if (SubdirectoriesCheckBox.Checked)
            {
                // Получаем список подкаталогов
                DirectoryInfo[] diSub = di.GetDirectories();
                // Для каждого из них вызываем (рекурсивно)
                // эту же функцию поиска
                foreach (DirectoryInfo diSubDir in diSub)
                    CountOfMatchFiles += FindTextInFiles(regText, diSubDir, regMask);
            }
            // Возврат количества обработанных файлов
            return CountOfMatchFiles;
        }

        private void AddToListView(FileInfo file)
        {
        
            // Создадим элемент списка и три подэлемента 
            ListViewItem item = new ListViewItem(file.Name);
            item.SubItems.Add(file.DirectoryName);
            item.SubItems.Add(file.Length.ToString() + " байт");
            item.SubItems.Add(file.LastWriteTime.ToString());

         
            Icon icon = Icon.ExtractAssociatedIcon(file.FullName);

            // Инициализируем списки изображений картинками
            imageListSmall.Images.Add(icon);  
            imageListLarge.Images.Add(icon);

            item.ImageIndex = imageListSmall.Images.Count-1;

            listView1.Items.Add(item);
        }


        private void button3_Click(object sender, EventArgs e)// Малые значки
        {

            listView1.View = View.SmallIcon;
          
        }

        private void button4_Click(object sender, EventArgs e)// Плитки
        {
            listView1.View = View.Tile;
        }

        private void button5_Click(object sender, EventArgs e)// Большие значки
        {
            listView1.View = View.LargeIcon;

        }

        private void button6_Click(object sender, EventArgs e) //  Таблица
        {
            // Установим табличный режим отображения
            listView1.View = View.Details;

            // При выборе элемента списка будет подсвечена вся строка
            listView1.FullRowSelect = true;

            // Отобразим линии сетки в табличном режиме
            listView1.GridLines = true;

            // Установим сортировку элементов в порядке возрастания
            listView1.Sorting = SortOrder.Ascending;

            if (listView1.Columns.Count == 0)
            {
                // Создадим колонки (1 параметр - название столбца, 2 параметр - ширина столбца, выравнивание названия)
                listView1.Columns.Add("Имя ", 200);
                listView1.Columns.Add("Папка", 300);
                listView1.Columns.Add("Размер", 100);
                listView1.Columns.Add("Дата Модификации", 100);
            }
        }
    }



}
