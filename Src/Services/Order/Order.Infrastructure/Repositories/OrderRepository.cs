using Microsoft.EntityFrameworkCore;
using Order.Application.Contracts;
using Order.Domain.Entities;
using Order.Infrastructure.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Infrastructure.Repositories
{
    public class OrderRepository : RepositoryBase<OrderEntity>, IOrderAsyncRepository
    {
        public OrderRepository(OrderDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<OrderEntity>> GetOrdersByUserName(string userName)
        {
            var queryResult = await _dbContext.Orders.Where(f=> f.UserName  == userName).ToListAsync();
            return queryResult;
        }
    }
}
