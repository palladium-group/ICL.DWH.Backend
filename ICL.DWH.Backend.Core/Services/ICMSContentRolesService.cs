using ICL.DWH.Backend.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICL.DWH.Backend.Core.Services
{
    public interface ICMSContentRolesService
    {
        CMSContentRoles NewContent(CMSContentRoles content);
        IEnumerable<CMSContentRoles> GetCmsContentsRoles();
        IEnumerable<CMSContentRoles_dataQ> GetCmsContentsRolesByID(string Id_content, string Type);
    }
}
