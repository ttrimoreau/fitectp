using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ContosoUniversity.DAL;

namespace ContosoUniversity.BusinessLayer
{
    public static class AvatarView
    {
        static SchoolContext db = new SchoolContext();

        public static int getImage(int id)
        {
            int imageId = db.FileImages.First(x => x.PersonID == id).ID;
            return imageId;
        }

        //Return true if user has an image. Returns false if user has no image
        public static bool userHasAvatar(int id)
        {
            return db.FileImages.Any(x => x.PersonID == id);
        }

    }
}