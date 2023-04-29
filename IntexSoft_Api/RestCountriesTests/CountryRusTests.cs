using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using IntexSoft_Api.Assertions;
using IntexSoft_Api.Helpers;
using IntexSoft_Api.Helpers.Constants;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static IntexSoft_Api.Helpers.Constants.Constants;

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
        public void JsonIsValid()
        {
            var response = ApiCall.GetResponse(ServerUrl, CountryCodes[Countries.Rus]);
            var countryModel = JsonHelper.DeserializeResponseCountryModel(response);

            MultipleAssertion.AssertAll(
                () => Assert.IsNotNull(countryModel, "Country Model is empty."),
                () => Assert.AreEqual(NameRus, countryModel.Name, $"Unexpected Name = '{countryModel.Name}' has been returned."));
        }
        
        [TestMethod, Priority(1), WorkItem(3)]
        public void ResponseContainsAllRequiredFields()
        {
            var response = ApiCall.GetResponse(ServerUrl, CountryCodes[Countries.Rus]);
            var isResponseContainsAllRequiredFields = JsonHelper.IsResponseContainsAllRequiredFields(response, RequiredResponseFields, out var fieldsNotFound);

            MultipleAssertion.AssertAll(
                () => Assert.IsNotNull(response, "Response is empty."),
                () => Assert.IsTrue(isResponseContainsAllRequiredFields, $"Fields which weren't found in the response: '{string.Join(", ", fieldsNotFound.ToArray())}'."));
        }
        
        [TestMethod, Priority(2), WorkItem(4)]
        public void CountryWithBorderCodes()
        {
            #region Data

            var expectedBorders = new List<string>() { "AZE", "BLR", "CHN", "EST", "FIN", "GEO", "KAZ", "PRK", "LVA", "LTU", "MNG", "NOR", "POL", "UKR" };

            #endregion
            
            var response = ApiCall.GetResponse(ServerUrl, CountryCodes[Countries.Rus]);
            var countryModel = JsonHelper.DeserializeResponseCountryModel(response);

            MultipleAssertion.AssertAll(
                () => Assert.IsNotNull(response, "Response is empty."),
                () => CollectionAssert.AreEqual(expectedBorders, countryModel.Borders, $"Unexpected country code = '{countryModel.Borders}' was found."));
        }
        
        [TestMethod, Priority(3), WorkItem(5)]
        public void CountryCodeRus_HasDefinedCountryList()
        {
            var response = ApiCall.GetResponse(ServerUrl, CountryCodes[Countries.Rus]);
            var countryModel = JsonHelper.DeserializeResponseCountryModel(response);

            MultipleAssertion.AssertAll(
                () => Assert.IsNotNull(response, "Response is empty."),
                () => CollectionAssert.AreEquivalent(BordersRus, countryModel.Borders, $"Unexpected country code = '{countryModel.Borders}' was found."));
        }
        
        [TestMethod, Priority(3), WorkItem(6)]
        public void CountryCodeRus_ExistsForEachBorderCountry()
        {
            var response = ApiCall.GetResponse(ServerUrl, CountryCodes[Countries.Rus]);
            var countryModel = JsonHelper.DeserializeResponseCountryModel(response);
            var isAllCountriesContainsTheBorderCode = JsonHelper.IsAllCountriesContainsTheBorderCode(Countries.Rus, countryModel.Borders,
                out var codesNotFound);
            
            Assert.IsTrue(isAllCountriesContainsTheBorderCode, $"Country codes which weren't found in the response: '{string.Join(", ", codesNotFound.ToArray())}'.");
        }
        
        [TestMethod, Priority(3), WorkItem(7)]
        public async Task StatusCodeAndResponse_WrongCodeParameter()
        {
            #region Data

            var notFoundMessage = "Not Found";
            var badRequestMessage = "Bad Request";

            #endregion
            
            var responseNotFound = ApiCall.GetResponse(ServerUrl, "RUZ");
            var responseNotFoundModel = JsonHelper.DeserializeResponseModel(responseNotFound);
            var responseBadRequest = await ApiCall.GetAsyncResponse(ServerUrl, "Russia");
            var responseBadRequestModel = JsonHelper.DeserializeResponseModel(responseBadRequest);

            MultipleAssertion.AssertAll(
                () => Assert.AreEqual(notFoundMessage, responseNotFoundModel.Message, $"Unexpected message displayed = '{responseNotFoundModel.Message}'."),
                () => Assert.AreEqual(HttpStatusCode.NotFound, responseNotFound.StatusCode, $"Unexpected status code = '{responseNotFound.StatusCode}' has been returned."),
                () => Assert.AreEqual(badRequestMessage, responseBadRequestModel.Message, $"Unexpected message displayed = '{responseBadRequestModel.Message}'."),
                () => Assert.AreEqual(HttpStatusCode.BadRequest, responseBadRequest.StatusCode, $"Unexpected status code = '{responseNotFound.StatusCode}' has been returned."));
        }
    }
}