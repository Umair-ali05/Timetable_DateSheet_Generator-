using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timetable_DateSheet_Generator.Data.DbContext;
using Timetable_DateSheet_Generator.Data.Repositories.OfferedCourseTimeSlot;
using Timetable_DateSheet_Generator.Models;
using Timetable_DateSheet_Generator.Models.ViewModels;

namespace Timetable_DateSheet_Generator.Data.Repositories.TimeTableTimeSlot
{
    public class TimeTableTimings : IRepository<TimeSlots, int>
    {
        private readonly Timetable_DateSheet_Context _context;
        private readonly CourseTimeSlotRepository courseTimeSlotRepository;
        public TimeTableTimings(Timetable_DateSheet_Context context)
        {
            _context = context;
            courseTimeSlotRepository=new CourseTimeSlotRepository(context);
        }
        public async Task Delete(int ID)
        {
            var Obj = await _context.TimeSlots.FindAsync(ID);
            _context.TimeSlots.Remove(Obj);
        }
        public async Task RemoveRange(int TimetableID)
        {
            courseTimeSlotRepository.RemoveRange(TimetableID);
            var list = await _context.TimeSlots.Where(c => c.TimeTableID == TimetableID).ToListAsync();
            _context.TimeSlots.RemoveRange(list);
        }
        public async Task<List<TimeSlots>> GetByTimetable(int timetable)
        {
            return await _context.TimeSlots
                .Include(c => c.TimeTable.Institute)
                .Include(c => c.TimeTable.Semester)
                .Include(c => c.Time)
                .Where(c => c.TimeTableID == timetable)
                .ToListAsync();
        }
        public List<TimeSlots> GetByTimetableSync(int timetable)
        {
            return _context.TimeSlots
                .Include(c => c.TimeTable.Institute)
                .Include(c => c.TimeTable.Semester)
                .Include(c => c.Time)
                .Where(c => c.TimeTableID == timetable)
                .ToList();
        }
        public int getCount(int timetable)
        {
            return _context.TimeSlots
                .Where(c => c.TimeTableID == timetable)
                .ToList().Count;
        }
        public async Task<TimeSlots> GetById(int ID)
        {
            return await _context.TimeSlots
                .Include(c => c.TimeTable.Institute)
                .Include(c => c.TimeTable.Semester)
                .Include(c => c.Time)
                .Where(c => c.TimeSlotID == ID)
                .FirstOrDefaultAsync();
        }
        public async Task<List<TimeSlots>> GetAll(string TextSearch)
        {
            return await _context.TimeSlots
                .Include(c => c.TimeTable.Institute)
                .Include(c => c.TimeTable.Semester)
                .Include(c => c.Time)
                .ToListAsync();
        }

        public async Task Insert(TimeSlots Object)
        {
            await _context.TimeSlots.AddAsync(Object);
            await SaveChangesAsync();
        }
        public async Task InsertRangeAsync(List<TimeSlots> Object)
        {
            await _context.TimeSlots.AddRangeAsync(Object);
            await SaveChangesAsync();
        }
        public void InsertRange(List<TimeSlots> Object)
        {
            _context.TimeSlots.AddRange(Object);
            SaveChanges();
        }
        public void InsertSync(TimeSlots Object)
        {
            _context.TimeSlots.Add(Object);
            SaveChanges();
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void RemoveByTimetable(int timetableID)
        {
            var Obj = _context.TimeSlots
                .Where(c => c.TimeTableID == timetableID)
                .ToList();
            if (Obj != null)
                _context.TimeSlots.RemoveRange(Obj);
        }
        public bool checkSlots(int Day, int Hour, int TimetableID)
        {
            bool isExists = false;
            foreach (var slot in (Times.HourSubSlots[])Enum.GetValues(typeof(Times.HourSubSlots)))
            {
                var Slot = slot.GetDisplayName();
                var TimeID = Convert.ToInt32(Convert.ToString(Day) + Convert.ToString(Hour) + Convert.ToString(Slot));
                if (IsExistsSync(TimetableID, TimeID))
                    return true;
                else
                    isExists = false;
            }
            return isExists;
        }
        public void GenerateAndSave(int Hour, int Day, Times.HourSubSlots startSlot, Times.HourSubSlots endSlot, int TimetableID)
        {
            string startSlt = startSlot.GetDisplayName();
            string endSlt = endSlot.GetDisplayName();
            if (Convert.ToInt16(startSlot) <= Convert.ToInt16(endSlt))
            {
                for (int i = 0; i < Enum.GetNames(typeof(Times.HourSubSlots)).Length; i++)
                {
                    var TimeID = Convert.ToInt32(Convert.ToString(Day) + Convert.ToString(Hour) + Convert.ToString(startSlt));
                    if (_context.Times.Any(c => c.TimeID == TimeID))
                    {
                        int nextSlt;
                        if (Convert.ToInt32(startSlt) != Convert.ToInt32(endSlt))
                            nextSlt = Convert.ToInt32(startSlt) + 15;
                        else
                        {
                            nextSlt = Convert.ToInt32(startSlt);
                        }
                        if (!IsExistsSync(TimetableID, TimeID))
                        {

                            TimeSlots timetable_Timings = new TimeSlots
                            {
                                TimeTableID = TimetableID,
                                TimeID = TimeID
                            };
                            InsertSync(timetable_Timings);
                            SaveChanges();

                        }
                        startSlt = Convert.ToString(nextSlt);
                        if (startSlt.Length == 1)
                            startSlt += "0";
                    }
                }
            }
        }
        public List<TimeSlots> getByTimeTable_Day(int TimeTable, int Day)
        {
            return
                _context.TimeSlots
                .Include(c => c.TimeTable.Institute)
                .Include(c => c.TimeTable.Semester)
                .Include(c => c.Time)
                .Where(c => c.TimeTableID == TimeTable && c.Time.TimeWeekDay == Day)
                .ToList();
        }
        public TimeSlots getByTimeTable_Time(int TimeTable, int Time)
        {
            return
                _context.TimeSlots
                .Include(c => c.TimeTable.Institute)
                .Include(c => c.TimeTable.Semester)
                .Include(c => c.Time)
                .Where(c => c.TimeTableID == TimeTable && c.TimeID == Time)
                .FirstOrDefault();
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
        public void Update(TimeSlots Object)
        {
            _context.TimeSlots.Update(Object);
        }
        public bool IsExistsSync(int ID)
        {
            return _context.TimeSlots.Any(c => c.TimeSlotID == ID);
        }
        public bool IsExistsDay(int Day)
        {
            return _context
                .TimeSlots
                .Include(c => c.Time)
                .Any(c => c.Time.TimeWeekDay == Day);
        }
        public async Task<bool> IsExists(int ID)
        {
            return await _context.TimeSlots.AnyAsync(c => c.TimeSlotID == ID);
        }
        public bool IsExistsSync(int timetableID, int TimeID)
        {
            return _context.TimeSlots.Any(c => c.TimeTableID == timetableID && c.TimeID == TimeID);
        }
        public async Task<bool> IsAnytimeTableEntryExistAsync(int timetableID)
        {
            return await _context.TimeSlots.AnyAsync(c => c.TimeTableID == timetableID);
        }
        public bool IsAnytimeTableEntryExist(int timetableID)
        {
            return _context.TimeSlots.Any(c => c.TimeTableID == timetableID);
        }
    }
}
