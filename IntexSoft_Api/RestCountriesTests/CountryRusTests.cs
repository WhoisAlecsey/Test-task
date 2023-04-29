using System;
using System.Collections.Generic;
using System.Net;
using IntexSoft_Api.Assertions;
using IntexSoft_Api.Constants;
using IntexSoft_Api.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static IntexSoft_Api.Constants.Constants;

namespace IntexSoft_Api.RestCountriesTests
{
    [TestClass]
    public class CountryRusTests
    {
        [TestMethod, Priority(1), WorkItem(1)]
        public void StatusCodeIsOk()
        {
            var response = ApiCall.GetResponse(ServerUrl, CountryCodes[Countries.Rus]);

            MultipleAssertion.AssertAll(
                () => Assert.IsNotNull(response, "Response is empty."),
                () => Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, $"Unexpected status code = '{response.StatusCode}' has been returned."));
        }
        
        [TestMethod, Priority(1), WorkItem(2)]
        public void ResponseContainsAllRequiredFields()
        {
            var response = ApiCall.GetResponse(ServerUrl, CountryCodes[Countries.Rus]);
            var isResponseContainsAllRequiredFields = JsonFormatter.IsResponseContainsAllRequiredFields(response, RequiredResponseFields, out var fieldsNotFound);

            MultipleAssertion.AssertAll(
                () => Assert.IsNotNull(response, "Response is empty."),
                () => Assert.IsTrue(isResponseContainsAllRequiredFields, $"Fields which weren't found in the response: '{String.Join(", ", fieldsNotFound.ToArray())}'."));
        }
        
        [TestMethod, Priority(2), WorkItem(3)]
        public void CountryWithBorderCodes()
        {
            #region Data

            var expectedBorders = new List<string>() { "AZE", "BLR", "CHN", "EST", "FIN", "GEO", "KAZ", "PRK", "LVA", "LTU", "MNG", "NOR", "POL", "UKR" };

            #endregion
            
            var response = ApiCall.GetResponse(ServerUrl, CountryCodes[Countries.Rus]);
            var countryModel = JsonFormatter.DeserializeResponse(response);

            MultipleAssertion.AssertAll(
                () => Assert.IsNotNull(response, "Response is empty."),
                () => CollectionAssert.AreEqual(expectedBorders, countryModel.Borders, $"Unexpected country code = '{countryModel.Borders}' was found."));
        }
    }
}