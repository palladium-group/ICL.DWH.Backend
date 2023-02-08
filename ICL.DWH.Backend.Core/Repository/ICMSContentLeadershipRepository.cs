using ICL.DWH.Backend.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICL.DWH.Backend.Core.Repository
{
    public interface ICMSContentLeadershipRepository : IRepository<CMSContentLeadership>
    {
        public IQueryable<CMSContentLeadership> GetContentLeadershipByRol(string roleId);
        public IQueryable<CMSContentLeadership> GetContentLeadershipByName(string name);
    }
}
