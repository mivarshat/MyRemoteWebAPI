using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models.Domain;

namespace WebApplication1.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private readonly WalkDBContext walkDBContext;

        public WalkRepository(WalkDBContext walkDBContext)
        {
            this.walkDBContext = walkDBContext;
        }

        public async Task<Walk> AddWalkAsynch(Walk walk)
        {
            walk.Id = new Guid();

            await walkDBContext.Walks.AddAsync(walk);
            await walkDBContext.SaveChangesAsync();
            return walk;

        }

        public async Task<Walk> DeleteAsynch(Guid id)
        {
            var walk = await walkDBContext.Walks.FindAsync(id);

            if (walk == null)
                return null;
            walkDBContext.Walks.Remove(walk);
            await walkDBContext.SaveChangesAsync();

            return walk;
        }

        public async Task<IEnumerable<Walk>> GetAllAsynch()
        {
            return await walkDBContext.Walks
                .Include(x => x.Region)
                .Include(x => x.WalkDifficulty)
                .ToListAsync();
        }

        public async Task<Walk> GetAsynch(Guid id)
        {
            return await walkDBContext.Walks
                .Include(x => x.Region)
                .Include(x => x.WalkDifficulty)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk> UpdateWalkAsynch(Guid id, Walk walk)
        {
            var existingWalk =await walkDBContext.Walks.FindAsync(id);
            
            if(existingWalk !=null)
            {
                existingWalk.Length = walk.Length;
                existingWalk.Name=walk.Name;
                existingWalk.WalkDifficultyId = walk.WalkDifficultyId;
                existingWalk.RegionId = walk.RegionId;
                await walkDBContext.SaveChangesAsync();
                return existingWalk;
            }
            return null;
        }
    }
}
