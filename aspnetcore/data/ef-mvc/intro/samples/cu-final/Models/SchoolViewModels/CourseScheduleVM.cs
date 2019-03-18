using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity.Models.SchoolViewModels
{
    public class CourseScheduleVM
    {
        public int CourseScheduleID { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int InstructorId { get; set; }
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

    public class CourseScheduleVMCompare : IEqualityComparer<CourseScheduleVM>
    {
        public bool Equals(CourseScheduleVM x, CourseScheduleVM y)
        {
            return (x.CourseId == y.CourseId) && (x.InstructorId == y.InstructorId) && (x.ScheduleDate.Date == y.ScheduleDate);
        }

        public int GetHashCode(CourseScheduleVM obj)
        {
            return obj.CourseId.GetHashCode()+obj.InstructorId.GetHashCode()+obj.ScheduleDate.Date.GetHashCode();
        }
    }
}
