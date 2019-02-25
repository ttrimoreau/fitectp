using ContosoUniversity.DAL;
using ContosoUniversity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContosoUniversity.BusinessLayer
{
    public class UserSession
    {
        private SchoolContext db = new SchoolContext();
        public SchoolContext DbContext
        {
            get { return db; }
            set { db = value; }
        }

        public Person GetPersonByUserName(string login, SchoolContext db)
        {
            try
            {
                Person person = db.People.FirstOrDefault(p => p.UserName == login);
                return person;
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}