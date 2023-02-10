using ICL.DWH.Backend.Core.Entities;
using ICL.DWH.Backend.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICL.DWH.Backend.Core.Services
{
    public class CMSContentRolesService : ICMSContentRolesService
    {
        private readonly ICMSContentRolesRepository _cmsContentRolesRepository;

        public CMSContentRolesService(ICMSContentRolesRepository cmsContentRolesRepository)
        {
            _cmsContentRolesRepository = cmsContentRolesRepository;
        }

        public IEnumerable<CMSContentRoles> GetCmsContentsRoles()
        {
            try
            {
                return _cmsContentRolesRepository.GetAll();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<CMSContentRoles_dataQ> GetCmsContentsRolesByID(string Id_content, string Type)
        {
            try
            {
                return _cmsContentRolesRepository.GetRolesByID(Id_content,Type);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public CMSContentRoles NewContent(CMSContentRoles content)
        {
            try
            {
                return _cmsContentRolesRepository.Create(content);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
