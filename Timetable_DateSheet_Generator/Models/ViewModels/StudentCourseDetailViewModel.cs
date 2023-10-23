using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Timetable_DateSheet_Generator.Models.ViewModels
{
    public class StudentCourseDetailViewModel
    {
        public bool isMarked { get;set;}
        public int course { get;set;}
        public string Name { get; set; }
        public string Email { get; set; }
        public string path { get; set; }
        public string enrollment { get;set;}
        public string User { get;set;}        
        public string programName { get;set;}
        public int studentId { get;set;}
        public StudentCourseDetailViewModel()
        {
            
        }
    }
}
