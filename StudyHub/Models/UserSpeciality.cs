//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StudyHub.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserSpeciality
    {
        public int UserSpecialityId { get; set; }
        public Nullable<int> UserId { get; set; }
        public int SpecialityId { get; set; }
    
        public virtual Speciality Speciality { get; set; }
        public virtual User User { get; set; }
    }
}
