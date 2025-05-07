using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPasswordHasher
    {
        public string HashPassword(string password);
        public bool Verify(string password, string hashedPassword);
    }
}
