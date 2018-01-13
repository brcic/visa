using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Visa.Admin.Models
{
    public class LoginViewModel
    {
        public string textUser { get; set; }
        public string textPassword { get; set; }
    }
    public class ChangePasswordViewModel
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}