using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timetable_DateSheet_Generator.Data.DbContext;
using Timetable_DateSheet_Generator.Models;

namespace Timetable_DateSheet_Generator.Data.Repositories.Program
{
    public class ProgramRepository : IRepository<Programs, int>
    {
        private readonly Timetable_DateSheet_Context _context;
        public ProgramRepository(Timetable_DateSheet_Context context)
        {
            _context = context;
        }
        public async Task Delete(int ID)
        {
            Programs Program = await _context.Programs.FindAsync(ID);
            _context.Programs.Remove(Program);
        }
        public async Task<Programs> GetById(int ID)
        {
            return await _context.Programs.
                Include(c => c.Department.Institute).
                Where(c => c.ProgramID == ID).FirstOrDefaultAsync();
        }
        public async Task<List<Programs>> GetAll(string TextSearch)
        {
            return await _context.Programs
                .Include(c => c.Department.Institute)
                .Where(c =>
                (c.ProgramName.Trim().ToLower().Contains(TextSearch) && !string.IsNullOrEmpty(TextSearch)) || (string.IsNullOrEmpty(TextSearch))).ToListAsync();
        }
        public async Task<List<Programs>> GetProgramsByDeparment(int Department)
        {
            return await _context.Programs
                .Include(c => c.Department.Institute)
                .Where(c => c.DepartmentID == Department)
                .ToListAsync();
        }
        public async Task<List<Programs>> GetProgramsByInstitute(int Institute)
        {
            return await _context.Programs
                .Include(c => c.Department.Institute)
                .Where(c => c.Department.InstituteID == Institute)
                .ToListAsync();
        }
        public async Task Insert(Programs Object)
        {
            await _context.Programs.AddAsync(Object);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
        public void Update(Programs Object)
        {
            _context.Programs.Update(Object);
        }
        public async Task<bool> IsExists(int ID)
        {
            return await _context.Programs.AnyAsync(c => c.ProgramID == ID);
        }
        public IEnumerable<object> GetForSelectList(int? Department)
        {
            return _context.Programs
                .Where(c => (Department.HasValue && Department.Value == c.DepartmentID) || (!Department.HasValue))
                .ToList()
                .Select(c => new { ID = c.ProgramID, Name = c.ProgramName });
        }
        //public bool isAnySpecialProgramEntry(int program)
        //{
        //    return _context.ProgramSpecialTimings.Any(c => c.ProgramID == program);
        //}
        //public bool isAnyRegularProgramEntry(int program)
        //{
        //    return _context.ProgramRegularTimings.Any(c => c.ProgramID == program);
        //}
        public IEnumerable<object> GetForSelectList(int? Institute, int? Department, bool? IsSpecial)
        {
            return _context.Programs
                .Where(c =>
                ((Institute.HasValue && Institute.Value == c.Department.InstituteID) || (!Institute.HasValue))
                && ((Department.HasValue && Department.Value == c.DepartmentID) || (!Department.HasValue))
                && (((IsSpecial.HasValue &&
                (IsSpecial.Value == true && !_context.ProgramSpecialTimings.Any(e => e.ProgramID == c.ProgramID)
                || (IsSpecial.Value == false && !_context.ProgramRegularTimings.Any(e => e.ProgramID == c.ProgramID)))
                || (!IsSpecial.HasValue)))))
                .ToList()
                .Select(c => new { ID = c.ProgramID, Name = c.ProgramName });
        }
        public IEnumerable<object> GetForSelectList(int? Institute, int? Department)
        {
            return _context.Programs
                .Where(c =>
                ((Institute.HasValue && Institute.Value == c.Department.InstituteID) || (!Institute.HasValue))
                && ((Department.HasValue && Department.Value == c.DepartmentID) || (!Department.HasValue)))
                .ToList()
                .Select(c => new { ID = c.ProgramID, Name = c.ProgramName });
        }
    }
}
