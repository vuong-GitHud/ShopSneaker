﻿using Microsoft.AspNetCore.Http;
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

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest model)
        {
            var result = await _userService.LoginByEmail(model.Email, model.Password);

            if (result == null)
            {
                return BadRequest();
            }

            return Ok(result);
        }
    }
}