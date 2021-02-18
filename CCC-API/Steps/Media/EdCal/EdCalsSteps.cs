using BoDi;
using CCC_API.Data.Responses.Common;
using CCC_API.Data.Responses.Media.EdCal;
using CCC_API.Services.Media.EdCal;
using CCC_API.Steps.Common;
using CCC_Infrastructure.Utils;
using CCC_API.Utils.Assertion;
using RestSharp;
using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;
using static CCC_API.Services.Media.EdCal.EdCalsService;
using Is = NUnit.Framework.Is;

namespace CCC_API.Steps.Media.EdCal
{
    public class EdCalsSteps : AuthApiSteps
    {

        public EdCalsSteps(IObjectContainer objectContainer) : base(objectContainer) { }

        public const string GET_EDCALS_RESPONSE_KEY = "GetEdCalsResponse";

        #region When Steps

        [When(@"I perform a GET for EdCals by '(.*)' '(.*)'")]
        public void WhenIPerformAGETForEdCalsBy(EdCalSearchCriteria criteria, string parameter)
        {            
            var response = new EdCalsService(SessionKey).GetEdCals(criteria, parameter);
            PropertyBucket.Remember(GET_EDCALS_RESPONSE_KEY, response);
        }

        [When(@"I perform a GET for EdCals by Issue Date with a '(start|end)' date of '(.*)'")]
        public void WhenIPerformAGETForEdCalsByIssueDateWithAStartDateOf(string dateType, DateTime date)
        {
            var response = new EdCalsService(SessionKey).GetEdCalsByIssueStartOrEndDate(dateType, date);
            PropertyBucket.Remember(GET_EDCALS_RESPONSE_KEY, response);
        }

        [When(@"I perform a GET for EdCals by Issue Date with a start date of '(.*)' and an end date of '(.*)'")]
        public void WhenIPerformAGETForEdCalsByIssueDateWithAStartDateOfAndAnEndDateOf(DateTime startDate, DateTime endDate)
        {
            var response = new EdCalsService(SessionKey).GetEdCalsByIssueDate(startDate, endDate);
            PropertyBucket.Remember(GET_EDCALS_RESPONSE_KEY, response);
        }

        [When(@"I perform a POST to export EdCals")]
        public void WhenIPerformAPOSTToExportEdCals()
        {
            IRestResponse<EdCals> response = PropertyBucket.GetProperty<IRestResponse<EdCals>>(GET_EDCALS_RESPONSE_KEY);
            var postResponse = new EdCalsService(SessionKey).ExportEdCals(response.Data.Key);
            PropertyBucket.Remember(GET_EDCALS_RESPONSE_KEY, postResponse, true);
        }

        #endregion

        #region Then Steps

        [Then(@"all returned outlets should contain '(.*)' in their name")]
        public void ThenAllReturnedOutletsShouldContainInTheirName(string name)
        {            
            IRestResponse<EdCals> response = PropertyBucket.GetProperty<IRestResponse<EdCals>>(GET_EDCALS_RESPONSE_KEY);
            List<EdCalsItem> items = response.Data.Items;
            items.ForEach(i => Assert.IsTrue(i.OutletFullName.Contains(name), $"Not all results contain '{name}'; Found Outlet Name: '{i.OutletFullName}'"));
        }

        [Then(@"all returned EdCals should have '(.*)' as their Contact name")]
        public void ThenAllReturnedEdCalsShouldHaveAsTheirContactName(string name)
        {
            IRestResponse<EdCals> response = PropertyBucket.GetProperty<IRestResponse<EdCals>>(GET_EDCALS_RESPONSE_KEY);
            List<EdCalsItem> items = response.Data.Items;
            items.ForEach(i => Assert.That(i.ContactSortName, Is.EqualTo(name), $"Not all Contact Names are '{name}'; Found Contact Name: '{i.ContactSortName}'"));
        }

        [Then(@"all returned EdCals should have '(.*)' as their Outlet Country")]
        public void ThenAllReturnedEdCalsShouldHaveAsTheirOutletCountry(string country)
        {
            IRestResponse<EdCals> response = PropertyBucket.GetProperty<IRestResponse<EdCals>>(GET_EDCALS_RESPONSE_KEY);
            List<EdCalsItem> items = response.Data.Items;
            items.ForEach(i => Assert.That(i.OutletCountryName, Is.EqualTo(country), $"Not all Countrys are '{country}'; Found Country: '{i.OutletCountryName}'"));
        }

        [Then(@"all returned EdCals should have an Issue Date '(greater|less)' than or equal to '(.*)'")]
        public void ThenAllReturnedEdCalsShouldHaveAnIssueDateThanOrEqualTo(string comparer, DateTime date)
        {
            IRestResponse<EdCals> response = PropertyBucket.GetProperty<IRestResponse<EdCals>>(GET_EDCALS_RESPONSE_KEY);
            var items = response.Data.Items;
            Assert.That(items.Count, Is.GreaterThan(0), "No EdCals were returned in the response");

            switch (comparer)
            {
                case "greater":
                    items.ForEach(i => Assert.That(i.IssueDate, Is.GreaterThanOrEqualTo(date), "Not all dates are greater than or equal to the start date"));
                    break;

                case "less":
                    items.ForEach(i => Assert.That(i.IssueDate, Is.LessThanOrEqualTo(date), "Not all dates are less than or equal to the end date"));
                    break;

                default:
                    throw new ArgumentException(Err.Msg("Comparer must be either greater or less"));
            }
        }

        [Then(@"all returned EdCals should have an Issue Date between '(.*)' and '(.*)'")]
        public void ThenAllReturnedEdCalsShouldHaveAnIssueDateBetweenAnd(DateTime startDate, DateTime endDate)
        {
            IRestResponse<EdCals> response = PropertyBucket.GetProperty<IRestResponse<EdCals>>(GET_EDCALS_RESPONSE_KEY);
            var items = response.Data.Items;
            Assert.That(items.Count, Is.GreaterThan(0), "No EdCals were returned in the response");
            items.ForEach(i => Assert.That(i.IssueDate, Is.InRange(startDate, endDate), "Not all dates are in between the start and end date"));
        }

        [Then(@"the EdCals Export response has a valid id, status, and file")]
        public void ThenTheEdCalsExportResponseHasAValidIdStatusAndFile()
        {
            IRestResponse<JobResponse> response = PropertyBucket.GetProperty<IRestResponse<JobResponse>>(GET_EDCALS_RESPONSE_KEY);
            var data = response.Data;
            Assert.Multiple(() =>
            {
                Assert.That(data.Id, Is.Not.Null, "Id was null and should have a valid value");
                Assert.That(data.Status.Message, Is.Null, "Message was not null and should be");
                Assert.That(data.Status.Progress, Is.EqualTo(0), "Progress was not equal to zero");
                Assert.That(data.Status.State, Is.Not.EqualTo("Failed"), "State was 'Failed', file download failed");
                Assert.That(data._links.file, Is.Not.Null, "File url is null and should not be");
                Assert.That(data._links.self, Is.EqualTo($"jobs/{data.Id}"), "Self url was not correct");
            });
        }

        #endregion

    }
}
