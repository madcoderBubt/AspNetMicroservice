using Discount.API.Models;
using Discount.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Discount.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountRepository _discountRepository;
        public DiscountController(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
        }
        [HttpGet("{productName}")]
        public async Task<ActionResult<Coupon>> GetDiscount(string productName)
        {
            var data = await _discountRepository.GetCoupon(productName);
            return Ok(data);
        }
        [HttpPost]
        public async Task<ActionResult<Coupon>> CreateDiscount(Coupon coupon)
        {
            await _discountRepository.CreateCoupon(coupon);
            return CreatedAtRoute("GetDiscount", new { productName=coupon.ProductName},coupon);
        }
        [HttpPut]
        public async Task<ActionResult<Coupon>> UpdateDiscount(Coupon coupon)
        {
            await _discountRepository.CreateCoupon(coupon);
            return CreatedAtRoute("GetDiscount", new { productName = coupon.ProductName }, coupon);
        }

        [HttpDelete("{productName}")]
        public async Task<ActionResult<bool>> DeleteDiscount(string productName)
        {
            var data = await _discountRepository.DeleteCoupon(productName);
            return Ok(data);
        }
    }
}
