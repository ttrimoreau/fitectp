using ContosoUniversity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ContosoUniversity.ViewModels
{
    public class StudentApiVM
    {
        public int id { get; set; }
        public string lastname { get; set; }
        public string firstname { get; set; }
        public string enrollmentDate { get; set; }

        public List<EnrollmentApiVM> enrollments { get; set; }

    }
}