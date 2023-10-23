using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Timetable_DateSheet_Generator.Models.ViewModels
{
    public class FacultyViewModel
    {
        public RegisterViewModel registerViewModel { get;set;}
        public FacultyMembers FacultyMember { get;set;}
        public FacultyViewModel()
        {
            registerViewModel = new RegisterViewModel();
            FacultyMember=new FacultyMembers();            
        }
    }
}
