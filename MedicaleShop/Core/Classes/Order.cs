using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace MedicaleShop.Core.Classes
{
    [DataContract]
    public enum OderStatus
    {
        [EnumMember] Formed, // Сформований
        [EnumMember] Confirmed, // підтверджений
        [EnumMember] ToProvide, // в забезпеченні
        [EnumMember] InGetPoint, // в точці отримання
        [EnumMember] Received, // отриманий
        [EnumMember] Deflected // відхилений
    }

    // Замовлення
    [DataContract]
    public class Order : Base<Order>, IFileManager
    {
        [DataMember] private Guid? _clientId; // ІД Клієнта
        [DataMember] private Guid? _pharmacyId; //  ІД аптеки доставки

        [DataMember] public int OrderId;

        public Order(string name = "Замовлення №{0}") : base(string.Format(name, SecondId))
        {
            OrderId = SecondId;
            CreateTime = DateTime.Now;
            Status = OderStatus.Formed;
        }

        public static int SecondId => Items.Values.Select(order => order.OrderId).Concat(new[] {0}).Max() + 1;

        public Client Client
        {
            get { return _clientId != null ? Client.Items[_clientId.Value] : null; }
            set { _clientId = value?.Id; }
        }

        public Pharmacy Pharmacy
        {
            get { return _pharmacyId == null ? null : Pharmacy.Items[_pharmacyId.Value]; }
            set { _pharmacyId = value?.Id; }
        }

        [DataMember]
        public DateTime CreateTime { get; set; } // Дата створення

        [DataMember]
        public DateTime CloseOrderTime { get; set; } // Дата закриття замовлення

        [DataMember]
        public OderStatus Status { get; set; } // Статус замовлення

        [DataMember]
        public int Discount { get; set; } // Дисконт

        [DataMember]
        public decimal Price { get; set; } // Сума заказу без знижки

        [DataMember]
        public string ReasonRejection { get; set; } // Причина відхилення замовлення

        [DataMember]
        public bool NeedRecipe { get; set; } // Потрібен рецепт для отримання

        [DataMember]
        public Dictionary<Medicin, int> Medicines
        {
            get
            {
                var temp = new Dictionary<Medicin, int>();
                foreach (var value in Purchase.Items.Values)
                {
                    if (value.Order == this)
                    {
                        if (!temp.ContainsKey(value.Medicin))
                            temp.Add(value.Medicin, value.Count);
                    }
                }
                return temp;
            }
        }

        public void ChangeStatus(OderStatus status)
        {
            Status = status;
            if (status == OderStatus.Received || status == OderStatus.Deflected)
                CloseOrderTime = DateTime.Now;
        }
    }
}