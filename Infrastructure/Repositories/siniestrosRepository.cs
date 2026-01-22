using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Infrastructure.Repositories
{
    public class siniestrosRepository : Repository<Siniestros>, ISiniestrosRepository
    {
        public siniestrosRepository(Context context) : base(context)
        {
        }
    }
}
