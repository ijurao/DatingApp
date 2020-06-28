using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Models
{
    public class Like

    {
        public int LikerId { get; set; }
        public int LikeeId { get; set; }
        public UserApplication Liker { get; set; }
        public  UserApplication  Likee{ get; set; }
    }
}

