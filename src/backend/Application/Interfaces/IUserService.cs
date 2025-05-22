using Application.Enums;
using Application.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserService
    {
        public Task<BasicResponse<string>> Login(string email, string password);

        public Task<BasicResponse<string>> Register(User user);
        public Task<BasicResponse<string>> RegisterBusiness(User user, UserBusiness businessInfo);

        public LoginType CheckLoginType(string login);

        public Task<User> GetUserAsync(string email);
    }
}
