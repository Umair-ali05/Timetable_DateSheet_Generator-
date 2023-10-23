namespace Timetable_DateSheet_Generator.Models.ViewModels
{
    public class StudentAttendanceSummaryViewModel
    {
        public Students Students { get; set; }
        public OfferedCourses OfferedCourse { get; set; }
        public double totalPresentHours { get; set; }
        public double totalAbsentHours { get; set; }
        public double Percentage { get; set; }
        public StudentAttendanceSummaryViewModel()
        {
            Students = new Students();
            OfferedCourse = new OfferedCourses();
            totalPresentHours=0;
            totalAbsentHours=0;
            Percentage=0;
        }
    }
}
