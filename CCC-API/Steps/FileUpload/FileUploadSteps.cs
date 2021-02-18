using BoDi;
using CCC_API.Services.FileUpload;
using CCC_API.Steps.Common;
using CCC_Infrastructure.Utils;
using CCC_API.Utils.Assertion;
using RestSharp;
using System.Net;
using System.Reflection;
using TechTalk.SpecFlow;

namespace CCC_API.Steps.FileUpload
{
    [Binding]
    public sealed class FileUploadSteps : AuthApiSteps
    {
        public FileUploadSteps(IObjectContainer objectContainer) : base(objectContainer) { }
        // For additional details on SpecFlow step definitions see http://go.specflow.org/doc-stepdef
        private const string GET_RESPONSE_KEY = "get_response";


        [When("I upload a file (.*) to create a distribution")]
        public void WhenIPressUploadAFile(string filepath)
        {
            var fileService = new FileUploadService(SessionKey);

            var response = fileService.Upload(0, TestData.GetTestFileAbsPath(filepath, Assembly.GetExecutingAssembly()));
            
            PropertyBucket.Remember(GET_RESPONSE_KEY, response);
        }

        [Then("The result should be (.*) on the screen")]
        public void ThenTheResultShouldBe(string status = "Ok")
        {
            var response = PropertyBucket.GetProperty<IRestResponse>(GET_RESPONSE_KEY);

            //  Verify response's status
            Assert.AreEqual(status.Replace("\"", ""), HttpStatusCode.OK.ToString(), "Wrong Status code on the response");
        }
    }
}
