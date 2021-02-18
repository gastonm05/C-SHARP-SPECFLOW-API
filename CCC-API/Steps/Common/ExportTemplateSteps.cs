using BoDi;
using CCC_API.Data.PostData.Grids;
using CCC_API.Services.Common.CCC_API.Services.Common;
using Newtonsoft.Json;
using CCC_API.Utils.Assertion;
using RestSharp;
using System;
using System.Linq;
using CCC_Infrastructure.Utils;
using TechTalk.SpecFlow;
using Zukini;

namespace CCC_API.Steps.Common
{
    [Binding]
    public class ExportTemplateSteps : AuthApiSteps
    {
        public enum PostOrPut { POST, PUT }
        public enum ExportType { Contact, Outlet, News }

        private const string COMPANYID_KEY = "CompanyId";
        private const string CONTACT_EXPORT_TEMPLATE_ID_KEY = "ContactExportTemplateId";
        private const string DATA_EXPORT_TEMPLATE_KEY = "ExportTemplateData";
        private const string DATA_GROUP_ID_KEY = "DataGroupId";
        private const string DELETE_EXPORT_TEMPLATE_RESPONSE_KEY = "DeleteExportTemplateResponse";
        private const string GET_EXPORT_RESPONSE_KEY = "GetExportTemplateResponse";
        private const string NEWS_EXPORT_TEMPLATE_ID_KEY = "NewsExportTemplateId";
        private const string OUTLET_EXPORT_TEMPLATE_ID_KEY = "OutletExportTemplateId";
        private const string POST_EXPORT_TEMPLATE_CREATE_RESPONSE_KEY = "CreateExportTemplateResponse";
        private const string PUT_EXPORT_DATA_KEY = "PutExportTemplateData";
        private const string PUT_EXPORT_TEMPLATE_DATA_RESTORED_KEY = "ExportTemplateDataRestored";
        private const string PUT_EXPORT_TEMPLATE_MODIFY_RESPONSE_KEY = "ModifyExportTemplateResponse";
        
        public ExportTemplateSteps(IObjectContainer objectContainer) : base(objectContainer) { }

        [When(@"I perform a GET on export template endpoint using a '(.*)' exporttemplateid")]
        public void WhenIPerformAGETOnExportTemplateEndpointUsingAExporttemplateid(ExportType exportType)
        {
            int exportTemplateId = 0;
            switch (exportType) {
                case ExportType.Contact:
                    {
                        exportTemplateId = Int32.Parse(PropertyBucket.GetProperty<string>(CONTACT_EXPORT_TEMPLATE_ID_KEY));
                        break;
                    }
                case ExportType.Outlet:
                    {
                        exportTemplateId = Int32.Parse(PropertyBucket.GetProperty<string>(OUTLET_EXPORT_TEMPLATE_ID_KEY));
                        break;
                    }
                case ExportType.News:
                    {
                        exportTemplateId = Int32.Parse(PropertyBucket.GetProperty<string>(NEWS_EXPORT_TEMPLATE_ID_KEY));
                        break;
                    }
            }
            var response = new ExportTemplateService(SessionKey).GetExportTemplate(exportTemplateId);
            PropertyBucket.Remember(GET_EXPORT_RESPONSE_KEY, response);
        }

        [When(@"I perform a PUT on Export Template endpoint using a '(.*)' exporttemplateid")]
        public void WhenIPerformAPUTOnExportTemplateEndpointUsingAExporttemplateid(ExportType exportType)
        {
            int exportTemplateId = 0;
            switch (exportType)
            {
                case ExportType.Contact:
                    {
                        exportTemplateId = Int32.Parse(PropertyBucket.GetProperty<string>(CONTACT_EXPORT_TEMPLATE_ID_KEY));
                        break;
                    }
                case ExportType.Outlet:
                    {
                        exportTemplateId = Int32.Parse(PropertyBucket.GetProperty<string>(OUTLET_EXPORT_TEMPLATE_ID_KEY));
                        break;
                    }
                case ExportType.News:
                    {
                        exportTemplateId = Int32.Parse(PropertyBucket.GetProperty<string>(NEWS_EXPORT_TEMPLATE_ID_KEY));
                        break;
                    }
            }                    
            IRestResponse<ExportTemplatePostData> responseGet = PropertyBucket.GetProperty<IRestResponse<ExportTemplatePostData>>(GET_EXPORT_RESPONSE_KEY);
            var content = JsonConvert.DeserializeObject<ExportTemplatePostData>(responseGet.Content);
            var exportTemplateGet = new ExportTemplatePostData(exportTemplateId, content.name , content.exportType,
                                                               content.companyId, content.dataGroupId, content.columns);
            PropertyBucket.Remember(PUT_EXPORT_DATA_KEY, exportTemplateGet);

            string nameModified  = $"Automation Test Export Template - {exportType} {StringUtils.RandomAlphaNumericString(5)}";
            string[] columnsModified = { "/Firstname", "/Lastname", "/OutletName", "/SocialProfiles/Twitter", "/Outlet/UniqueVisitorsPerMonth", "/Outlet/CirculationAudience", "/Email", "/Phone", "/AddressLine1", "/AddressLine2", "/Mobile", "/City", "/ZipCode", "/CountryName", "/ProprietaryData/Email", "/ProprietaryData/Phone", "/ProprietaryData/Mobile", "/ProprietaryData/Fax", "/ProprietaryData/AddressLine1", "/ProprietaryData/AddressLine2", "/ProprietaryData/City", "/ProprietaryData/State", "/ProprietaryData/ZipCode", "/ProprietaryData/CountryId", "/ProprietaryData/WebsiteURL", "/Outlet/AddressLine1", "/Outlet/AddressLine2", "/Outlet/City", "/Outlet/State", "/Outlet/ZipCode", "/Outlet/CountryName"};

            var exportTemplateModified = new ExportTemplatePostData(exportTemplateId, nameModified, exportTemplateGet.exportType, exportTemplateGet.companyId, exportTemplateGet.dataGroupId, columnsModified);
            var putResponse = new ExportTemplateService(SessionKey).ModifyExportTemplate(exportTemplateId, exportTemplateModified);
            PropertyBucket.Remember(PUT_EXPORT_TEMPLATE_MODIFY_RESPONSE_KEY, putResponse);
            var contentPut = JsonConvert.DeserializeObject<ExportTemplatePostData>(putResponse.Content);
            var exportTemplatePut = new ExportTemplatePostData(exportTemplateId, contentPut.name, contentPut.exportType,
                                                               contentPut.companyId, contentPut.dataGroupId, contentPut.columns);
            PropertyBucket.Remember(PUT_EXPORT_TEMPLATE_DATA_RESTORED_KEY, exportTemplatePut);
        }    
        
        [Then(@"Get ExportTemplate endpoint response code should be (.*)")]
        public void ThenGetExportTemplateEndpointResponseCodeShouldBe(int statusCode)
        {
            var responseGet = PropertyBucket.GetProperty<IRestResponse<ExportTemplatePostData>>(GET_EXPORT_RESPONSE_KEY);
            Assert.AreEqual(statusCode, (int)responseGet.StatusCode, responseGet.Content);
        }
        
        [Then(@"Export Template endpoint '(.*)' response code should be (.*)")]
        public void ThenJExportTemplateEndpointResponseCodeShouldBe(PostOrPut postOrPut, int responseCode)
        {
            IRestResponse<ExportTemplatePostData> response = null;
            if (postOrPut.Equals(PostOrPut.POST))
            {
                response = PropertyBucket.GetProperty<IRestResponse<ExportTemplatePostData>>(POST_EXPORT_TEMPLATE_CREATE_RESPONSE_KEY);
            }
            if (postOrPut.Equals(PostOrPut.PUT))
            {
                response = PropertyBucket.GetProperty<IRestResponse<ExportTemplatePostData>>(PUT_EXPORT_TEMPLATE_MODIFY_RESPONSE_KEY);
            }
            Assert.NotNull(response, "Export Template transaction {postOrPut} failed", response.Content );
            Assert.AreEqual(responseCode, (int)response.StatusCode, response.Content);
        }
        
        [Then(@"I verify all Export Template data was changed")]
        public void ThenJIVerifyAllExportTemplateDataWasChanged()
        {
            var contentDataExportTemplate = PropertyBucket.GetProperty<ExportTemplatePostData>(PUT_EXPORT_DATA_KEY);
            var dataExportTemplate = new ExportTemplatePostData(contentDataExportTemplate.exportTemplateId, contentDataExportTemplate.name, contentDataExportTemplate.exportType, 
                                                        contentDataExportTemplate.companyId, contentDataExportTemplate.dataGroupId,
                                                        contentDataExportTemplate.columns);
            var contentPutExportTemplate = PropertyBucket.GetProperty<ExportTemplatePostData>(PUT_EXPORT_TEMPLATE_DATA_RESTORED_KEY);
            var putExportTemplate = new ExportTemplatePostData(contentPutExportTemplate.exportTemplateId, contentPutExportTemplate.name, contentPutExportTemplate.exportType,
                                                   contentPutExportTemplate.companyId, contentPutExportTemplate.dataGroupId,
                                                   contentPutExportTemplate.columns);
            Assert.IsTrue(putExportTemplate.exportTemplateId != 0, "The export template id shouldn't be null.");
            Assert.AreEqual(dataExportTemplate.exportTemplateId, putExportTemplate.exportTemplateId);
            Assert.AreNotEqual(dataExportTemplate.exportTemplateId.ToString(), putExportTemplate.name);
            bool equalColumnResultModified = putExportTemplate.columns.SequenceEqual(dataExportTemplate.columns);
            Assert.IsFalse(equalColumnResultModified, "Columns are identical when they should differ");

            //step to set scenario after steps
            var putResponse = new ExportTemplateService(SessionKey).ModifyExportTemplate(dataExportTemplate.exportTemplateId, dataExportTemplate);
            string[] restoredColumns = JsonConvert.DeserializeObject<ExportTemplatePostData>(putResponse.Content).columns;
            string[] dataGridColumn = dataExportTemplate.columns;
            bool equalColumnResultRestored = restoredColumns.SequenceEqual(dataGridColumn);

            Assert.IsTrue(equalColumnResultRestored, "Columns differ when they should be identical");
        }

        [When(@"I perform a POST on export template endpoint with export type '(.*)'")]
        public void WhenIPerformAPOSTOnExportTemplateEndpointWithExportType(int exportType)
        {
            string name = $"Export Template Automation Script {StringUtils.RandomAlphaNumericString(5)} {exportType}";
            string companyId = PropertyBucket.GetProperty<string>(COMPANYID_KEY);
            string dataGroupId = PropertyBucket.GetProperty<string>(DATA_GROUP_ID_KEY);
            string[] columnsPost = { "/Firstname", "/Lastname", "/Title", "/Subjects", "/ProprietaryData/Notes",
                "/PitchingProfile", "/SocialProfiles/Twitter", "/Outlet/UniqueVisitorsPerMonth", "/Outlet/CirculationAudience",
                "/Email", "/Phone", "/AddressLine1", "/AddressLine2", "/Mobile", "/City", "/ZipCode", "/CountryName",
                "/ProprietaryData/Email", "/ProprietaryData/Phone", "/ProprietaryData/Mobile", "/ProprietaryData/Fax",
                "/ProprietaryData/AddressLine1", "/ProprietaryData/AddressLine2", "/ProprietaryData/City", "/ProprietaryData/State",
                "/ProprietaryData/ZipCode", "/ProprietaryData/CountryId", "/ProprietaryData/WebsiteURL", "/Outlet/AddressLine1",
                "/Outlet/AddressLine2", "/Outlet/City", "/Outlet/State", "/Outlet/ZipCode", "/Outlet/CountryName", "/IsProprietaryContact" };
            var exportTemplateData = new ExportTemplatePostData(0, name, exportType, Int32.Parse(companyId), Int32.Parse(dataGroupId), columnsPost);

            var response = new ExportTemplateService(SessionKey).CreateExportTemplate(exportTemplateData);
            PropertyBucket.Remember(POST_EXPORT_TEMPLATE_CREATE_RESPONSE_KEY, response);
        }

        [When(@"I perform a DELETE on export template endpoint to delete just created export template")]
        public void WhenIPerformADELETEOnExportTemplateEndpointToDeleteJustCreatedExportTemplate()
        {
            var responsePost = PropertyBucket.GetProperty<IRestResponse<ExportTemplatePostData>>(POST_EXPORT_TEMPLATE_CREATE_RESPONSE_KEY);
            int toBeDeletedTemplateId = JsonConvert.DeserializeObject<ExportTemplatePostData>(responsePost.Content).exportTemplateId;
            var responseDelete = new ExportTemplateService(SessionKey).deleteExportTemplate(toBeDeletedTemplateId);
            PropertyBucket.Remember(DELETE_EXPORT_TEMPLATE_RESPONSE_KEY, responseDelete);
        }

        [Then(@"Delete ExportTemplate endpoint response code should be (.*)")]
        public void ThenDeleteExportTemplateEndpointResponseCodeShouldBe(int statusCode)
        {
            var responseDelete = PropertyBucket.GetProperty<IRestResponse>(DELETE_EXPORT_TEMPLATE_RESPONSE_KEY);
            Assert.AreEqual(statusCode, (int)responseDelete.StatusCode, responseDelete.Content);
        }
    }
}