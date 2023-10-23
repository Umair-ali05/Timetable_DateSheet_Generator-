using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Timetable_DateSheet_Generator.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name ="User Email")]
        public string UserEmail { get;set;}
        [Required]
        [DataType(DataType.Password)]
        public string Password { get;set;}
        [Required]
        [Display(Name ="Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Password and Confirmation Password do not match.")]
        public string ConfirmPassword { get;set;}
        [Required]
        [Display(Name = "Name")]        
        public string Name { get; set; }
        public IFormFile Image { get; set; }
        public string path { get;set;}
        public string ID { get;set;}
    }
}
