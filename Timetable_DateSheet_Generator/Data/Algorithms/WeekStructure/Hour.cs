using System.Collections.Generic;
using Timetable_DateSheet_Generator.Models;

namespace Timetable_DateSheet_Generator.Data.Algorithms.WeekStructure
{
    public class Hour
    {
        public List<Times> Times { get; set; }
        public Hour()
        {
            Times = new List<Times>();
        }
    }
}
