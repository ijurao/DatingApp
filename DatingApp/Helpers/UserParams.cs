using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Helpers
{
    public class UserParams
    {
		private const int MaximunPageSize = 50;
        public int PageNumber { get; set; } = 1;
		private int pageSize = 5 ;

		public int PageSize
		{
			get { return pageSize; }
			set { pageSize = Math.Min(value, MaximunPageSize); }
		}

		public int UserApplicationId { get; set; }
		public string Gender { get; set; }
		public int MinAge { get; set; } = 18;
		public int MaxAge { get; set; } = 99;
		public string OrderBy { get; set; }
		public bool Likees { get; set; } = false;
		public bool Likers { get; set; } = false;



	}
}
