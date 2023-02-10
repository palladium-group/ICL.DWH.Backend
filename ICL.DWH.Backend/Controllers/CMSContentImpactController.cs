using ICL.DWH.Backend.Core.Entities;
using ICL.DWH.Backend.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ICL.DWH.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CMSContentImpactController : ControllerBase
    {
        private readonly ICMSContentImpactService _cmsContentImpactService;

        public CMSContentImpactController(ICMSContentImpactService cmsContentImpactService)
        {
            _cmsContentImpactService = cmsContentImpactService;
        }



        [HttpPost]
        public IActionResult NewCMSContentImpact([FromBody] CMSContentImpact cmsContentImpact)
        {
            try
            {
                _cmsContentImpactService.NewContent(cmsContentImpact);
                return Ok(cmsContentImpact);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public IActionResult GetCMSContentsImpact()
        {
            try
            {
                var cmsContentImpacts = _cmsContentImpactService.GetCmsContentsImpact();
                return Ok(cmsContentImpacts);
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
                var cmsContentImpacts = _cmsContentImpactService.GetContentImpactByRol(Id_rol.ToString());
                return Ok(cmsContentImpacts);
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
                var cmsContentImpacts = _cmsContentImpactService.GetContentImpactByName(name);
                return Ok(cmsContentImpacts);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
