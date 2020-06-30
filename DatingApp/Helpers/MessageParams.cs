using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Helpers
{
    public class MessageParams
    {
        public int UserId { get; set; }
        public string MessageContainer { get; set; } = "Unread";
       

        private const int MaximunPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int pageSize = 5;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = Math.Min(value, MaximunPageSize); }
        }
    }
}
