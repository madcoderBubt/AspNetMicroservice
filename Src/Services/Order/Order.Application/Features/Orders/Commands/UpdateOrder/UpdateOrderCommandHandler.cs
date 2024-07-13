using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Order.Application.Contracts;
using Order.Application.Exceptions;
using Order.Application.Models;
using Order.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Features.Orders.Commands.CheckoutOrder
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
    {
        private readonly IMapper _mapper;
        private readonly IOrderAsyncRepository _orderRepository;
        private readonly IEmailService _emailService;
        private readonly ILogger<UpdateOrderCommandHandler> _logger;

        public UpdateOrderCommandHandler(IMapper mapper, IOrderAsyncRepository orderRepository, IEmailService emailService, ILogger<UpdateOrderCommandHandler> logger)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
            _emailService = emailService;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var entityToUpdate = await _orderRepository.GetByIdAsync(request.ID);
            if (entityToUpdate == null) {
                //_logger.LogError("404: Data not found!");
                throw new NotFoundException();
            }
            _mapper.Map(request, entityToUpdate, typeof(UpdateOrderCommand), typeof(OrderEntity));

            await _orderRepository.UpdateAsync(entityToUpdate);

            _logger.LogInformation($"Order {entityToUpdate.ID} is updated.");
            //await SendEmail(entityToUpdate);

            return Unit.Value;
        }

        //private async Task SendEmail(OrderEntity result)
        //{
        //    var email = new Email { To = "", Subject = "", Body = "" };
        //    try
        //    {
        //        await _emailService.SendEmail(email);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Order {result.ID} is failed due to email error: {ex.Message}");
        //        throw;
        //    }
        //}
    }
}
