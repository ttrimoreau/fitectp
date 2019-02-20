using ContosoUniversity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ContosoUniversity.ViewModels
{
    public class LoginVM
    {
        [Required]
        [MinLength(3, ErrorMessage = "Username  must be at least 3 characters long.")]
        [MaxLength(15, ErrorMessage = "Username cannot be longer than 15 characters.")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(64, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Password { get; set; }

        public Person Person { get; set; }
        public bool Authentified { get; set; }
    }
}