using BoDi;
using CCC_API.Services;
using CCC_API.Utils.Assertion;
using TechTalk.SpecFlow;
using ZukiniWrap;

namespace CCC_API.Steps.Common
{
    [Binding]
    public class SupportTranslationsSteps : ApiSteps
    {

        public SupportTranslationsSteps(IObjectContainer objectContainer) : base(objectContainer)
        {}

        private const string GET_RESULT_KEY = "GetResultKey";


        [Given(@"I get '(.*)' translations for '(.*)' release")]
        public void GivenIGetTranslationsForRelease(string sourceId, string release)
        {
            SupportTranslationsService supportTranslationsService = new SupportTranslationsService();
            string translations = supportTranslationsService.GetTranslations(sourceId, release);
            PropertyBucket.Remember(GET_RESULT_KEY, translations);
        }

        [Then(@"the value '(.*)' is present in translations")]
        public void ThenTheValueIsPresentInTranslations(string expectedValue)
        {
            string translations = PropertyBucket.GetProperty<string>(GET_RESULT_KEY);
            Assert.IsTrue(translations.Contains(expectedValue), $"Expected {expectedValue} to be present in translations");
        }

    }
}
