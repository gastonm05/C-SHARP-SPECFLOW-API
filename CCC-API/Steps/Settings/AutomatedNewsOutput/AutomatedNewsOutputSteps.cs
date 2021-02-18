using BoDi;
using CCC_API.Data.Responses.Settings.AutomatedNewsOutput;
using CCC_API.Services.Settings.FtpExportManagement;
using CCC_API.Steps.Common;
using CCC_API.Utils.Assertion;
using CCC_Infrastructure.Utils;
using RestSharp;
using System;
using TechTalk.SpecFlow;

namespace CCC_API.Steps.Settings.AutomatedNewsOutput
{
    [Binding]
    public class AutomatedNewsOutputSteps : AuthApiSteps
    {
        public AutomatedNewsOutputSteps(IObjectContainer objectContainer) : base(objectContainer) { }
        private const string GET_EXPORT_FTP_RESPONSE_KEY = "GetExportFTPResponse";
        private const string PUT_EXPORT_FTP_RESPONSE_KEY = "PutExportFTPResponse";

        [When(@"I perform a GET on newsftpexport endpoint to view Automated News Output Settings")]
        public void WhenIPerformAGETOnExportFTPConfigEndpoint()
        {
            var response = new FtpExportManagementService(SessionKey).GetFtpExportConfig();
            PropertyBucket.Remember(GET_EXPORT_FTP_RESPONSE_KEY, response);
        }
        [Then(@"NewsFtpExport endpoint GET response code should be (.*)")]
        public void ThenNewsFtpExportConfigGETEndpointResponseCodeShouldBe(int statusCode)
        {
            var responseGet = PropertyBucket.GetProperty<IRestResponse<FtpExportConfig>>(GET_EXPORT_FTP_RESPONSE_KEY);
            Assert.AreEqual(statusCode, (int)responseGet.StatusCode, Err.Line("Status code was different from expected"));
        }
        
        [When(@"I perform a PUT on newsftpexport endpoint to edit Automated News Output Settings setting IncludeDuplicates to '(.*)'")]
        public void WhenIPerformAPUTOnNewsftpexportEndpointToEditCurrentAutomatedNewsOutputSettingsTurningIncludeDuplicates(bool includeDuplicates)
        {
            var newsFtpExportConfig = new FtpExportConfig
            {
                IncludeDuplicates = includeDuplicates                
            };

            var putResponse = new FtpExportManagementService(SessionKey).EditFtpExportConfig (newsFtpExportConfig);
            PropertyBucket.Remember(PUT_EXPORT_FTP_RESPONSE_KEY, putResponse);
        }

        [Then(@"NewsFtpExport endpoint PUT response code should be (.*)")]
        public void ThenNewsFtpExportEndpointPUTResponseCodeShouldBe(int statusCode)
        {
            IRestResponse<FtpExportConfig> responsePut = PropertyBucket.GetProperty<IRestResponse<FtpExportConfig>>(PUT_EXPORT_FTP_RESPONSE_KEY);
            Assert.AreEqual(statusCode, (int)responsePut.StatusCode, Err.Line(responsePut.Content));            
        }
    }
}
