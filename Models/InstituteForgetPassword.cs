using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Project.Models
{
    public class InstituteForgetPassword
    {
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Please Update the highlighted mandatory field(s)")]
        public int InstitutionID { get; set; }

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