﻿using Microsoft.Build.Framework;

namespace ShopSneaker.Models
{
    public class AuthenVm
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public bool RememberMe { get; set; } = false;
    }
}
