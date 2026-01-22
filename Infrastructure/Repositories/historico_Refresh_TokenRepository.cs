using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class historico_Refresh_TokenRepository : Repository<historico_refresh_token>, Ihistorico_refresh_tokenRepository
    {
        public historico_Refresh_TokenRepository(Context context) : base(context)
        {
        }

        public async Task<historico_refresh_token> GetByTokenAsync(string token)
        {
            return await _context.historico_refresh_token.AsNoTracking().SingleOrDefaultAsync(rt => rt.refresh_token == token && rt.activo);
        }
    }
}
