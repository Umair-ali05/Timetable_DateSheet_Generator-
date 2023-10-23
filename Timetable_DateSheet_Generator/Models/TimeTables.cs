using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Timetable_DateSheet_Generator.Models
{
    [Table("TimeTables")]
    public class TimeTables
    {
        [Key]
        [Required]
        [Column(nameof(TimeTableID), TypeName = "int")]
        public int TimeTableID { get; set; }
        [Required]
        [Column(nameof(InstituteID), TypeName = "int")]
        public int InstituteID { get; set; }
        [Required]
        [Column(nameof(SemesterID), TypeName = "int")]
        public int SemesterID { get; set; }
        [Column(nameof(User), TypeName = "nvarchar(450)")]
        public string User { get; set; }
        [Column(nameof(Last_Modified), TypeName = "datetime")]
        public DateTime Last_Modified { get; set; }
        public Institutes Institute { get; set; }
        public Semesters Semester { get; set; }
        public List<TimeSlots> TimeSlots { get; set; }
    }
}
