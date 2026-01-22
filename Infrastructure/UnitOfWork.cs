using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.Repositories;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private string error = "";
        private IDbContextTransaction _transaction;
        //no se ponen en el constructor
        private readonly Context _context;

        public IciudadesRepository ciudadesRepository { get; }

        public IdepartamentosRepository departamentosRepository { get; }

        public ILogs_SiniestrosRepository Logs_SiniestrosRepository { get; }

        public ISiniestrosRepository siniestrosRepository { get; }

        public Itipos_siniestroRepository tipos_SiniestroRepository { get; }

        public Ihistorico_refresh_tokenRepository historico_Refresh_TokenRepository { get; }

        public UnitOfWork(Context context)
        {
            _context = context;

            ciudadesRepository = new ciudadesRepository(_context);
            departamentosRepository = new departamentosRepository(_context);
            this.historico_Refresh_TokenRepository = new historico_Refresh_TokenRepository(_context);
            Logs_SiniestrosRepository = new Logs_SiniestrosRepository(_context);
            siniestrosRepository = new siniestrosRepository(_context);
            tipos_SiniestroRepository = new tipos_SiniestroRepository(_context);
        }

        public async Task<long> SaveChangesAsync()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                error = ex.InnerException.Message;
                foreach (var entry in ex.Entries)
                {
                    Console.WriteLine($"Error en la entidad: {entry.Entity.GetType().Name}");
                }
                throw new Exception("Error : " + error + " " + ex.Message); // Re-lanzar la excepción si es necesario
            }
        }

        public void Dispose()
        {
            if (_context != null)
            {
                _transaction?.Dispose();
                _context.Dispose();
            }
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            await _transaction.CommitAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            await _transaction.RollbackAsync();
        }
    }
}
