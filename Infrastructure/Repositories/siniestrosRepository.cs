using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class siniestrosRepository : Repository<Siniestros>, ISiniestrosRepository
    {
        public siniestrosRepository(Context context) : base(context)
        {
        }

        public IQueryable<Siniestros> Query()
        {
            return _context.Siniestros.AsNoTracking().Include(x => x.Departamentos).Include(x => x.Ciudades).Include(x => x.Tipos_Siniestro).AsQueryable();
        }
        public async Task<int> CountAsync(IQueryable<Siniestros> query)
        {
            return await query.AsNoTracking().CountAsync();
        }
    }
}
