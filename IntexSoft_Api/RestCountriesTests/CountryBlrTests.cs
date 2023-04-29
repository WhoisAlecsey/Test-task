using System.Net;
using System.Threading.Tasks;
using IntexSoft_Api.Assertions;
using IntexSoft_Api.Helpers;
using IntexSoft_Api.Helpers.Constants;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntexSoft_Api.RestCountriesTests
{
    [TestClass]
    public class CountryBlrTests
    {
        private string serverUrl;
        private string countryCode;

        [TestInitialize]
        public void TestInitialize()
        {
            serverUrl = Constants.ServerUrl;
            countryCode = Constants.CountryCodes[Countries.Blr];
        }
        
        [TestMethod, Priority(1), WorkItem(8)]
        public void StatusCodeIsOk()
        {
            var response = ApiCall.GetResponse(serverUrl, countryCode);

            MultipleAssertion.AssertAll(
                () => Assert.IsNotNull(response, "Response is empty."),
                () => Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, $"Unexpected status code = '{response.StatusCode}' has been returned."));
        }
        
        [TestMethod, Priority(1), WorkItem(9)]
        public void JsonIsValid()
        {
            var response = ApiCall.GetResponse(serverUrl, countryCode);
            var countryModel = JsonHelper.DeserializeResponseCountryModel(response);

            MultipleAssertion.AssertAll(
                () => Assert.IsNotNull(countryModel, "Country Model is empty."),
                () => Assert.AreEqual(Constants.NameBlr, countryModel.Name, $"Unexpected Name = '{countryModel.Name}' has been returned."));
        }
        
        [TestMethod, Priority(1), WorkItem(10)]
        public void ResponseContainsAllRequiredFields()
        {
            var response = ApiCall.GetResponse(serverUrl, countryCode);
            var isResponseContainsAllRequiredFields = JsonHelper.IsResponseContainsAllRequiredFields(response, Constants.RequiredResponseFields, out var fieldsNotFound);

            MultipleAssertion.AssertAll(
                () => Assert.IsNotNull(response, "Response is empty."),
                () => Assert.IsTrue(isResponseContainsAllRequiredFields, $"Fields which weren't found in the response: '{string.Join(", ", fieldsNotFound.ToArray())}'."));
        }
        
        [TestMethod, Priority(2), WorkItem(11)]
        public void CountryWithBorderCodes()
        {
            var response = ApiCall.GetResponse(serverUrl, countryCode);
            var countryModel = JsonHelper.DeserializeResponseCountryModel(response);

            Assert.IsNotNull(countryModel.Borders, "Response is empty.");
        }
        
        [TestMethod, Priority(3), WorkItem(12)]
        public void CountryCodeRus_HasDefinedCountryList()
        {
            var response = ApiCall.GetResponse(serverUrl, countryCode);
            var countryModel = JsonHelper.DeserializeResponseCountryModel(response);

            MultipleAssertion.AssertAll(
                () => Assert.IsNotNull(response, "Response is empty."),
                () => CollectionAssert.AreEquivalent(Constants.BordersBlr, countryModel.Borders, $"Unexpected country code = '{string.Join(", ", countryModel.Borders.ToArray())}' was found."));
        }
        
        [TestMethod, Priority(3), WorkItem(13)]
        public void CountryCodeRus_ExistsForEachBorderCountry()
        {
            var response = ApiCall.GetResponse(serverUrl, countryCode);
            var countryModel = JsonHelper.DeserializeResponseCountryModel(response);
            var isAllCountriesContainsTheBorderCode = JsonHelper.IsAllCountriesContainsTheBorderCode(countryCode, countryModel.Borders,
                out var codesNotFound);
            
            Assert.IsTrue(isAllCountriesContainsTheBorderCode, $"Country codes which weren't found in the response: '{string.Join(", ", codesNotFound.ToArray())}'.");
        }
        
        [TestMethod, Priority(3), WorkItem(14)]
        public async Task StatusCodeAndResponse_WrongCodeParameter()
        {
            #region Data

            var notFoundMessage = "Not Found";
            var badRequestMessage = "Bad Request";

            #endregion
            
            var responseNotFound = ApiCall.GetResponse(serverUrl, "RUZ");
            var responseNotFoundModel = JsonHelper.DeserializeResponseModel(responseNotFound);
            var responseBadRequest = await ApiCall.GetAsyncResponse(serverUrl, "Russia");
            var responseBadRequestModel = JsonHelper.DeserializeResponseModel(responseBadRequest);

            MultipleAssertion.AssertAll(
                () => Assert.AreEqual(notFoundMessage, responseNotFoundModel.Message, $"Unexpected message displayed = '{responseNotFoundModel.Message}'."),
                () => Assert.AreEqual(HttpStatusCode.NotFound, responseNotFound.StatusCode, $"Unexpected status code = '{responseNotFound.StatusCode}' has been returned."),
                () => Assert.AreEqual(badRequestMessage, responseBadRequestModel.Message, $"Unexpected message displayed = '{responseBadRequestModel.Message}'."),
                () => Assert.AreEqual(HttpStatusCode.BadRequest, responseBadRequest.StatusCode, $"Unexpected status code = '{responseNotFound.StatusCode}' has been returned."));
        }
    }
}