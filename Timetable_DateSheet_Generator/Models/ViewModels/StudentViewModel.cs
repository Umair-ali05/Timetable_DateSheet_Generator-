using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Timetable_DateSheet_Generator.Models.ViewModels
{
    public class StudentViewModel
    {
        public RegisterViewModel RegisterViewModel { get; set; }
        public Students Student { get; set; }
        public StudentViewModel()
        {
            RegisterViewModel = new RegisterViewModel();
            Student = new Students();
        }
    }
}
