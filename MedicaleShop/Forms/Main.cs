using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using MedicaleShop.Core.Classes;
using MedicaleShop.Utils;

namespace MedicaleShop.Forms
{
    public partial class Main : Form
    {
        private int _searchIndex = -1;
        public Dictionary<Guid, int> BasketItems = new Dictionary<Guid, int>();
        public Client SelectedClient;

        public Main(Client client)
        {
            InitializeComponent();

            #region About

            labelProductName.Text = AssemblyProduct;
            labelVersion.Text = $"Версія програми: {AssemblyVersion}";
            labelCopyright.Text = AssemblyCopyright;
            labelCompanyName.Text = AssemblyCompany;
            textBoxDescription.Text = AssemblyDescription;

            #endregion

            tabControl1.DrawItem += tabControl1_DrawItem;

            SelectedClient = client;
        }

        public void Refresh<T>(object element, T dataSource, bool toTag = false)
        {
            base.Refresh();
            var box = element as ListBox;
            if (box != null)
            {
                var tempListBox = box;
                tempListBox.DataSource = null;
                tempListBox.DataSource = dataSource;
                tempListBox.SelectedItem = null;
            }
            else
            {
                var comboBox = element as ComboBox;
                if (comboBox != null)
                {
                    var tempComboBox = comboBox;
                    tempComboBox.DataSource = null;
                    tempComboBox.DataSource = dataSource;
                    tempComboBox.SelectedItem = null;
                }
            }
        }

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            var g = e.Graphics;

            var tabPage = tabControl1.TabPages[e.Index];
            var tabBounds = tabControl1.GetTabRect(e.Index);

            Brush textBrush;
            if (e.State == DrawItemState.Selected)
            {
                textBrush = new SolidBrush(Color.DarkTurquoise);
                g.FillRectangle(Brushes.Gray, e.Bounds);
            }
            else
            {
                textBrush = new SolidBrush(e.ForeColor);
                g.FillRectangle(new SolidBrush(SystemColors.Control), e.Bounds);
            }

            var tabFont = new Font("Trebuchet MS", (float) 8.25, FontStyle.Regular, GraphicsUnit.Point);

            var stringFlags = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            g.DrawString(tabPage.Text, tabFont, textBrush, tabBounds, new StringFormat(stringFlags));
        }

        public void RefreshClient()
        {
            var clients = Client.Items.Values.Where(client => !client.IsAdmin).ToList();
            listViewClient.Items.Clear();
            foreach (var client in clients)
            {
                listViewClient.Items.Add(
                    new ListViewItem(new[] {client.Name, client.Login, client.Phote, client.Discount + "%"})
                    {
                        Tag = client
                    });
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            var tempName = SelectedClient.Name.Split(' ');
            textBoxFirstName.Text = tempName[1];
            textBoxSecondName.Text = tempName[0];
            maskedTextBoxPhone.Text = SelectedClient.Phote;
            textBoxDiscount.Text = SelectedClient.Discount.ToString();

            Refresh(listBoxMedicin, Medicin.Items.Values.ToList());
            Refresh(listBoxPharmacy, Pharmacy.Items.Values.ToList());

            List<Order> validOrders = new List<Order>();
            if (!SelectedClient.IsAdmin)
            {
                foreach (Order ord in SelectedClient.Orders)
                {
                    if (ord.Status != OderStatus.Deflected)
                    {
                        validOrders.Add(ord);
                    }

                }
                Refresh(listBoxOrder, validOrders);
            }
            else
            {
                Refresh(listBoxOrder, SelectedClient.Orders);
            }
            
           

            listBoxMedicin.SelectedItem = listBoxMedicin.Items.Count != 0
                ? listBoxMedicin.Items[listBoxMedicin.Items.Count - 1]
                : null;
            listBoxPharmacy.SelectedItem = listBoxPharmacy.Items.Count != 0
                ? listBoxPharmacy.Items[listBoxPharmacy.Items.Count - 1]
                : null;
            listBoxOrder.SelectedItem = listBoxOrder.Items.Count != 0
                ? listBoxOrder.Items[listBoxOrder.Items.Count - 1]
                : null;

            if (SelectedClient.IsAdmin)
            {
                Refresh(listBoxAdminOrder,
                    Order.Items.Values.Where(order => order.Status != OderStatus.Received).ToList());
                listBoxAdminOrder.SelectedItem = listBoxAdminOrder.Items.Count != 0
                    ? listBoxAdminOrder.Items[listBoxAdminOrder.Items.Count - 1]
                    : null;
                RefreshClient();
                tabControl1.TabPages.RemoveAt(0); // Delete first tabPage
                listBoxAdminOrder.ContextMenuStrip = contextMenuStripOrder;

                #region Pharmacy

                textBoxPharmacyName.ReadOnly = false;
                textBoxPharmacyAdress.ReadOnly = false;
                richTextBoxPharmacyDesc.ReadOnly = false;
                listBoxPharmacy.ContextMenuStrip = contextMenuStripPharmacy;

                #endregion

                listBoxMedicin.ContextMenuStrip = contextMenuStripMedicin;
                textBoxMedicinName.ReadOnly =
                    textBoxMedicinProducer.ReadOnly =
                        textBoxMedicinCountOfActiveSubstance.ReadOnly =
                            textBoxMedicinUnitActiveSubstance.ReadOnly =
                                textBoxMedicinPacking.ReadOnly =
                                    textBoxMedicinPrice.ReadOnly = richTextBoxMedicinDesc.ReadOnly = false;
                checkBoxMedicinRecipe.Enabled = true;

                buttonChipBasket.Hide();
                numericUpDownMedicinCount.Hide();
                buttonMedicinAddToBasket.Hide();
            }
            else
            {
                tabControl1.TabPages.RemoveAt(1);
                tabControl1.TabPages.RemoveAt(1);
                textBoxMedicinCountOfActiveSubstance.Hide();
                textBoxMedicinUnitActiveSubstance.Hide();
                buttonAddInstruction.Hide();
                buttonMedicinApply.Hide();

                buttonPharmacyApply.Hide();
                listBoxPharmacy.ContextMenuStrip = null;
                listBoxMedicin.ContextMenuStrip = null;
            }
        }

        private void buttonLeave_Click(object sender, EventArgs e)
        {
            Hide();
            Program.Start();
        }

        private void buttonAccountSave_Click(object sender, EventArgs e)
        {
            SelectedClient.Name = textBoxSecondName.Text + @" " + textBoxFirstName.Text;
            SelectedClient.Phote = maskedTextBoxPhone.Text;
            MessageBox.Show(@"Збережено!");
        }

        private void listBoxPharmacy_SelectedIndexChanged(object sender, EventArgs e)
        {
            var pharmacy = listBoxPharmacy.SelectedItem as Pharmacy;
            if (pharmacy == null)
                return;
            textBoxPharmacyAdress.Text = pharmacy.Address;
            textBoxPharmacyName.Text = pharmacy.Name;
            richTextBoxPharmacyDesc.Text = pharmacy.Description;
        }

        private void buttonPharmacyApply_Click(object sender, EventArgs e)
        {
            if (!SelectedClient.IsAdmin)
                return;
            var pharmacy = listBoxPharmacy.SelectedItem as Pharmacy;
            if (pharmacy == null)
                return;
            pharmacy.Address = textBoxPharmacyAdress.Text;
            pharmacy.Name = textBoxPharmacyName.Text;
            pharmacy.Description = richTextBoxPharmacyDesc.Text;
            Refresh(listBoxPharmacy, Pharmacy.Items.Values.ToList());
            MessageBox.Show(@"Збережено!");
        }

        private void buttonPharmacyMap_Click(object sender, EventArgs e)
        {
            var pharmacy = listBoxPharmacy.SelectedItem as Pharmacy;
            if (pharmacy == null)
                return;
            Process.Start(pharmacy.MapUri);
        }

        private void додатиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!SelectedClient.IsAdmin) return;
            var pharmacy = new Pharmacy("Аптека №0", "Киев, вул. Янгеля 20");
            Refresh(listBoxPharmacy, Pharmacy.Items.Values.ToList());
            listBoxPharmacy.SelectedItem = pharmacy;
            MessageBox.Show(@"Додано!");
        }

        private void видалитиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!SelectedClient.IsAdmin) return;
            var pharmacy = listBoxPharmacy.SelectedItem as Pharmacy;
            if (pharmacy == null)
                return;
            Pharmacy.Items.Remove(pharmacy.Id);
            Refresh(listBoxPharmacy, Pharmacy.Items.Values.ToList());
            MessageBox.Show(@"Видалено!");
        }

        private void додатиToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!SelectedClient.IsAdmin) return;
            var medicin = new Medicin("Додані ліки");
            Refresh(listBoxMedicin, Medicin.Items.Values.ToList());
            listBoxMedicin.SelectedItem = medicin;
            MessageBox.Show(@"Додано!");
        }

        private void видалитиToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!SelectedClient.IsAdmin) return;
            var medicin = listBoxMedicin.SelectedItem as Medicin;
            if (medicin == null)
                return;
            Medicin.Items.Remove(medicin.Id);
            Refresh(listBoxMedicin, Medicin.Items.Values.ToList());
            MessageBox.Show(@"Видалено!");
        }

        private void listBoxMedicin_SelectedIndexChanged(object sender, EventArgs e)
        {
            var medicin = listBoxMedicin.SelectedItem as Medicin;
            if (medicin == null)
                return;
            textBoxMedicinName.Text = medicin.Name;
            textBoxMedicinPacking.Text = medicin.Packing.ToString();
            textBoxMedicinPrice.Text = medicin.Price.ToString(CultureInfo.InvariantCulture);
            textBoxMedicinProducer.Text = medicin.Producer;
            richTextBoxMedicinDesc.Text = medicin.Appointment;
            checkBoxMedicinRecipe.Checked = medicin.NeedRecipe;
            if (!SelectedClient.IsAdmin)
                labelMedicinActiveSubstr.Text =
                    $"Кількість діючої речовини: {medicin.CountOfActiveSubstance} {medicin.UnitActiveSubstance}";
            else
            {
                textBoxMedicinCountOfActiveSubstance.Text = medicin.CountOfActiveSubstance.ToString();
                textBoxMedicinUnitActiveSubstance.Text = medicin.UnitActiveSubstance;
            }
        }

        private void buttonMedicinOpenInstruction_Click(object sender, EventArgs e)
        {
            var medicin = listBoxMedicin.SelectedItem as Medicin;
            if (medicin == null)
                return;
            if (File.Exists(medicin.Instruction))
                Process.Start(medicin.Instruction);
            else
                MessageBox.Show(@"Інструкція не знайдена!");
        }

        private void buttonMedicinAddToBasket_Click(object sender, EventArgs e)
        {
            var medicin = listBoxMedicin.SelectedItem as Medicin;
            if (medicin == null)
                return;
            if (!BasketItems.ContainsKey(medicin.Id))
                BasketItems.Add(medicin.Id, Convert.ToInt32(numericUpDownMedicinCount.Value));
            else
                BasketItems[medicin.Id] += Convert.ToInt32(numericUpDownMedicinCount.Value);
            MessageBox.Show(@"Ліки були додані до кошику.");
        }

        private void buttonMedicinApply_Click(object sender, EventArgs e)
        {
            if (!SelectedClient.IsAdmin) return;
            var medicin = listBoxMedicin.SelectedItem as Medicin;
            if (medicin == null)
                return;
            medicin.Name = textBoxMedicinName.Text;
            int packing;
            if (int.TryParse(textBoxMedicinPacking.Text, out packing))
                medicin.Packing = packing;
            decimal price;
            if (decimal.TryParse(textBoxMedicinPrice.Text, out price))
                medicin.Price = price;
            medicin.Producer = textBoxMedicinProducer.Text;
            medicin.Appointment = richTextBoxMedicinDesc.Text;
            medicin.NeedRecipe = checkBoxMedicinRecipe.Checked;
            int countUnit;
            if (int.TryParse(textBoxMedicinCountOfActiveSubstance.Text, out countUnit))
                medicin.CountOfActiveSubstance = countUnit;
            medicin.UnitActiveSubstance = textBoxMedicinUnitActiveSubstance.Text;
            Refresh(listBoxMedicin, Medicin.Items.Values.ToList());
            MessageBox.Show(@"Збережено!");
        }

        private void buttonAddInstruction_Click(object sender, EventArgs e)
        {
            if (!SelectedClient.IsAdmin) return;
            var medicin = listBoxMedicin.SelectedItem as Medicin;
            if (medicin == null)
                return;
            var dialog = new OpenFileDialog();
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                var file = dialog.FileName;
                var temp = file.Split('\\');
                var fileName = temp[temp.Length - 1];
                if (!Directory.Exists(@"Saves"))
                    Directory.CreateDirectory(@"Saves");
                if (!Directory.Exists(@"Saves\Instructions"))
                    Directory.CreateDirectory(@"Saves\Instructions");
                if (File.Exists(@"Saves\Instructions\" + fileName))
                {
                    temp = fileName.Split('.');
                    var rand = new Random();
                    do
                    {
                        fileName = temp[0] + rand.Next() + "." + temp[temp.Length - 1];
                    } while (!File.Exists(@"Saves\Instructions\" + fileName));
                }
                File.Copy(file, @"Saves\Instructions\" + fileName);
                medicin.Instruction = @"Saves\Instructions\" + fileName;
                MessageBox.Show(@"Інструкція додана до ліків!");
            }
        }

       

        private void listViewClient_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewClient.SelectedItems.Count != 1) return;
            var client = listViewClient.SelectedItems[0].Tag as Client;
            if (client == null) return;
            var temp = client.Name.Split(' ');
            textBoxClientFirstName.Text = temp[1];
            textBoxClientSecondName.Text = temp[0];
            maskedTextBoxClientPhone.Text = client.Phote;
            textBoxClientDiscount.Text = client.Discount.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listViewClient.SelectedItems.Count != 1) return;
            var client = listViewClient.SelectedItems[0].Tag as Client;
            if (client == null) return;
            client.Name = textBoxClientSecondName.Text + @" " + textBoxClientFirstName.Text;
            client.Phote = maskedTextBoxClientPhone.Text;
            int discount;
            if (int.TryParse(textBoxClientDiscount.Text, out discount))
                client.Discount = discount;
            RefreshClient();
            MessageBox.Show(@"Збережено!");
        }

        private void buttonChipBasket_Click(object sender, EventArgs e)
        {
            if (BasketItems.Count == 0)
            {
                MessageBox.Show(@"Ваша корзина наразі порожня.");
                return;
            }
            var dialog = new Basket(SelectedClient, BasketItems);
            var result = dialog.ShowDialog(this);
            if (result == DialogResult.Yes)
            {
                MessageBox.Show(@"Замовлення сформоване.");
                BasketItems.Clear();
                Refresh(listBoxOrder, SelectedClient.Orders);
            }
            else if (result == DialogResult.Abort)
                BasketItems.Clear();
        }

        private void listBoxOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            var order = listBoxOrder.SelectedItem as Order;
            if (order == null)
                return;
            buttonOrderCancale.Visible = order.Status != OderStatus.Received;
            label22.Text =
                MathUtil.RoundUp(order.Price*(1 - Convert.ToDecimal(order.Discount)/100), 2)
                    .ToString(CultureInfo.InvariantCulture) + @" грн.";
            label24.Text = order.Pharmacy.ToString();
            switch (order.Status)
            {
                case OderStatus.Confirmed:
                    label26.Text = @"Підтверджений";
                    break;
                case OderStatus.Deflected:
                    label26.Text = @"Відхилений";
                    break;
                case OderStatus.Formed:
                    label26.Text = @"Сформований";
                    break;
                case OderStatus.ToProvide:
                    label26.Text = @"В забезпеченні";
                    break;
                case OderStatus.InGetPoint:
                    label26.Text = @"В точці отримання";
                    break;
                case OderStatus.Received:
                    label26.Text = @"Отриманий";
                    break;
            }
            label27.Text = order.CreateTime.ToString(CultureInfo.InvariantCulture);
            listViewOrder.Items.Clear();
            foreach (var medicine in order.Medicines)
                listViewOrder.Items.Add(new ListViewItem(new[] {medicine.Key.ToString(), medicine.Value.ToString()}));
        }

        private void buttonOrderCancale_Click(object sender, EventArgs e)
        {
            var order = listBoxOrder.SelectedItem as Order;
            if (order == null)
                return;
            var dialog = new ReasonRejection();
            if (dialog.ShowDialog(this) == DialogResult.Yes)
            {
                order.ReasonRejection = dialog.Reason;
                order.ChangeStatus(OderStatus.Deflected);

                List<Order> validOrders = new List<Order>();
                    foreach (Order ord in SelectedClient.Orders)
                    {
                        if (ord.Status != OderStatus.Deflected)
                        {
                            validOrders.Add(ord);
                        }

                    }
                Refresh(listBoxOrder, validOrders);
                listViewOrder.Items.Clear();
                label22.Text = "";
                label24.Text = "";
                label26.Text = "";
                label27.Text = "";
            }
        }

        private void listBoxAdminOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            var order = listBoxAdminOrder.SelectedItem as Order;
            if (order == null)
                return;
            comboBoxAdminOrder.SelectedIndex = (int) order.Status;
            richTextBoxAdminOrderReason.Text = order.ReasonRejection;
            label32.Text = order.CreateTime.ToString(CultureInfo.InvariantCulture);
            textBoxAdminOrderPrice.Text = order.Price.ToString(CultureInfo.InvariantCulture);
            listViewAdminOrder.Items.Clear();
            foreach (var medicine in order.Medicines)
                listViewAdminOrder.Items.Add(new ListViewItem(new[] {medicine.Key.ToString(), medicine.Value.ToString()}));
        }

        private void buttonOrderApply_Click(object sender, EventArgs e)
        {
            var order = listBoxAdminOrder.SelectedItem as Order;
            if (order == null)
                return;
            order.Status = (OderStatus) comboBoxAdminOrder.SelectedIndex;
            order.ReasonRejection = richTextBoxAdminOrderReason.Text;
            MessageBox.Show("Збережено!");
        }

        #region Методы доступа к атрибутам сборки

        public string AssemblyTitle
        {
            get
            {
                var attributes = Assembly.GetExecutingAssembly()
                    .GetCustomAttributes(typeof (AssemblyTitleAttribute), false);
                if (attributes.Length <= 0)
                    return Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
                var titleAttribute = (AssemblyTitleAttribute) attributes[0];
                return titleAttribute.Title != ""
                    ? titleAttribute.Title
                    : Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion => Assembly.GetExecutingAssembly().GetName().Version.ToString();

        public string AssemblyDescription
        {
            get
            {
                var attributes =
                    Assembly.GetExecutingAssembly().GetCustomAttributes(typeof (AssemblyDescriptionAttribute), false);
                return attributes.Length == 0 ? "" : ((AssemblyDescriptionAttribute) attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                var attributes = Assembly.GetExecutingAssembly()
                    .GetCustomAttributes(typeof (AssemblyProductAttribute), false);
                return attributes.Length == 0 ? "" : ((AssemblyProductAttribute) attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                var attributes = Assembly.GetExecutingAssembly()
                    .GetCustomAttributes(typeof (AssemblyCopyrightAttribute), false);
                return attributes.Length == 0 ? "" : ((AssemblyCopyrightAttribute) attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                var attributes = Assembly.GetExecutingAssembly()
                    .GetCustomAttributes(typeof (AssemblyCompanyAttribute), false);
                return attributes.Length == 0 ? "" : ((AssemblyCompanyAttribute) attributes[0]).Company;
            }
        }

        #endregion

        private void видалитиToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (!SelectedClient.IsAdmin) return;
            var order = listBoxAdminOrder.SelectedItem as Order;
            if (order == null)
                return;
           Order.Items.Remove(order.Id);
            Refresh(listBoxAdminOrder, Order.Items.Values.ToList());
            listViewAdminOrder.Clear();
            MessageBox.Show(@"Видалено!");
        }
    }
}