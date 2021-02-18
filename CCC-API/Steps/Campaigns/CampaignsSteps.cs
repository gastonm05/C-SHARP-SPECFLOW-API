using BoDi;
using CCC_API.Data.PostData.Settings.UserManagement;
using CCC_API.Data.Responses.Campaigns;
using CCC_API.Data.Responses.News;
using CCC_API.Services.Campaigns;
using CCC_API.Services.News;
using CCC_API.Steps.Common;
using CCC_Infrastructure.Utils;
using CCC_API.Utils.Assertion;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using TechTalk.SpecFlow;
using Is = NUnit.Framework.Is;
using CCC_API.Data.PostData.News;

namespace CCC_API.Steps.Campaigns
{
    [Binding]
    public class CampaignsSteps : AuthApiSteps
    {
        private readonly CampaignsService _campaigns;
        public const string Campaign = "Campaign", Campaigns = "Campaigns";
        private readonly IObjectContainer _objectContainer;

        public CampaignsSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
            _campaigns = new CampaignsService(SessionKey);
            var created = new List<Campaign>();
            PropertyBucket.Remember(Campaigns, created);
            _objectContainer = objectContainer;
        }

        [When(@"I POST campaign '(\d+)' times")]
        public void WhenIPerformPostToCampaigns(int times)
        {
            var created = PropertyBucket.GetProperty<List<Campaign>>(Campaigns);
            for (var i = 0; i < times; i++)
            {
                var c = new Campaign { Name = "Auto " + Guid.NewGuid(), Description = "" };
                var cp = _campaigns.PostCampaign(c);
                created.Add(cp);
            }

            PropertyBucket.Remember(Campaign, created[0]);
        }

        [When(@"I POST campaign with '(.*)' and '(.*)'")]
        public void WhenIPerformPostToCampaigns(string name, string description)
        {
            Func<string, string> assign = item =>
            {
                if (string.IsNullOrEmpty(item)) return item;
                if (item.Contains("some")) return "Auto " + Guid.NewGuid();
                var size = Convert.ToInt32(Regex.Match(item, "\\d+").Value);
                return StringUtils.RandomAlphaNumericString(size);
            };

            var c = new Campaign { Name = assign(name), Description = assign(description) };
            var cp = _campaigns.TryPostCampaign(c);
            PropertyBucket.Remember("campaign response", cp);

            // For cleaning stuff
            if (cp.StatusCode.ToString().StartsWith("2")) { }
            PropertyBucket.GetProperty<List<Campaign>>(Campaigns).Add(cp.Data);
        }

        [Then(@"I have to be given '(.*)'")]
        public void ThenIHaveToBeGivenResult(int resultCode)
        {
            var cp = PropertyBucket.GetProperty<IRestResponse<Campaign>>("campaign response");
            Assert.AreEqual(resultCode, (int)cp.StatusCode);
        }

        [Then(@"I have to be given id(s)?")]
        public void ThenShouldBeGivenId(string ids)
        {
            var campaigns = PropertyBucket.GetProperty<List<Campaign>>(Campaigns);
            Assert.IsNotEmpty(campaigns, "At least one campaign response expected");
            foreach (var campaign in campaigns)
            {
                Assert.Multiple(() =>
                {
                    Assert.IsNotNull(campaign, "Campaign was not created");
                    Assert.That(campaign.Id, Is.GreaterThan(0), "Wrong id");
                });
            }
        }

        [Then(@"I can GET campaign by id")]
        public void ThenCanGetCampaignById()
        {
            var ca = PropertyBucket.GetProperty<Campaign>(Campaign);
            var caResp = _campaigns.GetCampaign(ca.Id);
            Assert.AreEqual(ca, caResp, $"Wrong response from {CampaignsService.CampaignsUri} endpoint");
        }

        [Then(@"delete campaign by id")]
        public void ThenDeleteCampaignById()
        {
            var ca = PropertyBucket.GetProperty<Campaign>(Campaign);
            var resp = _campaigns.DeleteCampaign(ca.Id);
            Assert.AreEqual(HttpStatusCode.OK, resp.StatusCode, "Delete doesn't work");
        }

        [Then(@"I can find '(\d+)' created campaigns in campaigns list")]
        public void ThenSeeCampaignsInTheList(int size)
        {
            var caCreated = PropertyBucket.GetProperty<List<Campaign>>(Campaigns);
            var caDisplayed = _campaigns.GetCampaigns();

            Assert.AreEqual(caCreated.Count, size, "Not all the items were created");
            foreach (var createdCa in caCreated)
            {
                Assert.Contains(caDisplayed.Items, createdCa,
                    "Not all campaigns were diplayed in the list");
                Assert.That(caDisplayed.Items.Any(item => item.Name == createdCa.Name && item.Description == createdCa.Description));
            }
        }

        [Then(@"campaign is not in campaigns list")]
        public void ThenCampaignIsNotInCampaignsList()
        {
            var ca = PropertyBucket.GetProperty<Campaign>(Campaign);
            var caDisplayed = _campaigns.GetCampaigns();
            Assert.DoesNotContain(caDisplayed.Items, ca,
                "Campaign is not expected to be visible");
        }

        [Then(@"I can edit campaign")]
        public void ThenICanEditCampaign()
        {
            var caToBeEdited = PropertyBucket.GetProperty<Campaign>(Campaign);
            caToBeEdited.Name = StringUtils.RandomAlphaNumericString(50);
            caToBeEdited.Description = StringUtils.RandomAlphaNumericString(250);

            var caEdited = _campaigns.EditCampaign(caToBeEdited);
            Assert.AreEqual(caToBeEdited, caEdited, "Campaign was not edited");
            PropertyBucket.Remember(Campaigns, new List<Campaign> { caToBeEdited }, true);
        }

        [Then(@"I can delete campaign")]
        public void ThenICanDeleteCampaign()
        {
            var ca = PropertyBucket.GetProperty<Campaign>(Campaign);
            var resp = _campaigns.DeleteCampaign(ca.Id);
            Assert.AreEqual(HttpStatusCode.OK, resp.StatusCode, $"Delete failed {CampaignsService.CampaignsUri}");
        }

        [When(@"I assign news article to campaign")]
        public void ThenICanAssignNewsArticleToCampaign()
        {
            var campaignsToAdd = new List<Campaign>() { PropertyBucket.GetProperty<Campaign>(Campaign + "Assign") };
            var newsItem = PropertyBucket.GetProperty<NewsItem>(NewsViewService.NewsViewEndPoint);

            var newsItemToCampaignsPostData = new NewsItemToCampaignsAssignmentData(campaignsToAdd.Select(x=>x.Id).ToArray());
            _campaigns.SetCampaignsOnSingleNewsItem(newsItem, newsItemToCampaignsPostData);
        }
        [When(@"I add an additional campaign")]
        public void WhenIAddSecondCampaignToNewsArticle()
        {
            var campaignsToAdd = new List<Campaign>() { PropertyBucket.GetProperty<Campaign>(Campaign + "Assign"), PropertyBucket.GetProperty<Campaign>(Campaign + "Assign 2") };
            
            var newsItem = PropertyBucket.GetProperty<NewsItem>(NewsViewService.NewsViewEndPoint);

            var newsItemToCampaignsPostData = new NewsItemToCampaignsAssignmentData(campaignsToAdd.Select(x => x.Id).ToArray());
            _campaigns.SetCampaignsOnSingleNewsItem(newsItem, newsItemToCampaignsPostData);
        }

        [When("I add and remove campaigns on news article")]
        public void WhenIAddAndRemoveCampaignsOnNewsArticle()
        {
            var campaignsToAdd = new List<Campaign>() { PropertyBucket.GetProperty<Campaign>(Campaign + "Assign"), PropertyBucket.GetProperty<Campaign>(Campaign + "Assign 3") };
            var newsItem = PropertyBucket.GetProperty<NewsItem>(NewsViewService.NewsViewEndPoint);

            var newsItemToCampaignsPostData = new NewsItemToCampaignsAssignmentData(campaignsToAdd.Select(x => x.Id).ToArray());
            _campaigns.SetCampaignsOnSingleNewsItem(newsItem, newsItemToCampaignsPostData);
        }

        [Given(@"campaign '(.*)' present \(to create: '(.*)' for '(.*)'\)")]
        public void GivenCampaignPresentIToCreateFor(string campaignName, DynamicUser.PermissionType permissions, string edition)
        {
            var ca = _campaigns.GetCampaigns().Items.FirstOrDefault(_ => _.Name.Equals(campaignName));
            if (ca == null) // If not present - create
            {
                var campaign = new Campaign { Name = campaignName, Description = DateTime.Now.ToShortTimeString() };
                var sessionKey = new LoginSteps(_objectContainer, FeatureContext, ScenarioContext).GivenSharedSessionForUserWithEdition(permissions, edition);
                ca = new CampaignsService(sessionKey).PostCampaign(campaign);
            }
            PropertyBucket.Remember(Campaign + campaignName, ca, true);
        }

        [When(@"I remove all campaigns from news article")]
        public void WhenIRemoveAllCampaignsFromNewsArticle()
        {
            var newsItem = PropertyBucket.GetProperty<NewsItem>(NewsViewService.NewsViewEndPoint);

            var newsItemToCampaignsPostData = new NewsItemToCampaignsAssignmentData(new int[0]);
            _campaigns.SetCampaignsOnSingleNewsItem(newsItem, newsItemToCampaignsPostData);
        }

        /// <summary>
        /// Clean up for created campaigns.
        /// </summary>
        [AfterScenario, Scope(Feature = Campaigns)]
        public void CleanCreatedCampaigns()
        {
            var ca = PropertyBucket.GetProperty<List<Campaign>>(Campaigns);
            ca?.ForEach(campaign =>
            {
                if (campaign?.Id != null)
                    _campaigns.DeleteCampaign(campaign.Id);
            });
        }
    }
}
