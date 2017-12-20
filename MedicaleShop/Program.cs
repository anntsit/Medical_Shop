using System;
using System.Windows.Forms;
using MedicaleShop.Core;
using MedicaleShop.Forms;

namespace MedicaleShop
{
    public static class Program
    {
        public static Main MainForm;
        private static Login _login;

        /// <summary>
        ///     Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            FileHandler.Load();

            Start();
        }

        private static void _formClosed(object sender, FormClosedEventArgs e)
        {
            FileHandler.Save();
        }

        public static void Start()
        {
            _login = new Login();
            _login.FormClosed += _formClosed;

            ShowForm();
        }

        private static void ShowForm()
        {
            if (_login.ShowDialog() == DialogResult.Yes)
            {
                MainForm = new Main(_login.Client)
                {
                    Text =
                        @"Магазин ліків, користувач: " + _login.Client.Login +
                        (_login.Client.IsAdmin ? " | Режим редагування" : "")
                };
                MainForm.FormClosed += _formClosed;
            }
            else
                return;

            MainForm.ShowDialog();
        }
    }
}