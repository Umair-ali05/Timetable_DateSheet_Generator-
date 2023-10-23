using System;
using System.Collections.Generic;
using System.Linq;
using Timetable_DateSheet_Generator.Data.Algorithms.WeekStructure;
using Timetable_DateSheet_Generator.Data.Repositories.Algorithm;
using Timetable_DateSheet_Generator.Models;

namespace Timetable_DateSheet_Generator.Data.Algorithms.GeneticAlgorithmForTimetable
{
    public class Chromosome
    {
        private readonly AlgorithmTimeTableRepository _RepositoryService;

        private readonly Random _random = new Random();
        private const int slotsPerHour = 4;
        private double RequiredFitness = 75;
        private Week TimetableSlotsWeek = new Week();

        private int InstituteID;
        private int SemesterID;
        private int TimetableID;
        public double ChromosomeFitness = 0;


        private List<FacultyMembers> FacultyMembers = new List<FacultyMembers>();
        private List<OfferedCourseTimeSlots> Solution = new List<OfferedCourseTimeSlots>();
        private List<OfferedCourseTimeSlots> underUtilizedCourseSlots = new List<OfferedCourseTimeSlots>();
        private List<OfferedCourseTimeSlots> UtilizedCourseSlots = new List<OfferedCourseTimeSlots>();
        private List<int> timeTableDays = new List<int>();
        private int SlotsCount = 0;
        public bool NewGenerated = true;
        private int CourseScheduleNumber = 0;
        public void Hours()
        {

        }
        public Chromosome(AlgorithmTimeTableRepository _algorithmTimeTableRepository, int TimetableID)
        {
            _RepositoryService = _algorithmTimeTableRepository;
            this.TimetableID = TimetableID;
        }
        public Chromosome(AlgorithmTimeTableRepository _algorithmTimeTableRepository, int InstituteID, int SemesterID, int TimetableID)
        {
            this.InstituteID = InstituteID;
            this.TimetableID = TimetableID;
            this.SemesterID = SemesterID;
            _RepositoryService = _algorithmTimeTableRepository;
            FacultyMembers = _RepositoryService.getbyInstitute(this.InstituteID);
            InitializeChromosome();
            getFitness();
        }
        public bool checkProgramTimings(OfferedCourses offeredCourse, Times time, List<ProgramRegularTimings> programRegularTimings, List<ProgramSpecialTimings> programSpecialTimings, List<OfferedCourseTimeSlots> ClassTimeSlots, List<OfferedCourseTimeSlots> FacultyCoursesSlots)
        {
            if (offeredCourse.OfferedCourseSpecial == 0)
            {
                if (!checkRegularTime(time, offeredCourse, ClassTimeSlots, FacultyCoursesSlots, programRegularTimings))
                    return false;
            }
            else
            {
                if (!checkSpecialTime(time, offeredCourse, ClassTimeSlots, FacultyCoursesSlots, programSpecialTimings))
                    return false;
            }
            return true;
        }
        public bool checkCourse(OfferedCourses offeredCourse, Times time)
        {
            if (Solution.Any(c => c.OfferedCourse.OfferedCourseID == offeredCourse.OfferedCourseID && c.TimeSlots.Time.TimeWeekDay == time.TimeWeekDay && c.TimeSlots.TimeTableID == TimetableID))
                return false;
            if (Solution.Any(c => c.OfferedCourse.OfferedCourseID == offeredCourse.OfferedCourseID && c.TimeSlots.TimeID == time.TimeID))
                return false;
            return true;
        }
        public Day getConsectiveSlots(List<TimeSlots> timeSlots, int Day, Day day, double requiredSlots, OfferedCourses offeredCourse, bool FacultyAvailibilityCheck, List<ProgramRegularTimings> programRegularTimings, List<ProgramSpecialTimings> programSpecialTimings, List<OfferedCourseTimeSlots> ClassTimeSlots, List<OfferedCourseTimeSlots> FacultyCoursesSlots)
        {
            var timetableTimings = timeSlots.Where(c => c.Time.TimeWeekDay == Day).ToList();
            var FacultyTimings = new List<FacultyMemberAvailabilities>();
            if (FacultyAvailibilityCheck)
                FacultyTimings = _RepositoryService.getByFaculty_Day(offeredCourse.FacultyMemberID, Day);
            foreach (var slot in timetableTimings)
            {
                if (checkCourse(offeredCourse, slot.Time) && checkProgramTimings(offeredCourse, slot.Time, programRegularTimings, programSpecialTimings, ClassTimeSlots, FacultyCoursesSlots) && checkDays(offeredCourse, slot.Time.TimeWeekDay))
                {
                    if (FacultyAvailibilityCheck)
                    {
                        if (FacultyTimings.Any(c => c.TimeID == slot.TimeID))
                            continue;
                    }
                    if (day.ConsectiveContactHoursSlots.Count == 0)
                    {
                        Hour hour = new Hour();
                        Times times = new Times();
                        hour.Times.Add(slot.Time);
                        day.ConsectiveContactHoursSlots.Add(hour);
                        continue;
                    }
                    var lastHour = day.ConsectiveContactHoursSlots.ElementAt(day.ConsectiveContactHoursSlots.Count - 1);
                    if (lastHour.Times.Count < requiredSlots)
                    {
                        var lastTimeSlot = lastHour.Times.ElementAt(lastHour.Times.Count - 1);
                        var FirstTwoDigits = lastTimeSlot.FinishTime.ToString();
                        var lastTwoDigits = lastTimeSlot.FinishTime.ToString();
                        var Hour = "";
                        if (FirstTwoDigits.Length > 2)
                        {
                            Hour = FirstTwoDigits.Remove(FirstTwoDigits.Length - 2);
                            lastTwoDigits = lastTwoDigits.Substring(lastTwoDigits.Length - 2);
                        }
                        else
                        {
                            Hour = "0";
                            if (FirstTwoDigits.Length == 2)
                                lastTwoDigits = FirstTwoDigits;
                            else
                                lastTwoDigits = "0";
                        }
                        if ((Convert.ToInt16(lastTwoDigits)) % 60 == 0)
                        {
                            Times times = new Times
                            {
                                TimeWeekDay = Day,
                                StartTime = Convert.ToInt16((Convert.ToInt16(Hour) + 1).ToString() + "00")
                            };
                            times.FinishTime = times.StartTime + 15;
                            times.TimeID = Convert.ToInt32(Day.ToString() + times.StartTime.ToString());
                            if (times.StartTime == slot.Time.StartTime)
                                lastHour.Times.Add(times);
                            else
                            {
                                Hour hour = new Hour();
                                hour.Times.Add(slot.Time);
                                day.ConsectiveContactHoursSlots.Add(hour);
                            }

                        }
                        else
                        {
                            Times times = new Times
                            {
                                TimeWeekDay = Day,
                                StartTime = Convert.ToInt16((Convert.ToInt16(Hour)).ToString() + lastTwoDigits)
                            };
                            times.FinishTime = times.StartTime + 15;
                            times.TimeID = Convert.ToInt32(Day.ToString() + Hour + lastTwoDigits);
                            if (times.StartTime == slot.Time.StartTime)
                                lastHour.Times.Add(times);
                            else
                            {
                                Hour hour = new Hour();
                                hour.Times.Add(slot.Time);
                                day.ConsectiveContactHoursSlots.Add(hour);
                            }
                        }
                    }
                    else
                    {
                        Hour hour = new Hour();
                        hour.Times.Add(slot.Time);
                        day.ConsectiveContactHoursSlots.Add(hour);
                    }
                }
            }
            Day validSlots = new Day();
            foreach (var Hour in day.ConsectiveContactHoursSlots)
                if (Hour.Times.Count >= requiredSlots)
                    validSlots.ConsectiveContactHoursSlots.Add(Hour);
            return validSlots;
        }
        public void setTimes(double requiredSlots, OfferedCourses offeredCourse, bool FacultyAvailibilityCheck, List<ProgramRegularTimings> programRegularTimings, List<ProgramSpecialTimings> programSpecialTimings, List<OfferedCourseTimeSlots> ClassTimeSlots, List<OfferedCourseTimeSlots> FacultyCoursesSlots)
        {
            var TimeTableTimings = _RepositoryService.getByTimeTable(TimetableID);
            TimeTableTimings = TimeTableTimings.OrderBy(c => c.Time.TimeID).ToList();
            TimetableSlotsWeek.Monday = getConsectiveSlots(TimeTableTimings, 1, TimetableSlotsWeek.Monday, requiredSlots, offeredCourse, FacultyAvailibilityCheck, programRegularTimings, programSpecialTimings, ClassTimeSlots, FacultyCoursesSlots);
            TimetableSlotsWeek.Tuesday = getConsectiveSlots(TimeTableTimings, 2, TimetableSlotsWeek.Tuesday, requiredSlots, offeredCourse, FacultyAvailibilityCheck, programRegularTimings, programSpecialTimings, ClassTimeSlots, FacultyCoursesSlots);
            TimetableSlotsWeek.Wednesday = getConsectiveSlots(TimeTableTimings, 3, TimetableSlotsWeek.Wednesday, requiredSlots, offeredCourse, FacultyAvailibilityCheck, programRegularTimings, programSpecialTimings, ClassTimeSlots, FacultyCoursesSlots);
            TimetableSlotsWeek.Thursday = getConsectiveSlots(TimeTableTimings, 4, TimetableSlotsWeek.Thursday, requiredSlots, offeredCourse, FacultyAvailibilityCheck, programRegularTimings, programSpecialTimings, ClassTimeSlots, FacultyCoursesSlots);
            TimetableSlotsWeek.Friday = getConsectiveSlots(TimeTableTimings, 5, TimetableSlotsWeek.Friday, requiredSlots, offeredCourse, FacultyAvailibilityCheck, programRegularTimings, programSpecialTimings, ClassTimeSlots, FacultyCoursesSlots);
            TimetableSlotsWeek.Saturday = getConsectiveSlots(TimeTableTimings, 6, TimetableSlotsWeek.Saturday, requiredSlots, offeredCourse, FacultyAvailibilityCheck, programRegularTimings, programSpecialTimings, ClassTimeSlots, FacultyCoursesSlots);
            TimetableSlotsWeek.Sunday = getConsectiveSlots(TimeTableTimings, 7, TimetableSlotsWeek.Sunday, requiredSlots, offeredCourse, FacultyAvailibilityCheck, programRegularTimings, programSpecialTimings, ClassTimeSlots, FacultyCoursesSlots);
        }
        public void setCourseScheduleNumber(OfferedCourses offeredCourse)
        {
            CourseScheduleNumber = 0;
            for (int i = 1; i <= 7; i++)
                if (Solution.Any(c => c.OfferedCourseID == offeredCourse.OfferedCourseID && c.TimeSlots.Time.TimeWeekDay == i))
                    CourseScheduleNumber += 1;
        }
        public OfferedCourses CourseCopy(OfferedCourses offeredCourse)
        {
            OfferedCourses offered_Course = new OfferedCourses
            {
                OfferedCourseID = offeredCourse.OfferedCourseID,
                OfferedCourseSection = offeredCourse.OfferedCourseSection,
                OfferedCourseCategory = offeredCourse.OfferedCourseCategory,
                ProgramID = offeredCourse.ProgramID,
                DepartmentID = offeredCourse.DepartmentID,
                SemesterID = offeredCourse.SemesterID,
                FacultyMemberID = offeredCourse.FacultyMemberID,
                FacultyMember = offeredCourse.FacultyMember,
                Program = offeredCourse.Program,
                Department = offeredCourse.Department,
                Semester = offeredCourse.Semester,
                OfferedCourseContactHours = offeredCourse.OfferedCourseContactHours,
                OfferedCourseCreditHours = offeredCourse.OfferedCourseCreditHours,
                OfferedCourseSemesterNo = offeredCourse.OfferedCourseSemesterNo,
                OfferedCourseTitle = offeredCourse.OfferedCourseTitle,
                OfferedCourseSpecial = offeredCourse.OfferedCourseSpecial
            };
            return offered_Course;
        }
        public void InitializeChromosome()
        {
            List<OfferedCourses> courses = new List<OfferedCourses>();
            foreach (var offeredCourse in _RepositoryService.GetByInstitute_Semester(InstituteID, SemesterID))
            {

                double numberofslots = Math.Ceiling(slotsPerHour * (double)offeredCourse.OfferedCourseContactHours);
                if (offeredCourse.OfferedCourseCategory == 4)
                {
                    offeredCourse.RemainingContactHours = (decimal)numberofslots / slotsPerHour;
                    courses.Add(offeredCourse);
                }
                else
                {
                    if (numberofslots <= slotsPerHour * 2)
                    {
                        offeredCourse.RemainingContactHours = (decimal)numberofslots / slotsPerHour;
                        courses.Add(offeredCourse);
                    }
                    else
                    {
                        double FirstContactHours = Math.Ceiling(((numberofslots) / 2) / slotsPerHour) * slotsPerHour;
                        double SecondHours = numberofslots - FirstContactHours;
                        offeredCourse.RemainingContactHours = (decimal)FirstContactHours / slotsPerHour;
                        courses.Add(offeredCourse);
                        var secondSlot = CourseCopy(offeredCourse);
                        secondSlot.RemainingContactHours = (decimal)SecondHours / slotsPerHour;
                        courses.Add(secondSlot);
                    }
                }
            }
            var ContactHoursGroup = courses.OrderByDescending(c => c.RemainingContactHours).GroupBy(c => new { c.RemainingContactHours }).ToList();
            bool CourseSchedule = true;
            foreach (var coursesSlots in ContactHoursGroup)
            {
                var teachersCourses = coursesSlots.GroupBy(c => new { c.FacultyMemberID }).ToList();
                teachersCourses = teachersCourses.OrderByDescending(c => c.Count()).ToList();
                foreach (var teacherCourses in teachersCourses)
                {
                    foreach (var course in teacherCourses)
                    {
                        try
                        {
                            setCourseScheduleNumber(course);
                            HardConstraints(course, (double)course.RemainingContactHours, (double)course.RemainingContactHours * slotsPerHour);
                        }
                        catch (Exception e)
                        {
                            GC.Collect();
                            GC.WaitForPendingFinalizers();
                            ChromosomeFitness = double.MinValue;
                            CourseSchedule = false;
                            break;
                        }
                    }
                    if (!CourseSchedule)
                        break;
                }
                if (!CourseSchedule)
                    break;
            }
        }
        public void DayLimit(OfferedCourses offeredCourse, double slotsRequired)
        {
            if (setAvailableDays(offeredCourse, slotsRequired, 1))
                timeTableDays.Add(1);
            if (setAvailableDays(offeredCourse, slotsRequired, 2))
                timeTableDays.Add(2);
            if (setAvailableDays(offeredCourse, slotsRequired, 3))
                timeTableDays.Add(3);
            if (setAvailableDays(offeredCourse, slotsRequired, 4))
                timeTableDays.Add(4);
            if (setAvailableDays(offeredCourse, slotsRequired, 5))
                timeTableDays.Add(5);
            if (setAvailableDays(offeredCourse, slotsRequired, 6))
                timeTableDays.Add(6);
            if (setAvailableDays(offeredCourse, slotsRequired, 7))
                timeTableDays.Add(7);
        }
        public int getCourseType(OfferedCourses offeredCourse)
        {
            if (offeredCourse.OfferedCourseCategory == 4)
                return offeredCourse.OfferedCourseCategory;
            else
                return 1;
        }
        public List<Rooms> getValidRooms(OfferedCourses offeredCourse, int Day)
        {
            List<Rooms> rooms = new List<Rooms>();
            int CourseCapacity = _RepositoryService.getRegisteredCoursesCount(offeredCourse.OfferedCourseID);
            var Rooms = _RepositoryService.GetRoomByInstitute_Type(offeredCourse.Department.InstituteID, getCourseType(offeredCourse));
            var RoomTimings = _RepositoryService.getByRoomDepartment_Day(offeredCourse.DepartmentID, Day);
            if (CourseCapacity == 0)
                return rooms;
            foreach (var room in Rooms)
            {
                if (RoomTimings.Any(c => c.RoomID == room.RoomID))
                {
                    if (room.SeatingCapacity > 0 && room.SeatingCapacity >= CourseCapacity)
                        rooms.Add(room);
                }

            }
            rooms = rooms.OrderBy(c => c.SeatingCapacity).ToList();
            return rooms;
        }
        public void HardConstraints(OfferedCourses offeredCourse, double SelectedContactHours, double numberOfSlots)
        {
            if (offeredCourse.RemainingContactHours > 0)
            {
                TimetableSlotsWeek = new Week();
                var programRegularTimings = _RepositoryService.getProgramRTimingsBy_Program(offeredCourse.ProgramID);
                var programSpecialTimings = _RepositoryService.getProgramSTimingsBy_Program(offeredCourse.ProgramID);
                var ClassTimeSlots = Solution.Where(c => c.OfferedCourse.ProgramID == offeredCourse.ProgramID && c.OfferedCourse.OfferedCourseSemesterNo == offeredCourse.OfferedCourseSemesterNo && c.OfferedCourse.OfferedCourseSection == offeredCourse.OfferedCourseSection).ToList();
                var FacultyCoursesSlots = Solution.Where(c => c.OfferedCourse.FacultyMemberID == offeredCourse.FacultyMemberID).ToList();
                setTimes(numberOfSlots, offeredCourse, false, programRegularTimings, programSpecialTimings, ClassTimeSlots, FacultyCoursesSlots);
                timeTableDays.Clear();
                DayLimit(offeredCourse, numberOfSlots);
                int Day = timeTableDays.ElementAt(_random.Next(0, timeTableDays.Count));
                while (!checkDays(offeredCourse, Day))
                    Day = timeTableDays.ElementAt(_random.Next(0, timeTableDays.Count));
                Day day = getDay(Day);
                int Hour = 0;
                Hour = _random.Next(0, day.ConsectiveContactHoursSlots.Count);
                var programRegularTiming = programRegularTimings.Where(c => c.Time.TimeWeekDay == Day).ToList();
                var programSpecialTiming = programSpecialTimings.Where(c => c.Time.TimeWeekDay == Day).ToList();
                while (!ConsectiveHoursInProgramTimings(Hour, day, Day, offeredCourse, (int)Math.Ceiling(SelectedContactHours), numberOfSlots, programSpecialTiming, programRegularTiming, ClassTimeSlots, FacultyCoursesSlots))
                    Hour = _random.Next(0, day.ConsectiveContactHoursSlots.Count);
                var rooms = getValidRooms(offeredCourse, Day);
                var room = rooms.ElementAt(0);
                while (!ConsectiveHoursInRoom(Hour, day, Day, offeredCourse, room, (int)Math.Ceiling(SelectedContactHours), numberOfSlots))
                {
                    bool found = false;
                    foreach (var _room in rooms)
                    {
                        found = ConsectiveHoursInRoom(Hour, day, Day, offeredCourse, _room, (int)Math.Ceiling(SelectedContactHours), numberOfSlots);
                        if (found)
                        {
                            room = _room;
                            break;
                        }
                    }
                    if (!found)
                    {
                        day.ConsectiveContactHoursSlots.Remove(day.ConsectiveContactHoursSlots.ElementAt(Hour));
                        if (day.ConsectiveContactHoursSlots.Count <= 0)
                        {
                            timeTableDays.Remove(Day);
                            Day = timeTableDays.ElementAt(_random.Next(0, timeTableDays.Count));
                            day = getDay(Day);
                            Hour = _random.Next(0, day.ConsectiveContactHoursSlots.Count);
                        }
                        else
                            Hour = _random.Next(0, day.ConsectiveContactHoursSlots.Count);
                    }
                }
                addToSolution(Hour, day, offeredCourse.OfferedCourseID, room.RoomID, numberOfSlots, SelectedContactHours);
            }
        }
        public double GeneFitness(OfferedCourseTimeSlots offeredCourseTimeSlot, List<FacultyMemberAvailabilities> facultyMemberAvailabilities)
        {
            double fitness = 0;
            if (!facultyMemberAvailabilities.Any(c => c.TimeID == offeredCourseTimeSlot.TimeSlots.TimeID && c.FacultyMemberID == offeredCourseTimeSlot.OfferedCourse.FacultyMemberID))
                fitness += 1;
            return fitness;
        }
        public bool setAvailableDays(OfferedCourses offeredCourse, double slotsRequired, int day)
        {
            Day Day = getDay(day);
            Times previous = new Times();
            int count = 0;
            if (Solution.Any(c => c.OfferedCourse.OfferedCourseID == offeredCourse.OfferedCourseID && c.TimeSlots.Time.TimeWeekDay == day && c.TimeSlots.TimeTableID == TimetableID))
                return false;
            foreach (var Hour in Day.ConsectiveContactHoursSlots)
            {
                foreach (var slot in Hour.Times)
                    count += 1;
            }
            if (count >= slotsRequired)
                return true;
            return false;
        }
        public bool ConsectiveHoursInProgramTimings(int HourNumber, Day DayTimes, int day, OfferedCourses offeredCourse, int ConsectiveHours, double slotsRequired, List<ProgramSpecialTimings> programSpecialTimings, List<ProgramRegularTimings> programRegularTimings, List<OfferedCourseTimeSlots> ClassTimeSlots, List<OfferedCourseTimeSlots> FacultyCoursesSlots)
        {
            if (offeredCourse.OfferedCourseSpecial == 0)
            {
                if (!checkAllRegular((HourNumber), DayTimes, offeredCourse, slotsRequired, programRegularTimings, ClassTimeSlots, FacultyCoursesSlots))
                    return false;
            }
            else
            {
                if (!checkAllSpecial((HourNumber), DayTimes, offeredCourse, slotsRequired, programSpecialTimings, ClassTimeSlots, FacultyCoursesSlots))
                    return false;
            }
            return true;
        }
        public bool ConsectiveHoursInRoom(int HourNumber, Day day, int Day, OfferedCourses offeredCourses, Rooms room, int ConsectiveHours, double slotsRequired)
        {
            var RoomTimings = _RepositoryService.getByRoom_Day(room.RoomID, Day);
            var AssignedCourseSlots = Solution.Where(c => c.TimeSlots.Time.TimeWeekDay == Day && c.RoomID == room.RoomID).ToList();
            if (!checkRoomTiming(HourNumber, day, room, slotsRequired, AssignedCourseSlots, RoomTimings))
                return false;
            return true;
        }
        public void addToSolution(int Hour, Day day, int CourseID, int roomiD, double slotsRequired, double SelectedContactHours)
        {
            Hour hour = day.ConsectiveContactHoursSlots.ElementAt(Hour);
            hour.Times.OrderBy(c => c.TimeID);
            List<OfferedCourseTimeSlots> OfferedCourseTimeSlots = new List<OfferedCourseTimeSlots>();
            var Timeslots = _RepositoryService.getByTimeTable_Day(TimetableID, hour.Times.ElementAt(0).TimeWeekDay);
            var offeredCourse = _RepositoryService.GetCourse(CourseID);
            if (hour.Times.Count >= slotsRequired)
            {
                foreach (var slot in hour.Times)
                {
                    var timeSlot = Timeslots.FirstOrDefault(c => c.TimeID == slot.TimeID);
                    OfferedCourseTimeSlots offeredCourseTimeSlots = new OfferedCourseTimeSlots
                    {
                        OfferedCourseID = CourseID,
                        RoomID = roomiD,
                        TimeSlotID = timeSlot.TimeSlotID,
                        TimeSlots = timeSlot,
                        OfferedCourse = offeredCourse,
                        AssignedContactHours = SelectedContactHours,
                        CourseScheduleNumber = CourseScheduleNumber
                    };
                    OfferedCourseTimeSlots.Add(offeredCourseTimeSlots);
                }
                foreach (var CourseSlot in OfferedCourseTimeSlots)
                {
                    Solution.Add(CourseSlot);
                }
            }
        }
        public bool checkRoomTiming(int HourNumber, Day day, Rooms Room, double slotsRequired, List<OfferedCourseTimeSlots> AssignedCourseSlots, List<RoomAvailibilities> RoomTimings)
        {
            SlotsCount = 0;
            if (day.ConsectiveContactHoursSlots.Count > HourNumber)
            {
                Hour hour = day.ConsectiveContactHoursSlots.ElementAt(HourNumber);
                hour.Times = hour.Times.OrderBy(c => c.TimeID).ToList();
                if (hour.Times.Count >= slotsRequired)
                {
                    foreach (var slot in hour.Times)
                    {
                        SlotsCount += 1;
                        if (SlotsCount <= slotsRequired)
                        {
                            if (!checkRoom(slot.TimeID, Room, AssignedCourseSlots, RoomTimings))
                                return false;
                        }
                    }
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }
        public bool checkRoom(int Time, Rooms Room, List<OfferedCourseTimeSlots> AssignedCoursesSlots, List<RoomAvailibilities> RoomTimings)
        {
            if (AssignedCoursesSlots.Any(c => c.TimeSlots.TimeID == Time && c.RoomID == Room.RoomID))
                return false;
            return RoomTimings.Any(c => c.TimeID == Time && c.RoomID == Room.RoomID);
        }
        public bool checkAllRegular(int HourNumber, Day day, OfferedCourses offeredCourses, double slotsRequired, List<ProgramRegularTimings> programRegularTimings, List<OfferedCourseTimeSlots> ClassTimeSlots, List<OfferedCourseTimeSlots> FacultyCoursesSlots)
        {
            if (day.ConsectiveContactHoursSlots.Count > HourNumber)
            {
                Hour hour = day.ConsectiveContactHoursSlots.ElementAt(HourNumber);
                hour.Times.OrderBy(c => c.TimeID);
                if (hour.Times.Count >= slotsRequired)
                {
                    foreach (var slot in hour.Times)
                    {
                        if (!checkRegularTime(slot, offeredCourses, ClassTimeSlots, FacultyCoursesSlots, programRegularTimings))
                            return false;
                    }
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }
        public bool checkRegularTime(Times Time, OfferedCourses offeredCourse, List<OfferedCourseTimeSlots> ClassTimeSlots, List<OfferedCourseTimeSlots> FacultyCoursesSlots, List<ProgramRegularTimings> programRegularTimings)
        {
            if (FacultyCoursesSlots.Any(c => c.OfferedCourse.FacultyMemberID == offeredCourse.FacultyMemberID && c.TimeSlots.TimeID == Time.TimeID && c.TimeSlots.TimeTableID == TimetableID))
                return false;
            if (ClassTimeSlots.Any(c => c.OfferedCourse.OfferedCourseSemesterNo == offeredCourse.OfferedCourseSemesterNo && c.OfferedCourse.OfferedCourseSection == offeredCourse.OfferedCourseSection && c.OfferedCourse.ProgramID == offeredCourse.ProgramID && c.TimeSlots.TimeID == Time.TimeID))
                return false;
            return programRegularTimings.Any(c => c.Time.TimeID == Time.TimeID && c.Program.ProgramID == offeredCourse.ProgramID);
        }

        public bool checkAllSpecial(int HourNumber, Day day, OfferedCourses offeredCourse, double slotsRequired, List<ProgramSpecialTimings> programSpecialTimings, List<OfferedCourseTimeSlots> ClassTimeSlots, List<OfferedCourseTimeSlots> FacultyCoursesSlots)
        {
            if (day.ConsectiveContactHoursSlots.Count > HourNumber)
            {
                Hour hour = day.ConsectiveContactHoursSlots.ElementAt(HourNumber);
                hour.Times.OrderBy(c => c.TimeID);
                if (hour.Times.Count >= slotsRequired)
                {
                    foreach (var slot in hour.Times)
                    {
                        if (!checkSpecialTime(slot, offeredCourse, ClassTimeSlots, FacultyCoursesSlots, programSpecialTimings))
                            return false;
                    }
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }
        public bool checkSpecialTime(Times Time, OfferedCourses offeredCourse, List<OfferedCourseTimeSlots> ClassTimeSlots, List<OfferedCourseTimeSlots> FacultyCoursesSlots, List<ProgramSpecialTimings> programSpecialTimings)
        {
            if (FacultyCoursesSlots.Any(c => c.OfferedCourse.FacultyMemberID == offeredCourse.FacultyMemberID && c.TimeSlots.TimeID == Time.TimeID && c.TimeSlots.TimeTableID == TimetableID))
                return false;
            if (ClassTimeSlots.Any(c => c.OfferedCourse.OfferedCourseSemesterNo == offeredCourse.OfferedCourseSemesterNo && c.OfferedCourse.OfferedCourseSection == offeredCourse.OfferedCourseSection && c.OfferedCourse.ProgramID == offeredCourse.ProgramID && c.TimeSlots.TimeID == Time.TimeID))
                return false;
            return programSpecialTimings.Any(c => c.Time.TimeID == Time.TimeID && c.Program.ProgramID == offeredCourse.ProgramID);
        }
        public Day getDay(int Day)
        {
            if (Day == 1)
                return TimetableSlotsWeek.Monday;
            if (Day == 2)
                return TimetableSlotsWeek.Tuesday;
            if (Day == 3)
                return TimetableSlotsWeek.Wednesday;
            if (Day == 4)
                return TimetableSlotsWeek.Thursday;
            if (Day == 5)
                return TimetableSlotsWeek.Friday;
            if (Day == 6)
                return TimetableSlotsWeek.Saturday;
            if (Day == 7)
                return TimetableSlotsWeek.Sunday;
            else
                return new Day();
        }
        public void saveSolution()
        {
            foreach (var CourseSlot in Solution)
            {
                if (_RepositoryService.GetCountByTimetable_Course(TimetableID, CourseSlot.OfferedCourseID) < Math.Ceiling(slotsPerHour * (double)CourseSlot.OfferedCourse.OfferedCourseContactHours))
                    _RepositoryService.Insert_CourseSlots(CourseSlot);
            }
        }
        public double getFitness()
        {
            Solution.OrderBy(c => c.TimeSlots.TimeID);
            var Groups = Solution.GroupBy(c => new { c.OfferedCourseID, c.AssignedContactHours, c.TimeSlots.Time.TimeWeekDay, c.CourseScheduleNumber }).ToList();
            foreach (var group in Groups)
            {
                double courseFitness = CourseSlotFitness(group.ToList());
                ChromosomeFitness += courseFitness;
                if (courseFitness < RequiredFitness)
                    foreach (var courseSlots in group)
                        underUtilizedCourseSlots.Add(courseSlots);
                else
                    foreach (var courseSlots in group)
                        UtilizedCourseSlots.Add(courseSlots);
            }
            if (ChromosomeFitness == 0 || Groups.Count == 0)
                return 0;
            ChromosomeFitness = ChromosomeFitness / Groups.Count;
            return ChromosomeFitness;
        }
        public bool checkDays(OfferedCourses offeredCourse, int Day)
        {
            if (Solution.Any(c => c.TimeSlots.Time.TimeWeekDay == Day && c.OfferedCourseID == offeredCourse.OfferedCourseID))
                return false;
            return _RepositoryService.IsExistsDay_TimeTableTimings(Day);
        }
        public double CourseSlotFitness(List<OfferedCourseTimeSlots> offeredCourseTimeSlots)
        {
            if (offeredCourseTimeSlots.Count > 0)
            {
                var FacultyTimings = _RepositoryService.getBy_Faculty_Day(offeredCourseTimeSlots.ElementAt(0).OfferedCourse.FacultyMemberID, offeredCourseTimeSlots.ElementAt(0).TimeSlots.Time.TimeWeekDay);
                double fitness = 0;
                foreach (var courseSlot in offeredCourseTimeSlots)
                    fitness += GeneFitness(courseSlot, FacultyTimings);
                fitness = (fitness / offeredCourseTimeSlots.Count) * 100;
                return fitness;
            }
            return 0;
        }
        public static Chromosome CrossOver(Chromosome FirstParent, Chromosome SecondParent)
        {
            var NewOffSpring = new Chromosome(FirstParent._RepositoryService, FirstParent.TimetableID);
            if (FirstParent.underUtilizedCourseSlots.Count > 0)
            {
                var FirstParentUtilizedSlots = FirstParent.UtilizedCourseSlots.GroupBy(c => new { c.AssignedContactHours, c.OfferedCourseID, c.TimeSlots.Time.TimeWeekDay, c.CourseScheduleNumber }).ToList();
                foreach (var FirstParentSlot in FirstParentUtilizedSlots)
                {
                    var Course = FirstParentSlot.ElementAt(0);
                    NewOffSpring.Solution.AddRange(FirstParentSlot.ToList());
                }
                var FirstParentUnderUtilizedEvents = FirstParent.underUtilizedCourseSlots.GroupBy(c => new { c.OfferedCourseID, c.AssignedContactHours, c.TimeSlots.Time.TimeWeekDay, c.CourseScheduleNumber }).ToList();
                foreach (var FirstParentCourseSlots in FirstParentUnderUtilizedEvents)
                {
                    if (SecondParent.Solution.Any(c => c.AssignedContactHours == FirstParentCourseSlots.ElementAt(0).AssignedContactHours && c.OfferedCourseID == FirstParentCourseSlots.ElementAt(0).OfferedCourseID && c.CourseScheduleNumber == FirstParentCourseSlots.ElementAt(0).CourseScheduleNumber))
                    {
                        foreach (var slot in FirstParentCourseSlots)
                            FirstParent.Solution.Remove(slot);
                        List<List<OfferedCourseTimeSlots>> offeredCourseTimeSlots = new List<List<OfferedCourseTimeSlots>>();
                        var CourseFitness = FirstParent.CourseSlotFitness(FirstParentCourseSlots.ToList());
                        var FittestSlots = FirstParentCourseSlots.ToList();
                        var CourseSlot = FirstParentCourseSlots.ElementAt(0);
                        var SecondParentSlot = SecondParent.Solution.Where(c => c.OfferedCourseID == CourseSlot.OfferedCourseID && c.AssignedContactHours == CourseSlot.AssignedContactHours && c.CourseScheduleNumber == CourseSlot.CourseScheduleNumber).ToList();
                        if (SecondParentSlot.Count != FirstParentCourseSlots.Count())
                            continue;
                        foreach (var slot in SecondParentSlot)
                            SecondParent.Solution.Remove(slot);
                        var ClassTimeSlots = NewOffSpring.Solution.Where(c => c.OfferedCourse.ProgramID == CourseSlot.OfferedCourse.ProgramID && c.OfferedCourse.OfferedCourseSemesterNo == CourseSlot.OfferedCourse.OfferedCourseSemesterNo && c.OfferedCourse.OfferedCourseSection == CourseSlot.OfferedCourse.OfferedCourseSection).ToList();
                        var FacultyCoursesSlots = NewOffSpring.Solution.Where(c => c.OfferedCourse.FacultyMemberID == CourseSlot.OfferedCourse.FacultyMemberID).ToList();
                        var ProgramRegularTimings = NewOffSpring._RepositoryService.getProgramRTimingsBy_Program(CourseSlot.OfferedCourse.ProgramID);
                        var ProgramSpecialTimings = NewOffSpring._RepositoryService.getProgramSTimingsBy_Program(CourseSlot.OfferedCourse.ProgramID);
                        offeredCourseTimeSlots.AddRange(FirstParent.GetAvailableSlots(CourseSlot.OfferedCourse, FirstParentCourseSlots.ToList().Count, FirstParentCourseSlots.ToList().Count / Chromosome.slotsPerHour, FirstParentCourseSlots.ElementAt(0).CourseScheduleNumber, false, ProgramRegularTimings, ProgramSpecialTimings, ClassTimeSlots, FacultyCoursesSlots));
                        offeredCourseTimeSlots.AddRange(FirstParent.GetAvailableSlots(CourseSlot.OfferedCourse, FirstParentCourseSlots.ToList().Count, FirstParentCourseSlots.ToList().Count / Chromosome.slotsPerHour, FirstParentCourseSlots.ElementAt(0).CourseScheduleNumber, true, ProgramRegularTimings, ProgramSpecialTimings, ClassTimeSlots, FacultyCoursesSlots));
                        offeredCourseTimeSlots.AddRange(SecondParent.GetAvailableSlots(CourseSlot.OfferedCourse, SecondParentSlot.ToList().Count, SecondParentSlot.ToList().Count / Chromosome.slotsPerHour, SecondParentSlot.ElementAt(0).CourseScheduleNumber, false, ProgramRegularTimings, ProgramSpecialTimings, ClassTimeSlots, FacultyCoursesSlots));
                        offeredCourseTimeSlots.AddRange(SecondParent.GetAvailableSlots(CourseSlot.OfferedCourse, SecondParentSlot.ToList().Count, SecondParentSlot.ToList().Count / Chromosome.slotsPerHour, SecondParentSlot.ElementAt(0).CourseScheduleNumber, true, ProgramRegularTimings, ProgramSpecialTimings, ClassTimeSlots, FacultyCoursesSlots));
                        foreach (var CourseSlots in offeredCourseTimeSlots)
                        {
                            if (CourseSlots == null)
                                continue;
                            var ProgramRegularTiming = NewOffSpring._RepositoryService.getProgramRTimingsBy_Program_Day(CourseSlots.ElementAt(0).OfferedCourse.ProgramID, CourseSlots.ElementAt(0).TimeSlots.Time.TimeWeekDay);
                            var ProgramSpecialTiming = NewOffSpring._RepositoryService.getProgramSTimingsBy_Program_Day(CourseSlots.ElementAt(0).OfferedCourse.ProgramID, CourseSlots.ElementAt(0).TimeSlots.Time.TimeWeekDay);
                            if (NewOffSpring.checkHardConstraints(CourseSlots.ToList(), ProgramRegularTiming, ProgramSpecialTiming, ClassTimeSlots, FacultyCoursesSlots) && CourseSlots != null)
                            {
                                var tempFitness = NewOffSpring.CourseSlotFitness(CourseSlots.ToList());
                                if (tempFitness > CourseFitness)
                                {
                                    FittestSlots = CourseSlots.ToList();
                                    CourseFitness = tempFitness;
                                }
                            }
                            else
                                offeredCourseTimeSlots.Remove(CourseSlots.ToList());
                        }
                        FirstParent.Solution.AddRange(FirstParentCourseSlots);
                        SecondParent.Solution.AddRange(SecondParentSlot);
                        NewOffSpring.Solution.AddRange(FittestSlots.ToList());
                    }
                }
                NewOffSpring.getFitness();
                if (NewOffSpring.ChromosomeFitness <= FirstParent.ChromosomeFitness && NewOffSpring.ChromosomeFitness <= SecondParent.ChromosomeFitness)
                    NewOffSpring.NewGenerated = false;
                return NewOffSpring;
            }
            else
                return null;
        }
        public List<List<OfferedCourseTimeSlots>> GetAvailableSlots(OfferedCourses offeredCourse, double SlotsRequired, double SelectedContactHours, double ScheduledNumber, bool facultyTimeCheck, List<ProgramRegularTimings> programRegularTimings, List<ProgramSpecialTimings> programSpecialTimings, List<OfferedCourseTimeSlots> ClassTimeSlots, List<OfferedCourseTimeSlots> FacultyCoursesSlots)
        {
            List<List<OfferedCourseTimeSlots>> offeredCoursesTimeSlots = new List<List<OfferedCourseTimeSlots>>();
            TimetableSlotsWeek = new Week();
            setTimes(SlotsRequired, offeredCourse, facultyTimeCheck, programRegularTimings, programSpecialTimings, ClassTimeSlots, FacultyCoursesSlots);
            timeTableDays.Clear();
            DayLimit(offeredCourse, SlotsRequired);
            foreach (var day in timeTableDays)
            {
                var Day = getDay(day);
                foreach (var Hour in Day.ConsectiveContactHoursSlots)
                {
                    int HourIndex = 0;
                    if (!ConsectiveHoursInProgramTimings(HourIndex, Day, day, offeredCourse, (int)Math.Ceiling(SelectedContactHours), SlotsRequired, programSpecialTimings, programRegularTimings, ClassTimeSlots, FacultyCoursesSlots))
                        continue;
                    if (!checkDays(offeredCourse, day))
                        continue;
                    var rooms = getValidRooms(offeredCourse, day);
                    foreach (var room in rooms)
                    {
                        if (!ConsectiveHoursInRoom(HourIndex, Day, day, offeredCourse, room, (int)Math.Ceiling(SelectedContactHours), SlotsRequired))
                            continue;
                        else
                        {
                            offeredCoursesTimeSlots.Add(SaveToList(offeredCourse, room, Hour, SelectedContactHours, SlotsRequired, ScheduledNumber));
                            break;
                        }
                    }
                    HourIndex += 1;
                }
            }
            return offeredCoursesTimeSlots;
        }
        public List<OfferedCourseTimeSlots> SaveToList(OfferedCourses offeredCourse, Rooms room, Hour hour, double SelectedContactHours, double SlotsRequired, double ScheduledNumber)
        {
            List<OfferedCourseTimeSlots> offeredCourseTimeSlot = new List<OfferedCourseTimeSlots>();
            foreach (var slot in hour.Times)
            {
                var timeSlot = _RepositoryService.getTimeSlotsByTime_Timetable(TimetableID, slot.TimeID);
                OfferedCourseTimeSlots offeredCourseTimeSlots = new OfferedCourseTimeSlots();
                var OfferedCourse = _RepositoryService.GetCourse(offeredCourse.OfferedCourseID);
                offeredCourseTimeSlots.OfferedCourseID = OfferedCourse.OfferedCourseID;
                offeredCourseTimeSlots.RoomID = room.RoomID;
                offeredCourseTimeSlots.TimeSlotID = timeSlot.TimeSlotID;
                offeredCourseTimeSlots.TimeSlots = timeSlot;
                offeredCourseTimeSlots.OfferedCourse = offeredCourse;
                offeredCourseTimeSlots.AssignedContactHours = SelectedContactHours;
                offeredCourseTimeSlots.CourseScheduleNumber = ScheduledNumber;
                offeredCourseTimeSlot.Add(offeredCourseTimeSlots);
            }
            if (offeredCourseTimeSlot.Count < SlotsRequired)
                return null;
            return offeredCourseTimeSlot;
        }
        public bool checkHardConstraints(List<OfferedCourseTimeSlots> offeredCourseTimeSlots, List<ProgramRegularTimings> ProgramRegularTiming, List<ProgramSpecialTimings> programSpecialTiming, List<OfferedCourseTimeSlots> ClassTimeSlots, List<OfferedCourseTimeSlots> FacultyCoursesSlots)
        {
            var RoomTimings = _RepositoryService.getByRoom_Day(offeredCourseTimeSlots.ElementAt(0).RoomID, offeredCourseTimeSlots.ElementAt(0).TimeSlots.Time.TimeWeekDay);
            var AssignedCourseSlots = Solution.Where(c => c.TimeSlots.Time.TimeWeekDay == offeredCourseTimeSlots.ElementAt(0).TimeSlots.Time.TimeWeekDay && c.RoomID == offeredCourseTimeSlots.ElementAt(0).RoomID).ToList();
            foreach (var courseSlot in offeredCourseTimeSlots)
            {
                if (!checkDays(courseSlot.OfferedCourse, courseSlot.TimeSlots.Time.TimeWeekDay))
                    return false;
                if (courseSlot.OfferedCourse.OfferedCourseSpecial == 0)
                {
                    if (!checkRegularTime(courseSlot.TimeSlots.Time, courseSlot.OfferedCourse, ClassTimeSlots, FacultyCoursesSlots, ProgramRegularTiming))
                        return false;
                }
                else
                {
                    if (!checkSpecialTime(courseSlot.TimeSlots.Time, courseSlot.OfferedCourse, ClassTimeSlots, FacultyCoursesSlots, programSpecialTiming))
                        return false;
                }
                var room = _RepositoryService.GetRoom(courseSlot.RoomID);
                if (!checkRoom(courseSlot.TimeSlots.TimeID, room, AssignedCourseSlots, RoomTimings))
                    return false;
            }
            return true;
        }
        public static Chromosome Mutation(Chromosome ParentChromosome)
        {
            var NewOffSpring = new Chromosome(ParentChromosome._RepositoryService, ParentChromosome.TimetableID);
            if (ParentChromosome.underUtilizedCourseSlots.Count > 0)
            {
                var FirstParentUtilizedSlots = ParentChromosome.UtilizedCourseSlots.GroupBy(c => new { c.AssignedContactHours, c.OfferedCourseID, c.TimeSlots.Time.TimeWeekDay, c.CourseScheduleNumber }).ToList();
                foreach (var FirstParentSlot in FirstParentUtilizedSlots)
                {
                    var Course = FirstParentSlot.ElementAt(0);
                    NewOffSpring.Solution.AddRange(FirstParentSlot.ToList());
                }
                var FirstParentUnderUtilizedEvents = ParentChromosome.underUtilizedCourseSlots.GroupBy(c => new { c.OfferedCourseID, c.AssignedContactHours, c.TimeSlots.Time.TimeWeekDay, c.CourseScheduleNumber }).ToList();
                foreach (var FirstParentCourseSlots in FirstParentUnderUtilizedEvents)
                {
                    List<List<OfferedCourseTimeSlots>> offeredCourseTimeSlots = new List<List<OfferedCourseTimeSlots>>();
                    var CourseFitness = ParentChromosome.CourseSlotFitness(FirstParentCourseSlots.ToList());
                    var FittestSlots = FirstParentCourseSlots.ToList();
                    var CourseSlot = FirstParentCourseSlots.ElementAt(0);
                    foreach (var slot in FirstParentCourseSlots)
                        ParentChromosome.Solution.Remove(slot);
                    var ProgramRegularTimings = NewOffSpring._RepositoryService.getProgramRTimingsBy_Program(CourseSlot.OfferedCourse.ProgramID);
                    var ProgramSpecialTimings = NewOffSpring._RepositoryService.getProgramSTimingsBy_Program(CourseSlot.OfferedCourse.ProgramID);
                    var ClassTimeSlots = NewOffSpring.Solution.Where(c => c.OfferedCourse.ProgramID == CourseSlot.OfferedCourse.ProgramID && c.OfferedCourse.OfferedCourseSemesterNo == CourseSlot.OfferedCourse.OfferedCourseSemesterNo && c.OfferedCourse.OfferedCourseSection == CourseSlot.OfferedCourse.OfferedCourseSection).ToList();
                    var FacultyCoursesSlots = NewOffSpring.Solution.Where(c => c.OfferedCourse.FacultyMemberID == CourseSlot.OfferedCourse.FacultyMemberID).ToList();
                    offeredCourseTimeSlots.AddRange(ParentChromosome.GetAvailableSlots(CourseSlot.OfferedCourse, FirstParentCourseSlots.ToList().Count, FirstParentCourseSlots.ToList().Count / Chromosome.slotsPerHour, FirstParentCourseSlots.ElementAt(0).CourseScheduleNumber, false, ProgramRegularTimings, ProgramSpecialTimings, ClassTimeSlots, FacultyCoursesSlots));
                    offeredCourseTimeSlots.AddRange(ParentChromosome.GetAvailableSlots(CourseSlot.OfferedCourse, FirstParentCourseSlots.ToList().Count, FirstParentCourseSlots.ToList().Count / Chromosome.slotsPerHour, FirstParentCourseSlots.ElementAt(0).CourseScheduleNumber, true, ProgramRegularTimings, ProgramSpecialTimings, ClassTimeSlots, FacultyCoursesSlots));
                    ParentChromosome.Solution.AddRange(FirstParentCourseSlots);
                    foreach (var CourseSlots in offeredCourseTimeSlots)
                    {
                        if (CourseSlots == null)
                            continue;
                        var ProgramRegularTiming = NewOffSpring._RepositoryService.getProgramRTimingsBy_Program_Day(CourseSlots.ElementAt(0).OfferedCourse.ProgramID, CourseSlots.ElementAt(0).TimeSlots.Time.TimeWeekDay);
                        var ProgramSpecialTiming = NewOffSpring._RepositoryService.getProgramSTimingsBy_Program_Day(CourseSlots.ElementAt(0).OfferedCourse.ProgramID, CourseSlots.ElementAt(0).TimeSlots.Time.TimeWeekDay);
                        if (NewOffSpring.checkHardConstraints(CourseSlots.ToList(), ProgramRegularTiming, ProgramSpecialTiming, ClassTimeSlots, FacultyCoursesSlots) && CourseSlots != null)
                        {
                            var tempFitness = NewOffSpring.CourseSlotFitness(CourseSlots.ToList());
                            if (tempFitness > CourseFitness)
                            {
                                FittestSlots = CourseSlots.ToList();
                                CourseFitness = tempFitness;
                            }
                        }
                        else
                            offeredCourseTimeSlots.Remove(CourseSlots.ToList());
                    }
                    NewOffSpring.Solution.AddRange(FittestSlots.ToList());
                }
                NewOffSpring.getFitness();
                if (NewOffSpring.ChromosomeFitness <= ParentChromosome.ChromosomeFitness)
                    NewOffSpring.NewGenerated = false;
                return NewOffSpring;
            }
            else
                return null;
        }
        public List<OfferedCourseTimeSlots> getSolution()
        {
            if (Solution.Count <= 0)
                return null;
            return Solution;
        }
    }
}
