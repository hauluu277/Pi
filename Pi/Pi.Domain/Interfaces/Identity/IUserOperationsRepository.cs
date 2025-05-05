using Pi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pi.Domain.Interfaces
{
    public interface IUserOperationsRepository : IGenericRepository<UserOperations>
    {
        Task<List<string>> GetOperationByUserIdAsync(long userId);
        Task AssignOperationToUserAsync(long userId, long operationId);
        Task RemoveOperationFromUserAsync(long userId, long operationId);
    }
}
