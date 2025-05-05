using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService(IJwtProvider jwtProvider) : IUserService
    {
        public string Login(string email, string password)
        {
            User user = new User
            {
                Name = "ivan",
                Email = "1212",
                Id = 12,
                Surname = "lol"
            };
            string token = jwtProvider.GenerateJwt(user);

            return token;

        }
    }
}
