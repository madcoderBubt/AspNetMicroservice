using Discount.Grpc.Models;
using Discount.Grpc.Protos;
using Discount.Grpc.Repositories;
using Grpc.Core;

namespace Discount.Grpc.Services
{
    public class DiscountService:DiscountProto.DiscountProtoBase
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly ILogger<DiscountService> _logger;

        public DiscountService(ILogger<DiscountService> logger, IDiscountRepository discountRepository)
        {
            _logger = logger;
            _discountRepository = discountRepository;
        }

        public override async Task<CouponModel> GetDiscount(DiscountGetRequest request, ServerCallContext context)
        {
            //return base.GetDiscount(request, context);
            var coupon = await _discountRepository.GetCoupon(request.ProductName);
            if (coupon == null)
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount with ProductName={request.ProductName} is not found."));
            _logger.LogInformation("Discount is retrieved for ProductName : {productName}, Amount : {amount}", coupon.ProductName, coupon.Amount);

            return MapModel(coupon);
        }
        public override async Task<CouponModel> CreateDiscount(DiscountChangeRequest request, ServerCallContext context)
        {
            //return base.CreateDiscount(request, context);
            var coupon = MapModel(request.Coupon);
            bool success = await _discountRepository.CreateCoupon(coupon);
            _logger.LogInformation("Discount is successfully Created. ProductName : {ProductName}", coupon.ProductName);
            return MapModel(coupon);
        }
        public override async Task<CouponModel> UpdateDiscount(DiscountChangeRequest request, ServerCallContext context)
        {
            var coupon = MapModel(request.Coupon);
            bool success = await _discountRepository.UpdateCoupon(coupon);
            _logger.LogInformation("Discount is successfully Updated. ProductName : {ProductName}", coupon.ProductName);
            return MapModel(coupon);
        }
        public override async Task<DeleteResponse> DeleteDiscount(DiscountGetRequest request, ServerCallContext context)
        {
            //return base.DeleteDiscount(request, context);
            bool success = await _discountRepository.DeleteCoupon(request.ProductName);
            _logger.LogInformation("Discount is successfully Deleted. ProductName : {ProductName}", request.ProductName);
            return new DeleteResponse { Success = success };
        }

        private CouponModel MapModel(Coupon coupon)
        {
            return new CouponModel
            { Id = coupon.ID, ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount };
        }
        private Coupon MapModel(CouponModel coupon)
        {
            return new Coupon
            { ID = coupon.Id, ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount };
        }
    }
}
