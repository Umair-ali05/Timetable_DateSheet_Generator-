using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timetable_DateSheet_Generator.Data.DbContext;
using Timetable_DateSheet_Generator.Data.Repositories.Room;
using Timetable_DateSheet_Generator.Models;
using Timetable_DateSheet_Generator.Models.ViewModels;

namespace Timetable_DateSheet_Generator.Data.Repositories.RoomAvailibility
{
    public struct Custom
    {
        public int NumberOfClassRooms { get; set; }
        public int NumberOfLabs { get; set; }
    }
    public class RoomAvailibilityRepository : IRepository<RoomAvailibilities, int>
    {

        private readonly Timetable_DateSheet_Context _context;
        private readonly RoomRepository roomRepository;
        public RoomAvailibilityRepository(Timetable_DateSheet_Context context)
        {
            _context = context;
            roomRepository = new RoomRepository(context);
        }
        public async Task Delete(int ID)
        {
            var Obj = await _context.RoomAvailibilities.FindAsync(ID);
            _context.RoomAvailibilities.Remove(Obj);
        }
        public async Task RemoveRange(int ID, int Department)
        {
            var list = await _context.RoomAvailibilities.Where(c => c.RoomID == ID && c.DepartmentID == Department).ToListAsync();
            _context.RoomAvailibilities.RemoveRange(list);
        }
        public void RemoveRange(List<RoomAvailibilities> roomAvailibilities)
        {
            _context.RoomAvailibilities.RemoveRange(roomAvailibilities);
        }
        public async Task<List<RoomAvailibilityViewModel>> GetUniqueList(Rooms room)
        {
            var returnList = new List<RoomAvailibilityViewModel>();
            if (await roomRepository.IsExists(room.RoomID))
            {
                foreach (var item in await _context.RoomAvailibilities
                    .Where(c => c.RoomID == room.RoomID)
                    .GroupBy(c => new { c.RoomID, c.DepartmentID })
                    .ToListAsync())
                {
                    returnList.Add(new RoomAvailibilityViewModel
                    {
                        Room = room,
                        Department = await _context.Departments.Include(c => c.Institute).Where(c => c.DepartmentID == item.Key.DepartmentID).FirstOrDefaultAsync(),
                        Count = item.Count() <= 0 ? "No Timings Available." : item.Count().ToString()
                    });
                }
            }
            return returnList;

        }
        public async Task<Custom> GetGroupedRoomsCount(int DepartmentID)
        {
            var temp = new Custom();
            var Rooms = await _context.RoomAvailibilities.Include(c => c.Room).GroupBy(c => new { c.DepartmentID, c.RoomID }).ToListAsync();
            foreach (var group in Rooms)
            {
                var room = group.ElementAt(0);
                if (room.DepartmentID == DepartmentID && room.Room.RoomType != 4)
                    temp.NumberOfClassRooms += 1;
                else if (room.DepartmentID == DepartmentID && room.Room.RoomType == 4)
                    temp.NumberOfLabs += 1;
                else
                {

                }
            }
            return temp;
            //return await _context.RoomAvailibilities
            //    .Include(c => c.Room)
            //    .GroupBy(c => new { c.DepartmentID, c.RoomID })
            //    .Select(c => new CustomRoom { DepartmentID = c.Key.DepartmentID, RoomID = c.Key.RoomID }).ToListAsync();
        }
        public async Task<List<RoomAvailibilities>> GetByRoomDepartment(int room, int Department)
        {
            return await _context.RoomAvailibilities
                .Include(c => c.Room.Building.Institute)
                .Include(c => c.Time)
                .Include(c => c.Department.Institute)
                .Where(c => c.RoomID == room && c.DepartmentID == Department)
                .ToListAsync();
        }
        public int getCount(int room, int? Department)
        {
            return _context.RoomAvailibilities
                .Where(c => c.RoomID == room
                && ((Department.HasValue && Department.Value == c.DepartmentID) || !Department.HasValue)
                )
                .ToList().Count;
        }
        public async Task<RoomAvailibilities> GetById(int ID)
        {
            return await _context.RoomAvailibilities
                .Include(c => c.Room.Building.Institute)
                .Include(c => c.Time)
                .Include(c => c.Department.Institute)
                .Where(c => c.RoomAvailibilityID == ID)
                .FirstOrDefaultAsync();
        }
        public async Task<List<RoomAvailibilities>> GetAll(string TextSearch)
        {
            return await _context.RoomAvailibilities
                .Include(c => c.Room.Building.Institute)
                .Include(c => c.Department.Institute)
                .Include(c => c.Time)
                .ToListAsync();
        }

        public async Task Insert(RoomAvailibilities Object)
        {
            await _context.RoomAvailibilities.AddAsync(Object);
            await SaveChangesAsync();
        }
        public async Task InsertRangeAsync(List<RoomAvailibilities> Object)
        {
            await _context.RoomAvailibilities.AddRangeAsync(Object);
            await SaveChangesAsync();
        }
        public void InsertRange(List<RoomAvailibilities> Object)
        {
            _context.RoomAvailibilities.AddRange(Object);
            SaveChanges();
        }
        public void InsertSync(RoomAvailibilities Object)
        {
            _context.RoomAvailibilities.Add(Object);
            SaveChanges();
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void RemoveByRoom(int roomID)
        {
            var Obj = _context.RoomAvailibilities
                .Where(c => c.RoomID == roomID)
                .ToList();
            if (Obj != null)
                _context.RoomAvailibilities.RemoveRange(Obj);
        }
        public bool checkSlots(int Day, int Hour, int roomID, int departmentID)
        {
            bool isExists = false;
            foreach (var slot in (Times.HourSubSlots[])Enum.GetValues(typeof(Times.HourSubSlots)))
            {
                var Slot = slot.GetDisplayName();
                var TimeID = Convert.ToInt32(Convert.ToString(Day) + Convert.ToString(Hour) + Convert.ToString(Slot));
                if (IsExistsSync(roomID, TimeID, departmentID))
                    return true;
                else
                    isExists = false;
            }
            return isExists;
        }
        public void GenerateAndSave(int Hour, int Day, Times.HourSubSlots startSlot, Times.HourSubSlots endSlot, int roomID, int departmentID)
        {
            string startSlt = startSlot.GetDisplayName();
            string endSlt = endSlot.GetDisplayName();
            if (Convert.ToInt16(startSlot) <= Convert.ToInt16(endSlt))
            {
                for (int i = 0; i < Enum.GetNames(typeof(Times.HourSubSlots)).Length; i++)
                {
                    var TimeID = Convert.ToInt32(Convert.ToString(Day) + Convert.ToString(Hour) + Convert.ToString(startSlt));
                    if (_context.Times.Any(c => c.TimeID == TimeID))
                    {
                        int nextSlt;
                        if (Convert.ToInt32(startSlt) != Convert.ToInt32(endSlt))
                            nextSlt = Convert.ToInt32(startSlt) + 15;
                        else
                        {
                            nextSlt = Convert.ToInt32(startSlt);
                        }
                        if (!IsExistsSync(roomID, TimeID, departmentID))
                        {

                            RoomAvailibilities roomTimings = new RoomAvailibilities
                            {
                                RoomID = roomID,
                                TimeID = TimeID,
                                DepartmentID = departmentID,
                            };
                            InsertSync(roomTimings);
                            SaveChanges();

                        }
                        startSlt = Convert.ToString(nextSlt);
                        if (startSlt.Length == 1)
                            startSlt += "0";
                    }
                }
            }
        }
        public List<RoomAvailibilities> getByDepartment_Day(int Department, int Day)
        {
            return _context.RoomAvailibilities
                .Include(c => c.Room)
                .Include(c => c.Department)
                .Include(c => c.Time)
                .Where(c => c.DepartmentID == Department && c.Time.TimeWeekDay == Day)
                .ToList();
        }
        public async Task<List<RoomAvailibilities>> getByRoom(int Room)
        {
            return await _context.RoomAvailibilities
                .Where(c => c.RoomID == Room)
                .ToListAsync();
        }
        public List<RoomAvailibilities> getByRoom_Ordered(int Room)
        {
            return _context.RoomAvailibilities
                .Include(c => c.Room.Building.Institute)
                .Include(c => c.Department.Institute)
                .Include(c => c.Time)
                .Where(c => c.RoomID == Room)
                .OrderBy(c => c.TimeID)
                .ToList();
        }
        public List<RoomAvailibilities> getByRoom_Day(int Room, int Day)
        {
            return
                 _context.RoomAvailibilities
                 .Include(c => c.Time)
                 .Where(c => c.Time.TimeWeekDay == Day && c.RoomID == Room)
                 .ToList();
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
        public void Update(RoomAvailibilities Object)
        {
            _context.RoomAvailibilities.Update(Object);
        }
        public bool IsExistsSync(int ID)
        {
            return _context.RoomAvailibilities.Any(c => c.RoomAvailibilityID == ID);
        }
        public bool IsExistsSync(int roomID, int TimeID, int DepartmentID)
        {
            return _context.RoomAvailibilities.Any(c => c.RoomID == roomID && c.TimeID == TimeID && c.DepartmentID == DepartmentID);
        }
        public bool IsExistsSync(int roomID, int TimeID)
        {
            return _context.RoomAvailibilities.Any(c => c.RoomID == roomID && c.TimeID == TimeID);
        }
        public async Task<bool> IsExists(int RoomID, int TimeID, int DepartmentID)
        {
            return await _context.RoomAvailibilities.AnyAsync(c => c.RoomID == RoomID && c.TimeID == TimeID && c.DepartmentID == DepartmentID);
        }
        public async Task<bool> IsExists(int ID)
        {
            return await _context.RoomAvailibilities.AnyAsync(c => c.RoomAvailibilityID == ID);
        }
        public async Task<bool> IsAnyRoomEntryExistAsync(int roomID, int Department)
        {
            return await _context.RoomAvailibilities.AnyAsync(c => c.RoomID == roomID && c.DepartmentID == Department);
        }
        public bool IsAnyRoomEntryExist(int roomID, int Department)
        {
            return _context.RoomAvailibilities.Any(c => c.RoomID == roomID && c.DepartmentID == Department);
        }
    }
}
