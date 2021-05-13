using System;
using System.Windows.Forms;
using SqlGenerator.UI.Forms;

namespace SqlGenerator.UI
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SelectorForm() { StartPosition = FormStartPosition.CenterScreen});
        }
    }
}
