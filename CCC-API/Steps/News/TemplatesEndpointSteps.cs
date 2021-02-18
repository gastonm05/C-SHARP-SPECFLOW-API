using BoDi;
using CCC_API.Data.Responses.News;
using CCC_API.Services.News;
using CCC_API.Steps.Common;
using CCC_API.Utils.Assertion;
using RestSharp;
using TechTalk.SpecFlow;

namespace CCC_API.Steps.News
{
    [Binding]
    public class TemplatesEndpointSteps : AuthApiSteps
    {
        public TemplatesEndpointSteps(IObjectContainer objectContainer) : base(objectContainer) { }

        private const string GET_NEWS_TEMPLATES_RESPONSE_KEY = "GetNewsTemplatesResponse";
        private const string GET_NEWS_SINGLE_CUSTOM_TEMPLATE_KEY = "GetNewsSingleCustomTemplateResponse";

        #region When Steps
        /// <summary>
        /// Performs a GET to endpoint api/v1/news/templates
        /// </summary>
        [When(@"I perform a GET for all news templates")]
        public void WhenIPerformAGETForAllNewsTemplates()
        {
            var response = new TemplatesService(SessionKey).GetAllNewsTemplates();
            PropertyBucket.Remember(GET_NEWS_TEMPLATES_RESPONSE_KEY, response);
        }

        /// <summary>
        /// 
        /// </summary>
        [When(@"I get a single custom template")]
        public void WhenIGetASingleCustomTemplate()
        {
            IRestResponse<Templates> templateResponse = PropertyBucket.GetProperty<IRestResponse<Templates>>(GET_NEWS_TEMPLATES_RESPONSE_KEY);
            var templateID = "";
            foreach( var response in templateResponse.Data.Items)
            {
                if (response.Id.Contains("XT"))
                {
                    templateID = response.Id;
                    break;
                }
            }
            var singleTemplateResponse = new TemplatesService(SessionKey).GetSingletemplate(templateID);
            PropertyBucket.Remember(GET_NEWS_SINGLE_CUSTOM_TEMPLATE_KEY, singleTemplateResponse);
        }
        #endregion

        #region Then Steps
        [Then(@"I should see a value of False for all permissions")]
        public void ThenIShouldSeeABoolValueForAGivenProperty()
        {
            IRestResponse<SingleTemplate> singleTemplateResponse = PropertyBucket.GetProperty<IRestResponse<SingleTemplate>>(GET_NEWS_SINGLE_CUSTOM_TEMPLATE_KEY);
            Assert.IsFalse(singleTemplateResponse.Data._meta.IsDefault, "IsDefault is true");
            Assert.IsFalse(singleTemplateResponse.Data._meta.CanCopy, "CanCopy is true");
        }
        /// <summary>
        /// Verifies promo for custom templates is present when parameter is set to true
        /// Toggle the company parameter NewsForward-Template-Promo-Enabled to true/false to show/hide the promo for custom templates
        /// </summary>
        [Then(@"I should see promo for custom templates when a company has this enabled and link should be (.*)")]
        public void ThenIShouldSeePromoForCustomTemplatesIsAvailable(string link)
        {

            // Get News templates remembered from the Property Bucket
            IRestResponse<Templates> templatesResponse = PropertyBucket.GetProperty<IRestResponse<Templates>>(GET_NEWS_TEMPLATES_RESPONSE_KEY);
            Assert.IsNotEmpty(templatesResponse.Data.Items);

            foreach (var response in templatesResponse.Data.Items)
            {
                // Verify the isPromo key is present when the parameter is enabled
                if (response.isPromo != null)
                {
                    Assert.AreEqual(true, response.isPromo, "The value for isPromo field is not the expected");
                    Assert.AreEqual(link, response.promoLink, "The URL link on the response is not the expected");
                    break;
                }
            }
        }
        /// <summary>
        /// Verifies Promo for custom templates is disabled by default
        /// Toggle the company parameter NewsForward-Template-Promo-Enabled to true/false to show/hide the promo for custom templates
        /// </summary>
        [Then(@"I verify promo for custom templates is disabled by default")]
        public void ThenIVerifyPromoForCustomTemplatesIsDisabledByDefault()
        {
            // Get News templates remembered from the Property Bucket
            IRestResponse<Templates> templatesResponse = PropertyBucket.GetProperty<IRestResponse<Templates>>(GET_NEWS_TEMPLATES_RESPONSE_KEY);
            Assert.IsNotEmpty(templatesResponse.Data.Items);

            foreach (var response in templatesResponse.Data.Items)
            {
                
                //  Verify the isPromo key is not present by default
                if (response.isPromo != null)
                {
                    Assert.Fail("isPromo payload should not be present by default");
                    break;
                }
            }
        }
        #endregion
    }
}
