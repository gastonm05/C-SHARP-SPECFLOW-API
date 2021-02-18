using BoDi;
using CCC_API.Data.Responses.Settings.WireDistribution;
using CCC_API.Services.Settings.WireDistribution;
using CCC_API.Steps.Common;
using CCC_Infrastructure.Utils;
using Newtonsoft.Json;
using CCC_API.Utils.Assertion;
using RestSharp;
using System;
using System.Net;
using TechTalk.SpecFlow;

namespace CCC_API.Steps.Settings.WireDistribution
{
    [Binding]
    public class WireDistributionSteps : AuthApiSteps
    {
        public WireDistributionSteps(IObjectContainer objectContainer) : base(objectContainer) { }
        public enum ImpactStartDateType { DEFAULT, NINETYDAYS }
        public const int AMOUNT_DAYS_FOR_IMPACT_START_DATE_PRIOR = -90;
        private const string GET_WIRE_DISTRIBUTION_RESPONSE_KEY = "GetWireDistributionResponse";
        private const string POST_WIRE_DISTRIBUTION_RESPONSE_KEY = "PostWireDistributionResponse";
        private const string WIREDISTRIBUTION = "WireDistribution";

        [When(@"I get current WireDistribution configuration")]
        public void WhenIGetCurrentWireDisrtributionConfiguration()
        {
            var response = new WireDistributionAccountService(SessionKey).GetCurrentWireDistributionConfiguration();
            PropertyBucket.Remember(GET_WIRE_DISTRIBUTION_RESPONSE_KEY, response);
        }
        [When(@"I update current WireDistribution configuration")]
        public void WhenIUpdateCurrentWireDisrtributionConfiguration()
        {
            var response = new WireDistributionAccountService(SessionKey).GetCurrentWireDistributionConfiguration();
            PropertyBucket.Remember(GET_WIRE_DISTRIBUTION_RESPONSE_KEY, response);
        }
        [When(@"I set WireDistribution configuration with multiple Oracle IDs, one for each DataGroup")]
        public void WhenISetWireDistributionConfigurationWithMultipleOracleIDsOneForEachDataGroup()
        {
            IRestResponse<WireDistributionConfig> getWireDistributionConfig = PropertyBucket.GetProperty<IRestResponse<WireDistributionConfig>>(GET_WIRE_DISTRIBUTION_RESPONSE_KEY);
            var currentWireDistributionConfig = JsonConvert.DeserializeObject<WireDistributionConfig>(getWireDistributionConfig.Content);
            foreach (DataGroupWireDistributionConfig dataGroupWireDistributionConfig in currentWireDistributionConfig.DataGroupWireDistributionAccounts)
            {
                if (dataGroupWireDistributionConfig.Disabled)
                {
                    dataGroupWireDistributionConfig.WireDistributionAccountId = "TestOracleIDDataGroupLevel";
                    dataGroupWireDistributionConfig.Enabled = true;
                }
            }
            var response = new WireDistributionAccountService(SessionKey).SetCurrentWireDistributionConfiguration(currentWireDistributionConfig);
            PropertyBucket.Remember(POST_WIRE_DISTRIBUTION_RESPONSE_KEY, response);
        }
        [When(@"I set WireDistribution configuration with only one Oracle ID for all DataGroup with View All for all data groups")]
        public void WhenISetWireDistributionConfigurationWithOnlyOneOracleIDForAllDataGroupWithViewAllForAllDataGroups()
        {
            IRestResponse<WireDistributionConfig> getWireDistributionConfig = PropertyBucket.GetProperty<IRestResponse<WireDistributionConfig>>(GET_WIRE_DISTRIBUTION_RESPONSE_KEY);
            var currentWireDistributionConfig = JsonConvert.DeserializeObject<WireDistributionConfig>(getWireDistributionConfig.Content);
            foreach (DataGroupWireDistributionConfig dataGroupWireDistributionConfig in currentWireDistributionConfig.DataGroupWireDistributionAccounts)
            {
                if (dataGroupWireDistributionConfig.Disabled)
                {
                    dataGroupWireDistributionConfig.WireDistributionAccountId = "TestOracleIDDataGroupLevel";
                    dataGroupWireDistributionConfig.Enabled = true;
                    dataGroupWireDistributionConfig.ViewAll = true;
                }
            }
            var response = new WireDistributionAccountService(SessionKey).SetCurrentWireDistributionConfiguration(currentWireDistributionConfig);
            PropertyBucket.Remember(POST_WIRE_DISTRIBUTION_RESPONSE_KEY, response);
        }

        [When(@"I set WireDistribution configuration with only one Oracle ID for all DataGroup")]
        public void WhenISetWireDistributionConfigurationWithOnlyOneOracleIDForAllDataGroup()
        {
            ResetWireDistributionConfigurationForNextTest();
        }
        [When(@"I set WireDistribution configuration with only one Oracle ID for all DataGroup with View All for all data groups and '(.*)' date as Impact Start Date")]
        public void WhenISetWireDistributionConfigurationWithOnlyOneOracleIDForAllDataGroupWithViewAllForAllDataGroupsAndDateAsImpactStartDate(ImpactStartDateType impactStartDateType)
        {
            IRestResponse<WireDistributionConfig> getWireDistributionConfig = PropertyBucket.GetProperty<IRestResponse<WireDistributionConfig>>(GET_WIRE_DISTRIBUTION_RESPONSE_KEY);
            var currentWireDistributionConfig = JsonConvert.DeserializeObject<WireDistributionConfig>(getWireDistributionConfig.Content);
            switch (impactStartDateType)
            {
                case ImpactStartDateType.DEFAULT:
                    {
                        currentWireDistributionConfig.ImpactStartDateDefaultEnabled = true;
                        break;
                    }
                case ImpactStartDateType.NINETYDAYS:
                    {
                        currentWireDistributionConfig.ImpactStartDateDefaultEnabled = false;
                        break;
                    }
            }
            var response = new WireDistributionAccountService(SessionKey).SetCurrentWireDistributionConfiguration(currentWireDistributionConfig);
            PropertyBucket.Remember(POST_WIRE_DISTRIBUTION_RESPONSE_KEY, response);
        }


        [Then(@"the response returns a valid WireDistribution configuration")]
        public void ThenTheResponseReturnsAValidWireDisrtributionConfiguration()
        {
            var responseGet = PropertyBucket.GetProperty<IRestResponse<WireDistributionConfig>>(GET_WIRE_DISTRIBUTION_RESPONSE_KEY);
            Assert.AreEqual((int)HttpStatusCode.OK, (int)responseGet.StatusCode, Err.Line(responseGet.Content));
            Assert.IsNotNull(responseGet.Data.DataGroupWireDistributionAccounts[0].DataGroupId, Err.Msg("DataGroup is null"));
        }
        [Then(@"the response returns a '(.*)' message")]
        public void ThenTheResponseReturnsAMessage(string expectedMessage)
        {
            var responseGet = PropertyBucket.GetProperty<IRestResponse<WireDistributionConfig>>(GET_WIRE_DISTRIBUTION_RESPONSE_KEY);
            Assert.AreEqual((int)HttpStatusCode.Forbidden, (int)responseGet.StatusCode, Err.Line("Status code was different from expected"));
            Assert.IsTrue(responseGet.Content.Contains(expectedMessage), Err.Msg("Message differs from expected message"));
        }

        [Then(@"I should see the proper WireDistribution response")]
        public void ThenIShouldSeeTheProperWireDistributionResponse()
        {
            var responsePost = PropertyBucket.GetProperty<IRestResponse<WireDistributionConfig>>(POST_WIRE_DISTRIBUTION_RESPONSE_KEY);
            Assert.AreEqual((int)HttpStatusCode.OK, (int)responsePost.StatusCode, Err.Line("Status code was different from expected"));
        }

        [Then(@"I should see the proper WireDistribution response for ImpactStartDateDefaultEnabled property which should be '(.*)'")]
        public void ThenIShouldSeeTheProperWireDistributionResponseforImpactStartDateDefaultEnabled(bool impactStartDateDefaultEnabledExpectedValue)
        {
            var responsePost = PropertyBucket.GetProperty<IRestResponse<WireDistributionConfig>>(POST_WIRE_DISTRIBUTION_RESPONSE_KEY);
            Assert.AreEqual((int)HttpStatusCode.OK, (int)responsePost.StatusCode, Err.Line("Status code was different from expected"));
            if (impactStartDateDefaultEnabledExpectedValue)
            {
                Assert.IsTrue(responsePost.Data.ImpactStartDateDefaultEnabled, Err.Msg("ImpactStartDateDefaultEnabled should be True"));
            }
            else
            {
                Assert.IsFalse(responsePost.Data.ImpactStartDateDefaultEnabled, Err.Msg("ImpactStartDateDefaultEnabled should be False"));
            }
        }

        [AfterScenario, Scope(Feature = WIREDISTRIBUTION, Tag = "NeedsCleanupWireDistribution")]
        public void ResetWireDistributionConfigurationForNextTest()
        {
            IRestResponse<WireDistributionConfig> getWireDistributionConfig = PropertyBucket.GetProperty<IRestResponse<WireDistributionConfig>>(GET_WIRE_DISTRIBUTION_RESPONSE_KEY);
            var currentWireDistributionConfig = JsonConvert.DeserializeObject<WireDistributionConfig>(getWireDistributionConfig.Content);
            foreach (DataGroupWireDistributionConfig dataGroupWireDistributionConfig in currentWireDistributionConfig.DataGroupWireDistributionAccounts)
            {
                if (dataGroupWireDistributionConfig.Enabled)
                {
                    dataGroupWireDistributionConfig.Enabled = false;
                    dataGroupWireDistributionConfig.ViewAll = false;
                }
            }
            var response = new WireDistributionAccountService(SessionKey).SetCurrentWireDistributionConfiguration(currentWireDistributionConfig);
        }
    }
}
