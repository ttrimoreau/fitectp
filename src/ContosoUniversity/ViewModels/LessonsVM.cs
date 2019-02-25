using ContosoUniversity.Enum;
using ContosoUniversity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ContosoUniversity.ViewModels
{
    public class LessonsVM
    {
        public int ID { get; set; }
        public Day Day { get; set; }
        [DataType(DataType.Time)]
        public DateTime HourStart { get; set; }
        public int Duration { get; set; }

        public int CourseID { get; set; }

        public virtual Course Course { get; set; }

    }
}