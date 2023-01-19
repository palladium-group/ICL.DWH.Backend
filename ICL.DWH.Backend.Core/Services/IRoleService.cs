using ICL.DWH.Backend.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICL.DWH.Backend.Core.Services
{
    public interface IRoleService
    {
        Role NewRole(Role role);
        IEnumerable<Role> GetRoles();
    }
}
