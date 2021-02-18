using BoDi;
using CCC_API.Data.Responses.Impact.CisionIDEarned;
using CCC_API.Data.Responses.Impact.Earned;
using CCC_API.Services.Impact;
using CCC_API.Steps.Common;
using CCC_API.Utils.Assertion;
using System;
using System.Linq;
using TechTalk.SpecFlow;
using static CCC_API.Data.Responses.Impact.CisionIDEarned.SearchCID;
using Is = NUnit.Framework.Is;

namespace CCC_API.Steps.Impact.EarnedMedia
{
    public class EarnedMediaSteps : AuthApiSteps
    {
        public const string RESPONSE = "response";
        public const string RESPONSE_SEARCH_CID = "responseSearchCid";

        private EarnedMediaServices _earnedMediaServices;

        public EarnedMediaSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
            _earnedMediaServices = new EarnedMediaServices(SessionKey);
        }

        [Given(@"searches are available for earned media")]
        [When(@"I call the HC search endpoint")]
        public void WhenICallTheHCSearchEndpoint()
        {
            SearchEarned response = _earnedMediaServices.GetSearches();
            PropertyBucket.Remember(RESPONSE, response);
        }

        [When(@"I call the cisionId search endpoint")]
        public void WhenICallTheCisionIdSearchEndpoint()
        {
            SearchCID responseSearchCid = _earnedMediaServices.GetSearchesCID();
            PropertyBucket.Remember(RESPONSE_SEARCH_CID, responseSearchCid);
        }

        [Then(@"the search endpoint has the correct response")]
        public void ThenTheSearchEndpointHasTheCorrectResponse()
        {
            SearchEarned response = PropertyBucket.GetProperty<SearchEarned>(RESPONSE);

            Searches[] searches = response.Searches;

            if (searches.Count() == 0) Assert.Ignore("There are not search to verify");

            Assert.That(response.TotalUrlLimit, Is.GreaterThan(0), "TotalUrlLimit is not correct");
            Assert.That(response.PercentageUrlUsed, Is.GreaterThan(0), "PercentageUrlUsed is not correct");
            Assert.True(response.SearchLimit >= 1, "SearchLimit is not correct");

            foreach (var search in searches)
            {
                Assert.That(search.Id, Is.GreaterThan(0), "Id is not correct");
                Assert.That(search.Name, Is.Not.Null.Or.Empty, "Name is not correct");
                Assert.True(search.WebEvents >= 0, "WebEvents is not correct");
                Assert.True(search.URLs >= 0, "URLs is not correct");
                Assert.True(search.Views >= 0, "Views is not correct");
            }
        }

        // <summary>
        /// Gets the sum of TotalURLCount for all seaches
        /// </summary>
        /// <returns sumOfUrls></returns>
        public double GetSumOfURLs()
        {
            SearchCID searchCIDresponse = PropertyBucket.GetProperty<SearchCID>(RESPONSE_SEARCH_CID);
            DataList[] searchCIDList = searchCIDresponse.Data;
            double sumOfUrls = 0;

            foreach (var search in searchCIDList)
            {
                sumOfUrls = sumOfUrls + search.TotalURLCount;
            }
            return sumOfUrls;
        }

        [Then(@"both retrieved data for searches match")]
        public void ThenBothRetrievedDataForSearchesMatch()
        {
            SearchEarned response = PropertyBucket.GetProperty<SearchEarned>(RESPONSE);
            SearchCID searchCIDresponse = PropertyBucket.GetProperty<SearchCID>(RESPONSE_SEARCH_CID);

            Searches[] searchHCList = response.Searches;
            DataList[] searchCIDList = searchCIDresponse.Data;

            Assert.That(searchHCList.Count() == searchCIDList.Count(), "Data does not match");

            if (searchHCList != null)
            {
                for (int i = 0; i < searchHCList.Count(); i++)
                {
                    double percentageOfUrls = searchCIDList[i].TotalURLCount * 100 / GetSumOfURLs();
                    string SeachNameHC = searchHCList[i].Name.Replace(" ", "");
                    Assert.That(searchHCList[i].Id == searchCIDList[i].SearchId, "The value ID is not the same");
                    Assert.That(Math.Round(percentageOfUrls, 2) == searchHCList[i].URLs, "The percentage of view is correct");
                    Assert.That(searchCIDList[i].SearchName.Contains(SeachNameHC), "The SearchName is not the same");
                }
            }
            else
            {
                Assert.That(searchCIDList.Count() == 0, "The server did not retrieve any data properly");
            }
        }
    }
}
