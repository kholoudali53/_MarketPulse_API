using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G01.Core.Entities.Order
{
    public class Order : BaseEntity<int>
    {
        public Order()
        {
            
        }
        public Order(string buyerEmail, Address shippingAddres, DeliveryMethod deliveryMethod, ICollection<OrderItem> items, decimal subTotal, string paymentIntenId/*, DateTimeOffset orderDate, OrderStatus status*/)
        {
            BuyerEmail = buyerEmail;
            ShippingAddres = shippingAddres;
            DeliveryMethod = deliveryMethod;
            Items = items;
            SubTotal = subTotal;
            PaymentIntenId = paymentIntenId;
            //OrderDate = orderDate;
            //Status = status;
        }

        public string BuyerEmail { get; set; }
        public Address ShippingAddres { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public ICollection<OrderItem> Items { get; set; }
        public decimal SubTotal { get; set; }
        public string PaymentIntenId { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public decimal GetTotal() => SubTotal + DeliveryMethod.Cost;
    }
}
