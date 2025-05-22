using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IEmailSenderService
    {
        public Task SendEmailAsync(string email, string subject, string text);
    }
}
