using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface Ihistorico_refresh_tokenRepository : IRepository<historico_refresh_token>
    {
        Task<historico_refresh_token> GetByTokenAsync(string token);

    }
}
