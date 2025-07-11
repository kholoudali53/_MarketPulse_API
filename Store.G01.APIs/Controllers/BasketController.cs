using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.G01.APIs.Errors;
using Store.G01.Core.Dtos.Baskets;
using Store.G01.Core.Entities;
using Store.G01.Core.Repositories.Contract;

namespace Store.G01.APIs.Controllers
{
    public class BasketController : BaseAPIController
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasket(string? id)
        {
            if (id is null) return BadRequest(new APIErrorResponse(400, "Invalid Id !!"));

            var basket = await _basketRepository.GetBasketAsync(id);

            if (basket is null)
            {
                if (int.TryParse(id, out int basketId))
                {
                    //basket = new CustomerBasket() { Id = basketId };
                }
                else
                {
                    return BadRequest(new APIErrorResponse(400, "Id is not a valid integer"));
                }
            }

            return Ok(basket);
        }
        /*public async Task<ActionResult<CustomerBasket>> GetBasket(string? id)
        {
            if (id is null) return BadRequest(new APIErrorResponse(400, "Invalid Id !!"));
            var basket = await _basketRepository.GEtBasketAsync(id);

            if (basket is null)
            {
               basket = new CustomerBasket() { Id = id };
            return Ok(basket);
        }*/


        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> CreatedOrUpdatedBasket(CustomerBasketDto model)
        {
            var basket = await _basketRepository.UpdateBasketAsync(_mapper.Map<CustomerBasket>(model));

            if (basket is null) return BadRequest(new APIErrorResponse(400));
            return Ok(basket);
        }
        [HttpDelete]
        public async Task DeleteBasket(string id)
        {
            await _basketRepository.DeleteBasketAsync(id);
        }
    }
}
