using System;
using Survey.Entities.User;

namespace Survey.Models.Login
{
    public class UserAccountDto
    {
        public Guid UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public static UserAccountDto ConvertToDto(UserAccount userAccount)
        {
            return new UserAccountDto
            {
                UserId = userAccount.UserAccountId,
                FirstName = userAccount.FirstName,
                LastName = userAccount.LastName,
                Email = userAccount.Email
            };
        }
    }
}