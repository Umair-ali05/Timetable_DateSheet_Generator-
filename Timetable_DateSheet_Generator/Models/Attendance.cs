using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Timetable_DateSheet_Generator.Models.ViewModels;

namespace Timetable_DateSheet_Generator.Models
{
    [Table("Attendance")]
    public class Attendance
    {

        public enum AttendanceHours
        {
            [Display(Name = "1.0")] First = 1,
            [Display(Name = "1.5")] Second = 2,
            [Display(Name = "2.0")] Third = 3,
            [Display(Name = "2.5")] Fourth = 4,
            [Display(Name = "3.0")] Fifth = 5,
            [Display(Name = "3.5")] Sixth = 6,
            [Display(Name = "4.0")] Seventh = 7,
            [Display(Name = "4.5")] Eighth = 8,
            [Display(Name = "5.0")] Nineth = 9,
            [Display(Name = "5.5")] Tenth = 10,
            [Display(Name = "6.0")] Eleventh = 11,
            [Display(Name = "6.5")] Twelveth = 12,

        }
        [Key]
        [Required]
        [Column(nameof(AttendanceID), TypeName = "int")]
        public int AttendanceID { get; set; }
        [Required]
        [Column(nameof(OfferedCourseID), TypeName = "int")]
        public int OfferedCourseID { get; set; }
        [Required]
        [Column(nameof(AttendanceDate), TypeName = "datetime")]
        public DateTime AttendanceDate { get; set; }
        [Required]
        [Column(nameof(AttendanceCreditHours), TypeName = "int")]
        public int AttendanceCreditHours { get; set; }
        [Column(nameof(User), TypeName = "nvarchar(450)")]
        public string User { get; set; }
        [Column(nameof(Last_Modified), TypeName = "datetime")]
        public DateTime Last_Modified { get; set; }
        public OfferedCourses OfferedCourse { get; set; }
        public List<StudentAttendance> StudentAttendances { get; set; }
        public static string getName(int value)
        {
            return ((Attendance.AttendanceHours)value).GetDisplayName();
        }
    }
}
