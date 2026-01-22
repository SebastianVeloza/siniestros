using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ciudadesRepository : Repository<ciudades>, IciudadesRepository
    {
        public ciudadesRepository(Context context) : base(context)
        {
        }

        public async Task<IEnumerable<ciudades>> GetallCiudades()
        {
            return await _context.ciudades.AsNoTracking().Include(x => x.Departamentos).ToListAsync();
        }
    }
}
