using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Application.Models;
using Application.Interfaces.Repository;
using Application.Enums;
using System.Text.RegularExpressions;


namespace Application.Services
{
    public class UserService<T>(IJwtProvider jwtProvider, IUserRepository<T> repository) : IUserService<T> where T: User
    {
        public async Task<BasicResponse<string>> Login(string login, string password)
        {
            User user = await repository.GetUserAsync(login, CheckLoginType(login));

            if(user == null)
            {
                return new BasicResponse<string>(StatusCode.NotFound, "User not found", "");
            }

            string token = jwtProvider.GenerateJwt(user);

            return new BasicResponse<string>(StatusCode.Success, "Token created", token);

        }
        public LoginType CheckLoginType(string login)
        {
            if (Regex.IsMatch(login, "^\\d{7,}$"))
            {
                return LoginType.Phone;
            }
            else if(Regex.IsMatch(login, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            {
                return LoginType.Email;
            }

            return LoginType.Login;
        }

        public Task<BasicResponse<string>> Register(User user)
        {
            throw new NotImplementedException();
        }

        public Task<BasicResponse<string>> RegisterAdmin(Admin user)
        {
            throw new NotImplementedException();
        }
    }
}
