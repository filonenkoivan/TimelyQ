using Application.Enums;
using Application.Interfaces.Repository;
using Application.Models.DTOs;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.DataBaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repository
{
    public class UserRepository(AppDbContext db) : IUserRepository
    {
        public async Task CreateUserAsync(User user)
        {
            await db.Users.AddAsync(user);
            await db.SaveChangesAsync();
        }

        public async Task CreateUserBusinessAsync(User user, UserBusiness businessInfo)
        {
            var isUserExist = await db.Users.FirstOrDefaultAsync(x => x.Login == user.Login);
            if (isUserExist != null)
            {
                return;
            }
            user.UserBusiness = businessInfo;
            businessInfo.User = user;

            await db.Users.AddAsync(user);
            await db.UserBusiness.AddAsync(businessInfo);
            await db.SaveChangesAsync();
        }

        public async Task<User> GetUserAsync(string login, LoginType type = LoginType.Login)
        {
            return type switch
            {
                LoginType.Phone => await db.Users.FirstOrDefaultAsync(x => x.PhoneNumber == login),

                LoginType.Email => await db.Users.FirstOrDefaultAsync(x => x.Email == login),

                _ => await db.Users.FirstOrDefaultAsync(x => x.Login == login)
            };
        }

        public async Task<ICollection<User>> GetCustomersAsync()
        {
            var users = await db.Users.Include(x=>x.Entries).ToListAsync();
            return users;
        }

    }
}
