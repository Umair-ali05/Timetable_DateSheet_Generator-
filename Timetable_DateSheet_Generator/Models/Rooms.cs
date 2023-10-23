using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Timetable_DateSheet_Generator.Models
{
    [Table("Rooms")]
    public class Rooms
    {
        public enum RoomTypes
        {
            [Display(Name = "Class")] Class = 1,
            [Display(Name = "Lab")] Lab = 4,
        }
        [Key]
        [Required]
        [Column(nameof(RoomID), TypeName = "int")]
        public int RoomID { get; set; }
        [Required]
        [Column(nameof(BuildingID), TypeName = "int")]
        public int BuildingID { get; set; }
        [Required]
        [Column(nameof(RoomType), TypeName = "int")]
        public int RoomType { get; set; }
        [Required]
        [Column(nameof(SeatingCapacity), TypeName = "int")]
        public int SeatingCapacity { get; set; }
        [Required]
        [Column(nameof(RoomName), TypeName = "nvarchar(200)")]
        public string RoomName { get; set; }
        [Column(nameof(User), TypeName = "nvarchar(450)")]
        public string User { get; set; }
        [Column(nameof(Last_Modified), TypeName = "datetime")]
        public DateTime Last_Modified { get; set; }
        public List<OfferedCourseTimeSlots> OfferedCourseTimeSlots { get; set; }        
        public List<RoomAvailibilities> RoomAvailibilities { get; set; }
        public Buildings Building { get; set; }
    }
}
