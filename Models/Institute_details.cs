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

    public partial class Institute_details
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Institute_details()
        {
            this.SchlorshipCriterias = new HashSet<SchlorshipCriteria>();
        }
    
        public int ScholarshipID { get; set; }
        [Required]
        [Display(Name ="Institution ID")]
        public int institutionID { get; set; }
        [Required]
        [Display(Name = "Course Name")]
        public string CourseName { get; set; }
        [Required]
        [Display(Name = "Scholarship Name")]
        public string ScholarshipName { get; set; }
        [Required]
        [Display(Name = "Scholarship Amount")]
        public int ScholarshipAmount { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchlorshipCriteria> SchlorshipCriterias { get; set; }
    }
}
