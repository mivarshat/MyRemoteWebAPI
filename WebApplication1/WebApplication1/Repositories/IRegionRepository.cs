using WebApplication1.Models.Domain;

namespace WebApplication1.Repositories
{
    public interface IRegionRepository
    {
       Task<IEnumerable<Region>> GetAllAsynch();

       Task<Region> GetAsynch(Guid id);

        Task<Region> AddAsynch(Region region);

        Task<Region> DeleteAsynch(Guid id);

        Task<Region> UpdateAsynch(Guid id,Region region);
    }
}
