using ICL.DWH.Backend.Core.Entities;
using ICL.DWH.Backend.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICL.DWH.Backend.Core.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public IEnumerable<Role> GetRoles()
        {
            try
            {
                return _roleRepository.GetAll();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Role NewRole(Role role)
        {
            try
            {
                return _roleRepository.Create(role);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
