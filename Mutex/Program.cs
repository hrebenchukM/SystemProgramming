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
      ������� ������������ ����� �������, ������� ����� ������������ �������� ������ � ������� ��� ���� ��������. 
       public Semaphore(
          int initialCount,  - ��������� ���������� �������� ��������, ������� ����� ���� ������������� ������������.
          int maximumCount,  - ������������ ���������� �������� ��������, ������� ����� ���� ������������� ������������.
          string name � ���  - ��� ������� ������������ ���������� ��������.
      );

      public int Release( - ������� �� �������� ��������� ����� ��� � ���������� ��������� �������� ��������.
          int releaseCount � ���������� ��������� ������� �� ��������.
      );

      public virtual bool WaitOne();  ������� ������� �������� � ���������� ���������

      public static Semaphore OpenExisting( - �������� ������������� ������������ ��������.
          string name
      );
      */

            ////������ ������ ����� ����� ����������
            string GUID = "1A9191BF-AA26-46E1-BB85-BDA396BC6469";
            int nowN = 1; //��������� 1 ����� ����������� �������
            int maxN = 1; // ������������ ���������� ������� ,������� ����� �������� ������

            Semaphore s = new Semaphore(nowN, maxN, GUID);


            if (!s.WaitOne(0))// ���� �� ������� ��������� �������
            {
                MessageBox.Show("Must be only one copy");
            }
            else // ������� ��������, ��������� ����������
            {
                Application.SetHighDpiMode(HighDpiMode.SystemAware);
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
            s.Dispose();



            ////������ ������ ����� ����� ����������
            //string GUID = "{6F9619FF-8B86-D011-B42D-00CF4FC964FF}";
            //bool CreatedNew;
            //Mutex mutex = new Mutex(false, GUID, out CreatedNew);
            //if (!CreatedNew)//������� ��� ��� ������
            //{
            //    MessageBox.Show("Must be only one copy");
            //}
            //else //������� �������� ������ ����������� ����������
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