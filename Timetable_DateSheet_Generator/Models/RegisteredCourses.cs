using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Timetable_DateSheet_Generator.Models
{
    [Table("RegisteredCourses")]
    public class RegisteredCourses
    {
        [Key]
        [Required]
        [Column(nameof(RegisteredCourseID), TypeName = "int")]
        public int RegisteredCourseID { get; set; }
        [Required]
        [Column(nameof(StudentID), TypeName = "int")]
        public int StudentID { get; set; }

        [Required]
        [Column(nameof(OfferedCourseID), TypeName = "int")]
        public int OfferedCourseID { get; set; }
        public OfferedCourses OfferedCourse { get; set; }
        public List<StudentAttendance> StudentAttendances { get;set;}
        public Students Student { get; set; }
    }
}
