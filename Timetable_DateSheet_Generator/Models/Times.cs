using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Timetable_DateSheet_Generator.Models
{
    [Table("Times")]
    public class Times
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(nameof(TimeID), TypeName = "int")]
        public int TimeID { get; set; }
        /// <summary>
        /// 4 Slots For one contact Hour
        /// </summary>
        public enum HourSubSlots
        {
            [Display(Name = "00")] Hour_First_Slot = 0,
            [Display(Name = "15")] Hour_Second_Slot = 15,
            [Display(Name = "30")] Hour_Third_Slot = 30,
            [Display(Name = "45")] Hour_Fourth_Slot = 45
        }
        [NotMapped]
        public bool isSelected { get; set; }
        [Required]
        [Column(nameof(TimeWeekDay), TypeName = "int")]
        public int TimeWeekDay { get; set; }

        [Required]
        [Column(nameof(StartTime), TypeName = "int")]
        public int StartTime { get; set; }
        [Required]
        [Column(nameof(FinishTime), TypeName = "int")]
        public int FinishTime { get; set; }

        public List<ProgramRegularTimings> ProgramRegularTimings { get; set; }
        public List<ProgramSpecialTimings> ProgramSpecialTimings { get; set; }
        public List<RoomAvailibilities> RoomAvailibilities { get; set; }
        public List<FacultyMemberAvailabilities> FacultyMemberAvailabilities { get; set; }
        public List<TimeSlots> TimeSlots { get; set; }        
        public string getDay(int Day)
        {
            if (Day == 1)
                return "Monday";
            else if (Day == 2)
                return "Tuesday";
            else if (Day == 3)
                return "Wednesday";
            else if (Day == 4)
                return "Thursday";
            else if (Day == 5)
                return "Friday";
            else if (Day == 6)
                return "Saturday";
            else if (Day == 7)
                return "Sunday";
            else
            {
                return "";
            }
        }
        public string getTime(int time)
        {
            string Time = "";
            var FirstTwoDigits = time.ToString();
            var lastTwoDigits = time.ToString();
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
            if (Hour.Length < 2)
                Hour = "0" + Hour;
            if (Convert.ToInt16(Hour) == 12)
            {
                return Hour + ":" + lastTwoDigits + " PM";
            }
            else
            {
                if (Convert.ToInt16(Hour) > 12)
                {
                    var temp = Convert.ToString(Convert.ToInt16(Hour) % 12);
                    if (temp.Length < 2)
                        temp = "0" + temp;
                    return temp + ":" + lastTwoDigits + " PM";
                }
                else
                    return Hour + ":" + lastTwoDigits + " AM";
            }
        }
    }
}
