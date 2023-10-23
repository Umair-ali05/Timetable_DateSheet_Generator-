using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Timetable_DateSheet_Generator.Models
{
    [Table("Programs")]
    public class Programs
    {
        [Key]
        [Required]
        [Column(nameof(ProgramID), TypeName = "int")]
        public int ProgramID { get; set; }
        [Required]
        [Column(nameof(DepartmentID), TypeName = "int")]
        public int DepartmentID { get; set; }
        [Required]
        [Column(nameof(ProgramName), TypeName = "nvarchar(200)")]
        public string ProgramName { get; set; }
        [Column(nameof(User), TypeName = "nvarchar(450)")]
        public string User { get; set; }
        [Column(nameof(Last_Modified), TypeName = "datetime")]
        public DateTime Last_Modified { get; set; }
        public Departments Department { get; set; }
        public List<OfferedCourses> OfferedCourses { get; set; }
        public List<Students> Students { get; set; }

        public List<ProgramRegularTimings> ProgramRegularTimings { get; set; }
        public List<ProgramSpecialTimings> ProgramSpecialTimings { get; set; }
    }
}
