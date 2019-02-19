using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ContosoUniversity.Models
{

    public enum FileType
    {

    }
    public class FileImage
    {

        [Key]
        public int ID { get; set; }

        public FileType FileType { get; set; }

        public string ContentType { get; set; }

        public byte[] Content { get; set; }
        public int PersonID { get; set; }

        public virtual Person Person { get; set; }

    }
}