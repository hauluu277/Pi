using Pi.Domain.Entities.Identity;
using Pi.Domain.Interfaces;
using Pi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Pi.Infrastracture.Services
{
    public class GenericService<T> : IGenericService<T> where T : class
    {
        private readonly IUnitOfWork _unitOfWork;
        protected readonly IGenericRepository<T> _repository;
        public GenericService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.Repository<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _repository.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _repository.Delete(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<IEnumerable<T>> FindBy(Expression<Func<T, bool>> expression)
        {
            return await _repository.FindByAsync(expression);
        }

        public async Task<T> GetByIdAsync(object id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<T> FirstOrDefault(Expression<Func<T, bool>> expression)
        {
            return await _repository.FirstOrDefaultAsync(expression);
        }

        public void Update(T entity)
        {
            _repository.Update(entity);
        }
        public IQueryable<T> GetQueryable()
        {
            return _repository.GetQueryable();
        }
    }
}
