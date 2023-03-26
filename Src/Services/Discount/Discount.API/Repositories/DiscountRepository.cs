using Dapper;
using Discount.API.Models;
using Npgsql;

namespace Discount.API.Repositories
{
    public class DiscountRepository:IDiscountRepository
    {
        private readonly IConfiguration configuration;
        public DiscountRepository(IConfiguration configuration)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<Coupon> GetCoupon(string productName)
        {
            using (var conn = new NpgsqlConnection(configuration.GetValue<string>("PostgreDbSetting:ConnectionString")))
            {
                Coupon coupon = await conn.QueryFirstOrDefaultAsync<Coupon>
                    ("Select * from Coupon where ProductName=@ProductName", new { ProductName = productName });

                return coupon ?? new Coupon { ProductName = "No Discount", Amount = 0, Description = "No Discount Available." };
            }
        }

        public async Task<bool> CreateCoupon(Coupon coupon)
        {
            using (var conn = new NpgsqlConnection(configuration.GetValue<string>("PostgreDbSetting:ConnectionString")))
            {
                var result = await conn.ExecuteAsync
                    ("INSERT INTO Coupon (ProductName, Description, Amount) VALUES (@ProductName, @Description, @Amount)",
                    new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Description });

                return (result > 0);
            }
        }

        public async Task<bool> DeleteCoupon(string productName)
        {
            using (var conn = new NpgsqlConnection(configuration.GetValue<string>("PostgreDbSetting:ConnectionString")))
            {
                var result = await conn.ExecuteAsync
                    ("delete from Coupon where ProductName=@ProductName)",
                    new { ProductName = productName });

                return (result > 0);
            }
        }

        public async Task<bool> UpdateCoupon(Coupon coupon)
        {
            using (var conn = new NpgsqlConnection(configuration.GetValue<string>("PostgreDbSetting:ConnectionString")))
            {
                var result = await conn.ExecuteAsync
                    ("Update Coupon set ProductName=@ProductName, Description=@Description, Amount=@Amount where ID=@Id)",
                    new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Description, Id=coupon.ID });

                return (result > 0);
            }
        }
    }
}
