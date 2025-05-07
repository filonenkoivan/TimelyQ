using Application.Enums;
using Application.Models.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repository
{
    public interface IUserRepository
    {
        public Task<User> GetUserAsync(string login, LoginType type);

        public Task CreateUserAsync(User user);
        public Task CreateUserBusinessAsync(User user, UserBusiness businessInfo);

    }
}
