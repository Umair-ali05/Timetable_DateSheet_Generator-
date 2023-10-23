using System.Collections.Generic;

namespace Timetable_DateSheet_Generator.Models.ViewModels
{
    public class AttendanceViewModel
    {
        public Attendance Attendance { get; set; }
        public List<StudentAttendance> studentAttendances { get; set; }
        public AttendanceViewModel()
        {
            Attendance = new Attendance();
            studentAttendances = new List<StudentAttendance>();
        }
    }
}
