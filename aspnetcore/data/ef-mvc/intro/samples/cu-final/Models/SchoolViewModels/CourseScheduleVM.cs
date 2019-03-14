using System;
using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity.Models.SchoolViewModels
{
    public class CourseScheduleVM
    {
        public int CourseScheduleID { get; set; }

        public string CourseName { get; set; }
        public string InstructorName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "上课日期")]
        public DateTime ScheduleDate { get; set; }
        public string DayOfWeek
        {
            get
            {
                return System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(ScheduleDate.DayOfWeek);
            }
        }
    }
}
