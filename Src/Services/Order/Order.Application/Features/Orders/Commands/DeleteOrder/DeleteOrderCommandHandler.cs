using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Order.Application.Contracts;
using Order.Application.Exceptions;
using Order.Application.Features.Orders.Commands.CheckoutOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Features.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
    {
        private readonly IMapper _mapper;
        private readonly IOrderAsyncRepository _orderRepository;
        //private readonly IEmailService _emailService;
        private readonly ILogger<CheckoutOrderCommandHandler> _logger;

        public DeleteOrderCommandHandler(IMapper mapper, IOrderAsyncRepository orderRepository, ILogger<CheckoutOrderCommandHandler> logger)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var orderEnitity = await _orderRepository.GetByIdAsync(request.ID);
            if (orderEnitity == null)
            {
                //_logger.LogInformation("404: Data not found!");
                throw new NotFoundException();
            }

            await _orderRepository.DeleteAsync(orderEnitity);
            return Unit.Value;
        }
    }
}
