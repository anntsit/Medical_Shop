using System;
using System.Linq;
using System.Windows.Forms;
using MedicaleShop.Core.Classes;
using MedicaleShop.Utils;

namespace MedicaleShop.Forms
{
    public partial class Register : Form
    {
        public Client Client;

        public Register()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Client.Items.Values.Where(client => client.Login.Equals(tbLoginR.Text)).ToList().Count == 0)
            {
                if (Client == null)
                    Client = new Client(lastname.Text + " " + firstname.Text, tel.Text);
                Client.Login = tbLoginR.Text;
                Client.Password = MathUtil.Md5(tbPassR.Text);
                DialogResult = DialogResult.Yes;
            }
            else
                MessageBox.Show(@"Такий логін вже зайнятий!");
        }

        private void tbLoginR_TextChanged(object sender, EventArgs e)
        {
            label5.Text = Client.Items.Values.Where(client => client.Login.Equals(tbLoginR.Text)).ToList().Count != 0
                ? @"Такий логін вже зайнятий!"
                : "";
        }
    }
}