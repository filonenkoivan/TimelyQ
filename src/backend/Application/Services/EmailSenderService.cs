using Application.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class EmailSenderService(IConfiguration configuration) : IEmailSenderService
    {

        public async Task SendEmailAsync(string emailAdress, string subject, string body)
        {
            string mail = "timelyqservice@gmail.com";
            string password = configuration["Email:password"];

            var client = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(mail, password),
                EnableSsl = true,
            };
            await client.SendMailAsync(from: mail, recipients: emailAdress, subject: subject, body: body);
        }
    }
}
