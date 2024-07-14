using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Order.Application.Contracts;
using Order.Application.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Order.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        public EmailSettings _emailSettings { get; }
        public ILogger<EmailService> _logger { get; }
        public EmailService(IOptions<EmailSettings> options, ILogger<EmailService> logger)
        {
            _emailSettings = options.Value;
            _logger = logger;
        }
        public async Task<bool> SendEmail(Email email)
        {
            var client = new SendGridClient(_emailSettings.ApiKey);
            var to = new EmailAddress(email.To);
            var from = new EmailAddress() { Email = _emailSettings.FromAddress, Name = _emailSettings.FromName };

            _logger.LogInformation("Email Sending...");

            var sendGridEmail = MailHelper.CreateSingleEmail(from, to, email.Subject, email.Body, email.Body);
            var sendResponse = await client.SendEmailAsync(sendGridEmail);

            if(sendResponse.StatusCode == HttpStatusCode.Accepted || sendResponse.StatusCode == HttpStatusCode.OK)
            {
                _logger.LogInformation("Email Sent...");
                return true;
            }

            _logger.LogError($"Send email failed.");
            return false;
        }
    }
}
