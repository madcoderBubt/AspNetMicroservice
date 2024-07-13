﻿using MediatR;
using Order.Domain.Entities;

namespace Order.Application.Features.Orders.Queries.GetOrderList;

public class GetOrderListQuery : IRequest<List<OrderVM>>
{
    public string UserName { get; set; }
    public GetOrderListQuery(string userName)
    {
        UserName = userName ?? throw new ArgumentNullException(nameof(userName));
    }
}
