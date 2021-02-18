using BoDi;
using CCC_API.Services.Analytics;
using CCC_API.Steps.Common;
using CCC_API.Utils.Assertion;
using TechTalk.SpecFlow;
using Is = NUnit.Framework.Is;

namespace CCC_API.Steps
{
    [Binding]
    public class AnalyticsArticlesAndOutletsEndPointSteps : AuthApiSteps
    {
        public const string GET_RESPONSE_KEY = "GetResponse";
        public const string GET_CONTENT_KEY = "GetContent";
        public const string COUNT_OF_KEY = "CountOf";

        public AnalyticsArticlesAndOutletsEndPointSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
        }

        [When(@"I perform a GET for outlets")]
        public void WhenIPerformAGETForOutlets()
        {
            var content = new ArticlesAndOutletsService(SessionKey).GetAllOutlets();
            PropertyBucket.Remember(GET_CONTENT_KEY, content);
        }

        [When(@"I perform a GET for '(.*)' outlets with '(.*)' pagination offset that are sorted by '(.*)'")]
        public void WhenIPerformAGetForOutletsWithPaginationOffset(int outletCount, int offset, ArticlesAndOutletsService.OutletSortField sortField)
        {
            var count = new ArticlesAndOutletsService(SessionKey).GetOutletsCount(outletCount, offset, sortField);
            PropertyBucket.Remember(COUNT_OF_KEY, count);
        }

        [When(@"I perform a GET for '(.*)' articles with '(.*)' pagination offset that are sorted by '(.*)'")]
        public void WhenIPerformAGETForArticlesWithPaginationOffsetThatAreSortedBy(int outletCount, int offset, ArticlesAndOutletsService.ArticleSortField sortField)
        {
            var count = new ArticlesAndOutletsService(SessionKey).GetArticlesCount(outletCount, offset, sortField);
            PropertyBucket.Remember(COUNT_OF_KEY, count);
        }

        [Then(@"there are NOD outlets")]
        public void ThenThereAreNODOutlets()
        {
            var content = PropertyBucket.GetProperty<string>(GET_CONTENT_KEY);
            var contentWithNoWhiteSpace = content.Replace(" ", string.Empty);
            Assert.IsTrue(contentWithNoWhiteSpace.Contains("\"IsNODOutlet\""), $"Could not find 'IsNodOutlet' property in response: {content}");
            Assert.IsTrue(contentWithNoWhiteSpace.Contains("\"IsNODOutlet\":true"), $"NOD outlet not found: {content}");
            Assert.IsFalse(contentWithNoWhiteSpace.Contains("\"IsNODOutlet\":0"), "Unexpected format for 'IsNodOutlet' property: false as 0");
            Assert.IsFalse(contentWithNoWhiteSpace.Contains("\"IsNODOutlet\":1"), "Unexpected format for 'IsNodOutlet' property: true as 1");
            Assert.IsFalse(contentWithNoWhiteSpace.Contains("\"IsNODOutlet\":\""), "Unexpected format for 'IsNodOutlet' property: string value");
        }

        [Then(@"there are '(.*)' (articles|outlets)")]
        public void ThenThereAreOutlets(int expected, string articlesOrOutletsLabel)
        {
            var actual = PropertyBucket.GetProperty(COUNT_OF_KEY);
            Assert.That(actual, Is.EqualTo(expected), $"Wrong number of {articlesOrOutletsLabel}");
        }
    }
}
