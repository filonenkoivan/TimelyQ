using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Providers
{
    public class PasswordHasher
    {
        public string HashPassword(string password)
        {
            var hashedPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(password, 13);
            return hashedPassword;
        }
        public bool Verify(string password, string hashedPassword)
        {
            var verifyPassword = BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);

            if (verifyPassword)
            {
                return true;
            }
            return false;
        }
    }
}
