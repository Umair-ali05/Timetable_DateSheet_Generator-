using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timetable_DateSheet_Generator.Data.DbContext;
using Timetable_DateSheet_Generator.Models;
using Timetable_DateSheet_Generator.Models.ViewModels;

namespace Timetable_DateSheet_Generator.Data.Repositories.FacultyMemberAvailibility
{
    public class FacultyMemberAvailibilityRepository : IRepository<FacultyMemberAvailabilities, int>
    {
        private readonly Timetable_DateSheet_Context _context;
        public FacultyMemberAvailibilityRepository(Timetable_DateSheet_Context context)
        {
            _context = context;
        }
        public async Task Delete(int ID)
        {
            var Obj = await _context.FacultyMemberAvailabilities.FindAsync(ID);
            _context.FacultyMemberAvailabilities.Remove(Obj);
        }
        public async Task RemoveRange(int ID)
        {
            var list = await _context.FacultyMemberAvailabilities.Where(c => c.FacultyMemberID == ID).ToListAsync();
            _context.FacultyMemberAvailabilities.RemoveRange(list);
        }
        public async Task<List<FacultyMemberAvailabilities>> GetByFaculty(int faculty)
        {
            return await _context.FacultyMemberAvailabilities
                .Include(c => c.FacultyMember.Department.Institute)
                .Include(c => c.Time)
                .Where(c => c.FacultyMemberID == faculty)
                .ToListAsync();
        }
        public int getCount(int faculty)
        {
            return _context.FacultyMemberAvailabilities
                .Where(c => c.FacultyMemberID == faculty)
                .ToList().Count;
        }
        public async Task<FacultyMemberAvailabilities> GetById(int ID)
        {
            return await _context.FacultyMemberAvailabilities
                .Include(c => c.FacultyMember.Department.Institute)
                .Include(c => c.Time)
                .Where(c => c.FacultyMemberAvailabilityID == ID)
                .FirstOrDefaultAsync();
        }
        public async Task<List<FacultyMemberAvailabilities>> GetAll(string TextSearch)
        {
            return await _context.FacultyMemberAvailabilities
                .Include(c => c.FacultyMember.Department.Institute)
                .Include(c => c.Time)
                .ToListAsync();
        }

        public async Task Insert(FacultyMemberAvailabilities Object)
        {
            await _context.FacultyMemberAvailabilities.AddAsync(Object);
            await SaveChangesAsync();
        }
        public async Task InsertRangeAsync(List<FacultyMemberAvailabilities> Object)
        {
            await _context.FacultyMemberAvailabilities.AddRangeAsync(Object);
            await SaveChangesAsync();
        }
        public void InsertRange(List<FacultyMemberAvailabilities> Object)
        {
            _context.FacultyMemberAvailabilities.AddRange(Object);
            SaveChanges();
        }
        public void InsertSync(FacultyMemberAvailabilities Object)
        {
            _context.FacultyMemberAvailabilities.Add(Object);
            SaveChanges();
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void RemoveByFaculty(int facultyID)
        {
            var Obj = _context.FacultyMemberAvailabilities
                .Where(c => c.FacultyMemberID == facultyID)
                .ToList();
            if (Obj != null)
                _context.FacultyMemberAvailabilities.RemoveRange(Obj);
        }
        public bool checkSlots(int Day, int Hour, int facultyID)
        {
            bool isExists = false;
            foreach (var slot in (Times.HourSubSlots[])Enum.GetValues(typeof(Times.HourSubSlots)))
            {
                var Slot = slot.GetDisplayName();
                var TimeID = Convert.ToInt32(Convert.ToString(Day) + Convert.ToString(Hour) + Convert.ToString(Slot));
                if (IsExistsSync(facultyID, TimeID))
                    return true;
                else
                    isExists = false;
            }
            return isExists;
        }
        public void GenerateAndSave(int Hour, int Day, Times.HourSubSlots startSlot, Times.HourSubSlots endSlot, int FacultyID)
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
                        if (!IsExistsSync(FacultyID, TimeID))
                        {

                            FacultyMemberAvailabilities facultyTimings = new FacultyMemberAvailabilities
                            {
                                FacultyMemberID = FacultyID,
                                TimeID = TimeID
                            };
                            InsertSync(facultyTimings);
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
        public void Update(FacultyMemberAvailabilities Object)
        {
            _context.FacultyMemberAvailabilities.Update(Object);
        }
        public bool IsExistsSync(int ID)
        {
            return _context.FacultyMemberAvailabilities.Any(c => c.FacultyMemberAvailabilityID == ID);
        }
        public bool IsExistsSync(int facultyID, int TimeID)
        {
            return _context.FacultyMemberAvailabilities.Any(c => c.FacultyMemberID == facultyID && c.TimeID == TimeID);
        }
        public async Task<bool> IsExists(int ID)
        {
            return await _context.FacultyMemberAvailabilities.AnyAsync(c => c.FacultyMemberAvailabilityID == ID);
        }
        public async Task<bool> IsAnyFacultyEntryExistAsync(int facultyID)
        {
            return await _context.FacultyMemberAvailabilities.AnyAsync(c => c.FacultyMemberID == facultyID);
        }
        public bool IsAnyFacultyEntryExist(int facultyID)
        {
            return _context.FacultyMemberAvailabilities.Any(c => c.FacultyMemberID == facultyID);
        }
        public List<FacultyMemberAvailabilities> getByFaculty_Day(int faculty, int Day)
        {
            return _context.FacultyMemberAvailabilities
                .Include(c => c.FacultyMember)
                .Include(c => c.Time)
                .Where(c => c.FacultyMemberID == faculty && c.Time.TimeWeekDay == Day).ToList();
        }
        public List<FacultyMemberAvailabilities> getBy_Faculty_Day(int faculty, int Day)
        {
            return
                _context.FacultyMemberAvailabilities
                .Where(c => c.Time.TimeWeekDay == Day && c.FacultyMemberID == faculty)
                .ToList();
        }
    }
}
