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

    public partial class SchlorshipCriteria
    {
        public int id { get; set; }
        [Required]
        [Display(Name ="Scholarship Name")]
        public string ScholarshipName { get; set; }
        [Required]
        [Display(Name = "Scholarship Merit")]
        public int PercentageMerit { get; set; }
        [Required]
        [Display(Name = "Annual Income")]
        public Nullable<int> AnnualIncome { get; set; }

        [Display(Name = "Institution ID")]
        public Nullable<int> InstituteId { get; set; }
        [Display(Name = "Scholarship ID")]
        public Nullable<int> ScholarshipID { get; set; }
        
        public virtual Institute_details Institute_details { get; set; }
    }
}