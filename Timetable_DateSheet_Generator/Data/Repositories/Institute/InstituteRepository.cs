using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timetable_DateSheet_Generator.Data.DbContext;
using Timetable_DateSheet_Generator.Models;
namespace Timetable_DateSheet_Generator.Data.Repositories.Institute
{
    public class InstituteRepository:IRepository<Institutes,int>
    {
        private readonly Timetable_DateSheet_Context _context;
        public InstituteRepository(Timetable_DateSheet_Context context)
        {            
            this._context = context;
        }
        public async Task Delete(int ID)
        {
            Institutes institute =await this. _context.Institutes.FindAsync(ID);
            this._context.Institutes.Remove(institute);
        }
        public async Task<Institutes> GetById(int ID)
        {
            return await this._context.Institutes.FindAsync(ID);
        }
        public async Task<List<Institutes>> GetAll(string TextSearch)
        {            
            return await this._context.Institutes
                .Where(c=>
                (c.InstituteName.Trim().ToLower().Contains(TextSearch) && !string.IsNullOrEmpty(TextSearch))||(string.IsNullOrEmpty(TextSearch))).ToListAsync();
        }
        public async Task Insert(Institutes Object)
        {
            await this._context.Institutes.AddAsync(Object);            
        }
        public async Task SaveChangesAsync()
        {
            await this._context.SaveChangesAsync();
        }
        public void SaveChanges()
        {
            this._context.SaveChanges();
        }
        public void Update(Institutes Object)
        {
            this._context.Institutes.Update(Object);
        }
        public async Task<bool> IsExists(int ID)
        {
            return await this._context.Institutes.AnyAsync(c=>c.InstituteID==ID);
        }
        public IEnumerable<object> GetForSelectList()
        {
            return this._context.Institutes.ToList().Select(c=>new{ ID=c.InstituteID,Name=c.InstituteName});
        }
    }
}
