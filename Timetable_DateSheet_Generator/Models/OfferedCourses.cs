using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Timetable_DateSheet_Generator.Models
{
    [Table("OfferedCourses")]
    public class OfferedCourses
    {
        public enum CourseCategoryOptions
        {
            [Display(Name = "Theory")] Theory = 1,
            [Display(Name = "Lab")] Lab = 4,
            [Display(Name = "Project")] Project = 8,
            [Display(Name = "Thesis")] Thesis = 16
        }
        public enum CourseTypeOptions
        {
            [Display(Name = "Regular")] Regular = 0,
            [Display(Name = "Special")] Special = 1
        }
        //public enum CourseShiftOptions
        //{
        //    [Display(Name = "Morning")] Morning = 0,
        //    [Display(Name = "Evening")] Evening = 1,
        //    [Display(Name = "Weekend")] Weekend = 2
        //}
        [Key]
        [Required]
        [Column(nameof(OfferedCourseID), TypeName = "int")]
        public int OfferedCourseID { get; set; }
        [Required]
        [Column(nameof(FacultyMemberID), TypeName = "int")]
        public int FacultyMemberID { get; set; }

        [Required]
        [Column(nameof(DepartmentID), TypeName = "int")]
        public int DepartmentID { get; set; }

        [Required]
        [Column(nameof(ProgramID), TypeName = "int")]
        public int ProgramID { get; set; }

        [Required]
        [Column(nameof(SemesterID), TypeName = "int")]
        public int SemesterID { get; set; }
        [Required]
        [Column(nameof(OfferedCourseTitle), TypeName = "nvarchar(200)")]
        public string OfferedCourseTitle { get; set; }
        [Required]
        [Column(nameof(OfferedCourseCreditHours), TypeName = "decimal(3,2)")]
        public decimal OfferedCourseCreditHours { get; set; }

        [Required]
        [Column(nameof(OfferedCourseContactHours), TypeName = "decimal(3,2)")]
        public decimal OfferedCourseContactHours { get; set; }

        [Required]
        [Column(nameof(OfferedCourseCategory), TypeName = "int")]
        public int OfferedCourseCategory { get; set; }

        [Required]
        [Column(nameof(OfferedCourseSpecial), TypeName = "int")]
        public int OfferedCourseSpecial { get; set; }

        //[Required]
        //[Column(nameof(OfferedCourseShift), TypeName = "int")]
        //public int OfferedCourseShift { get; set; }

        [Required]
        [Column(nameof(OfferedCourseSection), TypeName = "char(1)")]
        public char OfferedCourseSection { get; set; }

        [Required]
        [Column(nameof(OfferedCourseSemesterNo), TypeName = "int")]
        public int OfferedCourseSemesterNo { get; set; }

        [Column(nameof(User), TypeName = "nvarchar(450)")]
        public string User { get; set; }
        [Column(nameof(Last_Modified), TypeName = "datetime")]
        public DateTime Last_Modified { get; set; }
        [NotMapped]
        public decimal RemainingContactHours = 0;
        public Programs Program { get; set; }
        public Semesters Semester { get; set; }
        public FacultyMembers FacultyMember { get; set; }
        public List<RegisteredCourses> RegisteredCourses { get; set; }
        public List<OfferedCourseTimeSlots> OfferedCourseTimeSlots { get; set; }        
        public List<Attendance> Attendances { get; set; }
        public Departments Department { get; set; }
        public string getClassFormat(string str)
        {
            string[] strs = str.Split(new char[] { ' ', ',', '.', '-', '_', '/', '\n', '\t' });
            char firstChar = str[0];
            char secondChar = ' ';
            for (int i = 1; i < strs.Length; i++)
                if (strs[i].Trim() != "")
                    secondChar = strs[i][0];
            return firstChar.ToString().ToUpper() + secondChar.ToString().ToUpper();
        }
        public string getClassTitle()
        {
            string[] strs = OfferedCourseTitle.Split(new char[] { ' ', ',', '.', '-', '_', '/', '\n', '\t', '(', ')' });
            char firstChar = OfferedCourseTitle[0];
            string title = firstChar.ToString();
            for (int i = 1; i < strs.Length; i++)
            {
                if (strs[i].Trim() != "")
                {
                    if (strs[i].ToLower().Equals("for") || strs[i].ToLower().Equals("of") || strs[i].ToLower().Equals("in") || strs[i].ToLower().Equals("lab") || strs[i].ToLower().Equals("a") || strs[i].ToLower().Equals("&") || strs[i].ToLower().Equals("and"))
                        continue;
                    title += strs[i][0].ToString();
                }
            }
            if (OfferedCourseCategory == 4)
                title += " Lab";
            return title;
        }
        public string Class()
        {
            return (Program.ProgramName + "-" + OfferedCourseSemesterNo.ToString() + "(" + OfferedCourseSection.ToString().ToUpper() + ")");
        }

    }
}
