using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Model.Account
{
    public class ResetPasswordModel
    {
        [Required]
        public string Password { get; set; }
        [Required]
        public string Token { get; set; }
    }
}
