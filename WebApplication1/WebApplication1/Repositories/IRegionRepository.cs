using WebApplication1.Models.Domain;

namespace WebApplication1.Repositories
{
    public interface IRegionRepository
    {
       Task<IEnumerable<Region>> GetAllAsynch();
    }
}
