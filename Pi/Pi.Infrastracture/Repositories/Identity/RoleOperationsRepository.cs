using Microsoft.EntityFrameworkCore;
using Pi.Domain.Entities;
using Pi.Domain.Interfaces;
using Pi.Domain.Interfaces.Identity;
using Pi.Infrastracture.Data;
using Pi.Infrastracture.Repositories.General;

namespace Pi.Infrastracture.Repositories.Identity
{
    public class RoleOperationsRepository : GenericRepository<RoleOperations>, IRoleOperationsRepository
    {

        private readonly IUserRoleAssignmentsRepository _userAssignmentsRepository;
        private readonly IOperationsRepository _operationsRepository;
        public RoleOperationsRepository(PiContext context, IUserRoleAssignmentsRepository userAssignmentsRepository, IOperationsRepository operationsRepository) : base(context)
        {
            _userAssignmentsRepository = userAssignmentsRepository;
            _operationsRepository = operationsRepository;
        }

        public async Task<List<long>> GetOperationsByRoleAsync(long roleId)
        {
            return await GetQueryable().Where(x => x.RoleId == roleId).Select(x => x.OperationId).ToListAsync();
        }


        public async Task<List<string>> GetOperationsByUserAsync(long userId)
        {
            var result = await _userAssignmentsRepository.GetQueryable(x => x.IdUser == userId)
            .Join(
                GetQueryable(),
                userRole => userRole.RoleId,
                roleOp => roleOp.RoleId,
                (userRole, roleOp) => new { userRole, roleOp }
            )
            .Join(
                _operationsRepository.GetQueryable(),
                temp => temp.roleOp.OperationId,
                operation => operation.Id,
                (temp, operation) => operation.Code
            )
            .ToListAsync();

            return result ??= new List<string>();
        }
    }
}
