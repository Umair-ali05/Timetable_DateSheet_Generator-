using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timetable_DateSheet_Generator.Data.DbContext;
using Timetable_DateSheet_Generator.Models;
using Timetable_DateSheet_Generator.Models.ViewModels;

namespace Timetable_DateSheet_Generator.Data.Repositories.FacultyMember
{
    public class FacultyMemberRepository : IRepository<FacultyMembers, int>
    {
        private readonly Timetable_DateSheet_Context _context;
        public FacultyMemberRepository(Timetable_DateSheet_Context context)
        {
            _context = context;
        }
        public async Task Delete(int ID)
        {
            FacultyMembers facultyMember = await _context.FacultyMembers.FindAsync(ID);
            _context.FacultyMembers.Remove(facultyMember);
        }
        public async Task<FacultyMembers> GetById(int ID)
        {
            return await _context.FacultyMembers
                .Include(c => c.Department.Institute)
                .Where(c => c.FacultyMemberID == ID).FirstOrDefaultAsync();
        }
        public async Task<FacultyMembers> GetByUserId(string ID)
        {
            return await _context.FacultyMembers
                .Include(c => c.Department.Institute)
                .Where(c => c.UserID == ID).FirstOrDefaultAsync();
        }
        public RegisterViewModel get(string id)
        {
            var user = _context.Users.Where(c => c.Id == id).FirstOrDefault();
            if (user != null)
                return new RegisterViewModel() { Name = user.Name, UserEmail = user.Email, path = user.Image };
            return new RegisterViewModel();
        }
        public async Task<List<FacultyViewModel>> GetAllView()
        {
            List<FacultyViewModel> faculties = new List<FacultyViewModel>();
            var facultyList = await _context.FacultyMembers
                .Include(c => c.Department.Institute)
                .ToListAsync();
            foreach (var faculty in facultyList)
                faculties.Add(new FacultyViewModel() { FacultyMember = faculty, registerViewModel = get(faculty.UserID) });
            return faculties;
        }
        public async Task<List<FacultyMembers>> GetAll(string textSearch)
        {
            return await _context.FacultyMembers.ToListAsync();
        }
        public async Task Insert(FacultyMembers facultyMember)
        {
            await _context.FacultyMembers.AddAsync(facultyMember);
        }
        public void InsertSync(FacultyMembers facultyMember)
        {
            _context.FacultyMembers.Add(facultyMember);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
        public void Update(FacultyMembers facultyMember)
        {
            _context.FacultyMembers.Update(facultyMember);
        }
        public async Task<bool> IsExists(int ID)
        {
            return await _context.FacultyMembers.AnyAsync(c => c.FacultyMemberID == ID);
        }
        public IEnumerable<object> GetForSelectList(int? institute)
        {
            return (from fm in _context.FacultyMembers.Include(c => c.Department).ToList()
                    join u in _context.Users on fm.UserID equals u.Id
                    where ((institute.HasValue && fm.Department.InstituteID == institute.Value) || !institute.HasValue)
                    select new
                    {
                        ID = fm.FacultyMemberID,
                        Name = u.Name
                    }).ToList();

        }
        public IEnumerable<object> GetForSelectList(int? Institute, int? Department, bool? isTiming)
        {
            return (from fm in _context.FacultyMembers.Include(c => c.Department.Institute).ToList()
                    join u in _context.Users on fm.UserID equals u.Id
                    where (
                    ((Institute.HasValue && fm.Department.InstituteID == Institute.Value) || !Institute.HasValue)
                    && ((Department.HasValue && fm.Department.DepartmentID == Department.Value) || !Department.HasValue)
                    && ((isTiming.HasValue && isTiming.Value == true && !_context.FacultyMemberAvailabilities.Any(e => e.FacultyMemberID == fm.FacultyMemberID)) || !isTiming.HasValue || isTiming.Value == false)
                    )
                    select new
                    {
                        ID = fm.FacultyMemberID,
                        Name = u.Name
                    }).ToList();
        }
        public string getName(string id)
        {
            var user = _context.Users.Where(c => c.Id == id).FirstOrDefault();
            if (user != null)
                return user.Name;
            return "";
        }
        public List<FacultyMembers> getByInstitute(int Institute)
        {
            return _context.FacultyMembers.Include(c => c.Department.Institute).Where(c => c.Department.InstituteID == Institute).ToList();
        }
    }
}
