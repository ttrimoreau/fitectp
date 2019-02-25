using ContosoUniversity.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ContosoUniversity.BusinessLayer
{   
    public class UsernameAvailable
    {
        public SchoolContext db = new SchoolContext();

        public bool UsernameIsAvailable(string username)
        {

            var prevUser = db.People.Where(c => c.UserName == username).FirstOrDefault();
            if (prevUser == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}