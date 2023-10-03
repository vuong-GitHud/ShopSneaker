﻿using System.ComponentModel.DataAnnotations;

namespace ShopSneaker.Identity.Model
{
    public class LoginRequest
    {
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }
}
