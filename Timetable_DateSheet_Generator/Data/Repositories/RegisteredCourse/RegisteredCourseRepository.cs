using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timetable_DateSheet_Generator.Data.DbContext;
using Timetable_DateSheet_Generator.Models;

namespace Timetable_DateSheet_Generator.Data.Repositories.RegisteredCourse
{
    public class RegisteredCourseRepository : IRepository<RegisteredCourses, int>
    {
        private readonly Timetable_DateSheet_Context _context;
        public RegisteredCourseRepository(Timetable_DateSheet_Context context)
        {
            _context = context;
        }
        public async Task Delete(int ID)
        {
            OfferedCourses OfferedCourse = await _context.OfferedCourses.FindAsync(ID);
            _context.OfferedCourses.Remove(OfferedCourse);
        }
        public async Task RemoveRange(int OfferedCourseID)
        {
            var list = await _context.RegisteredCourses.Where(c => c.OfferedCourseID == OfferedCourseID).ToListAsync();
            _context.RegisteredCourses.RemoveRange(list);
        }
        public void RemoveRange(List<RegisteredCourses> registeredCourses)
        {
            _context.RegisteredCourses.RemoveRange(registeredCourses);
        }
        public async Task<List<RegisteredCourses>> GetByCourse(int Course)
        {
            return await _context.RegisteredCourses
                .Include(c => c.OfferedCourse.Program.Department.Institute)
                .Include(c => c.OfferedCourse.Semester)
                .Include(c => c.OfferedCourse.Department)
                .Include(c => c.OfferedCourse.FacultyMember)
                .Include(c => c.Student)
                .Where(c => c.OfferedCourseID == Course)
                .ToListAsync();
        }
        public int getCount(int Course)
        {
            return _context.RegisteredCourses
                .Where(c => c.OfferedCourseID == Course)
                .ToList().Count;
        }
        public async Task<RegisteredCourses> GetById(int ID)
        {
            return await _context.RegisteredCourses
                .Include(c => c.OfferedCourse.Program.Department.Institute)
                .Include(c => c.OfferedCourse.Semester)
                .Include(c => c.OfferedCourse.Department)
                .Include(c => c.OfferedCourse.FacultyMember)
                .Include(c => c.Student)
                .Where(c => c.RegisteredCourseID == ID)
                .FirstOrDefaultAsync();
        }
        public async Task<List<RegisteredCourses>> GetAll(string TextSearch)
        {
            return await _context.RegisteredCourses
                .Include(c => c.OfferedCourse)
                .Include(c => c.OfferedCourse.Semester)
                .Include(c => c.OfferedCourse.Program.Department.Institute)
                .Include(c => c.Student)
                .Where(c =>
                (c.OfferedCourse.OfferedCourseTitle.Trim().ToLower().Contains(TextSearch) && !string.IsNullOrEmpty(TextSearch)) || (string.IsNullOrEmpty(TextSearch))).ToListAsync();
        }
        public async Task<List<RegisteredCourses>> GetByStudent_Semester(int student, int semester)
        {
            return await _context.RegisteredCourses
                .Include(c => c.OfferedCourse)
                .Where(c => c.StudentID == student && c.OfferedCourse.SemesterID == semester)
                .ToListAsync();
        }
        public async Task<List<RegisteredCourses>> GetByStudent(int student)
        {
            return await _context.RegisteredCourses
                .Include(c => c.OfferedCourse.Program)
                .Include(c => c.OfferedCourse.Semester)
                .Where(c => c.StudentID == student)
                .ToListAsync();
        }
        public async Task Insert(RegisteredCourses Object)
        {
            await _context.RegisteredCourses.AddAsync(Object);
            await SaveChangesAsync();
        }
        public void InsertSync(RegisteredCourses Object)
        {
            _context.RegisteredCourses.Add(Object);
            SaveChanges();
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void RemoveByStudentAndCourse(int studentID, int courseID)
        {
            var Obj = _context.RegisteredCourses
                .Where(c => c.StudentID == studentID && c.OfferedCourseID == courseID)
                .ToList();
            if (Obj != null)
                _context.RemoveRange(Obj);
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
        public void Update(RegisteredCourses Object)
        {
            _context.RegisteredCourses.Update(Object);
        }
        public bool IsExistsSync(int ID)
        {
            return _context.RegisteredCourses.Any(c => c.RegisteredCourseID == ID);
        }
        public async Task<bool> IsExists(int ID)
        {
            return await _context.RegisteredCourses.AnyAsync(c => c.RegisteredCourseID == ID);
        }
        public async Task<bool> IsAnyCourseEntryExist(int OfferedCourseID)
        {
            return await _context.RegisteredCourses.AnyAsync(c => c.OfferedCourseID == OfferedCourseID);
        }
        public bool IsExists(int courseID, int studentID)
        {
            return _context.RegisteredCourses.Any(c => c.OfferedCourseID == courseID && c.StudentID == studentID);
        }
        public async Task<bool> IsExistsAsync(int courseID, int studentID)
        {
            return await _context.RegisteredCourses.AnyAsync(c => c.OfferedCourseID == courseID && c.StudentID == studentID);
        }
    }
}
