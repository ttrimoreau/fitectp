using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContosoUniversity.ViewModels.ApiVM
{
    public class LessonApiVM
    {
        public int courseId { get; set; }
        public string day { get; set; }
        public string startHour { get; set; }
        public string duration { get; set; }
    }
}