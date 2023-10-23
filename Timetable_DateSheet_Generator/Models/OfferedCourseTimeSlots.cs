using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Timetable_DateSheet_Generator.Models
{
    [Table("OfferedCourseTimeSlots")]
    public class OfferedCourseTimeSlots
    {
        [Key]
        [Required]
        [Column(nameof(OfferedCourseTimeSlotID), TypeName = "int")]
        public int OfferedCourseTimeSlotID { get; set; }
        [Required]
        [Column(nameof(OfferedCourseID), TypeName = "int")]
        public int OfferedCourseID { get; set; }
        [Required]
        [Column(nameof(TimeSlotID), TypeName = "int")]
        public int TimeSlotID { get; set; }

        [Required]
        [Column(nameof(RoomID), TypeName = "int")]
        public int RoomID { get; set; }
        [NotMapped]
        public string Label { get; set; }
        [NotMapped]
        public double AssignedContactHours = 0;
        [NotMapped]
        public double CourseScheduleNumber = 0;
        public OfferedCourses OfferedCourse { get; set; }
        public Rooms Room { get; set; }
        public TimeSlots TimeSlots { get; set; }
    }
}
