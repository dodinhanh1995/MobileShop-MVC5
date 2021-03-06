﻿using System.ComponentModel.DataAnnotations;

namespace MobileShop.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Tên đăng nhập không được để trống!")]
        public string Username { get; set; }

        [Required(ErrorMessage ="Mật khẩu không được để trống!")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}