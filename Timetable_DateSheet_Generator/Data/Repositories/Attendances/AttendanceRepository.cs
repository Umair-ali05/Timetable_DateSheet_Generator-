using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timetable_DateSheet_Generator.Data.DbContext;
using Timetable_DateSheet_Generator.Models;

namespace Timetable_DateSheet_Generator.Data.Repositories.Attendances
{
    public class AttendanceRepository : IRepository<Attendance, int>
    {
        private readonly Timetable_DateSheet_Context _context;
        public AttendanceRepository(Timetable_DateSheet_Context context)
        {
            _context = context;
        }
        public async Task Delete(int ID)
        {
            Attendance attendance = await _context.Attendances.FindAsync(ID);
            _context.Attendances.Remove(attendance);
        }

        public async Task<Attendance> GetById(int ID)
        {
            return await _context
                .Attendances
                .Include(c => c.OfferedCourse.Program.Department.Institute)
                .Include(c => c.OfferedCourse.FacultyMember)
                .Where(c => c.AttendanceID == ID)
                .FirstOrDefaultAsync();
        }
        public async Task<List<Attendance>> GetAll(string TextSearch)
        {
            return await _context
                .Attendances
                .Include(c => c.OfferedCourse.Program.Department.Institute)
                .Include(c => c.OfferedCourse.FacultyMember)
                .ToListAsync();
        }
        public async Task<List<Attendance>> GetByCourse(int CourseID)
        {
            return await _context
                .Attendances
                .Include(c => c.OfferedCourse.Program.Department.Institute)
                .Include(c => c.OfferedCourse.FacultyMember)
                .Where(c => c.OfferedCourseID == CourseID)
                .ToListAsync();
        }
        public async Task<double> GetTotalHoursMarked(int CourseID)
        {
            double total = 0;
            foreach (var item in await _context
                .Attendances
                .Where(c => c.OfferedCourseID == CourseID)
                .ToListAsync())
                total += Convert.ToDouble(Attendance.getName(item.AttendanceCreditHours));
            return total;
        }
        public Attendance getLatest(int Course)
        {
            return _context.Attendances
                .Where(c => c.OfferedCourseID == Course)
                .ToList()
                .LastOrDefault();
        }
        public async Task Insert(Attendance Object)
        {
            await _context.Attendances.AddAsync(Object);
        }
        public void InsertSync(Attendance Object)
        {
            _context.Attendances.Add(Object);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
        public void Update(Attendance Object)
        {
            _context.Attendances.Update(Object);
        }
        public async Task<bool> IsExists(int ID)
        {
            return await _context.Attendances.AnyAsync(c => c.AttendanceID == ID);
        }
    }
}
