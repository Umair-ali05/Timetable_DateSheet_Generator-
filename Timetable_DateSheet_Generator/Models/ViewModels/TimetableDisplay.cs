using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Timetable_DateSheet_Generator.Models.ViewModels
{
    public class TimetableDisplay
    {
        public List<List<OfferedCourseTimeSlots>> MondayCourseTimeSlots { get; set; }
        public List<List<OfferedCourseTimeSlots>> TuesdayCourseTimeSlots { get; set; }
        public List<List<OfferedCourseTimeSlots>> WednesdayCourseTimeSlots { get; set; }
        public List<List<OfferedCourseTimeSlots>> ThursdayCourseTimeSlots { get; set; }
        public List<List<OfferedCourseTimeSlots>> FridayCourseTimeSlots { get; set; }
        public List<List<OfferedCourseTimeSlots>> SaturdayCourseTimeSlots { get; set; }
        public List<List<OfferedCourseTimeSlots>> SundayCourseTimeSlots { get; set; }
        public String Description { get; set; }
        public int Number { get; set; }
        public TimetableDisplay()
        {
            this.MondayCourseTimeSlots = new List<List<OfferedCourseTimeSlots>>();
            this.TuesdayCourseTimeSlots = new List<List<OfferedCourseTimeSlots>>();
            this.WednesdayCourseTimeSlots = new List<List<OfferedCourseTimeSlots>>();
            this.ThursdayCourseTimeSlots = new List<List<OfferedCourseTimeSlots>>();
            this.FridayCourseTimeSlots = new List<List<OfferedCourseTimeSlots>>();
            this.SaturdayCourseTimeSlots = new List<List<OfferedCourseTimeSlots>>();
            this.SundayCourseTimeSlots = new List<List<OfferedCourseTimeSlots>>();
        }
    }
}
