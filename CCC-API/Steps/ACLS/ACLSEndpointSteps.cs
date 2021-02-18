using BoDi;
using CCC_API.Data.Responses.ACLS;
using CCC_API.Data.SpecFlowTableInput;
using CCC_API.Services.ACLS;
using CCC_API.Steps.Common;
using CCC_Infrastructure.Utils;
using CCC_API.Utils.Assertion;
using RestSharp;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace CCC_API.Steps.ACLS
{
    [Binding]
    public class ACLSEndpointSteps : AuthApiSteps
    {
        public static string GET_RESPONSE_ACLS_KEY = "GetResponseACLSKey";
        private const string INVALID_SESSION_KEY = "eyJraWQiOiJzMSIsImFsZyI6IlJTMjU2In0.eyJpc3MiOiJodHRwczpcL1wvYXV0aC1kZXYuYXBwLmNpc2lvbi5jb21cL2MyaWQiLCJzY3AiOlsidmkiLCJlbHkiXSwic3ViIjoiMTA0OHxhcGlpbnRlcm5hbHxSZWFkT25seVVzZXIiLCJleHAiOjE0OTgwNTIzNDMsImRhdCI6eyJFbHlzaXVtU2Vzc2lvbktleSI6IjF4TFd4Wmd3OUw2VnJxdmlheGhqU3MwQ1o1R1ZNOXYzVW05TURmMXlRK3ZvM3NVUzdaNTMxRVowTWxEaVRUSGRRaDhLU1E0T0hUS1JNWkdmamhqOFZXRWhcL1hCb1NoZVo0VndcL0dzS1JnSVwvbmk4SlE5Z3cyNkg1ZEhTekl4YW9pIiwiVjNBcGlLZXkiOiIifSwiY2lkIjoieGF2bjdqYXN3bXM2dSJ9.PkjFEgiCxM8WhxAFIRrQ1HQJNZt3du7Yc60tWrQnsL--E5L8S2lD91iTcna1Bc0GtgJiXyxd9PeVaYKeSMqpqglAIdkHv0wmEhKk-UgaVmfaok_qAZFOZ_iCJVljQcjcE4AdmJMldLDdI2RVW2jUXKzAGwK_fMsinxc5ki6syTw";

        public ACLSEndpointSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
        }


        /// <summary>
        /// Performs a GET to endpoint api/v1/acls
        /// </summary>
        [When(@"I perform a GET ACLS permissions")]
        public void WhenIPerformAGETForCustomFieldACLS()
        {
            ACLSService service = new ACLSService(SessionKey);
            var response = service.GetACLS();
            PropertyBucket.Remember(GET_RESPONSE_ACLS_KEY, response);
        }


        /// <summary>
        /// Validates the Status Code Response from an endpoint
        /// </summary>
        [Then(@"ACLS Endpoint response should be '(.*)'")]
        public void ThenTheEndpointResponseShouldBeAsExpected(int responseCode)
        {
            IRestResponse<ACLSView> response = PropertyBucket.GetProperty<IRestResponse<ACLSView>>(GET_RESPONSE_ACLS_KEY);
            Assert.AreEqual(responseCode, Services.BaseApiService.GetNumericStatusCode(response), Err.Line(response.Content));
        }

        [Then(@"ACLS permissions for (.*) should be:")]
        public void ThenPermissionShouldBeForPropertyFromSection(string section, Table table)
        {
            IRestResponse<ACLSView> response = PropertyBucket.GetProperty<IRestResponse<ACLSView>>(GET_RESPONSE_ACLS_KEY);
            var properties = table.CreateSet<ACLSProperty>();
            foreach (var property in properties)
            {
                Assert.AreEqual(property.Value, (property.GetValue(response.Data, section)), Err.Line($"{property.Property} {property.Permission} Value: '{property.Value}' did not matched."));
            }
        }
    }
}
