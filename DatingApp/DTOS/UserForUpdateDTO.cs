using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.DTOS
{
    public class UserForUpdateDTO
    {
        public string Intro { get; set; }
        public string Interests { get; set; }
        public string LookingFor { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
    }
}
