using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Timetable_DateSheet_Generator.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Required]
        [Column(nameof(Name), TypeName = "nvarchar(200)")]
        public string Name { get;set;}
        [Column(nameof(Image), TypeName = "nvarchar(MAX)")]
        public string Image { get;set;}
        [Column(nameof(Skills), TypeName = "nvarchar(MAX)")]
        public string Skills { get;set;}
        [Column(nameof(Experience), TypeName = "nvarchar(MAX)")]
        public string Experience { get;set;}
        [Column(nameof(Address), TypeName = "nvarchar(MAX)")]
        public string Address { get;set;}
        //[NotMapped]
    }
}
