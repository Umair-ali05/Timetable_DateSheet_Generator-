using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timetable_DateSheet_Generator.Data.DbContext;
using Timetable_DateSheet_Generator.Models;

namespace Timetable_DateSheet_Generator.Data.Repositories.Semester
{
    public class SemesterRepository : IRepository<Semesters, int>
    {
        private readonly Timetable_DateSheet_Context _context;
        public SemesterRepository(Timetable_DateSheet_Context context)
        {
            _context = context;
        }
        public async Task Delete(int ID)
        {
            Semesters Semester = await _context.Semesters.FindAsync(ID);
            _context.Semesters.Remove(Semester);
        }
        public async Task<Semesters> GetById(int ID)
        {
            return await _context.Semesters.FindAsync(ID);
        }
        public async Task<List<Semesters>> GetAll(string TextSearch)
        {
            return await _context.Semesters
                .Where(c =>
                (c.SemesterType.ToString().Trim().ToLower().Contains(TextSearch) && !string.IsNullOrEmpty(TextSearch)) || (string.IsNullOrEmpty(TextSearch))).ToListAsync();
        }
        public IEnumerable<object> GetForSelectList()
        {
            return _context.Semesters
                .ToList()
                .Select(c => new { ID = c.SemesterID, Name = c.getSemesterType + " - " + c.SemesterYear.ToString() });
        }
        public async Task Insert(Semesters Object)
        {
            await _context.Semesters.AddAsync(Object);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
        public void Update(Semesters Object)
        {
            _context.Semesters.Update(Object);
        }
        public async Task<bool> IsExists(int ID)
        {
            return await _context.Semesters.AnyAsync(c => c.SemesterID == ID);
        }
        public async Task<bool> IsExists(int semesterType, int SemesterYear, int? ID)
        {     
            return await _context.Semesters
                .AnyAsync(c => c.SemesterType == semesterType && c.SemesterYear == SemesterYear && ((ID.HasValue && c.SemesterID!=ID.Value)|| !ID.HasValue));
        }
    }
}
