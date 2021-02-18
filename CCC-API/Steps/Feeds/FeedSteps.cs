using BoDi;
using CCC_API.Data.TestDataObjects;
using CCC_API.Services.Feeds;
using CCC_API.Steps.Common;
using Newtonsoft.Json;
using CCC_API.Utils.Assertion;
using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;
using Is = NUnit.Framework.Is;

namespace CCC_API.Steps.Feeds
{
    [Binding]
    public sealed class FeedSteps : AuthApiSteps
    {
        public const string COMPANIES_FEED_KEY = "companyFeeds";

        public FeedSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
        }

        [When("I have the news feeds for each company under test")]
        public void WhenIHaveTheNewsFeedsForEachCompanyUnderTest()
        {
            var companies = JsonConvert.DeserializeObject<List<CompanyFeedsModel>>
                (JsonConvert.SerializeObject(PropertyBucket.GetProperty("companies")));
            PropertyBucket.Remember(COMPANIES_FEED_KEY, companies);
        }


        [Then("news was ingested for each feed")]
        public void ThenNewsWasIngestedForEachFeed()
        {
            var companies = PropertyBucket.GetProperty<List<CompanyFeedsModel>>(COMPANIES_FEED_KEY);
            var now = DateTime.Now;
            
            Assert.Multiple(() =>
            {
                foreach (var company in companies)
                {
                    using (var feedsDbService = new FeedsDbService(company.CompanyId))
                    {
                        foreach (var feed in company.Feeds)
                        {
                            var count = feedsDbService.GetNewsCount(feed.Id, now.AddDays(-7), now);
                            Assert.That(count, Is.GreaterThan(0), $"Company '{company.CompanyId}' feed '{feed.Name}' did not ingest news within the past 48 hours.");
                        }
                    }
                }
            });
        }
    }
}
