namespace Timetable_DateSheet_Generator.Models.ViewModels
{
    public class StudentAttendanceViewModel
    {
        public Attendance Attendance { get; set; }
        public Students Student { get; set; }
        public double PresentHours { get; set; }
        public double AbsentHours { get; set; }
        public bool isPresent { get; set; }
        public StudentAttendanceViewModel()
        {
            Attendance = new Attendance();
            PresentHours = 0;
            AbsentHours = 0;
            isPresent = false;
        }
    }
}
