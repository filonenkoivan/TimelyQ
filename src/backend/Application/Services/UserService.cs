﻿using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Domain.Enums;
using Application.Models;
using Application.Interfaces.Repository;
using Application.Enums;
using System.Text.RegularExpressions;


namespace Application.Services
{
    public class UserService(IJwtProvider jwtProvider, IUserRepository repository) : IUserService
    {
        public async Task<BasicResponse<string>> Login(string login, string password)
        {
            User user = await repository.GetUserAsync(login, CheckLoginType(login));

            if(user == null)
            {
                return new BasicResponse<string>(StatusCode.NotFound, "User not found", "");
            }

            string token = jwtProvider.GenerateJwt(user, user.Role);
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

        public async Task<BasicResponse<string>> Register(User user)
        {
            var result = await CheckUserExists(user);
            if(result.StatusCode != StatusCode.Success)
            {
                return result;
            }
            user.Role = Roles.User;
            await repository.CreateUserAsync(user);
            return result;
        }

        public async Task<BasicResponse<string>> RegisterBusiness(User user, UserBusiness businessInfo)
        {
            var result = await CheckUserExists(user);
            if (result.StatusCode != StatusCode.Success)
            {
                return result;
            }
            user.Role = Roles.UserBusiness;
            await repository.CreateUserBusinessAsync(user, businessInfo);
            return result;
        }

        public async Task<User> GetUserAsync(string login)
        {
            return await repository.GetUserAsync(login,  CheckLoginType(login));
        }
        public async Task<BasicResponse<string>> CheckUserExists(User user)
        {
            var dbUserEmail = await repository.GetUserAsync(user.Email, LoginType.Email);
            var dbUserLogin = await repository.GetUserAsync(user.Login, LoginType.Login);
            if (dbUserEmail != null)
            {
                return new BasicResponse<string>(StatusCode.BadRequest, "User with this email already exists", "");
            }
            else if (dbUserLogin != null)
            {
                return new BasicResponse<string>(StatusCode.BadRequest, "User with this login already exists", "");

            }
            return new BasicResponse<string>(StatusCode.Success, "User created", "");
        }

    }
}
