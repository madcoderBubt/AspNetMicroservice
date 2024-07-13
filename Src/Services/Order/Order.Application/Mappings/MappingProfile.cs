using AutoMapper;
using Order.Application.Features.Orders.Commands.CheckoutOrder;
using Order.Application.Features.Orders.Queries;
using Order.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<OrderEntity, OrderVM>().ReverseMap();
            CreateMap<OrderEntity, UpdateOrderCommand>().ReverseMap();
            CreateMap<OrderEntity, CheckoutOrderCommand>().ReverseMap();
        }
    }
}
