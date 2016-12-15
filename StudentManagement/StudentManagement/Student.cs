//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StudentManagement
{
    using System;
    using System.Collections.Generic;
    
    public partial class Student
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Student()
        {
            this.AcademicMark = new HashSet<AcademicMark>();
            this.BehaviorMark = new HashSet<BehaviorMark>();
            this.DiplomaProject = new HashSet<DiplomaProject>();
            this.RegisterStudyUnit = new HashSet<RegisterStudyUnit>();
            this.StudyFee = new HashSet<StudyFee>();
            this.StudentUser = new HashSet<StudentUser>();
        }
    
        public string StudentID { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public Nullable<bool> Gender { get; set; }
        public string IDNumber { get; set; }
        public string ClassID { get; set; }
        public string FacultyID { get; set; }
        public string Hometown { get; set; }
        public Nullable<int> AcademicYear { get; set; }
        public string PhoneNumber { get; set; }
        public string Policy { get; set; }
        public string CurrentAddress { get; set; }
        public string CandreID { get; set; }
        public string Email { get; set; }
        public string ImageID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AcademicMark> AcademicMark { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BehaviorMark> BehaviorMark { get; set; }
        public virtual Candre Candre { get; set; }
        public virtual Class Class { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DiplomaProject> DiplomaProject { get; set; }
        public virtual Faculty Faculty { get; set; }
        public virtual Parents Parents { get; set; }
        public virtual Policy Policy1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RegisterStudyUnit> RegisterStudyUnit { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudyFee> StudyFee { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentUser> StudentUser { get; set; }
        public virtual UserImage UserImage { get; set; }
    }
}
