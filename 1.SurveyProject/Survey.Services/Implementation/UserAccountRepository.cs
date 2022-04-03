using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Survey.DataAccess;
using Survey.Entities.User;

namespace Survey.Services.Implementation
{
    public class UserAccountRepository:IUserAccountRepository
    {
        private readonly ApplicationDbContext _context;

        public UserAccountRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public UserAccount Login(string email, string password)
        {
            return _context.UserAccounts.SingleOrDefault(x => x.Email == email.Trim().ToLower() && x.Password == GetHashPassword(password));
        }

        private static string GetHashPassword(string password)
        {
            var sha = SHA512.Create();
            var pwd = Encoding.UTF8.GetBytes(password.Trim());
            var hash = sha.ComputeHash(pwd);
            return Convert.ToBase64String(hash);
        }
    }
}