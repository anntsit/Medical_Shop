using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace MedicaleShop.Core.Classes
{
    // Клиенты
    [DataContract]
    public class Client : Base<Client>, IFileManager
    {
        public Client(string name, string phone, int discount = 0) : base(name)
        {
            Phote = phone;
            Discount = discount;
        }

        public Client()
        {
        }

        [DataMember]
        public string Phote { get; set; } // Номер телефону

        [DataMember]
        public int Discount { get; set; } // Знижка

        [DataMember]
        public bool IsAdmin { get; set; }

        [DataMember]
        public string Login { get; set; }

        [DataMember]
        public string Password { get; set; }

        public List<Order> Orders => Order.Items.Values.Where(value => value.Client == this).ToList();

        public override string ToString()
        {
            return base.ToString() + $" ({Login}), тел. " + Phote + ", " +
                   (Discount > 0 ? $"знижка - {Discount}%" : "знижки немає");
        }
    }
}