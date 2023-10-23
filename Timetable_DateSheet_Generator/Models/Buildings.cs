using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Timetable_DateSheet_Generator.Models
{
    [Table("Buildings")]
    public class Buildings
    {
        [Key]
        [Required]
        [Column(nameof(BuildingID), TypeName = "int")]
        public int BuildingID { get; set; }
        [Required]
        [Column(nameof(InstituteID), TypeName = "int")]
        public int InstituteID { get; set; }
        [Required]
        [Column(nameof(BuildingName), TypeName = "nvarchar(200)")]
        public string BuildingName { get; set; }
        [Column(nameof(User), TypeName = "nvarchar(450)")]
        public string User { get; set; }
        [Column(nameof(Last_Modified), TypeName = "datetime")]
        public DateTime Last_Modified { get; set; }
        public Institutes Institute { get; set; }
        public List<Rooms> Rooms { get; set; }
    }
}
