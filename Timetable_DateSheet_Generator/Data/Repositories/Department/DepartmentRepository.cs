using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timetable_DateSheet_Generator.Data.DbContext;
using Timetable_DateSheet_Generator.Models;

namespace Timetable_DateSheet_Generator.Data.Repositories.Department
{
    public class DepartmentRepository : IRepository<Departments, int>
    {
        private readonly Timetable_DateSheet_Context _context;
        public DepartmentRepository(Timetable_DateSheet_Context context)
        {
            _context = context;
        }
        public async Task Delete(int ID)
        {
            Departments Department = await _context.Departments.FindAsync(ID);
            _context.Departments.Remove(Department);
        }
        public async Task<List<Departments>> GetByInstitute(int InstituteID)
        {
            return await _context.Departments.Include(c => c.Institute).Where(c => c.InstituteID == InstituteID).ToListAsync();
        }
        public async Task<Departments> GetById(int ID)
        {
            return await _context.Departments.Include(c => c.Institute).Where(c => c.DepartmentID == ID).FirstOrDefaultAsync();
        }
        public async Task<List<Departments>> GetAll(string TextSearch)
        {
            return await _context.Departments.Include(c => c.Institute)
                .Where(c =>
                (c.DepartmentName.Trim().ToLower().Contains(TextSearch) && !string.IsNullOrEmpty(TextSearch)) || (string.IsNullOrEmpty(TextSearch))).ToListAsync();
        }
        public async Task Insert(Departments Object)
        {
            await _context.Departments.AddAsync(Object);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
        public void Update(Departments Object)
        {
            _context.Departments.Update(Object);
        }
        public async Task<bool> IsExists(int ID)
        {
            return await _context.Departments.AnyAsync(c => c.DepartmentID == ID);
        }
        public IEnumerable<object> GetForSelectList(int? Institute)
        {
            return _context.Departments
                .Where(c => (Institute.HasValue && c.InstituteID == Institute.Value) || !Institute.HasValue)
                .Select(c => new { ID = c.DepartmentID, Name = c.DepartmentName })
                .ToList();
        }
    }
}
