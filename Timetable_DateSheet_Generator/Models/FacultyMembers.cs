using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Timetable_DateSheet_Generator.Models
{
    [Table("FacultyMembers")]
    public class FacultyMembers
    {
        [Key]
        [Required]
        [Column(nameof(FacultyMemberID), TypeName = "int")]
        public int FacultyMemberID { get; set; }
        [Required]
        [Column(nameof(DepartmentID), TypeName = "int")]
        public int DepartmentID { get; set; }
        [Column(nameof(UserID), TypeName = "nvarchar(450)")]
        public string UserID { get; set; }
        [Column(nameof(User), TypeName = "nvarchar(450)")]
        public string User { get; set; }
        [Column(nameof(Last_Modified), TypeName = "datetime")]
        public DateTime Last_Modified { get; set; }
        public Departments Department { get; set; }
        public List<OfferedCourses> OfferedCourses { get; set; }
        public List<FacultyMemberAvailabilities> FacultyMemberAvailabilities { get; set; }        
    }
}
