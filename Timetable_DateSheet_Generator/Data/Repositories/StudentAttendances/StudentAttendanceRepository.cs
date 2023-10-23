using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timetable_DateSheet_Generator.Data.DbContext;
using Timetable_DateSheet_Generator.Data.Repositories.Attendances;
using Timetable_DateSheet_Generator.Models;

namespace Timetable_DateSheet_Generator.Data.Repositories.StudentAttendances
{

    public class StudentAttendanceRepository : IRepository<StudentAttendance, long>
    {
        private readonly Timetable_DateSheet_Context _context;
        private readonly AttendanceRepository attendanceRepository;
        public StudentAttendanceRepository(Timetable_DateSheet_Context context)
        {
            _context = context;
            attendanceRepository = new AttendanceRepository(context);
        }
        public async Task Delete(long ID)
        {
            StudentAttendance attendance = await _context.StudentAttendances.FindAsync(ID);
            _context.StudentAttendances.Remove(attendance);
        }
        public void RemoveByCourse_Student(int Course, int Student)
        {
            _context.StudentAttendances.RemoveRange(
                 _context
                .StudentAttendances
                .Include(c => c.RegisteredCourse)
                .Where(c => c.RegisteredCourse.OfferedCourseID == Course && c.RegisteredCourse.StudentID == Student)
                .ToList());
        }
        public void RemoveByCourse(int Course)
        {
            _context.StudentAttendances.RemoveRange(
                 _context
                .StudentAttendances
                .Include(c => c.RegisteredCourse)
                .Where(c => c.RegisteredCourse.OfferedCourseID == Course)
                .ToList());
        }

        public async Task<StudentAttendance> GetById(long ID)
        {
            return await _context
                .StudentAttendances
                .Include(c => c.Attendance.OfferedCourse)
                .Include(c => c.RegisteredCourse.Student)
                .Where(c => c.StudentAttendanceID == ID)
                .FirstOrDefaultAsync();
        }
        public async Task<StudentAttendance> GetByAttendance_RegId(int Attendance, int RegID)
        {
            return await _context
                .StudentAttendances
                .Include(c => c.Attendance.OfferedCourse)
                .Include(c => c.RegisteredCourse.Student)
                .Where(c => c.AttendanceID == Attendance && c.RegisteredCourseID == RegID)
                .FirstOrDefaultAsync();
        }
        public async Task<List<StudentAttendance>> GetByAttendanceId(int ID)
        {
            return await _context
                .StudentAttendances
                .Include(c => c.Attendance.OfferedCourse)
                .Include(c => c.RegisteredCourse.Student)
                .Where(c => c.AttendanceID == ID)
                .ToListAsync();
        }
        public async Task<List<StudentAttendance>> GetByCourse(int Course)
        {
            return await _context
                .StudentAttendances
                .Include(c => c.Attendance.OfferedCourse)
                .Include(c => c.RegisteredCourse.Student)
                .Where(c => c.Attendance.OfferedCourseID == Course)
                .ToListAsync();
        }
        public async Task<List<StudentAttendance>> GetAll(string TextSearch)
        {
            return await _context
                .StudentAttendances
                .Include(c => c.Attendance.OfferedCourse)
                .Include(c => c.RegisteredCourse.Student)
                .ToListAsync();
        }
        public async Task<StudentAttendance> GetByStudent_Courses(int studentID, int CourseID)
        {
            return await _context
                .StudentAttendances
                .Include(c => c.Attendance.OfferedCourse)
                .Include(c => c.RegisteredCourse.Student)
                .Where(c => c.RegisteredCourse.StudentID == studentID && c.RegisteredCourse.OfferedCourseID == CourseID)
                .FirstOrDefaultAsync();
        }
        public async Task Insert(StudentAttendance Object)
        {
            if (!await IsExists(Object.RegisteredCourseID, Object.AttendanceID))
                await _context.StudentAttendances.AddAsync(Object);
        }
        public async Task<double> StudentAttendance(int RegID, bool isPresent)
        {
            double total = 0;
            foreach (var item in await _context
                .StudentAttendances
                .Include(c => c.RegisteredCourse)
                .Where(c => c.RegisteredCourseID == RegID && c.IsPresent == isPresent)
                .ToListAsync())
                total += Convert.ToDouble(Attendance.getName(item.Attendance.AttendanceCreditHours));
            return total;

        }
        public async Task AddRange(List<StudentAttendance> studentAttendances)
        {
            var newValues = new List<StudentAttendance>();
            foreach (var obj in studentAttendances)
                if (!await IsExists(obj.RegisteredCourseID, obj.AttendanceID))
                    newValues.Add(obj);
            await _context.StudentAttendances.AddRangeAsync(newValues);
        }
        public void UpdateRange(List<StudentAttendance> studentAttendances)
        {
            _context.StudentAttendances.UpdateRange(studentAttendances);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
        public void Update(StudentAttendance Object)
        {
            _context.StudentAttendances.Update(Object);
        }
        public async Task<bool> IsExists(long ID)
        {
            return await _context.StudentAttendances.AnyAsync(c => c.StudentAttendanceID == ID);
        }
        public async Task<bool> IsExists(int reg, int attendance)
        {
            return await _context.StudentAttendances.AnyAsync(c => c.RegisteredCourseID == reg && c.AttendanceID == attendance);
        }
        public async Task<bool> IsExist(int Course, int student)
        {
            return await _context.StudentAttendances
                .Include(c => c.RegisteredCourse)
                .AnyAsync(c => c.RegisteredCourse.OfferedCourseID == Course && c.RegisteredCourse.StudentID == student);
        }
    }
}

