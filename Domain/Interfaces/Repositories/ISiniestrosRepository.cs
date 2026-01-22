using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface ISiniestrosRepository : IRepository<Siniestros>
    {
        IQueryable<Siniestros> Query();
        Task<int> CountAsync(IQueryable<Siniestros> query);

    }
}
