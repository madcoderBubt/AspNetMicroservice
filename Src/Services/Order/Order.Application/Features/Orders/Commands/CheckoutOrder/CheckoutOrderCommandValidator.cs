using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Features.Orders.Commands.CheckoutOrder
{
    public class CheckoutOrderCommandValidator:AbstractValidator<UpdateOrderCommand>
    {
        public CheckoutOrderCommandValidator()
        {
            RuleFor(p => p.UserName)
                .NotEmpty().WithMessage("User name required")
                .NotNull()
                .MaximumLength(50).WithMessage("Not max than 50");
            RuleFor(p => p.EmailAddress)
                .NotEmpty().WithMessage("email required");
            RuleFor(p => p.TotalPrice)
                .GreaterThan(0).WithMessage("Total price should be greater than 0.");
        }
    }
}
