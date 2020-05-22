using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.DTOS
{
    public class UserListDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }
       
        public string Gender { get; set; }
        public int Age { get; set; }
        public string KnownAs { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastAcces { get; set; }
      
        public string City { get; set; }
        public string Country { get; set; }
        public object MainPhotoUrl { get; internal set; }
    }
}
