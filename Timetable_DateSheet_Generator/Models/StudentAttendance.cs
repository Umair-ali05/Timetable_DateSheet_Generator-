using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Timetable_DateSheet_Generator.Models
{
    [Table("StudentAttendance")]
    public class StudentAttendance
    {
        [Key]
        [Required]
        [Column(nameof(StudentAttendanceID), TypeName = "bigint")]
        public long StudentAttendanceID { get; set; }
        [Required]
        [Column(nameof(AttendanceID), TypeName = "int")]
        public int AttendanceID { get; set; }
        [Required]
        [Column(nameof(RegisteredCourseID), TypeName = "int")]
        public int RegisteredCourseID { get; set; }
        [Required]
        [Column(nameof(IsPresent), TypeName = "bit")]
        public bool IsPresent { get; set; }
        public Attendance Attendance { get; set; }
        public RegisteredCourses RegisteredCourse { get; set; }

    }
}
