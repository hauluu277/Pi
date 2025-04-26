using Pi.Domain.Entities.Identity;
using Pi.Domain.Interfaces;
using Pi.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pi.Infrastracture.Services
{
    public class UserService : GenericService<Users>, IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<Users> GetList()
        {
            return GetQueryable().ToList();
        }
    }
}
