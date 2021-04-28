using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class PasswordResetModel
    {
        [Required(ErrorMessage = "Please Update the highlighted mandatory field(s)")]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        [StringLength(255, ErrorMessage = "Must be of length 8 or more than 8", MinimumLength = 8)]
        public string NewPassword { get; set; }


        [Required(ErrorMessage = "Please Update the highlighted mandatory field(s)")]
        [DataType(DataType.Password)]
        [Display(Name = " Confirm Password")]
        [Compare("NewPassword")]
        [StringLength(255, ErrorMessage = "Must be of length 8 or more than 8", MinimumLength = 8)]
        public string ConfirmPassword { get; set; }
    }
}