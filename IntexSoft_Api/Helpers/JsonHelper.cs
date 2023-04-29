using System;
using System.Collections.Generic;
using System.Linq;
using IntexSoft_Api.Helpers.Constants;
using IntexSoft_Api.Helpers.Models;
using Newtonsoft.Json;
using RestSharp;
using static IntexSoft_Api.Helpers.Constants.Constants;

namespace IntexSoft_Api.Helpers
{
    /// <summary>
    /// Contains functionality for the JsonFormatter
    /// </summary>
    public static class JsonHelper
    {
        /// <summary>
        /// Deserialize Response to Country Model
        /// </summary>
        /// <param name="response">RestResponse value for parsing Content to model.</param>
        /// <returns>CountryModel</returns>
        public static CountryModel DeserializeResponseCountryModel(RestResponse response)
        {
            CountryModel countryModel;
            try
            {
                countryModel = JsonConvert.DeserializeObject<IList<CountryModel>>(response.Content).FirstOrDefault();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
                countryModel = null;
            }

            return countryModel;
        }
        
        /// <summary>
        /// Deserialize Response to Response Model
        /// </summary>
        /// <param name="response">RestResponse value for parsing Response to model.</param>
        /// <returns>ResponseModel</returns>
        public static ResponseModel DeserializeResponseModel(RestResponse response)
        {
            var responseModel = JsonConvert.DeserializeObject<ResponseModel>(response.Content);

            return responseModel;
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

        /// <summary>
        /// This method verifies that All Countries from list contains the Border Code
        /// </summary>
        /// <param name="countryCode">Country code value.</param>
        /// <param name="bordersList">List of country codes.</param>
        /// <param name="codesNotFound">Codes which weren't found.</param>
        /// <returns>Statement value.</returns>
        public static bool IsAllCountriesContainsTheBorderCode(Countries countryCode, List<string> bordersList, out List<string> codesNotFound)
        {
            var statement = true;
            var notFoundCodes = new List<string>();
            foreach (var borderCode in bordersList)
            {
                try
                {
                    var response = ApiCall.GetResponse(ServerUrl, borderCode);
                    var countryModel = DeserializeResponseCountryModel(response);
                    if (countryModel.Borders.Contains(CountryCodes[countryCode]) != true)
                    {
                        notFoundCodes.Add(borderCode);
                        statement = false;
                    } 
                }
                catch (Exception exc)
                {
                    Console.WriteLine(exc);
                }
            }
            codesNotFound = notFoundCodes;
            
            return statement;
        }
    }
}