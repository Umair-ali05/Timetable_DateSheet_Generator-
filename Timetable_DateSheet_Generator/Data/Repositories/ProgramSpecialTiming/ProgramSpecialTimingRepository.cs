using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timetable_DateSheet_Generator.Data.DbContext;
using Timetable_DateSheet_Generator.Models;
using Timetable_DateSheet_Generator.Models.ViewModels;

namespace Timetable_DateSheet_Generator.Data.Repositories.ProgramSpecialTiming
{
    public class ProgramSpecialTimingRepository : IRepository<ProgramSpecialTimings, int>
    {
        private readonly Timetable_DateSheet_Context _context;
        public ProgramSpecialTimingRepository(Timetable_DateSheet_Context context)
        {
            _context = context;
        }
        public async Task Delete(int ID)
        {
            var Obj = await _context.ProgramSpecialTimings.FindAsync(ID);
            _context.ProgramSpecialTimings.Remove(Obj);
        }
        public async Task RemoveRange(int ID)
        {
            var list = await _context.ProgramSpecialTimings.Where(c => c.ProgramID == ID).ToListAsync();
            _context.ProgramSpecialTimings.RemoveRange(list);
        }
        public async Task<List<ProgramSpecialTimings>> GetByProgram(int Program)
        {
            return await _context.ProgramSpecialTimings
                .Include(c => c.Program.Department.Institute)
                .Include(c => c.Time)
                .Where(c => c.ProgramID == Program)
                .ToListAsync();
        }
        public List<ProgramSpecialTimings> GetByProgramSync(int Program)
        {
            return _context.ProgramSpecialTimings
                .Include(c => c.Time)
                .Where(c => c.ProgramID == Program)
                .ToList();
        }
        public List<ProgramSpecialTimings> GetByProgram_DaySync(int Program, int Day)
        {
            return _context.ProgramSpecialTimings
                .Include(c => c.Time)
                .Where(c => c.ProgramID == Program && c.Time.TimeWeekDay == Day)
                .ToList();
        }
        public async Task<List<ProgramSpecialTimings>> GetOrdered_ByProgram(int Program)
        {
            return
                 await _context
                 .ProgramSpecialTimings
                 .Include(c => c.Program.Department.Institute)
                 .Include(c => c.Time)
                 .Where(c => c.ProgramID == Program)
                 .OrderBy(c => c.TimeID)
                 .ToListAsync();
        }
        public async Task<List<ProgramSpecialTimings>> GetOrdered_ByDepartment(int Department)
        {
            return
                 await _context
                 .ProgramSpecialTimings
                 .Include(c => c.Program.Department.Institute)
                 .Include(c => c.Time)
                 .Where(c => c.Program.DepartmentID == Department)
                 .OrderBy(c => c.TimeID)
                 .ToListAsync();
        }
        public int getCount(int Program)
        {
            return _context.ProgramSpecialTimings
                .Where(c => c.ProgramID == Program)
                .ToList().Count;
        }
        public async Task<ProgramSpecialTimings> GetById(int ID)
        {
            return await _context.ProgramSpecialTimings
                .Include(c => c.Program.Department.Institute)
                .Include(c => c.Time)
                .Where(c => c.ProgramSpecialTimingID == ID)
                .FirstOrDefaultAsync();
        }
        public async Task<List<ProgramSpecialTimings>> GetAll(string TextSearch)
        {
            return await _context.ProgramSpecialTimings
                .Include(c => c.Program.Department.Institute)
                .Include(c => c.Time)
                .ToListAsync();
        }
        public async Task Insert(ProgramSpecialTimings Object)
        {
            await _context.ProgramSpecialTimings.AddAsync(Object);
            await SaveChangesAsync();
        }
        public async Task InsertRangeAsync(List<ProgramSpecialTimings> Object)
        {
            await _context.ProgramSpecialTimings.AddRangeAsync(Object);
            await SaveChangesAsync();
        }
        public void InsertRange(List<ProgramSpecialTimings> Object)
        {
            _context.ProgramSpecialTimings.AddRange(Object);
            SaveChanges();
        }
        public void InsertSync(ProgramSpecialTimings Object)
        {
            _context.ProgramSpecialTimings.Add(Object);
            SaveChanges();
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void RemoveByProgram(int ProgramID)
        {
            var Obj = _context.ProgramSpecialTimings
                .Where(c => c.ProgramID == ProgramID)
                .ToList();
            if (Obj != null)
                _context.ProgramSpecialTimings.RemoveRange(Obj);
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

                            ProgramSpecialTimings programSpecialTimings = new ProgramSpecialTimings
                            {
                                ProgramID = ProgramID,
                                TimeID = TimeID
                            };
                            InsertSync(programSpecialTimings);
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
        public void Update(ProgramSpecialTimings Object)
        {
            _context.ProgramSpecialTimings.Update(Object);
        }
        public bool IsExistsSync(int ID)
        {
            return _context.ProgramSpecialTimings.Any(c => c.ProgramSpecialTimingID == ID);
        }
        public bool IsExistsSync(int ProgramID, int TimeID)
        {
            return _context.ProgramSpecialTimings.Any(c => c.ProgramID == ProgramID && c.TimeID == TimeID);
        }
        public async Task<bool> IsExists(int ID)
        {
            return await _context.ProgramSpecialTimings.AnyAsync(c => c.ProgramSpecialTimingID == ID);
        }
        public async Task<bool> IsAnyProgramEntryExistAsync(int programID)
        {
            return await _context.ProgramSpecialTimings.AnyAsync(c => c.ProgramID == programID);
        }
        public bool IsAnyProgramEntryExist(int programID)
        {
            return _context.ProgramSpecialTimings.Any(c => c.ProgramID == programID);
        }
    }
}
