using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Timetable_DateSheet_Generator.Models
{
    [Table("Semesters")]
    public class Semesters
    {
        public enum type { Spring = 1, Summer = 2, Fall = 3 }
        public string getSemesterType
        {
            get
            {
                return Enum.GetName(typeof(type), SemesterType);
            }
        }
        [Key]
        [Required]
        [Column(nameof(SemesterID), TypeName = "int")]
        public int SemesterID { get; set; }

        [Required]
        [Column(nameof(SemesterType), TypeName = "int")]
        public int SemesterType { get; set; }
        [Required]
        [Column(nameof(SemesterYear), TypeName = "int")]
        public int SemesterYear { get; set; }
        [Column(nameof(User), TypeName = "nvarchar(450)")]
        public string User { get; set; }
        [Column(nameof(Last_Modified), TypeName = "datetime")]
        public DateTime Last_Modified { get; set; }
        public List<OfferedCourses> OfferedCourses { get; set; }
        public List<TimeTables> TimeTables { get; set; }
    }
}
