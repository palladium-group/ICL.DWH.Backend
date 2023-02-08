using ICL.DWH.Backend.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICL.DWH.Backend.Core.Services
{
    public interface ICMSContentImpactService
    {
        CMSContentImpact NewContent(CMSContentImpact content);
        IEnumerable<CMSContentImpact> GetCmsContentsImpact();
        public IEnumerable<CMSContentImpact> GetContentImpactByRol(string roleId);
        public IEnumerable<CMSContentImpact> GetContentImpactByName(string name);
    }
}
