using System.Text.RegularExpressions;
using System.Text;
using System.Threading.Tasks;

namespace FileSearcherTPL
{
    public partial class Form1 : Form
    {
        CancellationTokenSource cancelTokSrc;
        public SynchronizationContext uiContext;//используется для взаимодействия потока с UI (чтобы обновлять интерфейс из потока)


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


            // Получим контекст синхронизации для текущего потока 
            uiContext = SynchronizationContext.Current;
        }


        private void MyTask(Object ct)
        {
            // Структура CancellationToken распространяет уведомление о том, что операции следует отменить.
            CancellationToken cancelTok = (CancellationToken)ct;

            // завершим задачу, если она была отменена ещё до запуска
            cancelTok.ThrowIfCancellationRequested();
            // ThrowIfCancellationRequested создает исключение OperationCanceledException, 
            // если для данного признака есть запрос на отмену.

            ulong Count = 0;

            try
            {


                // uiContext.Send отправляет синхронное сообщение в контекст синхронизации
                // SendOrPostCallback - делегат указывает метод, вызываемый при отправке сообщения в контекст синхронизации. 
                uiContext.Send(d => listView1.Items.Clear()/* Вызываемый делегат SendOrPostCallback */,
                null/* Объект, переданный делегату */);


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




                // IsCancellationRequested получает значение, указывающее, 
                // есть ли для данного объекта CancellationTokenSource запрос на отмену.
                if (cancelTok.IsCancellationRequested)
                {
                    MessageBox.Show("Получен запрос на отмену задачи!");
                    // выбрасываем исключение, если установлен признак отмены задачи
                    cancelTok.ThrowIfCancellationRequested();
                }




                // Вызываем функцию поиска
                Count = FindTextInFiles(regText, di, regMask, cancelTok);





            }
            catch (OperationCanceledException)
            {
                MessageBox.Show("Поиск отменён.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при поиске: " + ex.Message);
            }
            finally
            {
                // uiContext.Send отправляет синхронное сообщение в контекст синхронизации
                // SendOrPostCallback - делегат указывает метод, вызываемый при отправке сообщения в контекст синхронизации. 
                uiContext.Send(d => {
                    labelRes.Text = "Результаты поиска: файлов найдено " + Count + ".";
                }/* Вызываемый делегат SendOrPostCallback */,
                null/* Объект, переданный делегату */);

            }

        }





        private void SearchButton_Click(object sender, EventArgs e)
        {
            cancelTokSrc = new CancellationTokenSource();// создадим объект источника признаков отмены   
            // получим признак отмены из источника и передадим его задаче и делегату
            Task tsk = Task.Factory.StartNew(MyTask,
                cancelTokSrc.Token, /* Признак отмены CancellationToken, передаваемый в задачу */
                cancelTokSrc.Token /* Признак отмены CancellationToken, который будет назначен новой задаче Task */ );

        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            //отменим задачу, используя признак отмены
            cancelTokSrc.Cancel();
            //Application.Exit();

        }



        // Функция поиска
        private ulong FindTextInFiles(Regex regText, DirectoryInfo di, Regex regMask, CancellationToken ct)
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

                if (ct.IsCancellationRequested)
                    return CountOfMatchFiles;


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
                        // Увеличиваем счетчик
                        ++CountOfMatchFiles;

                        Thread.Sleep(100);

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
                    CountOfMatchFiles += FindTextInFiles(regText, diSubDir, regMask,ct);
            }
            // Возврат количества обработанных файлов
            return CountOfMatchFiles;
        }


        private void AddToListView(FileInfo file)
        {

            // uiContext.Send отправляет синхронное сообщение в контекст синхронизации
            // SendOrPostCallback - делегат указывает метод, вызываемый при отправке сообщения в контекст синхронизации. 
            uiContext.Send(d =>
            {

                foreach (ListViewItem i in listView1.Items)
                {
                    if (i.Text == file.Name && i.SubItems[1].Text == file.DirectoryName)
                    {
                        return;
                    }
                }


                // Создадим элемент списка и три подэлемента 
                ListViewItem item = new ListViewItem(file.Name);
                item.SubItems.Add(file.DirectoryName);
                item.SubItems.Add(file.Length.ToString() + " байт");
                item.SubItems.Add(file.LastWriteTime.ToString());


                Icon icon = Icon.ExtractAssociatedIcon(file.FullName);

                // Инициализируем списки изображений картинками
                imageListSmall.Images.Add(icon);
                imageListLarge.Images.Add(icon);

                item.ImageIndex = imageListSmall.Images.Count - 1;

                listView1.Items.Add(item);



            }/* Вызываемый делегат SendOrPostCallback */,
            null/* Объект, переданный делегату */);
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



        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //отменим задачу, используя признак отмены
            cancelTokSrc.Cancel();
        }


    }
}
