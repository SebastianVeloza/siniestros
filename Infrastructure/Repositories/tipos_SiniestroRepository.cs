using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Infrastructure.Repositories
{
    public class tipos_SiniestroRepository : Repository<tipos_siniestro>, Itipos_siniestroRepository
    {
        public tipos_SiniestroRepository(Context context) : base(context)
        {
        }
    }
}
