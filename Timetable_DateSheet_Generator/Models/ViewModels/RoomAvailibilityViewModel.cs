using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Timetable_DateSheet_Generator.Models.ViewModels
{
    public class RoomAvailibilityViewModel
    {
        public Rooms Room { get;set;}        
        public Departments Department { get;set;}
        public string Count { get;set;}
        public RoomAvailibilityViewModel()
        {
            Room=new Rooms();
        }
    }
}
