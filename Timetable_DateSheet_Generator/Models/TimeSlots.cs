using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Timetable_DateSheet_Generator.Models
{
    [Table("TimeSlots")]
    public class TimeSlots
    {
        [Key]
        [Required]
        [Column(nameof(TimeSlotID), TypeName = "int")]
        public int TimeSlotID { get; set; }
        [Required]
        [Column(nameof(TimeTableID), TypeName = "int")]
        public int TimeTableID { get; set; }
        [Required]
        [Column(nameof(TimeID), TypeName = "int")]
        public int TimeID { get; set; }
        public TimeTables TimeTable { get; set; }
        public Times Time { get; set; }
        public List<OfferedCourseTimeSlots> OfferedCourseTimeSlots { get; set; }
    }
}
