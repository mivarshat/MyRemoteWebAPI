using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models.Domain;

namespace WebApplication1.Repositories
{
    public class WalkDifficultyRepository : IWalkDifficultyRepository
    {
        private readonly WalkDBContext walkDBContext;

        public WalkDifficultyRepository(WalkDBContext walkDBContext)
        {
            this.walkDBContext = walkDBContext;
        }

        public async Task<WalkDifficulty> Add(WalkDifficulty walkDifficulty)
        {
            walkDifficulty.Id = Guid.NewGuid();
            await walkDBContext.AddAsync(walkDifficulty);
            await walkDBContext.SaveChangesAsync();
            return walkDifficulty;
        }

        public async Task<WalkDifficulty> Get(Guid id)
        {
            return await walkDBContext.WalkDifficulties.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<WalkDifficulty>> GetAll()
        {
            return await walkDBContext.WalkDifficulties.ToListAsync();
        }
    }
}
