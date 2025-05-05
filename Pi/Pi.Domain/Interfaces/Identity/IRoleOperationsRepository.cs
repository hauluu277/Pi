using Pi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pi.Domain.Interfaces.Identity
{
    public interface IRoleOperationsRepository : IGenericRepository<RoleOperations>
    {
        Task<List<long>> GetOperationsByRoleAsync(long roleId);
        Task<List<string>> GetOperationsByUserAsync(long userId);
    }
}
