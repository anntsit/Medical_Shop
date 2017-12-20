using System;
using System.Linq;
using System.Windows.Forms;
using MedicaleShop.Core.Classes;

namespace MedicaleShop.Forms
{
    public partial class Selector : Form
    {
        public Pharmacy Pharmacy;

        public Selector()
        {
            InitializeComponent();
        }

        private void Selector_Load(object sender, EventArgs e)
        {
            listBox1.DataSource = Pharmacy.Items.Values.ToList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var pharmacy = listBox1.SelectedItem as Pharmacy;
            if (pharmacy == null)
                return;
            Pharmacy = pharmacy;
            DialogResult = DialogResult.Yes;
        }
    }
}