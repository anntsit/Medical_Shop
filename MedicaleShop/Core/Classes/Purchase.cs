using System;
using System.Runtime.Serialization;
using MedicaleShop.Utils;

namespace MedicaleShop.Core.Classes
{
    // Замовленні медикаменти
    [DataContract]
    public class Purchase : Base<Purchase>, IFileManager
    {
        [DataMember] private Guid? _medicinId; // ІД медикамента
        [DataMember] private Guid? _orderId; // ІД заказ

        public Purchase(Order order, Medicin medicin, int count) : base("")
        {
            Order = order;
            Medicin = medicin;
            Count = count;
        }

        public Purchase()
        {
        }

        public Order Order
        {
            get { return _orderId == null ? null : Order.Items[_orderId.Value]; }
            set { _orderId = value?.Id; }
        }

        public Medicin Medicin
        {
            get { return _medicinId == null ? null : Medicin.Items[_medicinId.Value]; }
            set { _medicinId = value?.Id; }
        }

        [DataMember]
        public int Count { get; set; } // Кількість упаковок

        public decimal Price
            => (Order.Price - (Order.Discount != 0 ? MathUtil.RoundUp(Order.Price*Order.Discount, 2) : 0))*Count;

        // Ціна
    }
}