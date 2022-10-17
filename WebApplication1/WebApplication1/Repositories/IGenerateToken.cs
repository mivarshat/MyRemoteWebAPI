using WebApplication1.Models.Domain;

namespace WebApplication1.Repositories
{
    public interface IGenerateToken
    {
        Task<string> GenerateTokenAsync(User user);
    }
}
