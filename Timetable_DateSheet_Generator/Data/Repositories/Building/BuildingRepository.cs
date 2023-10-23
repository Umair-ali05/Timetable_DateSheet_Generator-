using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timetable_DateSheet_Generator.Data.DbContext;
using Timetable_DateSheet_Generator.Models;

namespace Timetable_DateSheet_Generator.Data.Repositories.Building
{
    public class BuildingRepository:IRepository<Buildings,int>
    {
        private readonly Timetable_DateSheet_Context _context;
        public BuildingRepository(Timetable_DateSheet_Context context)
        {
            this._context = context;
        }
        public async Task Delete(int ID)
        {
            Buildings Building = await this._context.Buildings.FindAsync(ID);
            this._context.Buildings.Remove(Building);
        }
        public async Task<Buildings> GetById(int ID)
        {
            return await this._context.Buildings.Include(c=>c.Institute).Where(c=>c.BuildingID==ID).FirstOrDefaultAsync();            
        }
        public async Task<List<Buildings>> GetAll(string TextSearch)
        {
            return await this._context.Buildings.Include(c=>c.Institute)
                .Where(c =>
                (c.BuildingName.ToString().Trim().ToLower().Contains(TextSearch) && !string.IsNullOrEmpty(TextSearch)) || (string.IsNullOrEmpty(TextSearch))).ToListAsync();
        }
        public async Task Insert(Buildings Object)
        {
            await this._context.Buildings.AddAsync(Object);
        }
        public async Task SaveChangesAsync()
        {
            await this._context.SaveChangesAsync();
        }
        public void SaveChanges()
        {
            this._context.SaveChanges();
        }
        public void Update(Buildings Object)
        {
            this._context.Buildings.Update(Object);
        }
        public async Task<bool> IsExists(int ID)
        {
            return await this._context.Buildings.AnyAsync(c => c.BuildingID == ID);
        }
        public IEnumerable<object> GetForSelectList(int? Institute)
        {
            return this._context.Buildings.ToList()
                .Where(c=>((Institute.HasValue && c.InstituteID==Institute.Value) ||!Institute.HasValue))
                .Select(c => new { ID = c.BuildingID, Name = c.BuildingName });
        }
    }
}
