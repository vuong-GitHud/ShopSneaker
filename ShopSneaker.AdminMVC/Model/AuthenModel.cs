﻿using System.ComponentModel.DataAnnotations;

namespace ShopSneaker.AdminMVC.Model
{
    public class AuthenModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public bool RememberMe { get; set; } = false;
    }
}
