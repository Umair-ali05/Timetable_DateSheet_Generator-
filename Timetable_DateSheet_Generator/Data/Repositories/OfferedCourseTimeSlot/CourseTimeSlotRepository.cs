using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timetable_DateSheet_Generator.Data.DbContext;
using Timetable_DateSheet_Generator.Models;
using Timetable_DateSheet_Generator.Models.ViewModels;

namespace Timetable_DateSheet_Generator.Data.Repositories.OfferedCourseTimeSlot
{
    public class CourseTimeSlotRepository : IRepository<OfferedCourseTimeSlots, int>
    {
        private readonly Timetable_DateSheet_Context _context;

        private readonly Random _random = new Random();
        public CourseTimeSlotRepository(Timetable_DateSheet_Context context)
        {
            _context = context;
        }
        public async Task Delete(int ID)
        {
            OfferedCourseTimeSlots ob = await _context.OfferedCourseTimeSlots.FindAsync(ID);
            _context.OfferedCourseTimeSlots.Remove(ob);
        }
        public async Task<OfferedCourseTimeSlots> GetById(int ID)
        {
            return await _context.OfferedCourseTimeSlots
                .Include(c => c.Room.Building.Institute)
                .Include(c => c.TimeSlots.Time)
                .Include(c => c.TimeSlots.TimeTable.Semester)
                .Include(c => c.TimeSlots.TimeTable.Institute)
                .Include(c => c.OfferedCourse.Program.Department.Institute)
                .Include(c => c.OfferedCourse.Department.Institute)
                .Include(c => c.OfferedCourse.FacultyMember.Department.Institute)
                .Include(c => c.OfferedCourse.Semester)
                .Where(c => c.OfferedCourseTimeSlotID == ID).FirstOrDefaultAsync();
        }
        public List<IGrouping<int, OfferedCourseTimeSlots>> GetByRoom_Day_Timetable(int Room, int Day, int Timetable)
        {
            return _context.OfferedCourseTimeSlots
                    .Include(c => c.OfferedCourse.Program)
                    .Include(c => c.Room)
                    .Include(c => c.OfferedCourse.FacultyMember)
                    .Include(c => c.TimeSlots.Time)
                    .Where(c => c.TimeSlots.Time.TimeWeekDay == Day && c.RoomID == Room && c.TimeSlots.TimeTableID == Timetable)
                    .OrderBy(c => c.TimeSlots.TimeID).GroupBy(c => c.OfferedCourseID)
                    .ToList();
        }
        public void GenerateColor(OfferedCourses offeredCourse)
        {
            if (Common.Colors.Count >= 34)
                return;
            else
            {
                int Color = _random.Next(0, 34);
                if (Common.Colors.Any(c => c.CourseID == offeredCourse.OfferedCourseID && c.Color == Color))
                    return;
                else if (Common.Colors.Any(c => c.Color == Color))
                    GenerateColor(offeredCourse);
                else
                {
                    Colors color = new Colors
                    {
                        Color = Color,
                        CourseID = offeredCourse.OfferedCourseID
                    };
                    Common.Colors.Add(color);
                }
            }
        }

        public async Task<List<TimetableDisplay>> GetByTimetable_Faculty(int timetable, int faculty)
        {
            List<TimetableDisplay> ProgramSemesters = new List<TimetableDisplay>();
            var Classes = await _context
                .OfferedCourseTimeSlots
                .Include(c => c.OfferedCourse.Program)
                .Include(c => c.OfferedCourse.FacultyMember)
                .Include(c => c.Room.Building.Institute)
                .Include(c => c.TimeSlots.Time)
                .Where(c => c.TimeSlots.TimeTableID == timetable && c.OfferedCourse.FacultyMemberID == faculty)
                .GroupBy(c => new { c.OfferedCourse.ProgramID, c.OfferedCourse.OfferedCourseSemesterNo, c.OfferedCourse.OfferedCourseSection })
                .ToListAsync();
            int i = 0;
            foreach (var Class in Classes)
            {
                TimetableDisplay ClasstimetableDisplay = new TimetableDisplay
                {
                    Description = Class.ElementAt(0).OfferedCourse.Class(),
                    Number = i
                };
                foreach (var MondayCourse in Class.Where(c => c.TimeSlots.Time.TimeWeekDay == 1).GroupBy(c => c.OfferedCourseID).ToList())
                {

                    GenerateColor(MondayCourse.ElementAt(0).OfferedCourse);
                    ClasstimetableDisplay.MondayCourseTimeSlots.Add(MondayCourse.ToList());

                }
                foreach (var TuesdayCourse in Class.Where(c => c.TimeSlots.Time.TimeWeekDay == 2).GroupBy(c => c.OfferedCourseID).ToList())
                {

                    GenerateColor(TuesdayCourse.ElementAt(0).OfferedCourse);
                    ClasstimetableDisplay.TuesdayCourseTimeSlots.Add(TuesdayCourse.ToList());

                }
                foreach (var WednesdayCourse in Class.Where(c => c.TimeSlots.Time.TimeWeekDay == 3).GroupBy(c => c.OfferedCourseID).ToList())
                {

                    GenerateColor(WednesdayCourse.ElementAt(0).OfferedCourse);
                    ClasstimetableDisplay.WednesdayCourseTimeSlots.Add(WednesdayCourse.ToList());

                }
                foreach (var ThursdayCourse in Class.Where(c => c.TimeSlots.Time.TimeWeekDay == 4).GroupBy(c => c.OfferedCourseID).ToList())
                {

                    GenerateColor(ThursdayCourse.ElementAt(0).OfferedCourse);
                    ClasstimetableDisplay.ThursdayCourseTimeSlots.Add(ThursdayCourse.ToList());

                }
                foreach (var FridayCourse in Class.Where(c => c.TimeSlots.Time.TimeWeekDay == 5).GroupBy(c => c.OfferedCourseID).ToList())
                {

                    GenerateColor(FridayCourse.ElementAt(0).OfferedCourse);
                    ClasstimetableDisplay.FridayCourseTimeSlots.Add(FridayCourse.ToList());

                }
                foreach (var SaturdayCourse in Class.Where(c => c.TimeSlots.Time.TimeWeekDay == 6).GroupBy(c => c.OfferedCourseID).ToList())
                {

                    GenerateColor(SaturdayCourse.ElementAt(0).OfferedCourse);
                    ClasstimetableDisplay.SaturdayCourseTimeSlots.Add(SaturdayCourse.ToList());

                }
                foreach (var SundayCourse in Class.Where(c => c.TimeSlots.Time.TimeWeekDay == 7).GroupBy(c => c.OfferedCourseID).ToList())
                {

                    GenerateColor(SundayCourse.ElementAt(0).OfferedCourse);
                    ClasstimetableDisplay.SundayCourseTimeSlots.Add(SundayCourse.ToList());

                }
                if (ClasstimetableDisplay.MondayCourseTimeSlots.Count > 0 || ClasstimetableDisplay.TuesdayCourseTimeSlots.Count > 0 || ClasstimetableDisplay.WednesdayCourseTimeSlots.Count > 0 || ClasstimetableDisplay.ThursdayCourseTimeSlots.Count > 0 || ClasstimetableDisplay.FridayCourseTimeSlots.Count > 0 || ClasstimetableDisplay.SaturdayCourseTimeSlots.Count > 0 || ClasstimetableDisplay.SundayCourseTimeSlots.Count > 0)
                    ProgramSemesters.Add(ClasstimetableDisplay);
                i += 1;
            }
            return ProgramSemesters;
        }

        public async Task<List<TimetableDisplay>> GetBy_Timetable(int timetable, List<RegisteredCourses> StudentCourses)
        {
            var ProgramSemesters = new List<TimetableDisplay>();
            var Classes = _context.OfferedCourseTimeSlots
                .Include(c => c.OfferedCourse.Program)
                .Include(c => c.Room.Building.Institute)
                .Include(c => c.TimeSlots.Time)
                .Include(c => c.OfferedCourse.FacultyMember)
                .Where(c => c.TimeSlots.TimeTableID == timetable)
                .GroupBy(c => new { c.OfferedCourse.ProgramID, c.OfferedCourse.OfferedCourseSemesterNo, c.OfferedCourse.OfferedCourseSection })
                .ToList();
            int i = 0;
            foreach (var Class in Classes)
            {
                TimetableDisplay ClasstimetableDisplay = new TimetableDisplay
                {
                    Description = Class.ElementAt(0).OfferedCourse.Class(),
                    Number = i
                };
                foreach (var MondayCourse in Class.Where(c => c.TimeSlots.Time.TimeWeekDay == 1).GroupBy(c => c.OfferedCourseID).ToList())
                {
                    if (StudentCourses.Any(c => c.OfferedCourseID == MondayCourse.ElementAt(0).OfferedCourseID))
                    {
                        GenerateColor(MondayCourse.ElementAt(0).OfferedCourse);
                        ClasstimetableDisplay.MondayCourseTimeSlots.Add(MondayCourse.ToList());
                    }
                }
                foreach (var TuesdayCourse in Class.Where(c => c.TimeSlots.Time.TimeWeekDay == 2).GroupBy(c => c.OfferedCourseID).ToList())
                {
                    if (StudentCourses.Any(c => c.OfferedCourseID == TuesdayCourse.ElementAt(0).OfferedCourseID))
                    {
                        GenerateColor(TuesdayCourse.ElementAt(0).OfferedCourse);
                        ClasstimetableDisplay.TuesdayCourseTimeSlots.Add(TuesdayCourse.ToList());
                    }
                }
                foreach (var WednesdayCourse in Class.Where(c => c.TimeSlots.Time.TimeWeekDay == 3).GroupBy(c => c.OfferedCourseID).ToList())
                {
                    if (StudentCourses.Any(c => c.OfferedCourseID == WednesdayCourse.ElementAt(0).OfferedCourseID))
                    {
                        GenerateColor(WednesdayCourse.ElementAt(0).OfferedCourse);
                        ClasstimetableDisplay.WednesdayCourseTimeSlots.Add(WednesdayCourse.ToList());
                    }
                }
                foreach (var ThursdayCourse in Class.Where(c => c.TimeSlots.Time.TimeWeekDay == 4).GroupBy(c => c.OfferedCourseID).ToList())
                {
                    if (StudentCourses.Any(c => c.OfferedCourseID == ThursdayCourse.ElementAt(0).OfferedCourseID))
                    {
                        GenerateColor(ThursdayCourse.ElementAt(0).OfferedCourse);
                        ClasstimetableDisplay.ThursdayCourseTimeSlots.Add(ThursdayCourse.ToList());
                    }
                }
                foreach (var FridayCourse in Class.Where(c => c.TimeSlots.Time.TimeWeekDay == 5).GroupBy(c => c.OfferedCourseID).ToList())
                {
                    if (StudentCourses.Any(c => c.OfferedCourseID == FridayCourse.ElementAt(0).OfferedCourseID))
                    {
                        GenerateColor(FridayCourse.ElementAt(0).OfferedCourse);
                        ClasstimetableDisplay.FridayCourseTimeSlots.Add(FridayCourse.ToList());
                    }
                }
                foreach (var SaturdayCourse in Class.Where(c => c.TimeSlots.Time.TimeWeekDay == 6).GroupBy(c => c.OfferedCourseID).ToList())
                {
                    if (StudentCourses.Any(c => c.OfferedCourseID == SaturdayCourse.ElementAt(0).OfferedCourseID))
                    {
                        GenerateColor(SaturdayCourse.ElementAt(0).OfferedCourse);
                        ClasstimetableDisplay.SaturdayCourseTimeSlots.Add(SaturdayCourse.ToList());
                    }
                }
                foreach (var SundayCourse in Class.Where(c => c.TimeSlots.Time.TimeWeekDay == 7).GroupBy(c => c.OfferedCourseID).ToList())
                {
                    if (StudentCourses.Any(c => c.OfferedCourseID == SundayCourse.ElementAt(0).OfferedCourseID))
                    {
                        GenerateColor(SundayCourse.ElementAt(0).OfferedCourse);
                        ClasstimetableDisplay.SundayCourseTimeSlots.Add(SundayCourse.ToList());
                    }
                }
                if (ClasstimetableDisplay.MondayCourseTimeSlots.Count > 0 || ClasstimetableDisplay.TuesdayCourseTimeSlots.Count > 0 || ClasstimetableDisplay.WednesdayCourseTimeSlots.Count > 0 || ClasstimetableDisplay.ThursdayCourseTimeSlots.Count > 0 || ClasstimetableDisplay.FridayCourseTimeSlots.Count > 0 || ClasstimetableDisplay.SaturdayCourseTimeSlots.Count > 0 || ClasstimetableDisplay.SundayCourseTimeSlots.Count > 0)
                    ProgramSemesters.Add(ClasstimetableDisplay);
                i += 1;
            }
            return ProgramSemesters;
        }
        public async Task<List<TimetableDisplay>> GetByProgram_Timetable(int Program, int Timetable)
        {
            List<TimetableDisplay> ProgramSemesters = new List<TimetableDisplay>();
            var Classes = await
                _context.OfferedCourseTimeSlots
                     .Include(c => c.OfferedCourse.Program)
                     .Include(c => c.TimeSlots.Time)
                     .Include(c => c.OfferedCourse.FacultyMember)
                     .Include(c => c.Room.Building.Institute)
                     .Include(c => c.TimeSlots.Time)
                     .Where(c => c.OfferedCourse.ProgramID == Program && c.TimeSlots.TimeTableID == Timetable)
                     .GroupBy(c => new { c.OfferedCourse.ProgramID, c.OfferedCourse.OfferedCourseSemesterNo, c.OfferedCourse.OfferedCourseSection, })
                     .ToListAsync();
            int i = 0;
            foreach (var Class in Classes)
            {
                TimetableDisplay ClasstimetableDisplay = new TimetableDisplay
                {
                    Description = Class.ElementAt(0).OfferedCourse.Class(),
                    Number = i
                };
                foreach (var MondayCourse in Class.Where(c => c.TimeSlots.Time.TimeWeekDay == 1).GroupBy(c => c.OfferedCourseID).ToList())
                {
                    GenerateColor(MondayCourse.ElementAt(0).OfferedCourse);
                    ClasstimetableDisplay.MondayCourseTimeSlots.Add(MondayCourse.ToList());
                }
                foreach (var TuesdayCourse in Class.Where(c => c.TimeSlots.Time.TimeWeekDay == 2).GroupBy(c => c.OfferedCourseID).ToList())
                {
                    GenerateColor(TuesdayCourse.ElementAt(0).OfferedCourse);
                    ClasstimetableDisplay.TuesdayCourseTimeSlots.Add(TuesdayCourse.ToList());
                }
                foreach (var WednesdayCourse in Class.Where(c => c.TimeSlots.Time.TimeWeekDay == 3).GroupBy(c => c.OfferedCourseID).ToList())
                {
                    GenerateColor(WednesdayCourse.ElementAt(0).OfferedCourse);
                    ClasstimetableDisplay.WednesdayCourseTimeSlots.Add(WednesdayCourse.ToList());
                }
                foreach (var ThursdayCourse in Class.Where(c => c.TimeSlots.Time.TimeWeekDay == 4).GroupBy(c => c.OfferedCourseID).ToList())
                {
                    GenerateColor(ThursdayCourse.ElementAt(0).OfferedCourse);
                    ClasstimetableDisplay.ThursdayCourseTimeSlots.Add(ThursdayCourse.ToList());
                }
                foreach (var FridayCourse in Class.Where(c => c.TimeSlots.Time.TimeWeekDay == 5).GroupBy(c => c.OfferedCourseID).ToList())
                {
                    GenerateColor(FridayCourse.ElementAt(0).OfferedCourse);
                    ClasstimetableDisplay.FridayCourseTimeSlots.Add(FridayCourse.ToList());
                }
                foreach (var SaturdayCourse in Class.Where(c => c.TimeSlots.Time.TimeWeekDay == 6).GroupBy(c => c.OfferedCourseID).ToList())
                {
                    GenerateColor(SaturdayCourse.ElementAt(0).OfferedCourse);
                    ClasstimetableDisplay.SaturdayCourseTimeSlots.Add(SaturdayCourse.ToList());
                }
                foreach (var SundayCourse in Class.Where(c => c.TimeSlots.Time.TimeWeekDay == 7).GroupBy(c => c.OfferedCourseID).ToList())
                {
                    GenerateColor(SundayCourse.ElementAt(0).OfferedCourse);
                    ClasstimetableDisplay.SundayCourseTimeSlots.Add(SundayCourse.ToList());
                }
                ProgramSemesters.Add(ClasstimetableDisplay);
                i += 1;
            }
            return ProgramSemesters;
        }
        public async Task<List<OfferedCourseTimeSlots>> GetAll(string TextSearch)
        {
            return await _context.OfferedCourseTimeSlots
                .Include(c => c.Room.Building.Institute)
                .Include(c => c.TimeSlots.Time)
                .Include(c => c.TimeSlots.TimeTable.Semester)
                .Include(c => c.TimeSlots.TimeTable.Institute)
                .Include(c => c.OfferedCourse.Program.Department.Institute)
                .Include(c => c.OfferedCourse.Department.Institute)
                .Include(c => c.OfferedCourse.FacultyMember.Department.Institute)
                .Include(c => c.OfferedCourse.Semester)
                .ToListAsync();
        }
        public int GetCountByTimetable_Course(int Timetable, int OfferedCourse)
        {
            return _context.OfferedCourseTimeSlots
                .Include(c => c.TimeSlots)
                .Where(c => c.TimeSlots.TimeTableID == Timetable && c.OfferedCourseID == OfferedCourse)
                .ToList()
                .Count;
        }
        public async Task<List<OfferedCourseTimeSlots>> GetByRoom(int ID)
        {
            return await _context
                .OfferedCourseTimeSlots
                .Where(c => c.RoomID == ID)
                .ToListAsync();
        }
        public async Task<List<OfferedCourseTimeSlots>> GetByTimeSlot(int ID)
        {
            return await _context
                .OfferedCourseTimeSlots
                .Where(c => c.TimeSlotID == ID)
                .ToListAsync();
        }
        public async Task Insert(OfferedCourseTimeSlots Object)
        {
            await _context.OfferedCourseTimeSlots.AddAsync(Object);
        }
        public void InsertSync(OfferedCourseTimeSlots Object)
        {
            _context.OfferedCourseTimeSlots.Add(Object);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
        public void Update(OfferedCourseTimeSlots Object)
        {
            _context.OfferedCourseTimeSlots.Update(Object);
        }
        public void RemoveRange(int TimetableID)
        {
            var list = _context.OfferedCourseTimeSlots
                .Include(c => c.TimeSlots.TimeTable)
                .Where(c => c.TimeSlots.TimeTable.TimeTableID == TimetableID)
                .ToList();
            _context.OfferedCourseTimeSlots.RemoveRange(list);
        }
        public void RemoveRange(List<OfferedCourseTimeSlots> offeredCourseTimeSlots)
        {
            _context.OfferedCourseTimeSlots.RemoveRange(offeredCourseTimeSlots);
        }
        public async Task<bool> IsExists(int ID)
        {
            return await _context.OfferedCourseTimeSlots.AnyAsync(c => c.OfferedCourseTimeSlotID == ID);
        }
        public bool IsExists_Timetable(int TimetableID)
        {
            return _context.OfferedCourseTimeSlots
                .Include(c => c.TimeSlots.TimeTable)
                .Any(c => c.TimeSlots.TimeTable.TimeTableID == TimetableID);
        }
    }
}
