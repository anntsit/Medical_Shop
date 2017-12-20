using System;
using System.Windows.Forms;

namespace MedicaleShop.Forms
{
    public partial class ReasonRejection : Form
    {
        public string Reason;

        public ReasonRejection()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Reason = richTextBox1.Text;
            DialogResult = DialogResult.Yes;
        }
    }
}