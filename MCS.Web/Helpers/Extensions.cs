// ======================================



// 

// ======================================

using MCS.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace MCS.Web.Helpers
{
    public static class Extensions
    {
        public static void AddPagination(this HttpResponse response, int currentPage, int itemsPerPage, int totalItems, int totalPages)
        {
            var paginationHeader = new PageHeader(currentPage, itemsPerPage, totalItems, totalPages);

            response.Headers.Add("Pagination", JsonConvert.SerializeObject(paginationHeader));
            // CORS
            //response.Headers.Add("access-control-expose-headers", "Pagination");
        }

        public static void AddApplicationError(this HttpResponse response, string message)
        {
            response.Headers.Add("Application-Error", message);
            // CORS
            //response.Headers.Add("access-control-expose-headers", "Application-Error");
        }

        public static async Task<HttpResponseMessage> PutAsJsonAsync(this HttpClient client, string addr, object obj)
        {
            var response = await client.PutAsync(addr, new StringContent(
                    Newtonsoft.Json.JsonConvert.SerializeObject(obj),
                    Encoding.UTF8, "application/json"));

            return response;
        }

        public static async Task<HttpResponseMessage> PostAsJsonAsync(this HttpClient client, string addr, object obj)
        {
            var response = await client.PostAsync(addr, new StringContent(
                    Newtonsoft.Json.JsonConvert.SerializeObject(obj),
                    Encoding.UTF8, "application/json"));

            return response;
        }
    }
}
