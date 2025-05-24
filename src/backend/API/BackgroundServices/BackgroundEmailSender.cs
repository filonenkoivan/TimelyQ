using Application.Interfaces;
using Application.Interfaces.Repository;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Reflection;

namespace API.BackgroundServices
{
    public class BackgroundEmailSender
    {
        private IUserRepository _repository;
        private IEmailSenderService _sender;

        public BackgroundEmailSender(IUserRepository repository, IEmailSenderService sender)
        {
            _repository = repository;
            _sender = sender;
        }
        public async Task EmailSender()
        {

            var users = await _repository.GetCustomersAsync();
            foreach (var i in users )
            {
                foreach(var e in i.Entries)
                {
                    if(DateTime.UtcNow.Subtract(e.DateTime) <= TimeSpan.FromMinutes(15))
                    {
                        await _sender.SendEmailAsync(i.Email, "Your appointment in Timelyq!", "Hi, we hope you didn't forget about your appointment! You have less than 15 minutes left.");
                    }
                }
                
            }
        }
    }
}
