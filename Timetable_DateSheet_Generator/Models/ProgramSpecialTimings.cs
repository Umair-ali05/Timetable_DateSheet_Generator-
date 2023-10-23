using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Timetable_DateSheet_Generator.Models
{
    [Table("ProgramSpecialTimings")]
    public class ProgramSpecialTimings
    {
        [Key]
        [Required]
        [Column(nameof(ProgramSpecialTimingID), TypeName = "int")]
        public int ProgramSpecialTimingID { get; set; }
        [Required]
        [Column(nameof(ProgramID), TypeName = "int")]
        public int ProgramID { get; set; }
        [Required]
        [Column(nameof(TimeID), TypeName = "int")]
        public int TimeID { get; set; }
        public Programs Program { get; set; }
        public Times Time { get; set; }
    }
}
