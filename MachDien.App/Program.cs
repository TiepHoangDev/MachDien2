using MachDien.App.Api;
using StartupHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MachDien.App
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            StartupController.Register();

            Startup.Run();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        public static StartupManager StartupController = new StartupManager("MD_PROGRAM", RegistrationScope.Local);
    }
}
