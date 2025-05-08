using Domain.Entities;
using Domain.Enums;

namespace API.Contracts
{
    public class RequestRegister
    {
        public string? Name { get; set; }
        public string? Login { get; set; }
        public string? Surname { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? PasswordConfirm { get; set; }

        public string? CompanyName { get; set; }
        public CompanyCategory CompanyCategory { get; set; }
    }
    public static class RequestRegisterExtension { 
        public static User ToUserModel(this RequestRegister register)
        {
            return new User
            {
                Name = register.Name,
                Surname = register.Surname,
                PhoneNumber = register.PhoneNumber,
                Email = register.Email,
                Password = register.Password,
                Login = register.Login
            };
        }
        public static UserBusiness ToBusinessInfo(this RequestRegister register)
        {
            return new UserBusiness
            {
                CompanyCategory = register.CompanyCategory,
                CompanyName = register.CompanyName
            };
        }
    }
}
