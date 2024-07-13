using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Order.Application.Contracts;
using Order.Application.Models;
using Order.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Features.Orders.Commands.CheckoutOrder
{
    public class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly IOrderAsyncRepository _orderRepository;
        private readonly IEmailService _emailService;
        private readonly ILogger<CheckoutOrderCommandHandler> _logger;

        public CheckoutOrderCommandHandler(IMapper mapper, IOrderAsyncRepository orderRepository, IEmailService emailService, ILogger<CheckoutOrderCommandHandler> logger)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
            _emailService = emailService;
            _logger = logger;
        }

        public async Task<int> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {
            var orderEntity = _mapper.Map<OrderEntity>(request);
            var result = await _orderRepository.AddAsync(orderEntity);

            _logger.LogInformation($"Order {result.ID} is created.");
            await SendEmail(result);

            return result.ID;
        }

        private async Task SendEmail(OrderEntity result)
        {
            var email = new Email { To = "shbsovon@gmail.com", Subject = "Checkout Email", Body = "Someone order something." };
            try
            {
                await _emailService.SendEmail(email);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Order {result.ID} is failed due to email error: {ex.Message}");
                throw;
            }
        }
    }
}
