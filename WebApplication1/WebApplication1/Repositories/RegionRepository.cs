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

        public async Task<Region> AddAsynch(Region region)
        {
            region.Id = Guid.NewGuid();
            await walkDBContext.Regions.AddAsync(region);
            await walkDBContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region> DeleteAsynch(Guid id)
        {
            var region = await walkDBContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if(region==null)
            {
                return null;
            }
            //Delete
            walkDBContext.Regions.Remove(region);
            await walkDBContext.SaveChangesAsync();
            return region;
        }

        public async Task<IEnumerable<Region>> GetAllAsynch()
        {
            return await walkDBContext.Regions.ToListAsync();
        }

        public async Task<Region> GetAsynch(Guid id)
        {
            return await walkDBContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region> UpdateAsynch(Guid id, Region region)
        {
            var searchregion = await walkDBContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (searchregion == null)
                return null;

            searchregion.Code = region.Code;
            searchregion.Area=region.Area;
            searchregion.Name=  region.Name;
            searchregion.Lat= region.Lat;
            searchregion.Long= region.Long;
            searchregion.Population= region.Population;

            await walkDBContext.SaveChangesAsync();

            return searchregion;
        }
    }
}
