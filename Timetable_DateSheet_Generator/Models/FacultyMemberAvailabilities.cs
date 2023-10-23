using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Timetable_DateSheet_Generator.Models
{
    [Table("FacultyMemberAvailabilities")]
    public class FacultyMemberAvailabilities
    {
        [Key]
        [Required]
        [Column(nameof(FacultyMemberAvailabilityID), TypeName = "int")]
        public int FacultyMemberAvailabilityID { get; set; }
        [Required]
        [Column(nameof(FacultyMemberID), TypeName = "int")]
        public int FacultyMemberID { get; set; }
        [Required]
        [Column(nameof(TimeID), TypeName = "int")]
        public int TimeID { get; set; }

        public FacultyMembers FacultyMember { get; set; }
        public Times Time { get; set; }
    }
}
