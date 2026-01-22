using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Infrastructure.Repositories
{
    public class ciudadesRepository : Repository<ciudades>, IciudadesRepository
    {
        public ciudadesRepository(Context context) : base(context)
        {
        }
    }
}
