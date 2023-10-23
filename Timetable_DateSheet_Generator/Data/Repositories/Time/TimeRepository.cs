using System;
using System.Collections.Generic;
using System.Linq;
using Timetable_DateSheet_Generator.Data.DbContext;
using Timetable_DateSheet_Generator.Models;
using Timetable_DateSheet_Generator.Models.ViewModels;

namespace Timetable_DateSheet_Generator.Data.Repositories.Time
{
    public class TimeRepository
    {
        private readonly Timetable_DateSheet_Context _context;
        public TimeRepository(Timetable_DateSheet_Context context)
        {
            _context = context;
        }
        public bool isExists(int TimeID)
        {
            return _context.Times.Any(c => c.TimeID == TimeID);
        }
        public void Initialize()
        {
            List<Times> times = new List<Times>();
            for (int Day = 1; Day < 8; Day++)
            {
                for (int Hour = 0; Hour < 24; Hour++)
                {
                    foreach (var slot in (Times.HourSubSlots[])Enum.GetValues(typeof(Times.HourSubSlots)))
                    {
                        var Slot = slot.GetDisplayName();
                        var ID = Convert.ToInt32(Convert.ToString(Day) + Convert.ToString(Hour) + Slot);
                        if (!isExists(ID))
                        {
                            Times time = new Times
                            {
                                TimeWeekDay = Day,
                                StartTime = Convert.ToInt32(Convert.ToString(Hour) + Slot.ToString()),
                                FinishTime = Convert.ToInt32(Convert.ToString(Hour) + (Convert.ToInt32(Slot) + 15).ToString()),
                                TimeID = ID
                            };
                            times.Add(time);
                        }
                    }
                }
            }
            addRange(times);
            SaveChanges();
        }
        public void addRange(List<Times> times)
        {
            _context.Times.AddRange(times);
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}

