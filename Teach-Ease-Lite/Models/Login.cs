using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Teach_Ease_Lite.Models
{
    public class Login
    {
        public string EmailID { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
    }
}