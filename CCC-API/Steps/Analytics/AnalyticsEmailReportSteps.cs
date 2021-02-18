using BoDi;
using CCC_API.Data.Responses.Analytics;
using CCC_API.Services.Analytics;
using CCC_API.Services.Common;
using CCC_API.Services.Common.db;
using CCC_API.Steps.Common;
using CCC_Infrastructure.API.Utils;
using CCC_Infrastructure.Utils;
using Newtonsoft.Json;
using CCC_API.Utils.Assertion;
using System;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;
using Is = NUnit.Framework.Is;

namespace CCC_API.Steps.Analytics
{
    [Binding]
    public class AnalyticsEmailReportSteps : AuthApiSteps
    {
        public const string EMAIL_CONFIG = "email config";
        public const string EMAIL_ALERT = "email periodic alert";

        private ShareAnalyticsReportService _shareService;

        public AnalyticsEmailReportSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
            _shareService = new ShareAnalyticsReportService(SessionKey);
        }

        [When(@"I schedule \(POST\) '(.*)' email alert with settings '([^']+)'")]
        public void WhenIScheduleOngoingEmailAlert(DateTime time, string file)
        {
            WhenISchedulePostEmailAlertWithSettingsAndDays(time, file, "Mon,Tue,Wed,Thu,Fri,Sat,Sun");
        }

        [When(@"I schedule \(POST\) '(.*)' email alert with settings '(.*)' and days: '(.*)'")]
        public void WhenISchedulePostEmailAlertWithSettingsAndDays(DateTime time, string file, string days)
        {
            var rawText = PropertyBucket.GetProperty<string>(file);
            var defaultConfiguration = JsonConvert.DeserializeObject<ShareRecurringEmail>(rawText);

            var emailConfig = defaultConfiguration.ReportData;
            emailConfig.From = StringUtils.RandomEmail(10);
            emailConfig.Message = StringUtils.RandomSentence(10);
            emailConfig.Recipients = Enumerable.Range(1, 10).Select(_ => StringUtils.RandomEmail(12)).ToList();

            emailConfig.Subject = "Periodic email API " + Guid.NewGuid();
            emailConfig.Message = StringUtils.RandomSentence(20);

            var schedule = defaultConfiguration.Schedule;
            var user = new AccountInfoService(SessionKey).Me.User;
            PropertyBucket.Remember(AccountInfoService.AccountSessionInfoEndPoint, user);

            var userTime = DateTime.Now.AddMinutes(5).ConvertIntoTimezone(user.TimeZone.Id);

            schedule.EndDate = time;
            if (time == DateTime.MinValue) schedule.EndDate = null;

            schedule.Hour = userTime.Hour;
            schedule.Minute = userTime.Minute;
            schedule.DaysOfWeek = ShareAnalyticsReportService.EncodeScheduledDays(days.Split(','));

            defaultConfiguration.ReportData = emailConfig;
            defaultConfiguration.Schedule = schedule;

            _shareService.SchedulePeriodicEmail(defaultConfiguration).CheckCode();
            PropertyBucket.Remember(EMAIL_CONFIG, defaultConfiguration);
        }

        [Then(@"job in scheduled in DB for days '([^']+)' excluding days '(.*)'")]
        public void ThenJobInScheduledInDbForDays(string expDays, string excludedDays)
        {
            var user = new AccountInfoService(SessionKey).Me;
            using (var service = new JobsDbService(user.Profile.AccountId))
            {
                var jobs = service.GetRecentJobScheduleJobs(user.User.Id);
                var row = jobs.Where(r => r.Frequency == "W").FirstOrError("Cannot find DB record with frequency");
                var scheduledRow = (IDictionary<string, object>) row;
                Assert.That(scheduledRow, Is.Not.Null.And.Not.Empty, "Empty row");

                foreach (var scheduledDay in expDays.Split(','))
                {
                    Assert.AreEqual(true, scheduledRow[scheduledDay]);
                }

                foreach (var scheduledDay in excludedDays.Split(','))
                {
                    Assert.AreEqual(false, scheduledRow[scheduledDay]);
                }

                Assert.That(scheduledRow["StartTime"], Is.Not.Null.And.Not.Contain("0000"), "Start time not populated");
                Assert.That(scheduledRow["StartDate"]?.ToString(), Is.Not.Empty, "Start time not populated");
                Assert.That(scheduledRow["NextRunDate"]?.ToString(), Is.Not.Empty, "Start time not populated");
            }
        }

        
    }
}
