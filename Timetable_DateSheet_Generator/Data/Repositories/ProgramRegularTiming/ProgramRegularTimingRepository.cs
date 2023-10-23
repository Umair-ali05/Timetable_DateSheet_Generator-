using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timetable_DateSheet_Generator.Data.DbContext;
using Timetable_DateSheet_Generator.Models;
using Timetable_DateSheet_Generator.Models.ViewModels;

namespace Timetable_DateSheet_Generator.Data.Repositories.ProgramRegularTiming
{
    public class ProgramRegularTimingRepository : IRepository<ProgramRegularTimings, int>
    {
        private readonly Timetable_DateSheet_Context _context;
        public ProgramRegularTimingRepository(Timetable_DateSheet_Context context)
        {
            _context = context;
        }
        public async Task Delete(int ID)
        {
            var Obj = await _context.ProgramRegularTimings.FindAsync(ID);
            _context.ProgramRegularTimings.Remove(Obj);
        }
        public async Task RemoveRange(int ID)
        {
            var list = await _context.ProgramRegularTimings.Where(c => c.ProgramID == ID).ToListAsync();
            _context.ProgramRegularTimings.RemoveRange(list);
        }
        public async Task<List<ProgramRegularTimings>> GetByProgram(int Program)
        {
            return await _context.ProgramRegularTimings
                .Include(c => c.Program.Department.Institute)
                .Include(c => c.Time)
                .Where(c => c.ProgramID == Program)
                .ToListAsync();
        }
        public List<ProgramRegularTimings> GetByProgramSync(int Program)
        {
            return _context.ProgramRegularTimings
                .Include(c => c.Time)
                .Where(c => c.ProgramID == Program)
                .ToList();
        }
        public List<ProgramRegularTimings> GetByProgram_DaySync(int Program, int Day)
        {
            return _context.ProgramRegularTimings
                .Include(c => c.Time)
                .Where(c => c.ProgramID == Program && c.Time.TimeWeekDay == Day)
                .ToList();
        }
        public async Task<List<ProgramRegularTimings>> GetOrdered_ByProgram(int Program)
        {
            return
                 await _context
                 .ProgramRegularTimings
                 .Include(c => c.Program.Department.Institute)
                 .Include(c => c.Time)
                 .Where(c => c.ProgramID == Program)
                 .OrderBy(c => c.TimeID)
                 .ToListAsync();
        }
        public async Task<List<ProgramRegularTimings>> GetOrdered_ByDepartment(int Department)
        {
            return
                 await _context
                 .ProgramRegularTimings
                 .Include(c => c.Program.Department.Institute)
                 .Include(c => c.Time)
                 .Where(c => c.Program.DepartmentID == Department)
                 .OrderBy(c => c.TimeID)
                 .ToListAsync();
        }
        public int getCount(int Program)
        {
            return _context.ProgramRegularTimings
                .Where(c => c.ProgramID == Program)
                .ToList().Count;
        }
        public async Task<ProgramRegularTimings> GetById(int ID)
        {
            return await _context.ProgramRegularTimings
                .Include(c => c.Program.Department.Institute)
                .Include(c => c.Time)
                .Where(c => c.ProgramRegularTimingID == ID)
                .FirstOrDefaultAsync();
        }
        public async Task<List<ProgramRegularTimings>> GetAll(string TextSearch)
        {
            return await _context.ProgramRegularTimings
                .Include(c => c.Program.Department.Institute)
                .Include(c => c.Time)
                .ToListAsync();
        }
        public async Task Insert(ProgramRegularTimings Object)
        {
            await _context.ProgramRegularTimings.AddAsync(Object);
            await SaveChangesAsync();
        }
        public async Task InsertRangeAsync(List<ProgramRegularTimings> Object)
        {
            await _context.ProgramRegularTimings.AddRangeAsync(Object);
            await SaveChangesAsync();
        }
        public void InsertRange(List<ProgramRegularTimings> Object)
        {
            _context.ProgramRegularTimings.AddRange(Object);
            SaveChanges();
        }
        public void InsertSync(ProgramRegularTimings Object)
        {
            _context.ProgramRegularTimings.Add(Object);
            SaveChanges();
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void RemoveByProgram(int ProgramID)
        {
            var Obj = _context.ProgramRegularTimings
                .Where(c => c.ProgramID == ProgramID)
                .ToList();
            if (Obj != null)
                _context.ProgramRegularTimings.RemoveRange(Obj);
        }
        public bool checkSlots(int Day, int Hour, int ProgramID)
        {
            bool isExists = false;
            foreach (var slot in (Times.HourSubSlots[])Enum.GetValues(typeof(Times.HourSubSlots)))
            {
                var Slot = slot.GetDisplayName();
                var TimeID = Convert.ToInt32(Convert.ToString(Day) + Convert.ToString(Hour) + Convert.ToString(Slot));
                if (IsExistsSync(ProgramID, TimeID))
                    return true;
                else
                    isExists = false;
            }
            return isExists;
        }
        public void GenerateAndSave(int Hour, int Day, Times.HourSubSlots startSlot, Times.HourSubSlots endSlot, int ProgramID)
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
                        if (!IsExistsSync(ProgramID, TimeID))
                        {

                            ProgramRegularTimings programRegularTimings = new ProgramRegularTimings
                            {
                                ProgramID = ProgramID,
                                TimeID = TimeID
                            };
                            InsertSync(programRegularTimings);
                            SaveChanges();

                        }
                        startSlt = Convert.ToString(nextSlt);
                        if (startSlt.Length == 1)
                            startSlt += "0";
                    }
                }
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
        public void Update(ProgramRegularTimings Object)
        {
            _context.ProgramRegularTimings.Update(Object);
        }
        public bool IsExistsSync(int ID)
        {
            return _context.ProgramRegularTimings.Any(c => c.ProgramRegularTimingID == ID);
        }
        public bool IsExistsSync(int ProgramID, int TimeID)
        {
            return _context.ProgramRegularTimings.Any(c => c.ProgramID == ProgramID && c.TimeID == TimeID);
        }
        public async Task<bool> IsExists(int ID)
        {
            return await _context.ProgramRegularTimings.AnyAsync(c => c.ProgramRegularTimingID == ID);
        }
        public async Task<bool> IsAnyProgramEntryExistAsync(int programID)
        {
            return await _context.ProgramRegularTimings.AnyAsync(c => c.ProgramID == programID);
        }
        public bool IsAnyProgramEntryExist(int programID)
        {
            return _context.ProgramRegularTimings.Any(c => c.ProgramID == programID);
        }
    }
}
