using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timetable_DateSheet_Generator.Data.DbContext;
using Timetable_DateSheet_Generator.Data.Repositories.TimeTableTimeSlot;
using Timetable_DateSheet_Generator.Models;

namespace Timetable_DateSheet_Generator.Data.Repositories.TimeTable
{
    public class TimeTableRepository : IRepository<TimeTables, int>
    {
        private readonly Timetable_DateSheet_Context _context;
        private readonly TimeTableTimings timeTableTimings;
        public TimeTableRepository(Timetable_DateSheet_Context context)
        {
            _context = context;
            timeTableTimings = new TimeTableTimings(context);
        }
        public async Task Delete(int ID)
        {
            TimeTables TimeTable = await _context.TimeTables.FindAsync(ID);
            _context.TimeTables.Remove(TimeTable);
        }
        public async Task<TimeTables> GetById(int ID)
        {
            return await _context.TimeTables.
                Include(c => c.Institute)
                .Include(c => c.Semester)
                .Where(c => c.TimeTableID == ID).FirstOrDefaultAsync();
        }
        public async Task<TimeTables> GetByInstitute_Semester(int Institute, int Semester)
        {
            return await _context.TimeTables.
                Include(c => c.Institute)
                .Include(c => c.Semester)
                .Where(c => c.InstituteID == Institute && c.SemesterID == Semester).FirstOrDefaultAsync();
        }
        public async Task<List<TimeTables>> GetAll(string TextSearch)
        {
            return await _context.TimeTables
                .Include(c => c.Institute)
                .Include(c => c.Semester)
                .ToListAsync();
        }
        public async Task Insert(TimeTables Object)
        {
            await _context.TimeTables.AddAsync(Object);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
        public void Update(TimeTables Object)
        {
            _context.TimeTables.Update(Object);
        }
        public async Task<bool> IsExists(int ID)
        {
            return await _context.TimeTables.AnyAsync(c => c.TimeTableID == ID);
        }
        public async Task<bool> IsExists(int InstituteID, int SemesterID, int? TimetableID)
        {
            return await _context
                .TimeTables
                .AnyAsync(c => c.InstituteID == InstituteID && c.SemesterID == SemesterID && ((TimetableID.HasValue && c.TimeTableID != TimetableID.Value) || !TimetableID.HasValue));
        }
        public IEnumerable<object> GetForSelectList(int? Institute, int? Semester)
        {
            var temp = new List<object>();
            foreach (var item in _context.TimeTables
                .Include(c => c.Semester)
                .Include(c => c.Institute).ToList()
                .Where(c => ((Institute.HasValue && c.InstituteID == Institute.Value) || !Institute.HasValue) &&
                ((Semester.HasValue && c.SemesterID == Semester.Value) || !Semester.HasValue))
                .Select(c => new { ID = c.TimeTableID, Name = c.Institute.InstituteName + "-" + c.Semester.getSemesterType + "(" + c.Semester.SemesterYear + ")" }))
            {
                if (!timeTableTimings.IsAnytimeTableEntryExist(item.ID))
                    temp.Add(item);
            }
            return temp;
        }
    }
}
