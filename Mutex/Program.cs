using System.Threading;

namespace MutexSemaphore
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            //ApplicationConfiguration.Initialize();
            //Application.Run(new Form1());


            /*
      Семафор ограничивает число потоков, которые могут одновременно получать доступ к ресурсу или пулу ресурсов. 
       public Semaphore(
          int initialCount,  - Начальное количество запросов семафора, которое может быть удовлетворено одновременно.
          int maximumCount,  - Максимальное количество запросов семафора, которое может быть удовлетворено одновременно.
          string name – имя  - Имя объекта именованного системного семафора.
      );

      public int Release( - Выходит из семафора указанное число раз и возвращает последнее значение счетчика.
          int releaseCount – Количество требуемых выходов из семафора.
      );

      public virtual bool WaitOne();  ожидает переход семафора в сигнальное состояние

      public static Semaphore OpenExisting( - Открытие существующего именованного семафора.
          string name
      );
      */

            ////запуск только одной копии приложения
            string GUID = "1A9191BF-AA26-46E1-BB85-BDA396BC6469";
            int nowN = 1; //Разрешаем 1 поток захватывать семафор
            int maxN = 1; // Максимальное количество потоков ,которым будет разрешен доступ

            Semaphore s = new Semaphore(nowN, maxN, GUID);


            if (!s.WaitOne(0))// Если не удалось захватить семафор
            {
                MessageBox.Show("Must be only one copy");
            }
            else // Семафор захвачен, запускаем приложение
            {
                Application.SetHighDpiMode(HighDpiMode.SystemAware);
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
            s.Dispose();



            ////запуск только одной копии приложения
            //string GUID = "{6F9619FF-8B86-D011-B42D-00CF4FC964FF}";
            //bool CreatedNew;
            //Mutex mutex = new Mutex(false, GUID, out CreatedNew);
            //if (!CreatedNew)//мьютекс уже был создан
            //{
            //    MessageBox.Show("Must be only one copy");
            //}
            //else //мьютекс создаётся данным экземпляром приложения
            //{
            //    Application.SetHighDpiMode(HighDpiMode.SystemAware);
            //    Application.EnableVisualStyles();
            //    Application.SetCompatibleTextRenderingDefault(false);
            //    Application.Run(new Form1());
            //}
            //mutex.Dispose();





        }

    }
}