using Store.G01.Core;
using Store.G01.Core.Entities;
using Store.G01.Core.Entities.Order;
using Store.G01.Core.Repositories.Contract;
using Store.G01.Core.Services.Contract;
using Store.G01.Core.Specifications.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G01.Service.Services.Orders
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketService _basketService;

        public OrderService(IUnitOfWork unitOfWork, IBasketService basketService)
        {
            _unitOfWork = unitOfWork;
            _basketService = basketService;
        }
        public async Task<Order> CreateOrderAsync(string BuyerEmail, string basketId, int deliveryMethodId, Address shippingAddress)
        {
            var basket = await _basketService.GetBasketAsync(basketId);
            if (basket is null) return null;

            var orderItems = new List<OrderItem>();

            if(basket.Items.Count()>0)
            {
                foreach (var item in basket.Items)
                {
                    var product = await _unitOfWork.Repository<Product, int>().GetAsync(item.Id);
                    var ProductOrderItem = new ProductItemOrder(product.Id, product.Name, product.PictureUrl);
                    var orderItem = new OrderItem(ProductOrderItem, product.Price, item.Quantity);

                    orderItems.Add(orderItem);
                }
            }

            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod, int>().GetAsync(deliveryMethodId);

            var subTotal = orderItems.Sum(I => I.Price * I.Quantity);

            var order = new Order(BuyerEmail, shippingAddress, deliveryMethod, orderItems, subTotal, "");

            await _unitOfWork.Repository<Order, int>().AddAsync(order);

            var result = await _unitOfWork.CompleteAsync();

            if (result <= 0) return null;

            return order;
        }

        public async Task<Order?> GetOrderByIdForSpecificUserAsync(string buyerEmail, int orderId)
        {
            var spec = new OrderSpecifications(buyerEmail, orderId);

            var order = await _unitOfWork.Repository<Order, int>().GetWithSpecAsync(spec);

            if (order is null) return null;

            return order;
        }

        public async Task<IEnumerable<Order>?> GetOrderForSpecificUserAsync(string buyerEmail)
        {
            var spec = new OrderSpecifications(buyerEmail);

            var orders = await _unitOfWork.Repository<Order, int>().GetAllWithSpecAsync(spec);

            if(orders is null) return null;

            return orders;
        }
    }
}
