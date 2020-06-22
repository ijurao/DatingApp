using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DatingApp.Helpers
{
    public static class Extensions
    {
        public static void addAplicationError(this HttpResponse response, string message)
        {
            response.Headers.Add("Application-Error", message);
            response.Headers.Add("Acces-Control-Expose-Headers", "Application-Error");
            response.Headers.Add("Acces-Control-Allow-Origin", "*");
        }

        public static int Age(this DateTime date)
        {
            var age = DateTime.Now.Year - date.Year;
            return age;

        }

        public static void AddPaginationInfo(this HttpResponse response, int currentPage, int count, int totalItems, int totalPages)
        {
            var paginationHeader = new PagimationInfo(currentPage, count, totalItems, totalPages);
            var camelCaseFormter = new JsonSerializerSettings();
            camelCaseFormter.ContractResolver = new CamelCasePropertyNamesContractResolver();
         
            response.Headers.Add("Acces-Control-Expose-Headers", "Pagination");

            response.Headers.Add("Pagination", JsonConvert.SerializeObject(paginationHeader, camelCaseFormter));


        }
    }
}
