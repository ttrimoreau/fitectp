using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace ContosoUniversity.BusinessLayer
{
    public static class Authentication
    {
        public const string SALT = "Contoso";

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