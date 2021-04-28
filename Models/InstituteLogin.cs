using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Project.Models
{
    public class InstituteLogin
    {
        //[Required(ErrorMessage = "Please Update the highlighted mandatory field(s)")]
        [Display(Name = "Institution ID")]
        public int InstitutionID { get; set; }

        //[Required(ErrorMessage = "Please Update the highlighted mandatory field(s)")]
        [DataType(DataType.Password)]

        public string Password { get; set; }

        
    }
}