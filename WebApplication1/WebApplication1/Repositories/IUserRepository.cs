using WebApplication1.Models.Domain;

namespace WebApplication1.Repositories
{
    public interface IUserRepository
    {
        Task<User> AuthenticateUser(string username, string password);
    }
}
