using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Helpers
{
    public class PagimationInfo
    {
        public int CurrentPage { get; set; }
        public int Count { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public PagimationInfo(int currentPage, int count, int totalItems, int totalPages)
        
        {
            CurrentPage = currentPage;
            Count = count;
            TotalItems = totalItems;
            TotalPages = totalPages;
        }
    }
}
