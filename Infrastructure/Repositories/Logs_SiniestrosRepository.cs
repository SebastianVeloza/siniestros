using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Infrastructure.Repositories
{
    internal class Logs_SiniestrosRepository : Repository<Logs_Siniestros>, ILogs_SiniestrosRepository
    {
        public Logs_SiniestrosRepository(Context context) : base(context)
        {
        }
    }
}
