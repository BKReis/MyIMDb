using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Api.Account {
    public class AccountLoginRequest {
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MaxLength(100, ErrorMessage = "Password can't be greater than {1} characters")]
        public string Password { get; set; }

        public Boolean RememberMe { get; set; }
    }
}