using Microsoft.EntityFrameworkCore;
using Pi.Domain.Entities;
using Pi.Domain.Interfaces;
using Pi.Domain.Interfaces.Identity;
using Pi.Infrastracture.Data;
using Pi.Infrastracture.Repositories.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pi.Infrastracture.Repositories.Identity
{
    public class UserOperationsRepository : GenericRepository<UserOperations>, IUserOperationsRepository
    {
        private readonly IOperationsRepository _operationsRepository;

        public UserOperationsRepository(PiContext context, IOperationsRepository operationsRepository) : base(context)
        {
            _operationsRepository = operationsRepository;
        }

        public async Task AssignOperationToUserAsync(long userId, long operationId)
        {
            await FirstOrDefaultAsync(x => x.UserId == userId && x.OperationId == operationId);
        }

        public async Task<List<string>> GetOperationByUserIdAsync(long userId)
        {
            var result = await GetQueryable().Join(_operationsRepository.GetQueryable(),
                userOp => userOp.OperationId, operation => operation.Id,
                (userOp, operation) => operation.Code).ToListAsync();
            return result ??= new List<string>();
        }

        public async Task RemoveOperationFromUserAsync(long userId, long operationId)
        {
            var entity = await FirstOrDefaultAsync(x => x.UserId == userId && x.OperationId == operationId);
            if (entity != null)
            {
                Delete(entity);
            }
        }
    }
}
