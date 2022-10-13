using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models.Domain;

namespace WebApplication1.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly WalkDBContext walkDBContext;

        public RegionRepository(WalkDBContext walkDBContext)
        {
            this.walkDBContext = walkDBContext;
        }

        public async Task<IEnumerable<Region>> GetAllAsynch()
        {
            return await walkDBContext.Regions.ToListAsync();
        }
    }
}
