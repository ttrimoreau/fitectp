using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContosoUniversity.ViewModels
{
    public class EnrollmentApiVM
    {
        public int courseId { get; set; }

        // Override equals (to be able to compare the lists of EnrollmentApiVM in the tests using AreEqual)
        public override bool Equals(object obj)
        {   if (!(obj is EnrollmentApiVM))
                return false;
            var toCompareWith = obj as EnrollmentApiVM;
            // Not sure if this "if" is needed
            if (toCompareWith == null)
                  return false;
            
            return this.courseId == toCompareWith.courseId;
        }
    }
}