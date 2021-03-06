//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class institute
    {
        public int RegistrationID { get; set; }
        [Required]
        [Display(Name ="Institution ID")]
        public int InstitutionID { get; set; }
        [Required]
        [Display(Name = "Institution Name")]
        public string InstitutionName { get; set; }
        [Required]
        [Display(Name = "Institution Address")]
        public string Address { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Password should be of length 8 or more")]
        public string Ins_Password { get; set; }

        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Contact Number")]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Wrong mobile")]
        public long Contact { get; set; }

        [Required]
        [Display(Name = "Favorite Pet")]
        public string FavoritePet { get; set; }

        [Required]
        [Display(Name = "Favorite Movie")]
        public string FavoriteMovie { get; set; }

        [Required]
        [Display(Name = "Lucky Number")]
        public int Luckynumber { get; set; }
    }
}
