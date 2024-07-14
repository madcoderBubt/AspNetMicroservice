using Microsoft.Extensions.Logging;
using Order.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Infrastructure.Data.Context
{
    public static class OrderDbContextSeed
    {
        public static async Task SeedAsync(OrderDbContext dbContext, ILogger<OrderDbContext> logger)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException("Database Context is null.");
            }

            if (!dbContext.Orders.Any())
            {
                dbContext.Orders.AddRange(GetDefaultOrders());
                await dbContext.SaveChangesAsync();
            }
        }

        private static IEnumerable<OrderEntity> GetDefaultOrders()
        {
            var orders = new List<OrderEntity>
            {
                new() {UserName = "testUser", FirstName="Sovon", LastName="Biswas", EmailAddress="shbsovon@gmail.com", TotalPrice=130 }
            };

            return orders;
        }
    }
}
