using System;
using RestSharp;

namespace IntexSoft_Api.Helpers
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
            RestResponse response;
            try
            {
                var client = new RestClient(url);
                var request = new RestRequest();
                request.AddParameter("codes", countryCode); 
                response = client.Get(request);
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
                response = null;
            }
            
            return response;
        }
    }
}