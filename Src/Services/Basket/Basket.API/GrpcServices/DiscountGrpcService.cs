using Discount.Grpc.Protos;

namespace Basket.API.GrpcServices
{
    public class DiscountGrpcService
    {
        private readonly DiscountProto.DiscountProtoClient _discountProtoClient;

        public DiscountGrpcService(DiscountProto.DiscountProtoClient discountProtoClient)
        {
            _discountProtoClient = discountProtoClient;
        }
        public async Task<CouponModel> GetDiscount(string productName)
        {
            var protoReq = new DiscountGetRequest { ProductName=productName };
            return await _discountProtoClient.GetDiscountAsync(protoReq);
        }
    }
}
