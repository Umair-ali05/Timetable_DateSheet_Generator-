using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Timetable_DateSheet_Generator.Models.ViewModels
{
    public class TimingList
    {
        public DaySlots MondaySlots { get; set; }
        public DaySlots TuesdaySlots { get; set; }
        public DaySlots WednesdaySlots { get; set; }
        public DaySlots ThursdaySlots { get; set; }
        public DaySlots FridaySlots { get; set; }
        public DaySlots SaturdaySlots { get; set; }
        public DaySlots SundaySlots { get; set; }
        public TimingList()
        {
            this.MondaySlots = new DaySlots();
            this.TuesdaySlots = new DaySlots();
            this.WednesdaySlots = new DaySlots();
            this.ThursdaySlots = new DaySlots();
            this.FridaySlots = new DaySlots();
            this.SaturdaySlots = new DaySlots();
            this.SundaySlots = new DaySlots();
        }
        public TimingList(int MondayHours, int TuesdayHours, int WednesdayHours, int ThursdayHours, int FridayHours, int SaturdayHours, int SundayHours)
        {
            this.MondaySlots = new DaySlots(MondayHours);
            this.TuesdaySlots = new DaySlots(TuesdayHours);
            this.WednesdaySlots = new DaySlots(WednesdayHours);
            this.ThursdaySlots = new DaySlots(ThursdayHours);
            this.FridaySlots = new DaySlots(FridayHours);
            this.SaturdaySlots = new DaySlots(SaturdayHours);
            this.SundaySlots = new DaySlots(SundayHours);
        }
    }
}
