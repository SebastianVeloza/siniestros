
using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IciudadesRepository ciudadesRepository { get; }    
        IdepartamentosRepository departamentosRepository { get; }
        ILogs_SiniestrosRepository Logs_SiniestrosRepository { get; }
        ISiniestrosRepository siniestrosRepository { get; }
        Itipos_siniestroRepository tipos_SiniestroRepository { get; }
        Ihistorico_refresh_tokenRepository historico_Refresh_TokenRepository { get; }
        

        Task<long> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task<string> RollbackTransactionAsync();
        IUnitOfWork CreateScoped();

    }
}
