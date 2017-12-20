using System.Runtime.Serialization;
using System.Web;

namespace MedicaleShop.Core.Classes
{
    // Аптеки
    [DataContract]
    public class Pharmacy : Base<Pharmacy>, IFileManager
    {
        public Pharmacy(string name, string address) : base(name)
        {
            Address = address;
        }

        public Pharmacy()
        {
        }

        [DataMember]
        public string Address { get; set; } // Адреса аптеки

        [DataMember]
        public string Description { get; set; } // Опис аптеки

        public string MapUri => "https://www.google.com.ua/maps/search/" + HttpUtility.UrlEncode(Address);
    }
}