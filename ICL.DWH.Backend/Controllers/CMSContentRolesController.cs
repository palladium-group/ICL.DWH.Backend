using ICL.DWH.Backend.Core.Entities;
using ICL.DWH.Backend.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ICL.DWH.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CMSContentRolesController : ControllerBase
    {
        private readonly ICMSContentRolesService _cmsContentRolesService;

        public CMSContentRolesController(ICMSContentRolesService cmsContentRolesService)
        {
            _cmsContentRolesService = cmsContentRolesService;
        }

        [HttpPost]
        public IActionResult NewCMSContentRoles([FromBody] CMSContentRoles cmsContentRoles)
        {
            try
            {
                _cmsContentRolesService.NewContent(cmsContentRoles);
                return Ok(cmsContentRoles);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public IActionResult GetCMSContentRoless()
        {
            try
            {
                var cmsContentRoless = _cmsContentRolesService.GetCmsContentsRoles();
                return Ok(cmsContentRoless);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet("rolesByid/{Id_content}/{Type}")]
        public IActionResult GetCMSContentRolessByID(Guid Id_content, string Type)
        {
            try
            {
                var cmsContentRoless = _cmsContentRolesService.GetCmsContentsRolesByID(Id_content.ToString(), Type);
                return Ok(cmsContentRoless);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
