using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class ForgotInstituteID
    {
        [Required(ErrorMessage = "Please Update the highlighted mandatory field(s)")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please Update the highlighted mandatory field(s)")]
        [Display(Name = "Favourite Pet")]
        public string FavouritePet { get; set; }

        [Required(ErrorMessage = "Please Update the highlighted mandatory field(s)")]
        [Display(Name = "Favourite Movie")]
        public string FavouriteMovie { get; set; }

        [Required(ErrorMessage = "Please Update the highlighted mandatory field(s)")]
        [Display(Name = "Lucky Number")]
        public int LuckyNumber { get; set; }
    }
}