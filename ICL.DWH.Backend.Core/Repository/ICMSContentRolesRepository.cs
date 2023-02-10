using ICL.DWH.Backend.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICL.DWH.Backend.Core.Repository
{
    public interface ICMSContentRolesRepository : IRepository<CMSContentRoles>
    {
        public IQueryable<CMSContentRoles_dataQ> GetRolesByID(string Id_content, string Type);

    }
}
