using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timetable_DateSheet_Generator.Data.DbContext;
using Timetable_DateSheet_Generator.Models;

namespace Timetable_DateSheet_Generator.Data.Repositories.OfferedCourse
{
    public class OfferedCourseRepository : IRepository<OfferedCourses, int>
    {
        private readonly Timetable_DateSheet_Context _context;
        public OfferedCourseRepository(Timetable_DateSheet_Context context)
        {
            _context = context;
        }
        public async Task Delete(int ID)
        {
            OfferedCourses OfferedCourse = await _context.OfferedCourses.FindAsync(ID);
            _context.OfferedCourses.Remove(OfferedCourse);
        }
        public async Task<OfferedCourses> GetById(int ID)
        {
            return await _context.OfferedCourses
                .Include(c => c.Program.Department.Institute)
                .Include(c => c.Semester)
                .Include(c => c.Department)
                .Include(c => c.FacultyMember)
                .Where(c => c.OfferedCourseID == ID)
                .FirstOrDefaultAsync();
        }
        public void RemoveRange(List<OfferedCourses> offeredCourses)
        {
            _context.RemoveRange(offeredCourses);
        }
        public async Task<List<OfferedCourses>> GetByProgramId(int ID)
        {
            return await _context.OfferedCourses
                .Where(c => c.ProgramID == ID)
                .ToListAsync();
        }
        public async Task<List<OfferedCourses>> GetByFacultyId(int ID)
        {
            return await _context.OfferedCourses
                .Where(c => c.FacultyMemberID == ID)
                .ToListAsync();
        }

        public OfferedCourses GetByIdSync(int ID)
        {
            return
                _context.OfferedCourses
                .Include(c => c.FacultyMember.Department.Institute)
                .Include(c => c.Department.Institute)
                .Include(c => c.Program.Department.Institute)
                .Include(c => c.Semester)
                .FirstOrDefault(c => c.OfferedCourseID == ID);
        }
        public async Task<List<OfferedCourses>> GetAll(string TextSearch)
        {
            return await _context.OfferedCourses
                .Include(c => c.Semester)
                .Include(c => c.Program.Department.Institute)
                .Where(c =>
                (c.OfferedCourseTitle.Trim().ToLower().Contains(TextSearch) && !string.IsNullOrEmpty(TextSearch)) || (string.IsNullOrEmpty(TextSearch))).ToListAsync();
        }
        public int GetLabCoursesCount(int programID, int InstituteID, int SemesterID)
        {
            return _context.OfferedCourses
                .Include(c => c.Department)
                .Where(c => c.OfferedCourseCategory == 4 && c.SemesterID == SemesterID && c.Department.InstituteID == InstituteID && c.ProgramID == programID)
                .ToList()
                .Count;
        }
        public int GetTheoryCoursesCount(int programID, int InstituteID, int SemesterID)
        {
            return _context.OfferedCourses
                .Include(c => c.Department)
                .Where(c => c.OfferedCourseCategory != 4 && c.SemesterID == SemesterID && c.Department.InstituteID == InstituteID && c.ProgramID == programID)
                .ToList()
                .Count;
        }
        public async Task<List<OfferedCourses>> GetAll(int faculty)
        {
            return await _context.OfferedCourses
                .Include(c => c.Semester)
                .Include(c => c.Program.Department.Institute)
                .Where(c => c.FacultyMemberID == faculty).ToListAsync();
        }
        public List<OfferedCourses> GetByInstitute_Semester(int Institute, int Semester)
        {
            return _context.OfferedCourses
                .Include(c => c.Program.Department.Institute)
                .Include(c => c.Department.Institute)
                .Include(c => c.Semester)
                .Include(c => c.FacultyMember.Department.Institute)
                .Where(c => c.Program.Department.InstituteID == Institute && c.SemesterID == Semester)
                .ToList();
        }
        public async Task<List<OfferedCourses>> GetByFaculty(int Faculty)
        {
            return await _context.OfferedCourses
                .Include(c => c.Program.Department.Institute)
                .Include(c => c.Department.Institute)
                .Include(c => c.Semester)
                .Include(c => c.FacultyMember.Department.Institute)
                .Where(c => c.FacultyMemberID == Faculty)
                .ToListAsync();
        }
        public async Task Insert(OfferedCourses Object)
        {
            await _context.OfferedCourses.AddAsync(Object);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
        public void Update(OfferedCourses Object)
        {
            _context.OfferedCourses.Update(Object);
        }
        public async Task<bool> IsExists(int ID)
        {
            return await _context.OfferedCourses.AnyAsync(c => c.OfferedCourseID == ID);
        }
        public IEnumerable<object> GetForSelectList(int? Institute, int? Department, int? Program)
        {
            return _context.OfferedCourses
                .Where(c =>
                ((Institute.HasValue && Institute.Value == c.Program.Department.InstituteID) || (!Institute.HasValue))
                && ((Department.HasValue && Department.Value == c.Program.DepartmentID) || (!Department.HasValue))
                && ((Program.HasValue && Program.Value == c.ProgramID) || (!Program.HasValue))
                )
                .Select(c => new { ID = c.OfferedCourseID, Name = c.OfferedCourseTitle })
                .ToList();
        }
    }
}
