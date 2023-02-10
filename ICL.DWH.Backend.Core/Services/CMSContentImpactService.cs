using ICL.DWH.Backend.Core.Entities;
using ICL.DWH.Backend.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICL.DWH.Backend.Core.Services
{
    public class CMSContentImpactService : ICMSContentImpactService
    {
        private readonly ICMSContentImpactRepository _cmsContentImpactRepository;

        public CMSContentImpactService(ICMSContentImpactRepository cmsContentImpactRepository)
        {
            _cmsContentImpactRepository = cmsContentImpactRepository;
        }

        public IEnumerable<CMSContentImpact> GetCmsContentsImpact()
        {
            try
            {
                return _cmsContentImpactRepository.GetAll();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<CMSContentImpact> GetContentImpactByRol(string roleId)
        {
            try
            {
                return _cmsContentImpactRepository.GetContentImpactByRol(roleId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IEnumerable<CMSContentImpact> GetContentImpactByName(string name)
        {
            try
            {
                return _cmsContentImpactRepository.GetContentImpactByName(name);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public CMSContentImpact NewContent(CMSContentImpact content)
        {
            try
            {
                return _cmsContentImpactRepository.Create(content);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
