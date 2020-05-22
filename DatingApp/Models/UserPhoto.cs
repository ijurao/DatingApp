using System;

namespace DatingApp.Models
{
    public class UserPhoto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsMain { get; set; }

        public int UserApplicationId { get; set; }
        public UserApplication user { get; set; }
    }
}