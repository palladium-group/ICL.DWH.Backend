using ICL.DWH.Backend.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Reflection.Emit;

namespace ICL.DWH.Backend.Core.Repository
{
    public class CMSContentRolesRepository : Repository<CMSContentRoles>, ICMSContentRolesRepository
    {
        public CMSContentRolesRepository(DataContext dataContext) : base(dataContext)
        {
            
        }

        public IQueryable<CMSContentRoles_dataQ> GetRolesByID(string Id_content, string Type)
        {
            string customQuery = "SELECT ir.\"Id\",ir.\"RoleName\", ir.\"CreateDate\", " +
                "ccr.\"Id\" AS CMS_content_roles_id, COALESCE(ccr.\"Status\",false) AS Status\r\nFROM icl_roles ir " +
                "LEFT OUTER JOIN (\r\n  SELECT * FROM cms_content_roles " +
                "WHERE \"Id_content\"='"+ Id_content + "' AND \"Type\"="+ Type + "\r\n  ) " +
                "ccr ON ir.\"Id\"=ccr.\"Id_roles\";";

            var resultData = _dataContext.CMSContentRoles_dataQ.FromSqlRaw<CMSContentRoles_dataQ>(customQuery);
            return resultData;
        }
    }
}
