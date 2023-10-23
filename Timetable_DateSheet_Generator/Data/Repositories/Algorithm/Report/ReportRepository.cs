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

namespace Timetable_DateSheet_Generator.Data.Repositories.Algorithm.Report
{
    public class ReportRepository
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

        public ReportRepository(Timetable_DateSheet_Context timetable_DateSheet_Context)
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
        public bool CheckProgramRegularTimings(int TimeID, int ProgramID)
        {
            return programRegularTimingRepository.IsExistsSync(ProgramID, TimeID);
        }
        public bool CheckProgramSpecialTimings(int TimeID, int ProgramID)
        {
            return programSpeicalTimingRepository.IsExistsSync(ProgramID, TimeID);
        }
        public bool CheckRoom(int TimeID, int RoomID)
        {
            return roomAvailibilityRepository.IsExistsSync(RoomID, TimeID);
        }
        public bool CheckFaculty(int TimeID, int FacultyID)
        {
            return facultyMemberAvailibilityRepository.IsExistsSync(FacultyID, TimeID);
        }
    }
}

