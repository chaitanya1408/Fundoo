using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Model.Account
{
    public class RegistrationModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string EmailId { get; set; }
        [Required]
        public string PassWord { get; set; }
        [Required]
        public string ServiceType { get; set; }
        [Required]
        public string UserType { get; set; }
    }
}