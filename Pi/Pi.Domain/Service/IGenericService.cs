using Pi.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Pi.Domain
{
    public interface IGenericService<T> where T : class
    {
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<IEnumerable<T>> FindBy(Expression<Func<T, bool>> expression);
        Task<T> GetByIdAsync(object id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> FirstOrDefault(Expression<Func<T, bool>> expression);
        IQueryable<T> GetQueryable();

    }
}
