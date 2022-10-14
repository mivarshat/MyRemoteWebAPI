using WebApplication1.Models.Domain;

namespace WebApplication1.Repositories
{
    public interface IWalkDifficultyRepository
    {
        Task<IEnumerable<WalkDifficulty>> GetAll();

        Task<WalkDifficulty> Get(Guid id);

        Task<WalkDifficulty> Add(WalkDifficulty walkDifficulty);
    }
}
