using BoDi;
using CCC_API.Data.PostData.Settings.CustomFields;
using CCC_API.Data.Responses.Activities;
using CCC_API.Data.Responses.Campaigns;
using CCC_API.Data.TestDataObjects.Activities;
using CCC_API.Services.Activities;
using CCC_API.Steps.Activities;
using CCC_API.Steps.Campaigns;
using CCC_API.Steps.Common;
using CCC_Infrastructure.Utils;
using CCC_API.Utils.Assertion;
using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;
using static CCC_API.Services.Analytics.Common;
using Is = NUnit.Framework.Is;

namespace CCC_API.Steps.Analytics
{
    [Binding]
    public class AnalyticsActivityCountsEndPointSteps : AuthApiSteps
    {
        private const string ACTIVITY_COUNTS = "ActivityCounts";
        private const string FREQUENCY_USED_KEY = "FrequencyExpected";

        private const string _campaign = "Campaign", _campaigns = "Campaigns";

        private CampaignsSteps _campaignSteps;
        private PublishActivitySteps _publishActivitySteps;
        private CustomActivitySteps _customActivitySteps;
        private MyActivitiesService _activitiesService;
        private CustomActivityService _customActivityService;

        public AnalyticsActivityCountsEndPointSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
            _campaignSteps = new CampaignsSteps(objectContainer);
            _publishActivitySteps = new PublishActivitySteps(objectContainer);
            _customActivitySteps = new CustomActivitySteps(objectContainer);
            _activitiesService = new MyActivitiesService(SessionKey);
            _customActivityService = new CustomActivityService(SessionKey);
        }

        [When(@"I perform a GET for activity counts for frequency '(.*)'")]
        public void WhenIPerformAGETForActivityCountsForFrequency(Frequency frequency)
        {
            const string SENT_PUBLICATION_STATE = "2";
            var activitiesService = new MyActivitiesService(SessionKey);
            var counts = activitiesService.GetCounts(frequency, SENT_PUBLICATION_STATE);
            PropertyBucket.Remember(ACTIVITY_COUNTS, counts);
            PropertyBucket.Remember(FREQUENCY_USED_KEY, frequency);
        }

        [Then(@"the activity counts endpoint returns activities by frequency '(.*)'")]
        public void ThenTheActivityCountsEndpointReturnsActivities(Frequency frequency)
        {
            var counts = PropertyBucket.GetProperty<ActivityCounts[]>(ACTIVITY_COUNTS);
            Assert.That(counts.Length, Is.GreaterThan(0)); // there are activities

            foreach (var count in counts)
            {
                Assert.That(count.ActivityCount, Is.GreaterThan(0)); // activities have counts
                Assert.That(count.Timestamp, Is.GreaterThan(0)); // timestamps are valid
                Assert.That(count.Date.ToLocalTime(), Is.EqualTo(DateTimeUtil.FromTimeStamp(count.Timestamp).ToLocalTime())); // sanity

                switch (frequency)
                {
                    case Frequency.Weekly:
                        Assert.That(count.Date.DayOfWeek, Is.EqualTo(System.DayOfWeek.Sunday));
                        break;
                    case Frequency.Monthly:
                        Assert.That(count.Date.Day, Is.EqualTo(1));
                        break;
                    case Frequency.Yearly:
                        Assert.That(count.Date.DayOfYear, Is.EqualTo(1));
                        break;
                }
            }
        }

        [When(@"I create one campaign '(.*)' and I assign to it an activity '(.*)' of type '(.*)' with time (.*) '(.*)'")]
        public void WhenICreateOneCampaignAndIAssignToItAnActivityOfTypeWithTime(string campaignName, string activityName, string type, double scheduleTime, string timeZoneIdentifier)
        {
            _campaignSteps.WhenIPerformPostToCampaigns(1);
            Campaign campaign = PropertyBucket.GetProperty<Campaign>(_campaign);

            CustomActivityService customActivityService = new CustomActivityService(SessionKey);

            var time = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow.AddDays(scheduleTime), timeZoneIdentifier);
            List<int> campaignsId = new List<int>();
            campaignsId.Add(campaign.Id);
            var data = new CustomActivity
            {
                Title = activityName,
                Type = ((int) type.ParseEnum<PublishActivityType>()).ToString(),
                TimeZoneIdentifier = timeZoneIdentifier ?? null,
                ScheduleTime = string.Format("{0:s}", time),
                CampaignIds= campaignsId,
                CustomFields = new List<AllowValue>()
            };

            customActivityService.PostActivity(data);

            var activity = _activitiesService.GetActivitiesByCampaign(campaign.Id.ToString())
                        .SelectActivity(it => it.Title == activityName);
            PropertyBucket.Remember(type, activity, true);
        }


        [Then(@"I can get the activity by campaignId grouped by '(.*)' with value '(.*)'")]
        public void ThenICanGetTheActivityByCampaignIdGroupedByType(string field, string value)
        {
            Campaign campaign = PropertyBucket.GetProperty<Campaign>(_campaign);

            ActivityCounts[] activityCounts = _activitiesService.GetActivitiesByCampaignGroupedBy(field, campaign.Id.ToString());

            if (field.Equals("Type"))
            {
                var exp = ((int) value.ParseEnum<PublishActivityType>()).ToString();
                Assert.That(activityCounts[0].Type, Is.EqualTo(exp), "Type is incorrect");
            }
            else if (field.Equals("PublicationState"))
            {
                Assert.That(activityCounts[0].PublicationState, Is.EqualTo(Int32.Parse(value)), "PublicationState is incorrect");
            }
            else
            {
                Assert.Fail("The selected field is incorrect. Valid fields: Type/PublicationState");
            }

            
        }

        [Then(@"I can delete the activity with type '(.*)' and the campaign")]
        public void ThenICanDeleteTheActivityAndTheCampaign(string type)
        {
            _publishActivitySteps.WhenIDeleteFromCampaign(type);
            _customActivitySteps.WhenICanDeleteCustomActivity("can",type);
            _campaignSteps.ThenICanDeleteCampaign();
        }


    }
}

