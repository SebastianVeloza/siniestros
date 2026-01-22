using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IciudadesRepository : IRepository<ciudades>
    {
        Task<IEnumerable<ciudades>> GetallCiudades();
    }
}
