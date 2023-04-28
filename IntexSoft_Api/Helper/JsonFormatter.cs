using System;
using System.Collections.Generic;
using System.Linq;
using IntexSoft_Api.Models;
using Newtonsoft.Json;
using RestSharp;

namespace IntexSoft_Api.Helper
{
    /// <summary>
    /// Contains functionality for the JsonFormatter
    /// </summary>
    public static class JsonFormatter
    {
        /// <summary>
        /// Deserialize Response to Country Model
        /// </summary>
        /// <param name="response">RestResponse value for parsing Content to model.</param>
        /// <returns>CountryModel</returns>
        public static CountryModel DeserializeResponse(RestResponse response)
        {
            var countryModel = JsonConvert.DeserializeObject<IList<CountryModel>>(response.Content).FirstOrDefault();

            return countryModel;
        }
    }
}