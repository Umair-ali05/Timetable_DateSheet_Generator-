namespace Timetable_DateSheet_Generator.Models.ViewModels
{
    public class RegisterCourseViewModel
    {
        public int OfferedCourseID { get;set;}
        public StudentViewModel StudentViewModel { get; set; }
        public bool isMarked { get; set; }
        public RegisterCourseViewModel()
        {
            StudentViewModel = new StudentViewModel();
        }
    }
}
