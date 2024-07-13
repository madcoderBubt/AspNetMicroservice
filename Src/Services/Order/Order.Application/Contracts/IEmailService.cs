using Order.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Contracts
{
    public interface IEmailService
    {
        Task<bool> SendEmail(Email email);
    }
}
