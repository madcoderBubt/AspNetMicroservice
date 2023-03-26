using Basket.API.GrpcServices;
using Basket.API.Models;
using Basket.API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basket.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository repository;
        private readonly DiscountGrpcService discountGrpcService;

        public BasketController(IBasketRepository repository, DiscountGrpcService discountGrpcService)
        {
            this.repository = repository;
            this.discountGrpcService = discountGrpcService;
        }
        [HttpGet("{userName}")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> GetBasket(string userName)
        {
            var basket = await repository.GetBasket(userName);
            return Ok(basket ?? new ShoppingCart(userName));
        }
        [HttpPut]
        public async Task<ActionResult<ShoppingCart>> UpdateBasket(ShoppingCart basket)
        {
            foreach (var item in basket.ShoppingCartItems)
            {
                var coupon = await discountGrpcService.GetDiscount(item.ProductName);
                item.Price -= coupon.Amount;
            }//Discount grpc
            return Ok(await repository.UpdateBasket(basket));
        }
        [HttpDelete("{userName}")]
        public async Task<ActionResult> DeleteBasket(string userName)
        {
            await repository.DeleteBasket(userName);
            return Ok();
        }
    }
}
