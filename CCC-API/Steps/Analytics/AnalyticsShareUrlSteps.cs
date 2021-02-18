using BoDi;
using CCC_API.Data.Responses.Analytics;
using CCC_API.Services.Analytics;
using CCC_API.Steps.Common;
using CCC_Infrastructure.Utils;
using CCC_API.Utils.Assertion;
using RestSharp;
using System;
using System.Net;
using TechTalk.SpecFlow;
using Does = NUnit.Framework.Does;

namespace CCC_API.Steps.Analytics
{
    [Binding]
    public class AnalyticsShareUrlSteps : AuthApiSteps
    {
        private readonly ShareUrlService _shareUrlService;
        public const string VIEW_PASS_KEY = "View pass";
        public const string RESP_KEY = "Response";
        public const string SHARE_URL_KEY = "shared url";

        public AnalyticsShareUrlSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
            _shareUrlService = new ShareUrlService(SessionKey);
        }
        
        [When(@"I share \(POST\) report as url with view id and (a|no) password")]
        public IRestResponse WhenISharePostReportAsUrlWithValidViewIdAndPassword(string pass)
        {
            var viewId = PropertyBucket.GetProperty<ViewsViewResponse>(AnalyticsDashboardsEndPointSteps.VIEW_KEY).Id;
            var password = pass.Contains("no") ? "" : StringUtils.RandomAlphaNumericString(5) + DateTime.Now;
            PropertyBucket.Remember(VIEW_PASS_KEY, password, true);

            var resp = _shareUrlService.ShareUrl(Convert.ToInt16(viewId), password);
            PropertyBucket.Remember(RESP_KEY, resp, true);
            return resp;
        }

        [Then(@"valid url is generated")]
        public string ThenValidUrlIsGenerated()
        {
            var resp = PropertyBucket.GetProperty<IRestResponse>(RESP_KEY);
            Assert.AreEqual(HttpStatusCode.OK, resp.StatusCode, "Failed to generate url");

            var urlString = resp.Content.Replace("\"", "");
            Assert.That(urlString, Does.Contain("accessKey"), "Wrong share url generated");
            PropertyBucket.Remember(SHARE_URL_KEY, urlString);
            return urlString;
        }

        [Then(@"the response: ""(.*)""")]
        public void ThenTheResponse(string message)
        {
            var resp = PropertyBucket.GetProperty<IRestResponse>(RESP_KEY);
            Assert.AreEqual(HttpStatusCode.BadRequest, resp.StatusCode, "Failed to generate url");

            var mess = resp.Content;
            Assert.That(mess, Does.Contain(message), "Wrong error message");
        }
    }
}
