using BoDi;
using CCC_API.Data.Responses.Impact;
using CCC_API.Data.Responses.Impact.CisionId;
using CCC_API.Steps.Common;
using System.Linq;
using TechTalk.SpecFlow;
using static CCC_API.Services.Impact.ImpactService;
using CCC_API.Utils.Assertion;
using Is = NUnit.Framework.Is;

namespace CCC_API.Services.Impact
{
    public class ImpactPressReleasesSteps : AuthApiSteps
    {
        public const string RESPONSE = "response";
        public const string CISION_ID_RESPONSE = "cision_id_response";
        public const string EMAIL_LOGIN = "admin@cision.com";
        public const string PASSWORD_LOGIN = "password";
        public const SortField DEFAULT_SORT_FIELD_CISION_ID = SortField.orderPart;


        private ImpactService _impactService;

        public ImpactPressReleasesSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
            _impactService = new ImpactService(SessionKey);
        }

        [When(@"I call the releases endpoint with (.*) sorting by (.*) field, (.*) limit and (.*) page (.*) all accounts")]
        public void WhenICallTheReleasesEndpointWithSortingByFieldLimitAndPageAllAccounts(SortDirection sorting, SortField field, int limit, int page, AllAccounts allAccounts)
        {
            ReleasesImpact response = _impactService.GetReleases(limit, page, sorting, field, allAccounts);
            PropertyBucket.Remember(RESPONSE, response);
        }

        [When(@"I call the datebounds endpoint")]
        public void WhenICallTheDateboundsEndpoint()
        {
            Datebounds response = _impactService.GetDatebound();
            PropertyBucket.Remember(RESPONSE, response);
        }

        [When(@"I call the cisionId releases endpoint with (.*) sorting by (.*) field, (.*) limit, (.*) page and (.*) all accounts")]
        public void WhenICallTheCisionIdReleasesEndpointWithSortingByFieldLimitPageAndAllAccounts(SortDirection sorting, SortField field, int limit, int page, AllAccounts allAccounts)
        {
            ReleasesCisionId responseReleaseImact = _impactService.GetReleasesCisionId(limit, page, sorting, field, allAccounts);
            PropertyBucket.Remember(CISION_ID_RESPONSE, responseReleaseImact);
        }

        [Then(@"the releases endpoint has the correct response")]
        public void ThenTheReleasesEndpointHasTheCorrectResponse()
        {
            ReleasesImpact response = PropertyBucket.GetProperty<ReleasesImpact>(RESPONSE);
            Releases[] releases = response.Releases;
            Assert.That(response.PageSize > 0, "There are not releases on this company");
            Assert.That(response.TotalCount > 0, "There are not releases on this company");
            Assert.That(response.TotalPages > 0, "There are not releases on this company");
            Assert.That(releases.All(i => !string.IsNullOrEmpty(i.Headline)),"Not all releases have valid headline");
            Assert.That(releases.All(i => !string.IsNullOrEmpty(i.Id)), "Not all releases have valid Id");
            Assert.That(releases.All(i => !string.IsNullOrEmpty(i.Language)), "Not all releases have valid language Id");
            Assert.That(releases.All(i => !string.IsNullOrEmpty(i.LanguageCode)), "Not all releases have valid language code");
        }

        [Then(@"the datebounds endpoint has the correct response")]
        public void ThenTheDateboundsEndpointHasTheCorrectResponse()
        {
            Datebounds response = PropertyBucket.GetProperty<Datebounds>(RESPONSE);
            Assert.That(response.MaxDate, !Is.Empty, "The MaxDate is not correct");
            Assert.That(response.MinDate, !Is.Empty, "The MinDate is not correct");
        }

        [Then(@"both retrieved data match")]
        public void ThenBothRetrievedDataMatch()
        {
            ReleasesCisionId responseCisionID = PropertyBucket.GetProperty<ReleasesCisionId>(CISION_ID_RESPONSE);
            ReleasesImpact responseRelease = PropertyBucket.GetProperty<ReleasesImpact>(RESPONSE);

            if (responseCisionID.Status == 200)
            {
                Assert.That(responseCisionID.PageSize == responseRelease.PageSize, "The page size is not correct");
                Assert.That(responseCisionID.ResultsCount == responseRelease.TotalCount, "The total count is not correct");
                Assert.That(responseCisionID.TotalPages == responseRelease.TotalPages, "The total page is not correct");

                for (int i = 0; i < responseCisionID.ResultsCount; i++)
                {
                    Assert.That(responseCisionID.Data[i].Headline.ToLower(), Is.EqualTo(responseRelease.Releases[i].Headline.ToLower()), "The Headlines are not the same");
                    Assert.That(responseCisionID.Data[i].OrderPart, Is.EqualTo(responseRelease.Releases[i].Id), "The Releases Ids are not the same");
                    Assert.That(responseCisionID.Data[i].Lang, Is.EqualTo(responseRelease.Releases[i].LanguageCode), "The language codes are not the same");
                    Assert.That(responseCisionID.Data[i].StoryDate, Is.EqualTo(responseRelease.Releases[i].Date), "The stories dates are not the same");
                }
            } else
            {
                Assert.That(responseRelease.TotalCount == 0, "The releases endpoint returned data that is not present in Cision Id");
            }
        }
    }
}