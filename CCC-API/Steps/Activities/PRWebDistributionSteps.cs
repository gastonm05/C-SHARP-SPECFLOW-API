using BoDi;
using CCC_API.Data.PostData.Activities;
using CCC_API.Data.PostData.Analytics;
using CCC_API.Data.Responses.Activities;
using CCC_API.Data.TestDataObjects;
using CCC_API.Data.TestDataObjects.Activities;
using CCC_API.Services.Activities;
using CCC_API.Services.Common;
using CCC_API.Services.UserParameters;
using CCC_API.Steps.Common;
using CCC_Infrastructure.Utils;
using Newtonsoft.Json;
using CCC_API.Utils.Assertion;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using TechTalk.SpecFlow;
using Is = NUnit.Framework.Is;
using ZukiniWrap;

namespace CCC_API.Steps.Activities
{
    [Binding]
    public class PRWebDistributionSteps : AuthApiSteps
    {
        private PRWebDistributionService _prwebDistributionService;

        public PRWebDistributionSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
            _prwebDistributionService = new PRWebDistributionService( SessionKey );
        }

        public const string GET_RESPONSE_KEY = "GetResponse";
        public const string PRWEB_DISTRIBUTION_KEY = "PRWebDistributionData";
        private const string PRWEB_DISTRIBUTION_SESSION_KEY = "PRWebDistributionSessionWithSubscription";
        private const string PRWEB_ADDON_SUBSCRIPTION_SESSION_KEY = "PRWebAddonSubscriptionSessionUpdate";
        private const string DISTRIBUTION_ID_KEY = "DistributionID";
        private const string ON_HOLD_REASON_KEY = "OnHoldReason";
        private const string PRWEB_DISTRIBUTION_VIDEO_KEY = "PRWebDistributionDataWithVideo";
        private const string GET_ANALYTICS_SENT_DISTRIBUTION_DATA_KEY = "AnalyticSentDistributionData";
        private const string GET_ADDONS_KEY = "GetAddons";
        private const string PRWEB_PHONE_EXTENSION_VALUE = "555";
        private const string PRWEB_PHONE_EXTENSION_PARAMETER_NAME = "PRWeb_CommWiz_PressContactPhoneExtension";
        public Key GET_RESPONSE_TWO_KEY;
        public Key PRWEB_DISTRIBUTION_NAME_KEY;
        public Key GET_OBJECT_DATA_KEY;
        public Key GET_RANDOM_COUNTS_KEY;
        public Key GET_JOBS_RESPONSE_KEY;

        [Given(@"the press contact phone extension userparameter has a value")]
        public void GivenThePressContactPhoneExtensionUserParameterHasAValue()
        {
            var response = new UserParametersService(SessionKey).CreateUserParameter(PRWEB_PHONE_EXTENSION_PARAMETER_NAME, PRWEB_PHONE_EXTENSION_VALUE);
        }

        [Given( @"I call save draft distribution" )]
        [When(@"I call save draft distribution")]
        public void WhenICallSaveDraftDistribution()
        { 
            var prwebDistribution = JsonConvert.DeserializeObject<PRWebDistributionData>
                (JsonConvert.SerializeObject(PropertyBucket.GetProperty(PRWEB_DISTRIBUTION_KEY)));

            //var stampedName = $"{prwebDistribution.DistributionName}_{PropertyBucket.TestId}";
            //stampedName = prwebDistribution.DistributionName = stampedName.Substring( 0, 39 );

            var response = new PRWebDistributionService(SessionKey).SaveDraftDistribution(prwebDistribution);
            PropertyBucket.Remember(GET_RESPONSE_KEY, response);
            PropertyBucket.Remember(GET_OBJECT_DATA_KEY, prwebDistribution);
            //PropertyBucket.Remember( PRWEB_DISTRIBUTION_NAME_KEY, stampedName );
        }

        [When(@"I call save draft distribution with subscription '(.*)'")]
        public void WhenICallSaveDraftDistributionWithSubscription(string subscriptionType)
        {
            var prwebDistribution = JsonConvert.DeserializeObject<PRWebDistributionData>
                (JsonConvert.SerializeObject(PropertyBucket.GetProperty(PRWEB_DISTRIBUTION_KEY)));
            var response = new PRWebDistributionService(SessionKey).
                SaveDraftDistributionForSubscription(prwebDistribution, subscriptionType);
            PropertyBucket.Remember(GET_RESPONSE_KEY, response);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "The distribution status code should be OK");
        }

        [When(@"I call send new distribution with video '(.*)'")]
        public void WhenICallSendNewDistributionWithVideo(string videoUrl)
        {
            var prwebDistribution = JsonConvert.DeserializeObject<PRWebDistributionData>
                (JsonConvert.SerializeObject(PropertyBucket.GetProperty(PRWEB_DISTRIBUTION_VIDEO_KEY)));
            prwebDistribution.VideoURL = videoUrl;
            var response = new PRWebDistributionService(SessionKey).SendDistribution(prwebDistribution, SubscriptionType.PREMIUM);
            Assert.AreEqual(prwebDistribution.VideoURL, response.Data.VideoURL, "The url of the video was not saved");
            PropertyBucket.Remember(GET_RESPONSE_KEY, response);
        }

        [Then(@"the press contact phone extension parameter is blank")]
        public void ThenThePressContactPhoneExtensionParameterIsBlank()
        {
            var response = new UserParametersService(SessionKey).GetUserParameterValue(PRWEB_PHONE_EXTENSION_PARAMETER_NAME);

            //An empty user parameter value is returned as not found.
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NotFound, "The phone extension parameter request should return a NotFound result.");
        }

        [Then(@"the distribution press contact phone extension is blank")]
        public void ThenTheDistributionPressContactPhoneExtensionIsBlank()
        {
            var distributionResponse = PropertyBucket.GetProperty<IRestResponse<PRWebDistributionResponse>>(GET_RESPONSE_KEY);
            var meResponse = new AccountInfoService(SessionKey).GetSessionInfo();
            var response = new PRWebDistributionService(SessionKey).GetDistributionPressContactPhoneExtension(meResponse.Data.Account.Id, distributionResponse.Data.DistributionID);

            Assert.NotNull(response, "The press contact phone extension query should not return null.");
            Assert.IsEmpty(response, "The press contact phone extension should be empty.");
        }

        [Then(@"The distribution is in the listing data")]
        public void ThenVerifyTheDistributionIsInTheListingData()
        {
            string gridDistributionName = "";
            var distributionResponse = PropertyBucket.GetProperty<IRestResponse<PRWebDistributionResponse>>(GET_RESPONSE_KEY);
            Assert.AreEqual(HttpStatusCode.OK, distributionResponse.StatusCode, distributionResponse.Content);
            var distResponseData = distributionResponse.Data;
            var response = new MyActivitiesService(SessionKey).GetActivities("1000", "0", "0");
            foreach (PublishActivity publishActivity in response.Data.PublishActivities)
            {
                if (publishActivity.EntityId == distResponseData.DistributionID)
                {
                    gridDistributionName = publishActivity.Title;
                    break;
                }
            }
            Assert.AreEqual(distResponseData.DistributionName, gridDistributionName,
                "The release was not listed in the grid properly.");
        }

        [When(@"I call send distribution and set status to '(.*)'")]
        public void WhenICallSendDistributionAndSetStatusTo(PRWebReleaseStatus status)
        {
            var prwebDistributionService = new PRWebDistributionService(SessionKey);
            var prwebDistribution = JsonConvert.DeserializeObject<PRWebDistributionData>
                (JsonConvert.SerializeObject(PropertyBucket.GetProperty(PRWEB_DISTRIBUTION_KEY)));
            var response = prwebDistributionService.SendDistribution(prwebDistribution, SubscriptionType.ADVANCE);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "The distribution status code should be OK");
            PropertyBucket.Remember(GET_RESPONSE_KEY, response);
            var meResponse = new AccountInfoService(SessionKey).GetSessionInfo();
            prwebDistributionService.SetPRWebReleaseStatus(response.Data.DistributionID, meResponse.Data.Account.Id, status);
        }

        [When(@"I submit distribution put it back to draft and resubmit with subscription '(.*)'")]
        public void WhenISubmitDistributionPutItBackToDraftAndResubmitWithSubscription(string subscriptionType)
        {
            var prwebDistribution = JsonConvert.DeserializeObject<PRWebDistributionData>
                (JsonConvert.SerializeObject(PropertyBucket.GetProperty(PRWEB_DISTRIBUTION_KEY)));
            var prwebDistributionService = new PRWebDistributionService(SessionKey);
            var responseSubmit = prwebDistributionService.SendDistribution(prwebDistribution, subscriptionType);
            PropertyBucket.Remember(GET_RESPONSE_KEY, responseSubmit);
            Assert.AreEqual(HttpStatusCode.OK, responseSubmit.StatusCode, "The distribution status code should be OK");
            var responseBackToDraft = prwebDistributionService.PutBackToDraft(responseSubmit.Data.DistributionID);
            Assert.AreEqual(HttpStatusCode.OK, responseBackToDraft.StatusCode, "The distribution status code should be OK");
            var distribution = prwebDistributionService.GetDistribution(responseSubmit.Data.DistributionID);
            distribution.Data.CisionSocialPost += " update";
            var responseResubmit = prwebDistributionService.ResubmitDistribution(distribution.Data);
            Assert.AreEqual(HttpStatusCode.OK, responseResubmit.StatusCode, "The distribution status code should be OK");
        }

        [When(@"I call send distribution")]
        public void WhenICallSendDistribution()
        {
            var prwebDistribution = JsonConvert.DeserializeObject<PRWebDistributionData>
                (JsonConvert.SerializeObject(PropertyBucket.GetProperty(PRWEB_DISTRIBUTION_KEY)));

            var response = new PRWebDistributionService(SessionKey).SendDistribution
                (prwebDistribution, SubscriptionType.ADVANCE);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "The distribution status code should be OK");
            PropertyBucket.Remember(GET_RESPONSE_KEY, response);
            PropertyBucket.Remember(GET_OBJECT_DATA_KEY, prwebDistribution);
        }

		[Then(@"Then Pull Out Quote Will Have Been Saved")]
		public void ThenPullOutQuoteWillHaveBeenSaved()
		{
            var prwebDistribution = JsonConvert.DeserializeObject<PRWebDistributionData>
                (JsonConvert.SerializeObject(PropertyBucket.GetProperty(PRWEB_DISTRIBUTION_KEY)));

            var originalDistributionResponse = PropertyBucket.GetProperty<IRestResponse<PRWebDistributionResponse>>(GET_RESPONSE_KEY);

			var response = new PRWebDistributionService(SessionKey).GetDistribution(originalDistributionResponse.Data.DistributionID);

			Assert.AreEqual(prwebDistribution.PullOutQuote, response.Data.PullOutQuote, "The pull out quote is not persisted.");
		}


		[Then(@"The distribution status is Pending Distribution Upon User Approval")]
        public void ThenVerifyTheDistributionStatusIsPendingDistributionUponUserApproval()
        {
            var distributionResponse = PropertyBucket.GetProperty<IRestResponse<PRWebDistributionResponse>>(GET_RESPONSE_KEY);
            Assert.AreEqual(HttpStatusCode.OK, distributionResponse.StatusCode, distributionResponse.Content);
            var response = new PRWebDistributionService(SessionKey).IsLimitedDistributionApproval(distributionResponse.Data.DistributionID);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, response.Content);
            Assert.IsTrue(response.Data, "The response should be true but found false");
        }

        [Then(@"Delete created distribution from Databases")]
        public void ThenDeleteCreatedDistributionFromDatabases()
        {
            var meResponse = new AccountInfoService(SessionKey).GetSessionInfo();
            var distributionResponse = PropertyBucket.GetProperty<IRestResponse<PRWebDistributionResponse>>(GET_RESPONSE_KEY);
            new PRWebDistributionService(SessionKey).DeletePrwebDistribution(meResponse.Data.Account.Id, distributionResponse.Data.DistributionID, distributionResponse.Data.PRWebPressReleaseID);
        }

        [When(@"I call the On hold Reason Endpoint")]
        public void WhenICallTheOnHoldReasonEndpoint()
        {
            var response = new PRWebDistributionService(SessionKey).
                GetOnHoldReasons((string)PropertyBucket.GetProperty(DISTRIBUTION_ID_KEY));
            PropertyBucket.Remember(GET_RESPONSE_KEY, response);
        }

        [Then(@"The on hold reason match as expected")]
        public void ThenTheOnHoldReasonMatchAsExpected()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<OnHoldReasonResponse>>(GET_RESPONSE_KEY);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, response.Content);
            Assert.AreEqual((string)PropertyBucket.GetProperty(ON_HOLD_REASON_KEY),
                response.Data.onHoldReasons[0], "The on Hold reason do not match as expected.");
        }

        [When(@"I Update prweb distribution expiration date to '(.*)' days from today")]
        public void WhenIUpdatePrwebDistributionExpirationDateToDaysFromToday(int days)
        {
            DateTime date = DateTime.Today;
            date = date.AddDays(days);
            string formatDate = date.ToString("yyyy-MM-dd hh:mm:ss.ss");
            var meResponse = new AccountInfoService(SessionKey).GetSessionInfo();
            new PRWebDistributionService(SessionKey).SetPRWebSubscriptionExpirationDate(meResponse.Data.Account.Id, formatDate);
        }


        [Then(@"The Company access to prweb distribution is '(.*)'")]
        public void ThenTheCompanyAccessToPrwebDistributionIs(bool hasPrwebAccess)
        {
            var response = new MyActivitiesService(SessionKey).GetDistributionPrivilege();
            Assert.AreEqual(response.Data.Distribution.PRWeb.CanCreate, hasPrwebAccess, 
                "The prweb access for the company should " + hasPrwebAccess + " but was " + response.Data.Distribution.HasPRWeb);
        }

        [When(@"I call get distribution preview")]
        public void WhenICallGetDistributionPreview()
        {
            var prwebDistribution = JsonConvert.DeserializeObject<PRWebDistributionData>
                (JsonConvert.SerializeObject(PropertyBucket.GetProperty(PRWEB_DISTRIBUTION_KEY)));
            var response = new PRWebDistributionService(SessionKey).GetPreviewDistribution(prwebDistribution);
            PropertyBucket.Remember(GET_RESPONSE_KEY, response);
            PropertyBucket.Remember(GET_OBJECT_DATA_KEY, prwebDistribution);
        }

        [When(@"I call send email preview to '(.*)'")]
        public void WhenICallSendEmailPreviewTo(string mail)
        {
            var prwebDistribution = JsonConvert.DeserializeObject<PRWebDistributionData>
                (JsonConvert.SerializeObject(PropertyBucket.GetProperty(PRWEB_DISTRIBUTION_KEY)));
            prwebDistribution.ReleaseDate = String.Format("{0:s}", DateTime.UtcNow.AddHours(5));
            var response = new PRWebDistributionService(SessionKey).SendPreviewToEmail(mail, prwebDistribution);
            PropertyBucket.Remember(GET_JOBS_RESPONSE_KEY, response);
        }

        [When(@"I call download preview to use the PDF link")]
        public void WhenICallDownloadPreviewToUseThePDFLink()
        {
            var prwebDistribution = JsonConvert.DeserializeObject<PRWebDistributionData>
                (JsonConvert.SerializeObject(PropertyBucket.GetProperty(PRWEB_DISTRIBUTION_KEY)));
            prwebDistribution.ReleaseDate = String.Format("{0:s}", DateTime.UtcNow.AddHours(5));
            var response = new PRWebDistributionService(SessionKey).DownloadPreview(prwebDistribution);
            PropertyBucket.Remember(GET_JOBS_RESPONSE_KEY, response);
        }

        [Then(@"The preview info match the provided data")]
        public void ThenThePreviewInfoMatchTheProvidedData()
        {
            var prwebDistribution = PropertyBucket.GetProperty<PRWebDistributionData>(GET_OBJECT_DATA_KEY);
            var response = PropertyBucket.GetProperty<IRestResponse<PRWebDistributionResponse>>(GET_RESPONSE_KEY);
            Assert.IsTrue(response.Content.Contains(prwebDistribution.Headline), Err.Msg("Headline isn't correct"));
            Assert.IsTrue(response.Content.Contains(prwebDistribution.Summary), Err.Msg("Summary isn't correct"));
            Assert.IsTrue(response.Content.Contains(prwebDistribution.Body), Err.Msg("Body isn't correct"));
            Assert.IsTrue(response.Content.Contains(prwebDistribution.ContactName), Err.Msg("Contact Name isn't correct"));
            Assert.IsTrue(response.Content.Contains(prwebDistribution.ContactCompany), Err.Msg("Contact Company isn't correct"));
            Assert.IsTrue(response.Content.Contains(prwebDistribution.ContactPhone), Err.Msg("Contact Phone isn't correct"));
            Assert.IsTrue(response.Content.Contains("Visit website"), Err.Msg("Company Website isn't correct"));
        }

        [Then(@"The IDL is in the response and added to the DB")]
        public void ThenTheIDLIsInTheResponseAndAddedToTheDB()
        {
            var prwebDistribution = PropertyBucket.GetProperty<PRWebDistributionData>(GET_OBJECT_DATA_KEY);
            var response = PropertyBucket.GetProperty<IRestResponse<PRWebDistributionResponse>>(GET_RESPONSE_KEY);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, response.Content);
            Assert.AreEqual(prwebDistribution.IndustryOutletCategories[0].CategoryName,
                response.Data.IndustryOutletCategories[0].CategoryName, "The response did not return the proper IDL.");
            var meResponse = new AccountInfoService(SessionKey).GetSessionInfo();
            var prwebIndustryOutletCategoryIDs = new PRWebDistributionService(SessionKey).GetIdlDistributionPrweb
                (meResponse.Data.Account.Id, response.Data.DistributionID);
            Assert.AreEqual(prwebDistribution.IndustryOutletCategories[0].Id, prwebIndustryOutletCategoryIDs,
                "The IDL was nos storage in the DB.");
        }

        [When(@"I clean and update count for release")]
        public void WhenICleanAndUpdateCountForRelease()
        {
            var prwebDistributionService = new PRWebDistributionService(SessionKey);
            var data = JsonConvert.DeserializeObject<AnalyticSentDistributionData>
                (JsonConvert.SerializeObject(PropertyBucket.GetProperty(GET_ANALYTICS_SENT_DISTRIBUTION_DATA_KEY)));

            Random rn = new Random();
            int[] counts = new int[] { rn.Next(1, 10), rn.Next(1, 10), rn.Next(1, 10) };
            PropertyBucket.Remember(GET_RANDOM_COUNTS_KEY, counts);

            prwebDistributionService.DeleteCountsForRelease(data.CompanyId, data.ReleaseId);
            prwebDistributionService.InsertCountsForRelease(data.CompanyId, data.ReleaseId, data.ReleaseDate, PRWebActivityType.EXTERNAL, counts[0]);
            prwebDistributionService.InsertCountsForRelease(data.CompanyId, data.ReleaseId, data.ReleaseDate, PRWebActivityType.VIEWS, counts[1]);
            prwebDistributionService.InsertCountsForRelease(data.CompanyId, data.ReleaseId, data.ReleaseDate, PRWebActivityType.READS, counts[2]);
        }

        [When(@"I call to API headlines impressions")]
        public void WhenICallToAPIHeadlinesImpressions()
        {
            var data = JsonConvert.DeserializeObject<AnalyticSentDistributionData>
                (JsonConvert.SerializeObject(PropertyBucket.GetProperty(GET_ANALYTICS_SENT_DISTRIBUTION_DATA_KEY)));
            var response = new PRWebDistributionService(SessionKey).GetHeadlineImpressions(data.ReleaseId);
            PropertyBucket.Remember(GET_RESPONSE_KEY, response);
        }

        [When(@"I call to API full release reads")]
        public void WhenICallToAPIFullReleaseReads()
        {
            var data = JsonConvert.DeserializeObject<AnalyticSentDistributionData>
                (JsonConvert.SerializeObject(PropertyBucket.GetProperty(GET_ANALYTICS_SENT_DISTRIBUTION_DATA_KEY)));
            var response = new PRWebDistributionService(SessionKey).GetFullReleaseReads(data.ReleaseId);
            PropertyBucket.Remember(GET_RESPONSE_TWO_KEY, response);
        }

        [Then(@"The news Aggregator and prweb impressions match to headlines impressions")]
        public void ThenTheNewsAggregatorAndPrwebImpressionsMatchToHeadlinesImpressions()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<List<ChartResponse>>>(GET_RESPONSE_KEY);
            var counts = PropertyBucket.GetProperty<int[]>(GET_RANDOM_COUNTS_KEY);
            Assert.AreEqual(counts[0], response.Data[0].Total, $"{response.Data[0].Name} do not match with add amount");
            Assert.AreEqual(counts[1], response.Data[1].Total, $"{response.Data[1].Name} do not match with add amount");
        }

        [Then(@"The count read match the full release reads")]
        public void ThenTheCountReadMatchTheFullReleaseReads()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<List<ChartResponse>>>(GET_RESPONSE_TWO_KEY);
            var counts = PropertyBucket.GetProperty<int[]>(GET_RANDOM_COUNTS_KEY);
            Assert.AreEqual(counts[2], response.Data[0].Total, $"{response.Data[0].Name} do not match with add amount");
        }

        [Then(@"I call distribution back to draft")]
        public void ThenICallDistributionBackToDraft()
        {
            var distributionResponse = PropertyBucket.GetProperty<IRestResponse<PRWebDistributionResponse>>(GET_RESPONSE_KEY);
            var response = new PRWebDistributionService(SessionKey).PutBackToDraft(distributionResponse.Data.DistributionID);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "The change status to back to draft fail");
        }

        [When(@"I call online pickup analitycs")]
        public void WhenICallOnlinePickupAnalitycs()
        {
            var data = JsonConvert.DeserializeObject<Dictionary<string, int>>
                (JsonConvert.SerializeObject(PropertyBucket.GetProperty(GET_ANALYTICS_SENT_DISTRIBUTION_DATA_KEY)));
            var response = new PRWebDistributionService(SessionKey).GetOnlinePickupAnalytics(data["prwebPressReleaseId"], data["distributionId"]);
            PropertyBucket.Remember(GET_RESPONSE_KEY, response);
        }

        [Then(@"I get all the online pickup and potencial audience in a descending order")]
        public void ThenIGetAllTheOnlinePickupAndPotencialAudienceInADescendingOrder()
        {
            var data = JsonConvert.DeserializeObject<Dictionary<string, int>>
                (JsonConvert.SerializeObject(PropertyBucket.GetProperty(GET_ANALYTICS_SENT_DISTRIBUTION_DATA_KEY)));
            var response = PropertyBucket.GetProperty<IRestResponse<PRWebOnlinePickupResponse>>(GET_RESPONSE_KEY);
            Assert.AreEqual(response.Data.Sources.Count, data["sourcesCount"], "There should be " + data["sourcesCount"] + " online pickup sources");
            Assert.AreEqual(response.Data.TotalOnlinePickup, data["onlinePickupCount"], "There should be " + data["onlinePickupCount"] + " total online pickup");
            Assert.AreEqual(response.Data.PotentialAudience, data["potentialAudienceCount"], "There should be " + data["potentialAudienceCount"] + " online pickup potential audience");

            Assert.True(response.Data.Sources[0].Audience > response.Data.Sources[1].Audience, "Online pickup should be ordered by Audience, descending");
            Assert.True(response.Data.Sources[1].Audience > response.Data.Sources[2].Audience, "Online pickup should be ordered by Audience, descending");
        }

        [Then(@"The values of the attachments are in the reponse")]
        public void ThenTheValuesOfTheAttachmentsAreInTheReponse()
        {
            var prwebDistribution = JsonConvert.DeserializeObject<PRWebDistributionData>
                (JsonConvert.SerializeObject(PropertyBucket.GetProperty(PRWEB_DISTRIBUTION_KEY)));
            var response = PropertyBucket.GetProperty<IRestResponse<PRWebDistributionResponse>>(GET_RESPONSE_KEY);
            Assert.AreEqual(prwebDistribution.AdditionalAttachments[0].AltText, response.Data.AdditionalAttachments[0].AltText, "The added AltText to the attachment should be persisted.");
            Assert.AreEqual(prwebDistribution.AdditionalAttachments[0].Caption, response.Data.AdditionalAttachments[0].Caption, "The added Caption to the attachment should be persisted.");
            Assert.AreEqual(prwebDistribution.AdditionalAttachments[0].Description, response.Data.AdditionalAttachments[0].Description, "The added Description to the attachment should be persisted.");
            Assert.AreEqual(prwebDistribution.NewsImage.AltText, response.Data.NewsImage.AltText, "The added AltText to the NewsImage should be persisted.");
            Assert.AreEqual(prwebDistribution.NewsImage.Caption, response.Data.NewsImage.Caption, "The added Caption to the NewsImage should be persisted.");
            Assert.AreEqual(prwebDistribution.NewsImage.Description, response.Data.NewsImage.Description, "The added Description to the NewsImage should be persisted.");
        }

        [Then(@"I see all the values in the response")]
        public void ThenISeeAllTheValuesInTheResponse()
        {
            var prwebDistribution = JsonConvert.DeserializeObject<PRWebDistributionData>
                (JsonConvert.SerializeObject(PropertyBucket.GetProperty(PRWEB_DISTRIBUTION_KEY)));
            var response = PropertyBucket.GetProperty<IRestResponse<PRWebDistributionResponse>>(GET_RESPONSE_KEY);
            
            Assert.AreEqual(prwebDistribution.DistributionName, response.Data.DistributionName, "Distribution Name isn't correct");
            Assert.That(response.Data.PRWebPressReleaseID, Is.GreaterThan(0), "Press ReleaseID isn't correct.");
            Assert.AreEqual(prwebDistribution.Headline, response.Data.Headline, "Headline isn't correct");
            Assert.AreEqual(prwebDistribution.Summary, response.Data.Summary, "Summary isn't correct");
            Assert.AreEqual(prwebDistribution.Body, response.Data.Body, "Body isn't correct");
            Assert.AreEqual(prwebDistribution.ReleaseDateTimezone, response.Data.ReleaseDateTimezone, "Release Date Timezone isn't correct");
            Assert.AreEqual(prwebDistribution.Cities[0].Id, response.Data.Cities[0].Id, "Cities isn't correct");
            Assert.AreEqual(prwebDistribution.User, response.Data.User, "User isn't correct");
            Assert.AreEqual(prwebDistribution.UserEmail, response.Data.UserEmail, "User Email isn't correct");
            Assert.AreEqual(prwebDistribution.UserPhone, response.Data.UserPhone, "User Phone isn't correct");
            Assert.AreEqual(prwebDistribution.Topics[0].Id, response.Data.Topics[0].Id, "Topics isn't correct");
            Assert.AreEqual(prwebDistribution.MediaDigests[0], response.Data.MediaDigests[0], "The selected Media Digests isn't correct");
            Assert.AreEqual(prwebDistribution.MediaDigests[1], response.Data.MediaDigests[1], "The selected Media Digests isn't correct");
            Assert.AreEqual(prwebDistribution.ContactName, response.Data.ContactName, "Contact Name isn't correct");
            Assert.AreEqual(prwebDistribution.ContactEmail, response.Data.ContactEmail, "Contact Email isn't correct");
            Assert.AreEqual(prwebDistribution.ContactPhone, response.Data.ContactPhone, "Contact Phone isn't correct");
            Assert.AreEqual(prwebDistribution.ContactCompany, response.Data.ContactCompany, "Contact Company isn't correct");
            Assert.AreEqual(prwebDistribution.ContactWebsite, response.Data.ContactWebsite, "Contact Website isn't correct");
            Assert.AreEqual(prwebDistribution.AdditionalContactName, response.Data.AdditionalContactName, "AdditionalContactName isn't correct");
            Assert.AreEqual(prwebDistribution.AdditionalContactCompany, response.Data.AdditionalContactCompany, "AdditionalContactCompany isn't correct");
            Assert.AreEqual(prwebDistribution.AdditionalContactEmail, response.Data.AdditionalContactEmail, "AdditionalContactEmail isn't correct");
            Assert.AreEqual(prwebDistribution.AdditionalContactPhone, response.Data.AdditionalContactPhone, "AdditionalContactPhone isn't correct");
            Assert.AreEqual(prwebDistribution.AdditionalContactWebsite, response.Data.AdditionalContactWebsite, "AdditionalContactWebsite isn't correct");
            Assert.AreEqual(prwebDistribution.CompanyWebsite, response.Data.CompanyWebsite, "CompanyWebsite isn't correct");
        }
        [Then(@"I see TwitterID and Message values in the response")]
        public void ThenISeeTwitterIDAndMessageValuesInTheResponse()
        {
            var prwebDistribution = JsonConvert.DeserializeObject<PRWebDistributionData>
               (JsonConvert.SerializeObject(PropertyBucket.GetProperty(PRWEB_DISTRIBUTION_KEY)));
            var response = PropertyBucket.GetProperty<IRestResponse<PRWebDistributionResponse>>(GET_RESPONSE_KEY);

            Assert.AreEqual(prwebDistribution.SocialMediaMessageAccount.Id, response.Data.SocialMediaMessageAccount.Id, Err.Line("Account Id isn't correct"));
            Assert.AreEqual(prwebDistribution.SocialMediaMessageAccount.Message, response.Data.SocialMediaMessageAccount.Message, Err.Line("Message isn't correct"));

        }

        [Then(@"All data is saved in the release")]
        public void ThenAllDataIsSavedInTheRelease()
        {
            var prwebDistribution = JsonConvert.DeserializeObject<PRWebDistributionData>
                (JsonConvert.SerializeObject(PropertyBucket.GetProperty(PRWEB_DISTRIBUTION_KEY)));
            var response = PropertyBucket.GetProperty<IRestResponse<PRWebDistributionResponse>>(GET_RESPONSE_KEY);
            
            var release = new PRWebDistributionService(SessionKey).GetDistribution(response.Data.DistributionID);

            Assert.AreEqual(prwebDistribution.DistributionName, release.Data.DistributionName, "Distribution Name isn't correct");
            Assert.That(release.Data.PRWebPressReleaseID, Is.GreaterThan(0), "Press ReleaseID isn't correct.");
            Assert.AreEqual(prwebDistribution.Headline, release.Data.Headline, "Headline isn't correct");
            Assert.AreEqual(prwebDistribution.Summary, release.Data.Summary, "Summary isn't correct");
            Assert.AreEqual(prwebDistribution.Body, release.Data.Body, "Body isn't correct");
            Assert.AreEqual(prwebDistribution.ReleaseDateTimezone, release.Data.ReleaseDateTimezone, "Release Date Timezone isn't correct");
            Assert.AreEqual(prwebDistribution.Cities[0].Id, release.Data.Cities[0].Id, "Cities isn't correct");
            Assert.AreEqual(prwebDistribution.User, release.Data.User, "User isn't correct");
            Assert.AreEqual(prwebDistribution.UserEmail, release.Data.UserEmail, "User Email isn't correct");
            Assert.AreEqual(prwebDistribution.UserPhone, release.Data.UserPhone, "User Phone isn't correct");
            Assert.AreEqual(prwebDistribution.Topics[0].Id, release.Data.Topics[0].Id, "Topics isn't correct");
            Assert.AreEqual(prwebDistribution.MediaDigests[0], release.Data.MediaDigests[0], "The selected Media Digests isn't correct");
            Assert.AreEqual(prwebDistribution.MediaDigests[1], release.Data.MediaDigests[1], "The selected Media Digests isn't correct");
            Assert.AreEqual(prwebDistribution.ContactName, release.Data.ContactName, "Contact Name isn't correct");
            Assert.AreEqual(prwebDistribution.ContactEmail, release.Data.ContactEmail, "Contact Email isn't correct");
            Assert.AreEqual(prwebDistribution.ContactPhone, release.Data.ContactPhone, "Contact Phone isn't correct");
            Assert.AreEqual(prwebDistribution.ContactCompany, release.Data.ContactCompany, "Contact Company isn't correct");
            Assert.IsTrue(release.Data.ContactWebsite.Contains(prwebDistribution.ContactWebsite), "Contact Website isn't correct");
            Assert.AreEqual(prwebDistribution.AdditionalContactName, release.Data.AdditionalContactName, "AdditionalContactName isn't correct");
            Assert.AreEqual(prwebDistribution.AdditionalContactCompany, release.Data.AdditionalContactCompany, "AdditionalContactCompany isn't correct");
            Assert.AreEqual(prwebDistribution.AdditionalContactEmail, release.Data.AdditionalContactEmail, "AdditionalContactEmail isn't correct");
            Assert.AreEqual(prwebDistribution.AdditionalContactPhone, release.Data.AdditionalContactPhone, "AdditionalContactPhone isn't correct");
            Assert.IsTrue(release.Data.AdditionalContactWebsite.Contains(prwebDistribution.AdditionalContactWebsite), "AdditionalContactWebsite isn't correct");
            Assert.IsTrue(release.Data.CompanyWebsite.Contains(prwebDistribution.CompanyWebsite), "CompanyWebsite isn't correct");
        }


        [When(@"I send multiple distribution and set publish activity status")]
        public void WhenISendMultipleDistributionAndSetPublishActivityStatus()
        {
            var prwebDistribution = JsonConvert.DeserializeObject<PRWebDistributionData>
                (JsonConvert.SerializeObject(PropertyBucket.GetProperty(PRWEB_DISTRIBUTION_KEY)));
            var meResponse = new AccountInfoService(SessionKey).GetSessionInfo();
            var releaseStatusList = new PRWebDistributionService(SessionKey).
                CreateTestDistributions(meResponse.Data.Account.Id, prwebDistribution);
            PropertyBucket.Remember(GET_RESPONSE_KEY, releaseStatusList);
        }

        [Then(@"I see the distribution are listed in order")]
        public void ThenISeeTheDistributionAreListedInOrder()
        {
            var releaseStatusList = PropertyBucket.GetProperty<List<DistroResponseStatus>>(GET_RESPONSE_KEY);
            var activities = new MyActivitiesService(SessionKey).GetActivities();

            var matchedDistribution = (from activity in activities.Data.PublishActivities
                      join releaseStatus in releaseStatusList on
                      activity.EntityId equals releaseStatus.ApiData.DistributionID
                      select activity).ToList();
            
            Assert.AreEqual((int)releaseStatusList[0].PublicationStatus, matchedDistribution[0].PublicationState, "On Hold Distribution not in the correct publication state");
            Assert.AreEqual(releaseStatusList[0].ApiData.DistributionID, matchedDistribution[0].EntityId, "On Hold Distribution not in the correct order");
            Assert.AreEqual((int)releaseStatusList[1].PublicationStatus, matchedDistribution[1].PublicationState, "Draft Distribution not in the correct publication state");
            Assert.AreEqual(releaseStatusList[1].ApiData.DistributionID, matchedDistribution[1].EntityId, "Draft Distribution not in the correct order");
            Assert.AreEqual((int)releaseStatusList[2].PublicationStatus, matchedDistribution[2].PublicationState, "In Review Distribution not in the correct publication state");
            Assert.AreEqual(releaseStatusList[2].ApiData.DistributionID, matchedDistribution[2].EntityId, "In Review Distribution not in the correct order");
            Assert.AreEqual((int)releaseStatusList[3].PublicationStatus, matchedDistribution[3].PublicationState, "Scheduled Distribution not in the correct publication state");
            Assert.AreEqual(releaseStatusList[3].ApiData.DistributionID, matchedDistribution[3].EntityId, "Scheduled Distribution not in the correct order");
            Assert.AreEqual((int)releaseStatusList[4].PublicationStatus, matchedDistribution[4].PublicationState, "Sent Distribution not in the correct publication state");
            Assert.AreEqual(releaseStatusList[4].ApiData.DistributionID, matchedDistribution[4].EntityId, "Sent Distribution not in the correct order");
            
        }

        [Then(@"Delete multiple distributions from database")]
        public void ThenDeleteMultipleDistributionsFromDatabase()
        {
            var meResponse = new AccountInfoService(SessionKey).GetSessionInfo();
            var releaseStatusList = PropertyBucket.GetProperty<List<DistroResponseStatus>>(GET_RESPONSE_KEY);

            foreach (DistroResponseStatus releaseStatus in releaseStatusList)
            {
                new PRWebDistributionService(SessionKey).DeletePrwebDistribution(meResponse.Data.Account.Id, releaseStatus.ApiData.DistributionID, releaseStatus.ApiData.PRWebPressReleaseID);
            }
        }

        [Then(@"The OAuth values are in the response")]
        public void ThenTheOAuthValuesAreInTheResponse()
        {
            var prwebDistribution = JsonConvert.DeserializeObject<PRWebDistributionData>
                (JsonConvert.SerializeObject(PropertyBucket.GetProperty(PRWEB_DISTRIBUTION_KEY)));
            var response = PropertyBucket.GetProperty<IRestResponse<PRWebDistributionResponse>>(GET_RESPONSE_KEY);
            Assert.AreEqual(prwebDistribution.SocialMediaAccounts[0].Type, response.Data.SocialMediaAccounts[0].Type);
            Assert.AreEqual(prwebDistribution.SocialMediaAccounts[0].AccountName, response.Data.SocialMediaAccounts[0].AccountName);
            Assert.AreEqual(prwebDistribution.SocialMediaAccounts[0].Avatar, response.Data.SocialMediaAccounts[0].Avatar);
            Assert.AreEqual(prwebDistribution.SocialMediaAccounts[0].Connected, response.Data.SocialMediaAccounts[0].Connected);
            Assert.AreEqual(prwebDistribution.SocialMediaAccounts[0].Selected, response.Data.SocialMediaAccounts[0].Selected);
            Assert.AreEqual(prwebDistribution.SocialMediaAccounts[1].Type, response.Data.SocialMediaAccounts[1].Type);
            Assert.AreEqual(prwebDistribution.SocialMediaAccounts[1].AccountName, response.Data.SocialMediaAccounts[1].AccountName);
            Assert.AreEqual(prwebDistribution.SocialMediaAccounts[1].Avatar, response.Data.SocialMediaAccounts[1].Avatar);
            Assert.AreEqual(prwebDistribution.SocialMediaAccounts[1].Connected, response.Data.SocialMediaAccounts[1].Connected);
            Assert.AreEqual(prwebDistribution.SocialMediaAccounts[1].Selected, response.Data.SocialMediaAccounts[1].Selected);
            Assert.AreEqual(prwebDistribution.SocialMediaAccounts[2].Type, response.Data.SocialMediaAccounts[2].Type);
            Assert.AreEqual(prwebDistribution.SocialMediaAccounts[2].AccountName, response.Data.SocialMediaAccounts[2].AccountName);
            Assert.AreEqual(prwebDistribution.SocialMediaAccounts[2].Avatar, response.Data.SocialMediaAccounts[2].Avatar);
            Assert.AreEqual(prwebDistribution.SocialMediaAccounts[2].Connected, response.Data.SocialMediaAccounts[2].Connected);
            Assert.AreEqual(prwebDistribution.SocialMediaAccounts[2].Selected, response.Data.SocialMediaAccounts[2].Selected);
        }

        [When(@"I call is send to Iris")]
        public void WhenICallIsSendToIris()
        {
            var response = new PRWebDistributionService(SessionKey).
                GetSendToIris((string)PropertyBucket.GetProperty(DISTRIBUTION_ID_KEY));
            PropertyBucket.Remember(GET_RESPONSE_KEY, response);
        }

        [Then(@"I see a true response")]
        public void ThenISeeATrueResponse()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<bool>>(GET_RESPONSE_KEY);
            Assert.IsTrue(response.Data, "Reponse is false");
        }

        [Then(@"I see a false response")]
        public void ThenISeeAFalseResponse()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<bool>>(GET_RESPONSE_KEY);
            Assert.IsFalse(response.Data, "Response is true");
        }

        [Then(@"I see a subscription where SendToIris is true")]
        public void ThenISeeASubscriptionWhereSendToIrisIsTrue()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<List<PRWebSubscriptionResponse>>>(GET_RESPONSE_KEY);
            var isSendToIris = response.Data[0].SendToIris;
            Assert.IsTrue(isSendToIris, "IsSendToIris is false");
        }

        [Then(@"I resubmit distribution with a different subscription to error validation")]
        public void ThenIResubmitDistributionWithADifferentSubscriptionToErrorValidation()
        {
            var distributionResponse = PropertyBucket.GetProperty<IRestResponse<PRWebDistributionResponse>>(GET_RESPONSE_KEY);
            var submittedDistro = new PRWebDistributionService(SessionKey).GetDistribution(distributionResponse.Data.DistributionID);
            var ubid = submittedDistro.Data.SubscriptionId;
            var validSubscription = new PRWebDistributionService(SessionKey).GetValidSubscriptions();
            foreach (var sub in validSubscription.Data)
            {
                if (sub.PRWebSubscriptionID != submittedDistro.Data.SubscriptionId)
                {
                    submittedDistro.Data.SubscriptionId = sub.PRWebSubscriptionID;
                    submittedDistro.Data.Subscription = sub;
                    break;
                }
            }
            var response = new PRWebDistributionService(SessionKey).ResubmitDistribution(submittedDistro.Data);
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.InternalServerError, 
                "The resubmit should have failed for selecting a different subscription");
        }

        [When(@"I call Resubmit distribution")]
        public void WhenICallResubmitDistribution()
        {
            var distroID = PropertyBucket.GetProperty("DistributionID");
            var distro = new PRWebDistributionService(SessionKey).GetDistribution(Convert.ToInt32(distroID));
            distro.Data.ReleaseDate = DateTime.Now.AddHours(25).ToString("MM/dd/yyyy");
            
            var response = new PRWebDistributionService(SessionKey).ResubmitDistribution(distro.Data);
            PropertyBucket.Remember<IRestResponse>(GET_RESPONSE_KEY, response);
        }

        [Then(@"I get the validation issue about the 30 day re-edit rule")]
        public void ThenIGetTheValidationIssueAboutTheDayRe_EditRule()
        {
            var response = PropertyBucket.GetProperty<IRestResponse>(GET_RESPONSE_KEY);
            Assert.True(response.Content.Contains("COMP_ACTIVITIES_PRWEB_NO_EDITS_30_DAYS_AFTER_INITIAL_PUBLISH"),
                "The resubmit should have failed for being on a release already published more than 30 days ago.");
        }

        [When(@"I call get selected addons")]
        public void WhenICallGetSelectedAddOns()
        {
            var data = JsonConvert.DeserializeObject<PRWebDistributionSessionWithSubscription>
                (JsonConvert.SerializeObject(PropertyBucket.GetProperty(PRWEB_DISTRIBUTION_SESSION_KEY)));
            var response = new PRWebDistributionService(SessionKey).GetSelectedAddons(data);
            PropertyBucket.Remember(GET_RESPONSE_KEY, response);
        }

        [Then(@"I get the selected addons for the Distribution")]
        public void ThenIGetTheSelectedAddonsForTheDistribution()
        {
            var addonsResponse = PropertyBucket.GetProperty<IRestResponse<List<PRWebSubscriptionAddonResponse>>>(GET_RESPONSE_KEY);
            Assert.AreEqual(HttpStatusCode.OK, addonsResponse.StatusCode, addonsResponse.Content);
            var addons = addonsResponse.Data;

            Assert.AreEqual(addons.Count, 2, "Get Selected Addons did not return the correct number of addons");
            Assert.AreEqual(addons.Count(a => a.PRWebSkuType == 2), 1, "Get Selected Addons did not return one addon of type 2");
            Assert.AreEqual(addons.Count(a => a.PRWebSkuType == 3), 1, "Get Selected Addons did not return one addon of type 3");
        }

        [When(@"I call update addons")]
        public void WhenICallUpdateAddons()
        {
            var data = JsonConvert.DeserializeObject<PRWebAddonSubscriptionSessionUpdate>
                (JsonConvert.SerializeObject(PropertyBucket.GetProperty(PRWEB_ADDON_SUBSCRIPTION_SESSION_KEY)));
            var response = new PRWebDistributionService(SessionKey).UpdateDistributionAddons(data);
            PropertyBucket.Remember(GET_RESPONSE_KEY, response);
        }

        [Then(@"the addon quantity is updated")]
        public void ThenTheAddonQuantityIsUpdated()
        {
            var updateResponse = PropertyBucket.GetProperty<IRestResponse>(GET_RESPONSE_KEY);
            var subscriptionData = JsonConvert.DeserializeObject<PRWebAddonSubscriptionSessionUpdate>
                (JsonConvert.SerializeObject(PropertyBucket.GetProperty(PRWEB_ADDON_SUBSCRIPTION_SESSION_KEY)));

            var dbQuantity = new PRWebDistributionService(SessionKey).GetDistributionAddOnsQuantityUsedFromDb(subscriptionData.CompanyId, subscriptionData.DistributionId, subscriptionData.AddOnPRWebSubscriptionId);

            Assert.AreEqual(HttpStatusCode.OK, updateResponse.StatusCode, updateResponse.Content);
            Assert.AreEqual(subscriptionData.QuantityUsedByDistribution, dbQuantity);
        }

        [Then(@"I reset the addon quantity in the DB")]
        public void ThenIResetTheAddonQuantityInTheDB()
        {
            var subscriptionData = JsonConvert.DeserializeObject<PRWebAddonSubscriptionSessionUpdate>
                (JsonConvert.SerializeObject(PropertyBucket.GetProperty(PRWEB_ADDON_SUBSCRIPTION_SESSION_KEY)));

            new PRWebDistributionService(SessionKey).UpdateDistributionAddOnsQuantityUsedInDb(subscriptionData.CompanyId, subscriptionData.DistributionId, subscriptionData.AddOnPRWebSubscriptionId, quantityUsed: 0);
        }

        [Then(@"I see the quantityUsed of available Addons has not increased for subscription '(.*)'")]
        public void ThenISeeTheQuantityUsedOfAvailableAddonsHasNotIncreasedForSubscription(string subscriptionType)
        {
            var initialAddons = PropertyBucket.GetProperty<List<AddOns>>(GET_ADDONS_KEY);
            List<AddOns> updatedAddons = new PRWebDistributionService(SessionKey).GetAddonsBySubscriptionName(subscriptionType);
            for (int i=0; i<initialAddons.Count; i++)
            {
                Assert.AreEqual(initialAddons[i].AddOnSubscriptionID, updatedAddons[i].AddOnSubscriptionID, 
                    "The addons are not the same " + initialAddons[i].AddOnSubscriptionID + " / " + updatedAddons[i].AddOnSubscriptionID);
                Assert.AreEqual(initialAddons[i].QuantityUsed, updatedAddons[i].QuantityUsed, "The QuantityUsed is not the same.");
            }
        }

        [Then(@"I see the CJL addon QuantityUsed has incread in '(.*)' for subscription '(.*)'")]
        public void ThenISeeTheCJLAddonQuantityUsedHasIncreadInForSubscription(int quantityUsed, string subscriptionType)
        {
            var initialAddons = PropertyBucket.GetProperty<List<AddOns>>(GET_ADDONS_KEY);
            List<AddOns> updatedAddons = new PRWebDistributionService(SessionKey).GetAddonsBySubscriptionName(subscriptionType);
            for (int i=0; i<initialAddons.Count; i++)
            {
                if(initialAddons[i].Description.Contains("Cision Journalist Lists")
                    && initialAddons[i].Description.Equals(updatedAddons[i].Description))
                {
                    Assert.AreEqual(initialAddons[i].QuantityUsed + quantityUsed, updatedAddons[i].QuantityUsed,
                "The addons maths for CJL is not working properly.");
                }
            }
        }

        [Then(@"I see the the CSP addon QuantityUsed has increased in one for subscription '(.*)'")]
        public void ThenISeeTheTheCSPAddonQuantityUsedHasIncreasedInOne(string subscriptionType)
        {
            var initialAddons = PropertyBucket.GetProperty<List<AddOns>>(GET_ADDONS_KEY);
            List<AddOns> updatedAddons = new PRWebDistributionService(SessionKey).GetAddonsBySubscriptionName(subscriptionType);
            for (int i = 0; i < initialAddons.Count; i++)
            {
                if (initialAddons[i].Description.Contains("Cision SocialPost")
                    && initialAddons[i].Description.Equals(updatedAddons[i].Description))
                {
                    Assert.AreEqual(initialAddons[i].QuantityUsed + 1, updatedAddons[i].QuantityUsed,
                "The addons maths for CSP is not working properly.");
                }
            }
        }


    }
}
