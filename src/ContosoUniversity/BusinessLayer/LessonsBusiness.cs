using ContosoUniversity.DAL;
using ContosoUniversity.Models;
using ContosoUniversity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContosoUniversity.BusinessLayer
{
    public class LessonsBusiness
    {
        private SchoolContext db;
        public LessonsBusiness()
        {
            db = new SchoolContext();
        }

        public List<Course> CourseList()
        {
            return db.Courses.OrderBy(c => c.Title).ToList();
        }

        public IQueryable<Lessons> ListeFiltreLesson(int? listCourse, int courseId)
        {
            return db.Lessons
                .Where(c => !listCourse.HasValue || c.Course.CourseID == courseId)
                .OrderBy(c => c.ID);
        }
        public void AddLesson(LessonsVM lessonVM)
        {
            Lessons lesson = new Lessons
            {
                Day = lessonVM.Day,
                CourseID = lessonVM.CourseID,
                Duration = lessonVM.Duration,
                HourStart = lessonVM.HourStart,
                InstructorID = lessonVM.InstructorID
            };
            db.Lessons.Add(lesson);
            db.SaveChanges();
        }
     
        public Lessons FindLesson(int? id)
        {
            return db.Lessons.Find(id);

        }
    }
}