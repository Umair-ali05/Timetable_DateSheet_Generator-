using System.Collections.Generic;
using Timetable_DateSheet_Generator.Data.DbContext;
using Timetable_DateSheet_Generator.Data.Repositories.FacultyMember;
using Timetable_DateSheet_Generator.Data.Repositories.FacultyMemberAvailibility;
using Timetable_DateSheet_Generator.Data.Repositories.OfferedCourse;
using Timetable_DateSheet_Generator.Data.Repositories.OfferedCourseTimeSlot;
using Timetable_DateSheet_Generator.Data.Repositories.ProgramRegularTiming;
using Timetable_DateSheet_Generator.Data.Repositories.ProgramSpecialTiming;
using Timetable_DateSheet_Generator.Data.Repositories.RegisteredCourse;
using Timetable_DateSheet_Generator.Data.Repositories.Room;
using Timetable_DateSheet_Generator.Data.Repositories.RoomAvailibility;
using Timetable_DateSheet_Generator.Data.Repositories.TimeTableTimeSlot;
using Timetable_DateSheet_Generator.Models;

namespace Timetable_DateSheet_Generator.Data.Repositories.Algorithm
{
    public class AlgorithmTimeTableRepository
    {
        private readonly Timetable_DateSheet_Context _context;

        private readonly FacultyMemberRepository facultyMemberRepository;
        private readonly FacultyMemberAvailibilityRepository facultyMemberAvailibilityRepository;
        private readonly TimeTableTimings timeTableTimings;
        private readonly OfferedCourseRepository offeredCourseRepository;
        private readonly RegisteredCourseRepository registeredCourseRepository;
        private readonly RoomRepository roomRepository;
        private readonly RoomAvailibilityRepository roomAvailibilityRepository;
        private readonly ProgramRegularTimingRepository programRegularTimingRepository;
        private readonly ProgramSpecialTimingRepository programSpeicalTimingRepository;
        private readonly CourseTimeSlotRepository courseTimeSlotRepository;

        public AlgorithmTimeTableRepository(Timetable_DateSheet_Context timetable_DateSheet_Context)
        {
            _context = timetable_DateSheet_Context;
            facultyMemberRepository = new FacultyMemberRepository(timetable_DateSheet_Context);
            facultyMemberAvailibilityRepository = new FacultyMemberAvailibilityRepository(timetable_DateSheet_Context);
            timeTableTimings = new TimeTableTimings(timetable_DateSheet_Context);
            offeredCourseRepository = new OfferedCourseRepository(timetable_DateSheet_Context);
            registeredCourseRepository = new RegisteredCourseRepository(timetable_DateSheet_Context);
            roomRepository = new RoomRepository(timetable_DateSheet_Context);
            roomAvailibilityRepository = new RoomAvailibilityRepository(timetable_DateSheet_Context);
            programRegularTimingRepository = new ProgramRegularTimingRepository(timetable_DateSheet_Context);
            programSpeicalTimingRepository = new ProgramSpecialTimingRepository(timetable_DateSheet_Context);
            courseTimeSlotRepository = new CourseTimeSlotRepository(timetable_DateSheet_Context);
        }
        public List<FacultyMembers> getbyInstitute(int Institute)
        {
            return facultyMemberRepository.getByInstitute(Institute);
        }
        public List<FacultyMemberAvailabilities> getByFaculty_Day(int faculty, int Day)
        {
            return facultyMemberAvailibilityRepository.getByFaculty_Day(faculty, Day);
        }
        public List<TimeSlots> getByTimeTable(int timetable)
        {
            return timeTableTimings.GetByTimetableSync(timetable);
        }
        public TimeSlots getTimeSlotsByTime_Timetable(int timetable, int Time)
        {
            return timeTableTimings.getByTimeTable_Time(timetable, Time);
        }
        public List<OfferedCourses> GetByInstitute_Semester(int Institute, int Semester)
        {
            return offeredCourseRepository.GetByInstitute_Semester(Institute, Semester);
        }
        public int getRegisteredCoursesCount(int Course)
        {
            return registeredCourseRepository.getCount(Course);
        }
        public List<Rooms> GetRoomByInstitute_Type(int Institute, int type)
        {
            return roomRepository.GetByInstitute_Type(Institute, type);
        }
        public Rooms GetRoom(int Room)
        {
            return roomRepository.GetByIdSync(Room);
        }
        public List<RoomAvailibilities> getByRoomDepartment_Day(int Department, int Day)
        {
            return roomAvailibilityRepository.getByDepartment_Day(Department, Day);
        }
        public List<ProgramRegularTimings> getProgramRTimingsBy_Program(int program)
        {
            return programRegularTimingRepository.GetByProgramSync(program);
        }
        public List<ProgramSpecialTimings> getProgramSTimingsBy_Program(int program)
        {
            return programSpeicalTimingRepository.GetByProgramSync(program);
        }
        public List<ProgramRegularTimings> getProgramRTimingsBy_Program_Day(int program, int Day)
        {
            return programRegularTimingRepository.GetByProgram_DaySync(program, Day);
        }
        public List<ProgramSpecialTimings> getProgramSTimingsBy_Program_Day(int program, int Day)
        {
            return programSpeicalTimingRepository.GetByProgram_DaySync(program, Day);
        }
        public List<RoomAvailibilities> getByRoom_Day(int Room, int Day)
        {
            return roomAvailibilityRepository.getByRoom_Day(Room, Day);
        }
        public List<TimeSlots> getByTimeTable_Day(int TimeTable, int Day)
        {
            return timeTableTimings.getByTimeTable_Day(TimeTable, Day);
        }
        public OfferedCourses GetCourse(int Course)
        {
            return offeredCourseRepository.GetByIdSync(Course);
        }
        public bool IsExistsDay_TimeTableTimings(int Day)
        {
            return timeTableTimings.IsExistsDay(Day);
        }
        public List<FacultyMemberAvailabilities> getBy_Faculty_Day(int faculty, int Day)
        {
            return facultyMemberAvailibilityRepository.getBy_Faculty_Day(faculty, Day);
        }
        public int GetCountByTimetable_Course(int Timetable, int OfferedCourse)
        {
            return courseTimeSlotRepository.GetCountByTimetable_Course(Timetable, OfferedCourse);
        }
        public void Insert_CourseSlots(OfferedCourseTimeSlots offeredCourseTimeSlots)
        {
            courseTimeSlotRepository.InsertSync(offeredCourseTimeSlots);
            courseTimeSlotRepository.SaveChanges();
        }
        public bool IsExists_Timetable(int TimetableID)
        {
            return courseTimeSlotRepository.IsExists_Timetable(TimetableID);
        }
        public void RemoveRange_CourseTimeSlots(int TimetableID)
        {
            courseTimeSlotRepository.RemoveRange(TimetableID);
            courseTimeSlotRepository.SaveChanges();
        }
    }
}
