using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Timetable_DateSheet_Generator.Models.ViewModels
{
    public class RegisterCourseViewModelList
    {        
        public List<StudentCourseDetailViewModel> studentCourseDetailViewModel { get; set; }
        public RegisterCourseViewModelList()
        {
            studentCourseDetailViewModel = new List<StudentCourseDetailViewModel>();
        }            
    }
}
