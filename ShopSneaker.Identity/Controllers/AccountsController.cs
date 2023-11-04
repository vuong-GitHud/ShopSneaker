using Microsoft.AspNetCore.Mvc;
using ShopSneaker.Identity.Infrastructure.Interface;

namespace ShopSneaker.Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("CheckEmail/{email}")]
        public async Task<IActionResult> CheckEmailExist(string email)
        {
            var result = await _accountService.CheckEmailExist(email);
            return Ok(result);
        }
        
        [HttpGet("Profile/{userId}")]
        public async Task<IActionResult> GetUserProfile(string userId)
        {
            var result = await _accountService.GetUserProfile(userId);
            return Ok(result);
        }
    }
}