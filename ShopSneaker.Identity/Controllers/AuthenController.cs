using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopSneaker.Identity.Infrastructure.Interface;
using ShopSneaker.Identity.Model;

namespace ShopSneaker.Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenController : ControllerBase
    {
        private readonly IUserService _userService;
        public AuthenController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest model)
        {
            var result = await _userService.LoginByEmail(model.Email, model.Password);

            if (result == null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest model)
        {
            model.RoleId = 3;
            var result = await _userService.Register(model);
            return Ok(result);
        }
        
    }
}
