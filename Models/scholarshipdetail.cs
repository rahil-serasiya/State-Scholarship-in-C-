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

    public partial class scholarshipdetail
    {
        public int student_Id { get; set; }
        public int Marks { get; set; }
        public string Grade { get; set; }
        public string DemoCourse { get; set; }
        public string College { get; set; }

        [Display(Name ="Approved By Scholarship Provider")]
        public string approvedbyScholarshipProvider { get; set; }

        [Display(Name = "Approved By Scholarship Provider")]
        public string approvedbyInstitution { get; set; }
    }
}
