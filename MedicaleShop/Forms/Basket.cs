using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using MedicaleShop.Core.Classes;
using MedicaleShop.Utils;

namespace MedicaleShop.Forms
{
    public partial class Basket : Form
    {
        public Client Client;
        public Dictionary<Guid, int> Medicins;

        public bool NeedRecipe;
        public decimal Price;

        public Basket(Client client, Dictionary<Guid, int> items)
        {
            InitializeComponent();
            Client = client;
            Medicins = items;
        }

        public override void Refresh()
        {
            base.Refresh();
            listView1.Items.Clear();
            foreach (var medicin in Medicins)
            {
                var price = Medicin.Items[medicin.Key].Price*medicin.Value;
                listView1.Items.Add(
                    new ListViewItem(new[]
                    {
                        Medicin.Items[medicin.Key].ToString(), medicin.Value.ToString(),
                        price.ToString(CultureInfo.InvariantCulture)
                    })
                    {
                        Tag = medicin
                    });
                if (Medicin.Items[medicin.Key].NeedRecipe)
                    NeedRecipe = true;
                Price += price;
            }
            label5.Text = Price + @" грн.";
            label6.Text = Client.Discount + @"%";
            var endPrice = MathUtil.RoundUp(Price*(1 - Convert.ToDecimal(Client.Discount)/100), 2);
            label7.Text = (endPrice > 0 ? endPrice.ToString(CultureInfo.InvariantCulture) : "0") + @" грн.";
        }

        private void Basket_Load(object sender, EventArgs e)
        {
            label5.Text = label6.Text = label7.Text = "";
            Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var selector = new Selector();
            if (selector.ShowDialog(this) == DialogResult.Yes)
            {
                if (Medicins.Count != 0)
                {
                    var order = new Order
                    {
                        Client = Client,
                        Discount = int.Parse(Client.Discount.ToString()),
                        NeedRecipe = NeedRecipe,
                        Price = Price,
                        Pharmacy = selector.Pharmacy
                    };
                    foreach (var medicin in Medicins)
                        new Purchase(order, Medicin.Items[medicin.Key], medicin.Value);

                    DialogResult = DialogResult.Yes;
                }
                else
                    DialogResult = DialogResult.Cancel;
            }
        }

        private void видалитиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem selectedItem in listView1.SelectedItems)
                Medicins.Remove(((Medicin) selectedItem.Tag).Id);
            Refresh();
        }

        private void видалитиВсеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Medicins.Clear();
            Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Abort;
        }
    }
}