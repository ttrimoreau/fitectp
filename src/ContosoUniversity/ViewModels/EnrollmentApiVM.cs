using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContosoUniversity.ViewModels
{
    public class EnrollmentApiVM
    {
        public int CourseId { get; set; }

        // Override equals (to be able to compare the lists of EnrollmentApiVM in the tests using AreEqual)
        public override bool Equals(object obj)
        {
            // Do not cast, compare types as well !!!
            var toCompareWith = obj as EnrollmentApiVM;
            if (toCompareWith == null)
                return false;
            return this.CourseId == toCompareWith.CourseId;
        }
    }
}