using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Timetable_DateSheet_Generator.Models
{
    public class Institutes
    {
        [Key]
        [Required]
        [Column(nameof(InstituteID), TypeName = "int")]
        public int InstituteID { get; set; }
        [Required]
        [Column(nameof(InstituteName), TypeName = "nvarchar(200)")]
        public string InstituteName { get; set; }
        [Column(nameof(User), TypeName = "nvarchar(450)")]
        public string User { get; set; }
        [Column(nameof(Last_Modified), TypeName = "datetime")]
        public DateTime Last_Modified { get; set; }
        public List<Buildings> Buildings { get; set; }
        public List<TimeTables> TimeTables { get; set; }
        public List<Departments> Departments { get; set; }        
    }
}
