using BoDi;
using CCC_API.Data.Responses.Accounts;
using CCC_API.Data.Responses.Analytics;
using CCC_API.Data.Responses.Settings.AlertManagement;
using CCC_API.Services.Analytics;
using CCC_API.Services.Common;
using CCC_API.Services.Settings.AlertManagement;
using CCC_API.Steps.Analytics;
using CCC_API.Steps.Common;
using CCC_Infrastructure.API.Utils;
using CCC_Infrastructure.Utils;
using CCC_API.Utils.Assertion;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;
using Is = NUnit.Framework.Is;

namespace CCC_API.Steps.Settings.AlertManagement
{
    public class AlertManagementSteps : AuthApiSteps
    {
        private readonly AlertManagementService _alertManagement;

        public AlertManagementSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
            _alertManagement = new AlertManagementService(SessionKey);
        }

        private const string GET_ALERTS_RESPONSE_KEY = "alerts response key";

        [When(@"I perform a GET for Management Alerts")]
        public void WhenIPerformAgetForManagementAlerts()
        {
            var response = _alertManagement.GetManagementNewsAlerts();
            PropertyBucket.Remember(GET_ALERTS_RESPONSE_KEY, response);
        }

        [Then(@"the Alerts response code should be '(.*)'")]
        public void ThenTheAlertsResponseCodeShouldBe(int statusCode)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<ManagementAlerts>>(GET_ALERTS_RESPONSE_KEY);
            Assert.That(Services.BaseApiService.GetNumericStatusCode(response), Is.EqualTo(statusCode), $"Expected {statusCode} not received");
        }

        [Then(@"there should be a management alert named '(.*)'")]
        public void ThenThereShouldBeAnAlertNamed(string name)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<ManagementAlerts>>(GET_ALERTS_RESPONSE_KEY);
            Assert.True(response.Data.Items.Any(a => a.SearchName.Equals(name)), "Expected alert not found");
        }

        [Then(@"there should be '(.*)' alerts returned in the response")]
        public void ThenThereShouldBeAlertsReturnedInTheResponse(int itemCount)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<ManagementAlerts>>(GET_ALERTS_RESPONSE_KEY);
            Assert.That(response.Data.ItemCount, Is.EqualTo(itemCount), "Incorrect item count returned in response");
        }

        [Then(@"periodic email exists in admin alert management list with proper time, delivery days, status '(.*)', recipients")]
        public void ThenPeriodicEmailIsCreatedWithProperTimeAndDeliveryDays(int status)
        {
            var user = PropertyBucket.GetProperty<UserMe>(AccountInfoService.AccountSessionInfoEndPoint);
            var alertSettings = PropertyBucket.GetProperty<ShareRecurringEmail>(AnalyticsEmailReportSteps.EMAIL_CONFIG);
            var alerts = _alertManagement.GetManagementAnalyticsAlerts();
            var created = alerts.Items
                .Where(a => a.Subject == alertSettings.ReportData.Subject)
                .FirstOrError("Cannot find analytics alert with subject: " + alertSettings.ReportData.Subject);

            Assert.Multiple(() =>
            {
                var expSchedule = alertSettings.Schedule;
                Assert.AreEqual(expSchedule.DaysOfWeek, created.DaysOfWeek, "Delivery days");

                Assert.AreEqual(expSchedule.EndDate, created.EndDate, "End date");
                Assert.IsNotNull(created.Id, "Id");
                Assert.IsNotNull(created.JobId, "Job id");
                Assert.AreEqual(alertSettings.ReportData.Message, created.Message, "Email message");
                Assert.AreEqual(user.Id, created.OwnerId, "Owner id");
                Assert.AreEqual(user.LoginName, created.OwnerName, "Owner name");
                Assert.AreEqual(status, created.Status, "Status");
                Assert.AreEqual($"{expSchedule.Hour.ToString().PadLeft(2, '0')}:{expSchedule.Minute.ToString().PadLeft(2, '0')}:00", 
                    created.Time, "Time");
                Assert.AreEqual(alertSettings.ReportData.Recipients, created.Recipients, "Recipients");
            });

            PropertyBucket.Remember(AnalyticsEmailReportSteps.EMAIL_ALERT, created, true);
        }

        [Then(@"I unschedule \(DELETE\) periodic email alert")]
        [When(@"I unschedule \(DELETE\) periodic email alert")]
        public void WhenIUnschedulePeriodicEmailAlert()
        {
            var alert = PropertyBucket.GetProperty<AnalyticsAlert>(AnalyticsEmailReportSteps.EMAIL_ALERT);
            _alertManagement.DeleteAlert(alert.Id).CheckCode();
        }

        [When(@"I edit \(PUT\) analytics alert with new subject, recipients, delivery days '([^']+)' and time")]
        public void WhenIEditPutAnalyticsAlertWithNewTitleRecipientsDeliveryDaysAndTime(string days)
        {
            var alert = PropertyBucket.GetProperty<AnalyticsAlert>(AnalyticsEmailReportSteps.EMAIL_ALERT);
            alert.Subject = "Updated alert " + DateTime.Now;
            alert.Recipients = new List<string> { "upd" + StringUtils.RandomEmail(8, "gmail.com")};
            alert.DaysOfWeek = ShareAnalyticsReportService.EncodeScheduledDays(days.Split(','));
            alert.Message    = StringUtils.RandomAlphaNumericString(1000);

            var time = DateTime.Now.AddHours(-5);
            alert.Time = $"{time.Hour.ToString().PadLeft(2, '0')}:{time.Minute}:00";
            alert.EndDate = null;
            
            _alertManagement.UpdateAnalyticsAlert(alert).CheckCodeGetData();

            // Update alerts settings in property bucket for future verification
            var alertSettings = PropertyBucket.GetProperty<ShareRecurringEmail>(AnalyticsEmailReportSteps.EMAIL_CONFIG);
            var updatedData            = alertSettings.ReportData;
            updatedData.Subject        = alert.Subject;
            updatedData.Recipients     = alert.Recipients;
            updatedData.Message        = alert.Message;
            alertSettings.ReportData   = updatedData;

            var updatedSchedule        = alertSettings.Schedule;
            updatedSchedule.Hour       = time.Hour;
            updatedSchedule.Minute     = time.Minute;
            updatedSchedule.EndDate    = alert.EndDate;
            updatedSchedule.DaysOfWeek = alert.DaysOfWeek;
            alertSettings.Schedule     = updatedSchedule;

            PropertyBucket.Remember(AnalyticsEmailReportSteps.EMAIL_CONFIG, alertSettings, true);
        }
    }
}
