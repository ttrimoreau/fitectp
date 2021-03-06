﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContosoUniversity.BL
{
    public class ErrorMessages
    {
        #region CheckImage
        public static string ErrorSize()
        {
            return "The size of the image is limited to 100kb";
        }

        public static string ErrorExtension()
        {
            return "Image extention authorized is .png, .jpeg or .jpg";
        }
        #endregion

        #region Login
        public static string LoginMessage = "Invalid login or password.";
        #endregion

        #region Lessons

        public static string ErrorMessageSameCourse = "you have already this course";

        public static string ErrorMessageNegativeTime = "Time can't be negative";

        public static string ErrorMessageNotSameDay = "the day of the course and the start date are not same";
        #endregion

    }
}