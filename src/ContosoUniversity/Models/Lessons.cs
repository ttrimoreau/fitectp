using ContosoUniversity.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ContosoUniversity.Models
{

    public class Lessons
    {

        #region Properties
        [Key]
        public int ID { get; set; }
        public Day Day { get; set; }
        public int InstructorID { get; set; }
        public int CourseID { get; set; }
        [DataType(DataType.Time)]
        public DateTime HourStart { get; set; }
        public int Duration { get; set; } 
        #endregion

        #region Navigation
        public virtual Course Course { get; set; } 
        #endregion


    }
}