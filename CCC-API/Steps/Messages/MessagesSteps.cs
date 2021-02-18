using BoDi;
using CCC_API.Data.PostData.Messages;
using CCC_API.Data.Responses.Messages;
using CCC_API.Data.TestDataObjects.Activities;
using CCC_API.Services.Activities;
using CCC_API.Services.Common;
using CCC_API.Services.Messages;
using CCC_API.Steps.Common;
using CCC_Infrastructure.API.Utils;
using CCC_Infrastructure.Utils;
using CCC_API.Utils.Assertion;
using RestSharp;
using System.Net;
using TechTalk.SpecFlow;
using System;

namespace CCC_API.Steps.Messages
{
    [Binding]
    public class MessagesSteps : AuthApiSteps
    {
        public const string RESP_KEY = "response";

        public MessagesSteps(IObjectContainer objectContainer) : base(objectContainer) { }

        [When(@"I schedule a message to send on (.*), (.*) to (.*) with a text length of (.*), and image (.*), linked (.*), from page (.*), shorten url service (.*) and (.*) as expected response code")]
        public MessageData WhenIPublishAMessageToWithAAnd(string time, string timezone, string platform, int textLength, string imageUrl, bool linked, string pageUrl, bool shortenUrls, string responseCode)
        {
            if (TestSettings.GetConfigValue("SocialMediaPublishing") != "true")
                Assert.Ignore("SocialMediaPublishing config is set as false by default to prevent QA sources " +
                    "from reaching post rate limits of external social media platforms. " +
                    "This parameter is set to true on the bamboo build.");

            // Prepare data to populate social media post 
            var messageService = new MessageService(SessionKey);
            var socialMediaList = messageService.PlatformAsProperty(platform);
            var randomText = messageService.GetRandomString(textLength);
            var sharedLink = messageService.SharedLink(imageUrl, pageUrl, linked);
            var publishTime = messageService.PublishTime(time, timezone);

            // Get connected Account information
            var accountData = messageService.GetAccount(platform);
            Assert.IsNotNull(accountData, Err.Msg($"There is no {platform} social network account connected"));
            var accountName = accountData.Data.AccountName;

            // Get the companyId to include it on the Error message
            var meResponse = new AccountInfoService(SessionKey).GetSessionInfo();
            var companyId = meResponse.Data.Account.LoginName;

            // Check if is there a Pinterest company authorized with an existing board
            if (platform.Equals("Pinterest", System.StringComparison.OrdinalIgnoreCase))

                Assert.IsNotNull(socialMediaList.Attributes, Err.Msg($"Company : {companyId}  has no Boards accesible for {accountName} pinterest account"));
            else
                Assert.IsNull(socialMediaList.Attributes, Err.Msg("Attributes should be null."));

            // Publish the message as a social media post
            var payload = messageService.PrepareMessage(socialMediaList, randomText, imageUrl, linked, sharedLink, publishTime, shortenUrls);
            var response = messageService.PostMessage(payload);

            // Verify Social Media POST was successful
            Assert.AreEqual(responseCode.Replace("\"", ""), response.StatusCode.ToString(), Err.Line($"Wrong Status code on the response. Check if the {platform} account is authorized for {companyId} company"));

            PropertyBucket.Remember(RESP_KEY, response);
            return payload;
        }
        
        [Given(@"a social post activity to '(Twitter|FacebookFanPage|LinkedInCompany|Pinterest)' with time '(tomorrow|now|past)', timezone '(.*)'")]
        public void WhenIScheduleSocialPostToWithTimeTimezone(string platform, string time, string timezone)
        {
            var timeToPost = time == "past" ? "now" : time;
            var post = WhenIPublishAMessageToWithAAnd(
                time: timeToPost,
                timezone: timezone,
                platform: platform,
                textLength: 100,
                imageUrl: "http://demo.cloudimg.io/s/crop/200x200/sample.li/boat.jpg",
                linked: false,
                pageUrl: "https://en.wikipedia.org",
                shortenUrls: true,
                responseCode: HttpStatusCode.OK.ToString());

            var service = new MyActivitiesService(SessionKey);
            var acc = service.GetRecentActivities(platform)
                             .GetActivity(it => it.ContentSnippet == post.Content);
            
            if (time == "past")
                new Poller(TimeSpan.FromSeconds(30)).TryUntil(() => 
                  service.GetRecentActivities(platform, "sent")
                         .SelectActivity(it => it.EntityId == acc.EntityId));

            PropertyBucket.Remember(platform, acc, true);
        }
        [When(@"I schedule a new (.*) Activity to send on (.*), (.*) with a title length of (.*), a notes length of (.*)")]
        public Activity WhenIScheduleANewSendMailingActivityToSendOnNowEasternStandardTimeWithATitleLengthOfANotesLengthOfAndOKAsExpectedResponseCode(string activityType, string time, string timezone, int titleTextLength, int notesTextLength)
        {
            var messageService = new MessageService(SessionKey);
            var titleText = messageService.GetRandomString(titleTextLength);
            var notesText = messageService.GetRandomString(titleTextLength);
            var timeFormated = messageService.formatTime(time);
            var type = ((int)activityType.ParseEnum<PublishActivityType>());

            // Publish the message as a new Activity
            var payload = messageService.PrepareActivity(type, timeFormated, timezone, titleText, notesText);
            var response = messageService.PostActivity(payload);
            PropertyBucket.Remember(RESP_KEY, response);
            return payload;
        }
        [Then(@"The message response was (.*)")]
        public void ThenTheResponseShouldBe(string status)
        {
            //  Get response from stream creation
            var response = PropertyBucket.GetProperty<IRestResponse<MessageResponse>>(RESP_KEY);

            //  Verify response's status
            Assert.AreEqual(status.Replace("\"", ""), response.StatusCode.ToString(), Err.Line("Wrong Status code on the response"));

        }

    }
}

