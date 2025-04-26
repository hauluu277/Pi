using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Pi.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pi.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        void BeginTransaction();
        void BeginTransactionAsync();
        Task CommitAsync();
        Task CommitTransactionAsync();
        void Commit();
        IGenericRepository<T> Repository<T>() where T : class;
        TRepository GetRepository<TRepository>();



    }
}
