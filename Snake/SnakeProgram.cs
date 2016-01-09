namespace Snake
{
    using System;
    using System.Windows.Forms;

    public static class SnakeProgram
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Snake());
        }
    }
}
