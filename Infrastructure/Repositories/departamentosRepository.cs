using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Infrastructure.Repositories
{
    public class departamentosRepository : Repository<departamentos>, IdepartamentosRepository
    {
        public departamentosRepository(Context context) : base(context)
        {
        }
    }
}
