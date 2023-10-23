using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Timetable_DateSheet_Generator.Models
{
    [Table("Students")]
    public class Students
    {
        [Key]
        [Required]
        [Column(nameof(StudentID), TypeName = "int")]
        public int StudentID { get; set; }
        [Required]
        [Column(nameof(ProgarmID), TypeName = "int")]
        public int ProgarmID { get; set; }       
        [Required]
        [Column(nameof(StudentEnrollment), TypeName = "nvarchar(200)")]
        public string StudentEnrollment { get; set; }
        
        [Column(nameof(UserID), TypeName = "nvarchar(450)")]
        public string UserID { get; set; }
        [Column(nameof(User), TypeName = "nvarchar(450)")]
        public string User { get; set; }
        [Column(nameof(Last_Modified), TypeName = "datetime")]
        public DateTime Last_Modified { get; set; }
        public Programs Program { get; set; }
        public List<RegisteredCourses> RegisteredCourses { get; set; }
    }
}
