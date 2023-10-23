using System.Collections.Generic;

namespace Timetable_DateSheet_Generator.Data.Algorithms.WeekStructure
{
    public class Day
    {
        public List<Hour> ConsectiveContactHoursSlots { get; set; }
        public Day()
        {
            ConsectiveContactHoursSlots = new List<Hour>();
        }
    }
}
