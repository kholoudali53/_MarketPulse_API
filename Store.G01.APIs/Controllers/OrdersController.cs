using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.G01.APIs.Errors;
using Store.G01.Core.Dtos.Orders;
using Store.G01.Core.Identity;
using Store.G01.Core.Entities.Order;
using Store.G01.Core.Services.Contract;
using System.Security.Claims;

namespace Store.G01.APIs.Controllers
{
    public class OrdersController : BaseAPIController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderDto model)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            if (userEmail is null) return Unauthorized(new APIErrorResponse(StatusCodes.Status401Unauthorized));

            var address = _mapper.Map<Core.Entities.Order.Address>(model.shipToAddress);

            var order = await _orderService.CreateOrderAsync(userEmail, model.BasketId, model.DeliveryMethodId, address);

            if (order is null) return BadRequest(new APIErrorResponse(StatusCodes.Status400BadRequest));

            return Ok(_mapper.Map<OrderToReturnDto>(order));
        }
    }
}
