using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContosoUniversity.ViewModels.ApiVM
{
    public class StudentAgendaApiVM
    {
        public int id { get; set; }
        public string lastname { get; set; }
        public string firstname { get; set; }
        
        public List<EnrollmentApiVM> enrollments { get; set; }

        public Dictionary<int,LessonApiVM> lessonsVMs { get; set; }
    }
}