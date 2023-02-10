using ICL.DWH.Backend.Core.Entities;
using ICL.DWH.Backend.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ICL.DWH.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CMSContentLeadershipController : ControllerBase
    {
        private readonly ICMSContentLeadershipService _cmsContentLeadershipService;

        public CMSContentLeadershipController(ICMSContentLeadershipService cmsContentLeadershipService)
        {
            _cmsContentLeadershipService = cmsContentLeadershipService;
        }

        [HttpPost]
        public IActionResult NewCMSContentLeadership([FromBody] CMSContentLeadership cmsContentLeadership)
        {
            try
            {
                _cmsContentLeadershipService.NewContent(cmsContentLeadership);
                return Ok(cmsContentLeadership);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public IActionResult GetCMSContentLeaderships()
        {
            try
            {
                var cmsContentLeaderships = _cmsContentLeadershipService.GetCmsContentsLeadership();
                return Ok(cmsContentLeaderships);
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        [HttpGet("contentByRol/{Id_rol}")]
        public IActionResult GetCMSContentByRol(Guid Id_rol)
        {
            try
            {
                var cmsContentLeaderships = _cmsContentLeadershipService.GetContentLeadershipByRol(Id_rol.ToString());
                return Ok(cmsContentLeaderships);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("contentByName/{name}")]
        public IActionResult GetCMSContentByName(string name)
        {
            try
            {
                var cmsContentLeaderships = _cmsContentLeadershipService.GetContentLeadershipByName(name);
                return Ok(cmsContentLeaderships);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
