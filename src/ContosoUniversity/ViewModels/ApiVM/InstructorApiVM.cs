using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContosoUniversity.ViewModels.ApiVM
{
    public class InstructorApiVM
    {
        public int instructorId { get; set; }

        public List<LessonApiVM> schedule { get; set; }
    }
}