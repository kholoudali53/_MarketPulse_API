using Store.G01.Core.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G01.Core.Services.Contract
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(string BuyerEmail, string basketId, int deliveryMethod, Address shippingAddress);
        Task<IEnumerable<Order>?> GetOrderForSpecificUserAsync(string buyerEmail);
        Task<Order?> GetOrderByIdForSpecificUserAsync(string buyerEmail, int orderId);
    }
}
