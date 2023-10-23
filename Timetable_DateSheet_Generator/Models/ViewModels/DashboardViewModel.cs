using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Timetable_DateSheet_Generator.Models.ViewModels
{
    public class DashboardViewModel
    {
        public ApplicationUser profileView { get;set;}
        
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and Confirmation Password do not match.")]
        public string ConfirmPassword { get; set; }
        public IFormFile image { get;set;}
        public int totalInstitutes { get;set;}=0;
        public int institutesCreatedBy { get; set; }=0;

        public int totalBuilings { get; set; } = 0;
        public int buildingsCreatedBy { get; set; } = 0;

        public int totalRooms { get; set; } = 0;
        public int roomsCreatedBy { get; set; } = 0;

        public int totalDepartments { get; set; } = 0;
        public int departmentsCreatedBy { get; set; } = 0;

        public int totalPrograms{ get; set; } = 0;
        public int programsCreatedBy { get; set; } = 0;

        public int totalSemesters { get; set; } = 0;
        public int semestersCreatedBy { get; set; } = 0;

        public int totalFaculties { get; set; } = 0;
        public int facultiesCreatedBy { get; set; } = 0;

        public int totalStudents { get; set; } = 0;
        public int studentsCreatedBy { get; set; } = 0;

        public int totalOfferedCourses { get; set; } = 0;
        public int offeredCoursesCreatedBy { get; set; } = 0;

        public int totalTimetables { get; set; } = 0;
        public int timeTablesCreatedBy { get; set; } = 0;
        public DashboardViewModel()
        {
            profileView=new ApplicationUser();
            //RegisterViewModel=new RegisterViewModel();
        }

    }
}
