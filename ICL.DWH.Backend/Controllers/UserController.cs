using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ICL.DWH.Backend.Core.Services;
using ICL.DWH.Backend.Core.Entities;

namespace ICL.DWH.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public IActionResult RegisterUser([FromBody]User user)
        {
            try
            {
                _userService.NewUserRegistration(user);
                return Ok(user);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public IActionResult GetUsers() 
        {
            try
            {
                var users = _userService.GetUsers();
                return Ok(users);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
