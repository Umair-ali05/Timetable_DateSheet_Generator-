using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Timetable_DateSheet_Generator.Models
{
    [Table("Departments")]
    public class Departments
    {
        [Key]
        [Required]
        [Column(nameof(DepartmentID), TypeName = "int")]
        public int DepartmentID { get; set; }
        [Required]
        [Column(nameof(InstituteID), TypeName = "int")]
        public int InstituteID { get; set; }
        [Required]
        [Column(nameof(DepartmentName), TypeName = "nvarchar(200)")]
        public string DepartmentName { get; set; }
        [Column(nameof(User), TypeName = "nvarchar(450)")]
        public string User { get; set; }
        [Column(nameof(Last_Modified), TypeName = "datetime")]
        public DateTime Last_Modified { get; set; }
        public Institutes Institute { get; set; }
        public List<Programs> Programs { get; set; }
        public List<FacultyMembers> FacultyMembers { get; set; }
        public List<OfferedCourses> OfferedCourses { get; set; }
        public List<RoomAvailibilities> RoomAvailibilities { get; set; }

    }
}
