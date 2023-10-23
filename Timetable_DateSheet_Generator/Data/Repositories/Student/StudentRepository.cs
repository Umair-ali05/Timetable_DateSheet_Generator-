using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timetable_DateSheet_Generator.Data.DbContext;
using Timetable_DateSheet_Generator.Models;
using Timetable_DateSheet_Generator.Models.ViewModels;

namespace Timetable_DateSheet_Generator.Data.Repositories.Student
{
    public class StudentRepository : IRepository<Students, int>
    {
        private readonly Timetable_DateSheet_Context _context;
        public StudentRepository(Timetable_DateSheet_Context context)
        {
            _context = context;
        }
        public async Task Delete(int ID)
        {
            Students student = await _context.Students.FindAsync(ID);
            _context.Students.Remove(student);
        }
        public async Task<Students> GetById(int ID)
        {
            return await _context.Students
                .Include(c => c.Program.Department.Institute)
                .Where(c => c.StudentID == ID).FirstOrDefaultAsync();
        }
        public async Task<Students> GetByUserId(string ID)
        {
            return await _context.Students
                .Include(c => c.Program.Department.Institute)
                .Where(c => c.UserID == ID).FirstOrDefaultAsync();
        }
        public async Task<List<StudentViewModel>> GetAllView(int? Institute)
        {
            List<StudentViewModel> students = new List<StudentViewModel>();
            var studentList = await _context.Students
                .Include(c => c.Program.Department.Institute)
                .Where(c=>((Institute.HasValue && c.Program.Department.InstituteID==Institute.Value) || (!Institute.HasValue)))
                .ToListAsync();
            foreach (var student in studentList)
                students.Add(new StudentViewModel() { Student = student, RegisterViewModel = get(student.UserID) });
            return students;
        }
        public RegisterViewModel get(string id)
        {
            var user = _context.Users.Where(c => c.Id == id).FirstOrDefault();
            if (user != null)
                return new RegisterViewModel() { Name = user.Name, UserEmail = user.Email,path=user.Image };
            return new RegisterViewModel();
        }
        public async Task<List<Students>> GetAll(string textSearch)
        {
            return await _context.Students
                .Where(c=>((!string.IsNullOrEmpty(textSearch) && c.StudentEnrollment.ToLower().Contains(textSearch)) ||string.IsNullOrEmpty(textSearch)))
                .Include(c => c.Program.Department.Institute)                
                .ToListAsync();
        }
        public async Task Insert(Students student)
        {
            await _context.Students.AddAsync(student);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
        public void Update(Students student)
        {
            _context.Students.Update(student);
        }
        public async Task<bool> IsExists(int ID)
        {
            return await _context.Students.AnyAsync(c => c.StudentID == ID);
        }
        public IEnumerable<object> GetForSelectList(int? Program)
        {
            return this._context.Students
                .Where(c => (Program.HasValue && c.ProgarmID == Program.Value) || (!Program.HasValue))
                .Select(c => new { ID = c.StudentID, Name = getName(c.UserID) })
                .ToList();
        }
        public string getName(string id)
        {
            var user = this._context.Users.Where(c => c.Id == id).FirstOrDefault();
            if (user != null)
                return user.Name;
            return "";
        }
    }
}
