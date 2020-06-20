using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.DTOS
{
    public class UserForRegister
    {
        [Required]
        public string UserName { get; set; }
        [Required]

        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Pass fields do not match.")]

        public string PasswordRepeat { get; set; }

        [Required]
        public string Gender { get; set; }
        [Required]
        public string KnownAs { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        public DateTime LastAcces { get; set; }

        public UserForRegister()
        {
            LastAcces = DateTime.Now;
            CreatedDate = DateTime.Now;
        }

    }
}
