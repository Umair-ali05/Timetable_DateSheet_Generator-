using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Timetable_DateSheet_Generator.Models
{
    [Table("RoomAvailibilities")]
    public class RoomAvailibilities
    {
        [Key]
        [Required]
        [Column(nameof(RoomAvailibilityID), TypeName = "int")]
        public int RoomAvailibilityID { get; set; }
        [Required]
        [Column(nameof(DepartmentID), TypeName = "int")]
        public int DepartmentID { get; set; }
        [Required]
        [Column(nameof(RoomID), TypeName = "int")]
        public int RoomID { get; set; }
        [Required]
        [Column(nameof(TimeID), TypeName = "int")]
        public int TimeID { get; set; }
        public Departments Department { get; set; }
        public Rooms Room { get; set; }
        public Times Time { get; set; }
    }
}
