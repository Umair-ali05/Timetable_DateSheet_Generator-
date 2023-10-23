using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Timetable_DateSheet_Generator.Models.ViewModels
{
    public class DaySlots
    {
        public const int totalHoursPerDays = 24;
        public bool Is_Day_Slot { get; set; }
        public Times.HourSubSlots Day_Start_Slots { get; set; }
        public Times.HourSubSlots Day_End_Slots { get; set; }
        public List<bool> Is_Hour_Slots { get; set; }
        public List<Times.HourSubSlots> Hour_Start_Slots { get; set; }
        public List<Times.HourSubSlots> Hour_End_Slots { get; set; }
        public List<int> Hours { get; set; }
        public List<string> hoursDetailLabels = new List<string>();
        public List<string> hoursLabels = new List<string>();
        public DaySlots()
        {
            this.initializeLabels();
            this.Hours = new List<int>();
            this.Is_Hour_Slots = new List<bool>();
            this.Hour_Start_Slots = new List<Times.HourSubSlots>();
            this.Hour_End_Slots = new List<Times.HourSubSlots>();
        }
        public DaySlots(int hoursPerDay)
        {
            this.initializeLabels();
            this.Hours = new List<int>();
            this.Is_Hour_Slots = new List<bool>();
            this.Hour_Start_Slots = new List<Times.HourSubSlots>();
            this.Hour_End_Slots = new List<Times.HourSubSlots>();
            for (int hour = 0; hour < hoursPerDay; hour++)
            {
                this.Is_Hour_Slots.Add(new bool());
                this.Hour_Start_Slots.Add(new Times.HourSubSlots());
                this.Hour_End_Slots.Add(new Times.HourSubSlots());
            }
        }
        public bool isHourExists(int hour)
        {
            return this.Hours.Any(c => c.Equals(hour));
        }
        public void initializeLabels()
        {

            /// Hour Labels
            this.hoursDetailLabels.Add("0000 (12:00AM)");
            this.hoursDetailLabels.Add("0100 (01:00AM)");
            this.hoursDetailLabels.Add("0200 (02:00AM)");
            this.hoursDetailLabels.Add("0300 (03:00AM)");
            this.hoursDetailLabels.Add("0400 (04:00AM)");
            this.hoursDetailLabels.Add("0500 (05:00AM)");
            this.hoursDetailLabels.Add("0600 (06:00AM)");
            this.hoursDetailLabels.Add("0700 (07:00AM)");
            this.hoursDetailLabels.Add("0800 (08:00AM)");
            this.hoursDetailLabels.Add("0900 (09:00AM)");
            this.hoursDetailLabels.Add("1000 (10:00AM)");
            this.hoursDetailLabels.Add("1100 (11:00AM)");
            this.hoursDetailLabels.Add("1200 (12:00PM)");
            this.hoursDetailLabels.Add("1300 (01:00PM)");
            this.hoursDetailLabels.Add("1400 (02:00PM)");
            this.hoursDetailLabels.Add("1500 (03:00PM)");
            this.hoursDetailLabels.Add("1600 (04:00PM)");
            this.hoursDetailLabels.Add("1700 (05:00PM)");
            this.hoursDetailLabels.Add("1800 (06:00PM)");
            this.hoursDetailLabels.Add("1900 (07:00PM)");
            this.hoursDetailLabels.Add("2000 (08:00PM)");
            this.hoursDetailLabels.Add("2100 (09:00PM)");
            this.hoursDetailLabels.Add("2200 (10:00PM)");
            this.hoursDetailLabels.Add("2300 (11:00PM)");


            this.hoursLabels.Add("12AM");
            this.hoursLabels.Add("01AM");
            this.hoursLabels.Add("02AM");
            this.hoursLabels.Add("03AM");
            this.hoursLabels.Add("04AM");
            this.hoursLabels.Add("05AM");
            this.hoursLabels.Add("06AM");
            this.hoursLabels.Add("07AM");
            this.hoursLabels.Add("08AM");
            this.hoursLabels.Add("09AM");
            this.hoursLabels.Add("10AM");
            this.hoursLabels.Add("11AM");
            this.hoursLabels.Add("12PM");
            this.hoursLabels.Add("01PM");
            this.hoursLabels.Add("02PM");
            this.hoursLabels.Add("03PM");
            this.hoursLabels.Add("04PM");
            this.hoursLabels.Add("05PM");
            this.hoursLabels.Add("06PM");
            this.hoursLabels.Add("07PM");
            this.hoursLabels.Add("08PM");
            this.hoursLabels.Add("09PM");
            this.hoursLabels.Add("10PM");
            this.hoursLabels.Add("11PM");
        }
    }
}
