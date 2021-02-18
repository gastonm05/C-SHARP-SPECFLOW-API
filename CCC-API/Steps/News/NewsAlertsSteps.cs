using BoDi;
using CCC_API.Data.Responses.News;
using CCC_API.Services.News;
using CCC_API.Steps.Common;
using CCC_Infrastructure.Utils;
using CCC_API.Utils.Assertion;
using RestSharp;
using System.Linq;
using TechTalk.SpecFlow;
using Is = NUnit.Framework.Is;

namespace CCC_API.Steps.News
{
    public class NewsAlertsSteps : AuthApiSteps
    {

        public const string ALERTSKEY = "alerts", ALERTKEY = "alert";

        public NewsAlertsSteps(IObjectContainer objectContainer) : base(objectContainer) { }

        [When(@"I perform a GET for All News Alerts")]
        public void WhenIPerformAGETForAllNewsAlerts()
        {
            var alerts = new NewsAlertService(SessionKey).GetAllNewsAlerts();
            PropertyBucket.Remember(ALERTSKEY, alerts);
        }

        [When(@"I perform a GET for a Single News Alert")]
        public void WhenIPerformAGETForASingleNewsAlert()
        {
            var alerts = PropertyBucket.GetProperty<IRestResponse<NewsAlerts>>(ALERTSKEY);
            var alert = alerts.Data.Items.FirstOrError();
            var response = new NewsAlertService(SessionKey).GetSingleNewsAlert(alert.Id);
            PropertyBucket.Remember(ALERTSKEY, response, true);
            PropertyBucket.Remember(ALERTKEY, alert);
        }

        [When(@"I perform a GET for a Single News Alert with id '(.*)'")]
        public void WhenIPerformAGETForASingleNewsAlertWithId(int id)
        {
            var response = new NewsAlertService(SessionKey).GetSingleNewsAlert(id);
            PropertyBucket.Remember(ALERTKEY, response);
        }

        [Then(@"the news alert endpoint response should return a '(.*)' status")]
        public void ThenTheNewsAlertEndpointResponseShouldReturnAStatus(int code)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<NewsAlerts>>(ALERTKEY);
            Assert.That(Services.BaseApiService.GetNumericStatusCode(response), Is.EqualTo(code), "Expected status code not returned");
        }

        [Then(@"returned news alert id is the same as the requested alert id")]
        public void ThenReturnedNewsAlertIdIsTheSameAsTheRequestedAlertId()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<NewsAlerts>>(ALERTSKEY);
            var alert = PropertyBucket.GetProperty<NewsAlert>(ALERTKEY);
            Assert.That(response.Data.ItemCount, Is.GreaterThan(0), "No Alerts were returned");
            Assert.IsTrue(response.Data.Items.Any(a => a.Id.Equals(alert.Id)), $"Expected alert with id '{alert.Id}' not found");
        }
    }
}
