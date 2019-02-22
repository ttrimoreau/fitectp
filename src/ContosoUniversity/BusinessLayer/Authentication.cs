using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using ContosoUniversity.ViewModels;
using ContosoUniversity.Models;
using ContosoUniversity.DAL;

namespace ContosoUniversity.BusinessLayer
{

    public static class Authentication
    {
        public const string SALT = "Contoso";
        private static SchoolContext db = new SchoolContext();

        public static string UserNameFromId(int id)
        {
            return db.People.Find(id).UserName;
        }

        public static void CreatePerson(RegisterVM vm)
        {

            switch (vm.PersonRole)
            {
                case Role.Student:
                    CreateStudent(vm);
                    break;
                case Role.Instructor:
                    CreateInstructor(vm);
                    break;
                default:
                    throw new NotImplementedException("Attempting to register a non-Student / non-Instructor");
            }
        }

        public static void CreateStudent(RegisterVM vm)
        {
            Student p = new Student();
            //EnrollmentDate is particular to Student
            p.EnrollmentDate = DateTime.Now;
            p.Password = SaltAndHash(vm.Password); //salt and hash password before saving in database
            p.Email = vm.Email;
            p.FirstMidName = vm.FirstMidName;
            p.LastName = vm.LastName;
            p.UserName = vm.UserName;
            db.Students.Add(p);
            db.SaveChanges();
        }

        public static void CreateInstructor(RegisterVM vm)
        {
            Instructor p = new Instructor();
            //HireDate is particular to Instructor
            p.HireDate = vm.HireDate;
            p.Password = SaltAndHash(vm.Password); //salt and hash password before saving in database
            p.Email = vm.Email;
            p.FirstMidName = vm.FirstMidName;
            p.LastName = vm.LastName;
            p.UserName = vm.UserName;
            db.Instructors.Add(p);
            db.SaveChanges();
        }



        //Returns the salted and hashed string of inputString (usual use: salted and hashed password)
        public static string SaltAndHash(string inputString)
        {
            string SaltedString = string.Concat(SALT, inputString);
            return sha256_hash(SaltedString);
        }

        //Returns the SHA256 hash of a given input string
        public static string sha256_hash(string value)
        {
            StringBuilder Sb = new StringBuilder();

            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(value));

                foreach (Byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }

    }
}