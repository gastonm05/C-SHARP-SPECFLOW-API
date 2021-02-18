using BoDi;
using CCC_API.Data.Responses.Common;
using CCC_API.Services.Common.Jobs;
using CCC_API.Steps.Media.Contact;
using CCC_API.Steps.Media.EdCal;
using CCC_Infrastructure.Utils;
using CCC_API.Utils.Assertion;
using RestSharp;
using TechTalk.SpecFlow;
using ZukiniWrap;

namespace CCC_API.Steps.Common
{
    /// <summary>
    /// This class represents steps associated with jobs (Tasks)
    /// </summary>
    /// <seealso cref="CCC_API.Steps.Common.AuthApiSteps" />
    public class JobSteps : AuthApiSteps
    {
        public JobSteps(IObjectContainer objectContainer) : base(objectContainer) { }

        public Key GET_OUTLET_EXPORT_RESPONSE_KEY; // also in OutletsSteps
        public Key GET_JOBS_RESPONSE_KEY;

        [When(@"I perform a GET for EdCal jobs with the id from the export")]
        public void WhenIPerformAGETForEdCalJobsWithTheIdFromTheExport()
        {
            IRestResponse<JobResponse> edcalResponse = PropertyBucket.GetProperty<IRestResponse<JobResponse>>(EdCalsSteps.GET_EDCALS_RESPONSE_KEY);
            var response = new JobsService(SessionKey).GetJobs(edcalResponse.Data.Id);
            PropertyBucket.Remember(GET_JOBS_RESPONSE_KEY, response);
        }

        [When(@"I perform a GET for Outlet jobs with the id from the export")]
        public void WhenIPerformAGETForOutletJobsWithTheIdFromTheExport()
        {
            IRestResponse<JobResponse> outletResponse = PropertyBucket.GetProperty<IRestResponse<JobResponse>>(GET_OUTLET_EXPORT_RESPONSE_KEY);
            var response = new JobsService(SessionKey).GetJobs(outletResponse.Data.Id);
            PropertyBucket.Remember(GET_JOBS_RESPONSE_KEY, response);
        }

        [When(@"I perform a GET for Contacts jobs with the id from the export")]
        public void WhenIPerformAGETForContactsJobsWithTheIdFromTheExport()
        {
            IRestResponse<JobResponse> contactResponse = PropertyBucket.GetProperty<IRestResponse<JobResponse>>(ContactSearchSteps.GET_CONTACTS_EXPORT_RESPONSE);
            var response = new JobsService(SessionKey).GetJobs(contactResponse.Data.Id);
            PropertyBucket.Remember(GET_JOBS_RESPONSE_KEY, response);
        }

        [Then(@"the job response status code is '(.*)'")]
        public void ThenTheJobResponseStatusCodeIs(int statusCode)
        {
            IRestResponse<JobResponse> response = PropertyBucket.GetProperty<IRestResponse<JobResponse>>(GET_JOBS_RESPONSE_KEY);
            Assert.AreEqual(statusCode, Services.BaseApiService.GetNumericStatusCode(response),Err.Line("The status code are no equal"));
        }
    }
}
