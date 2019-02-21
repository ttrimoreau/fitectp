using ContosoUniversity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ContosoUniversity.ViewModels
{
    public class CoursesNotEnrolledVM
    {
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}