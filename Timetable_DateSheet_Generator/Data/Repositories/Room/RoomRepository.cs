using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timetable_DateSheet_Generator.Data.DbContext;
using Timetable_DateSheet_Generator.Models;

namespace Timetable_DateSheet_Generator.Data.Repositories.Room
{
    public class RoomRepository : IRepository<Rooms, int>
    {
        private readonly Timetable_DateSheet_Context _context;
        public RoomRepository(Timetable_DateSheet_Context context)
        {
            _context = context;
        }
        public async Task Delete(int ID)
        {
            Rooms Room = await _context.Rooms.FindAsync(ID);
            _context.Rooms.Remove(Room);
        }
        public async Task<Rooms> GetById(int ID)
        {
            return await _context.Rooms
                .Include(c => c.Building.Institute)
                .Where(c => c.RoomID == ID).FirstOrDefaultAsync();
        }
        public Rooms GetByIdSync(int ID)
        {
            return _context.Rooms
                .Include(c => c.Building.Institute)
                .Where(c => c.RoomID == ID).FirstOrDefault();
        }
        public async Task<List<Rooms>> GetAll(string TextSearch)
        {
            return await _context.Rooms
                .Include(c => c.Building.Institute)
                .Where(c =>
                (c.RoomName.Trim().ToLower().Contains(TextSearch) && !string.IsNullOrEmpty(TextSearch)) || (string.IsNullOrEmpty(TextSearch))).ToListAsync();
        }
        public async Task Insert(Rooms Object)
        {
            await _context.Rooms.AddAsync(Object);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
        public void Update(Rooms Object)
        {
            _context.Rooms.Update(Object);
        }
        public async Task<bool> IsExists(int ID)
        {
            return await _context.Rooms.AnyAsync(c => c.RoomID == ID);
        }
        public IEnumerable<object> GetForSelectList(int? Institute, int? Building)
        {
            return _context.Rooms
                .Include(c => c.Building.Institute)
                .Where(c => ((Institute.HasValue && Institute.Value == c.Building.InstituteID) || !Institute.HasValue)
                && ((Building.HasValue && Building.Value == c.BuildingID) || !Building.HasValue)
                )
                .ToList().Select(c => new { ID = c.RoomID, Name = c.RoomName });
        }
        public IEnumerable<object> GetForSelectList_WithTimings(int? Institute, int? Building)
        {

            return (from o in _context.Rooms.Include(c => c.Building.Institute).ToList()
                    join s in _context.RoomAvailibilities.ToList() on o.RoomID equals s.RoomID
                    where (((Institute.HasValue && Institute.Value == o.Building.InstituteID) || !Institute.HasValue)
                    && ((Building.HasValue && Building.Value == o.BuildingID) || !Building.HasValue))
                    select new
                    {
                        ID = o.RoomID,
                        Name = o.RoomName
                    }).Distinct().ToList();
        }
        public List<Rooms> GetByInstitute_Type(int Institute, int type)
        {
            return _context
                .Rooms
                .Include(c => c.Building)
                .Where(c => c.Building.InstituteID == Institute && c.RoomType == type)
                .ToList();
        }
    }
}
