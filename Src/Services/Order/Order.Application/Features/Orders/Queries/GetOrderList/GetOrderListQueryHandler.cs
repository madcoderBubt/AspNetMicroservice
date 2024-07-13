using MediatR;
using Order.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Features.Orders.Queries.GetOrderList
{
    public class GetOrderListQueryHandler : IRequestHandler<GetOrderListQuery, List<OrderVM>>
    {
        private readonly IOrderAsyncRepository orderRepository;

        public GetOrderListQueryHandler(IOrderAsyncRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public async Task<List<OrderVM>> Handle(GetOrderListQuery request, CancellationToken cancellationToken)
        {
            var result = await orderRepository.GetOrdersByUserName(request.UserName);
            return result.Select((item) =>
                new OrderVM
                {
                    UserName = item.UserName,
                }).ToList();
        }
    }
}
