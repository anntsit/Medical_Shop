using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using MedicaleShop.Core.Classes;
using MedicaleShop.Utils;

namespace MedicaleShop.Forms
{
    public partial class Login : Form
    {
        public Client Client;

        public Login()
        {
            InitializeComponent();
        }

        private void accept_Click(object sender, EventArgs e)
        {
            var find = false;
            foreach (
                var client in
                    Client.Items.Values.Where(
                        client =>
                            client.Login.Equals(tbLogin.Text) &&
                            client.Password.ToLower().Equals(MathUtil.Md5(tbPass.Text.ToLower()))))
            {
                Client = client;
                find = true;
                break;
            }
            if (find)
            {
                if (chbEnter.Checked)
                    File.WriteAllText("temp.dat", tbLogin.Text + @"|" + tbPass.Text);
                DialogResult = DialogResult.Yes;
            }
            else
                MessageBox.Show(@"Вибачте, такий користувач не існує.");
        }

        private void Login_Load(object sender, EventArgs e)
        {
            if (File.Exists("temp.dat"))
            {
                var temp = File.ReadAllText("temp.dat").Split('|');
                tbLogin.Text = temp[0];
                tbPass.Text = temp[1];
                chbEnter.Checked = true;
            }
        }

        private void registerForm_Click(object sender, EventArgs e)
        {
            var dialog = new Register();
            if (dialog.ShowDialog(this) == DialogResult.Yes)
            {
                tbLogin.Text = dialog.Client.Login;
                tbPass.Text = "";
                return;
            }
            if (dialog.Client != null)
                Client.Items.Remove(dialog.Client.Id);
        }
    }
}