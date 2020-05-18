using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Models
{
    public class Error
    {
         public string Code { get; set; }
         public string Message { get; set; }
        public Error(string code,string message)
        {
            Code = code;
            Message = message;
        }
    }
}
