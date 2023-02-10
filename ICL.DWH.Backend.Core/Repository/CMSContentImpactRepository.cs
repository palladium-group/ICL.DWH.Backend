using ICL.DWH.Backend.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ICL.DWH.Backend.Core.Repository
{
    public class CMSContentImpactRepository : Repository<CMSContentImpact>, ICMSContentImpactRepository
    {
        public CMSContentImpactRepository(DataContext dataContext) : base(dataContext)
        {
        }

        public IQueryable<CMSContentImpact> GetContentImpactByRol(string roleId)
        {
            string customQuery = "select cci.* from cms_content_impact cci\r\ninner join " +
                "(select * from cms_content_roles where \"Id_roles\"='"+roleId+"' and \"Type\"=1 and \"Status\"=true) ccr on cci.\"Id\"=ccr.\"Id_content\";";

            var resultData = _dataContext.CMSContentImpact.FromSqlRaw<CMSContentImpact>(customQuery);
            return resultData;
        }
        public IQueryable<CMSContentImpact> GetContentImpactByName(string name)
        {
            string customQuery = "select cci.* from cms_content_impact cci\r\ninner join " +
                "( select t1.* from cms_content_roles t1\r\n  inner join icl_roles t2 on t1.\"Id_roles\"=t2.\"Id\" " +
                "where t2.\"RoleName\"='"+ name + "' and t1.\"Type\"=1\r\n and t1.\"Status\"=true ) ccr on cci.\"Id\"=ccr.\"Id_content\";  ";

            var resultData = _dataContext.CMSContentImpact.FromSqlRaw<CMSContentImpact>(customQuery);
            return resultData;
        }
    }
}
