using Order.Domain.Entities;

namespace Order.Application.Contracts
{
    public interface IOrderAsyncRepository : IAsyncRepository<OrderEntity>
    {
        Task<IEnumerable<OrderEntity>> GetOrdersByUserName(string userName);
    }
}
