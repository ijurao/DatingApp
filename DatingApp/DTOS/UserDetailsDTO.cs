﻿using DatingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.DTOS
{
    public class UserDetailsDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }
       
        public string Gender { get; set; }
        public int Age { get; set; }
        public string KnownAs { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastAcces { get; set; }
        public string Intro { get; set; }
        public string LookingFor { get; set; }
        public string Interests { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string MainPhotoUrl { get; set; }
        public List<UserPhotoDTO> Photos { get; set; }

    }
}
