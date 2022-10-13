using WebApplication1.Models.Domain;

namespace WebApplication1.Repositories
{
    public interface IWalkRepository
    {
        Task<IEnumerable<Walk>> GetAllAsynch();

        Task<Walk> GetAsynch(Guid id);

        Task<Walk> DeleteAsynch(Guid id);

        Task<Walk> AddWalkAsynch(Walk walk);

        Task<Walk> UpdateWalkAsynch(Guid id, Walk walk);
    }
}
