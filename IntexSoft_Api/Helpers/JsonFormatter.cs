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
        
        /// <summary>
        /// This method verifies that response contains all required fields
        /// </summary>
        /// <param name="response">RestResponse value for parsing Content.</param>
        /// <param name="requiredResponseFields">Required fields for verification.</param>
        /// <param name="fieldsNotFound">Fields which weren't found.</param>
        /// <returns>Statement value.</returns>
        public static bool IsResponseContainsAllRequiredFields(RestResponse response, List<string> requiredResponseFields, out List<string> fieldsNotFound)
        {
            var statement = true;
            var notFoundFields = new List<string>();
            foreach (var field in requiredResponseFields)
            {
                try
                {
                    if (response.Content.Contains(field) != true)
                    {
                        notFoundFields.Add(field);
                        statement = false;
                    } 
                }
                catch (Exception exc)
                {
                    Console.WriteLine(exc);
                }
            }
            fieldsNotFound = notFoundFields;
            
            return statement;
        }
    }
}