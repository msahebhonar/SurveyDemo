using Survey.Entities.User;

namespace Survey.Services
{
    public interface IUserAccountRepository
    {
        UserAccount Login(string email, string password);
    }
}