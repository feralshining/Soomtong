using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace KioskBGRC
{
    static class Program
    {
        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            #region "[     중복 실행 방지     ]"
            Process[] ps = null;
            string ps_name = Process.GetCurrentProcess().ProcessName.ToUpper();
            ps = Process.GetProcessesByName(ps_name);
            if (ps.Length > 1)
            {
                Application.Exit();
                return;
            }
            #endregion
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainMenu());
        }
    }
}
