using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Login_and_SignUP.Models
{
    public class UserLogin 
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = ("Email is Required "))]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string EmailID { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = ("Password is Required "))]
        [MinLength(8, ErrorMessage = "min Length of password char = 8")]
        [MaxLength(64, ErrorMessage = "Max Length of password char = 64")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}