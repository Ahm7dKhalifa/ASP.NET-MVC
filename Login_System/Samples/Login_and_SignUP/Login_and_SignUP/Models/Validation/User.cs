using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Login_and_SignUP.Models
{ 
    [MetadataType(typeof(UserMetaData))]
    public partial class User
    {
        public string ConfirmPassword { get; set; }
    }
    public class UserMetaData
    {
        [Key]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false,ErrorMessage = ("First Name is Required "))]
        [MinLength(3,ErrorMessage = "min Length of First Name char = 3")]
        [MaxLength(20, ErrorMessage = "Max Length of First Name char = 20")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ("Last Name is Required "))]
        [MinLength(3, ErrorMessage = "min Length of Last Name char = 3")]
        [MaxLength(20, ErrorMessage = "Max Length of Last Name char = 20")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ("Last Name is Required "))]
        [MinLength(3, ErrorMessage = "min Length of Last Name char = 3")]
        [MaxLength(10, ErrorMessage = "Max Length of Last Name char = 10")]
        [Display(Name = "user name")]
        public string UserName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ("Email is Required "))]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string EmailID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ("Password is Required "))]
        [MinLength(8,ErrorMessage = "min Length of password char = 8")]
        [MaxLength(64, ErrorMessage = "Max Length of password char = 64")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

      
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Birth Day")]
        public DateTime DateOfBirth { get; set; }
      
    }

}