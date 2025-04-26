using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Pi.Domain.Interfaces;
using Pi.Infrastracture.Data;
using System.Collections.Concurrent;

namespace Pi.Infrastracture.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PiContext _context;
        private ConcurrentDictionary<Type, object> _repositories = new();
        private IDbContextTransaction _transaction;
        private readonly IServiceProvider _serviceProvider;


        public UnitOfWork(PiContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _serviceProvider = serviceProvider;
        }

        public TRepository GetRepository<TRepository>()
        {
            return _serviceProvider.GetRequiredService<TRepository>();
        }


        public void BeginTransaction()
        {
            if (_transaction != null)
            {
                throw new InvalidOperationException("Transaction already started");
            }
            _transaction = _context.Database.BeginTransaction();
        }
        public async Task BeginTransactionAsync()
        {
            if (_transaction != null)
            {
                throw new InvalidOperationException("Transaction already started");
            }
            _transaction = await _context.Database.BeginTransactionAsync();
        }


        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction == null) throw new InvalidOperationException("Transaction has not been started");
            try
            {
                await _context.SaveChangesAsync();
                await _transaction.CommitAsync();
            }
            catch
            {
                await RollBack();
                throw;
            }
            finally
            {
                DisposeTransaction();
            }
        }

        private async Task RollBack()
        {
            if (_transaction == null) throw new InvalidOperationException("Transaction has not been started");
            await _transaction.RollbackAsync();
            DisposeTransaction();
        }

        public void Commit()
        {
            _context.SaveChanges();
        }



        public IGenericRepository<T> Repository<T>() where T : class
        {
            if (_repositories == null) _repositories = new();
            var type = typeof(T);

            var result = (IGenericRepository<T>)_repositories.GetOrAdd(type, _ =>
            {
                return _serviceProvider.GetRequiredService<IGenericRepository<T>>();

                //var repositoryType = typeof(GenericRepository<>).MakeGenericType(typeof(T));
                //return Activator.CreateInstance(repositoryType, _context);
            });
            return result;
        }

        private void DisposeTransaction()
        {
            _transaction?.Dispose();
            _transaction = null;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        void IUnitOfWork.BeginTransactionAsync()
        {
            throw new NotImplementedException();
        }


    }
}
