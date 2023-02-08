using ICL.DWH.Backend.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICL.DWH.Backend.Core.Services
{
    public interface ICMSContentLeadershipService
    {
        CMSContentLeadership NewContent(CMSContentLeadership content);
        IEnumerable<CMSContentLeadership> GetCmsContentsLeadership();
        public IEnumerable<CMSContentLeadership> GetContentLeadershipByRol(string roleId);
        public IEnumerable<CMSContentLeadership> GetContentLeadershipByName(string name);
    }
}
