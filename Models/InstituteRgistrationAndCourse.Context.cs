﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class InstituteRegistrationAndScholarship : DbContext
    {
        public InstituteRegistrationAndScholarship()
            : base("name=InstituteRegistrationAndScholarship")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<institute> institutes { get; set; }
        public virtual DbSet<Institute_details> Institute_details { get; set; }
        public virtual DbSet<SchlorshipCriteria> SchlorshipCriterias { get; set; }
        public virtual DbSet<ScholarshipRequest> ScholarshipRequests { get; set; }
    }
}