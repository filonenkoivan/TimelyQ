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
    public class UserRepository<T>(AppDbContext db) : IUserRepository<T> where T: User
    {
        public async Task<T> GetUserAsync(string login, LoginType type = LoginType.Login)
        {
            return type switch
            {
                LoginType.Phone => await db.Set<T>().FirstOrDefaultAsync(x => x.PhoneNumber == login),

                LoginType.Email => await db.Set<T>().FirstOrDefaultAsync(x => x.Email == login),

                _ => await db.Set<T>().FirstOrDefaultAsync(x => x.Name == login)
        };
        }
    }
}
