using ContosoUniversity.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContosoUniversity.BusinessLayer
{
    public class UsernameAvailable
    {
        public SchoolContext db = new SchoolContext();
        public bool UsernameIsAvailable(string username)
        {
                int countUsername = db.People.Where(c => c.UserName == username).Count();

            return (countUsername == 0);
        }
    }
}