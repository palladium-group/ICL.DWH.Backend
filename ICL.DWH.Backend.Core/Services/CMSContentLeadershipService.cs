using ICL.DWH.Backend.Core.Entities;
using ICL.DWH.Backend.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICL.DWH.Backend.Core.Services
{
    public class CMSContentLeadershipService : ICMSContentLeadershipService
    {
        private readonly ICMSContentLeadershipRepository _cmsContentLeadershipRepository;

        public CMSContentLeadershipService(ICMSContentLeadershipRepository cmsContentLeadershipRepository)
        {
            _cmsContentLeadershipRepository = cmsContentLeadershipRepository;
        }

        public IEnumerable<CMSContentLeadership> GetCmsContentsLeadership()
        {
            try
            {
                return _cmsContentLeadershipRepository.GetAll();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<CMSContentLeadership> GetContentLeadershipByRol(string roleId)
        {
            try
            {
                return _cmsContentLeadershipRepository.GetContentLeadershipByRol(roleId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IEnumerable<CMSContentLeadership> GetContentLeadershipByName(string name)
        {
            try
            {
                return _cmsContentLeadershipRepository.GetContentLeadershipByName(name);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public CMSContentLeadership NewContent(CMSContentLeadership content)
        {
            try
            {
                return _cmsContentLeadershipRepository.Create(content);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
