using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JoBoard.Models
{
    public class RegisterViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Confirm Password doesn't match Password!")]
        public string ConfirmPasword { get; set; }
        public string Message { get; set; }

    }

    public class LoginViewModel
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        public string Message { get; set; }

    }
}