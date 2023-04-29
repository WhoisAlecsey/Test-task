using System;
using System.Net.Http;
using RestSharp;

namespace IntexSoft_Api.Helper
{
    /// <summary>
    /// Contains functionality for the Api Call
    /// </summary>
    public class ApiCall
    {
        /// <summary>
        /// Execute Api Call with GET method.
        /// </summary>
        /// <param name="url">Url server value.</param>
        /// <param name="countryCode">Country code 3 letters.</param>
        /// <returns></returns>
        public static RestResponse GetResponse(string url, string countryCode)
        {
            var client = new RestClient(url);
            var request = new RestRequest();
            request.AddParameter("codes", countryCode);
            var response = client.Get(request);
            
            return response;
        }
    }
}