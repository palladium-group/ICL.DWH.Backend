using ICL.DWH.Backend.Core.Entities;
using ICL.DWH.Backend.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ICL.DWH.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpPost]
        public IActionResult NewRole([FromBody] Role role)
        {
            try
            {
                _roleService.NewRole(role);
                return Ok(role);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public IActionResult GetRoles()
        {
            try
            {
                var roles = _roleService.GetRoles();
                return Ok(roles);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
