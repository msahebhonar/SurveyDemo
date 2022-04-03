using System;

namespace Survey.Entities.User
{
    public class UserAccount
    {
        public Guid UserAccountId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Fullname => FirstName + LastName;

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
