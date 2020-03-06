using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Model.Account
{
    public class AccountResponse
    {
        public string UserID { get; set; }
        public string FirstName { get; set; }
       
        public string LastName { get; set; }

        public string UserName { get; set; }

        public string EmailID { get; set; }

        public string ServiceType { get; set; }

        public string UserType { get; set; }
    }
}
