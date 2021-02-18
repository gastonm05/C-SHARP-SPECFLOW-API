using BoDi;
using CCC_API.Data.PostData.Settings.CustomFields;
using CCC_API.Data.Responses.Settings.CustomFields;
using CCC_API.Services.Settings.CustomFields;
using CCC_API.Steps.Common;
using CCC_Infrastructure.Utils;
using Newtonsoft.Json;
using CCC_API.Utils.Assertion;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;

namespace CCC_API.Steps.Settings.UserManagement
{
    [Binding]
    public class SettingsCustomFieldsNewsSteps : AuthApiSteps

    {
        private const string POST_CUSTOMFIELDS_CREATE_RESPONSE_KEY = "CreateCustoFieldsResponse";
        private const string PUT_CUSTOMFIELDS_CREATE_RESPONSE_KEY = "ModifyCustoFieldsResponse";
        private const string PUT_CUSTOMFIELDS_DATA = "DataToModifyCustomFieldJustCreated";
        private const string GET_CUSTOMFIELDS_RESPONSE_KEY = "GetCustomFieldsResponse";
        public  const string CUSTOM_FIELDS = "custom fields";
        public  const string REQUIRED_CUSTOM_FIELDS = "required custom fields";
        public SettingsCustomFieldsNewsSteps(IObjectContainer objectContainer) : base(objectContainer) { }

        [When(@"I perform a POST on customfields endpoint to create a '(.*)' '(.*)' customfield")]
        public void WhenIPerformAPOSTOnCustomfieldsEndpoint(string type, string concept)
        {
            var name = "Test automation CustomField" + System.Guid.NewGuid();
            var customFieldsService = new CustomFieldsService(SessionKey);
            var customFieldToCreate = customFieldsService.PrepareCustomFieldsPostData(name, type, concept);
            var createCustomFieldResponse = customFieldsService.CreateCustomFields(customFieldToCreate);
            PropertyBucket.Remember(POST_CUSTOMFIELDS_CREATE_RESPONSE_KEY, createCustomFieldResponse);
        }

        [When(@"I perform a PUT on CustomField endpoint to modify the one just created with this data :")]
        public void WhenIPerformAPUTtoModifyJustCreatedCustomField(Table table) {
            IRestResponse<CustomFieldsPostData> postResponse = PropertyBucket.GetProperty<IRestResponse<CustomFieldsPostData>>(POST_CUSTOMFIELDS_CREATE_RESPONSE_KEY);
            CustomFieldsPostData customField = JsonConvert.DeserializeObject<CustomFieldsPostData>(postResponse.Content);
            List<AllowValue> allowedValues = new List<AllowValue>();
            allowedValues.Add(new AllowValue("test"));
            if ("Single-Select".Equals(customField.Type) && "Multi-Select".Equals(customField.Type)) {
                allowedValues = customField.AllowedValues;
            }
            
            int idToModified = customField.Id;
            string name = customField.Name;
            string type = customField.Type;
            bool multiselect = false;
            string defaultValue = customField.DefaultValue;
            string entityType = customField.EntityType;
            string maxlength = customField.MaxLength;
            foreach (var row in table.Rows) {
                idToModified = (((string[])row.Values)[0].Equals("")) ? customField.Id : System.Int32.Parse(((string[])row.Values)[0]);
                name = (((string[])row.Values)[1].Equals("")) ? customField.Name : ((string[])row.Values)[1]+System.Guid.NewGuid();
                type = (((string[])row.Values)[2].Equals("") || 
                        ((string[])row.Values)[2].Equals("Number") ||
                        ((string[])row.Values)[2].Equals("Memo") ||
                        ((string[])row.Values)[2].Equals("Single-Select") ||
                        ((string[])row.Values)[2].Equals("Multi-Select")) ? customField.Type : ((string[])row.Values)[2];
                defaultValue = (((string[])row.Values)[3].Equals("")) ? customField.DefaultValue : ((string[])row.Values)[3];
                entityType = (((string[])row.Values)[4].Equals("")) ? customField.EntityType : ((string[])row.Values)[4];
                maxlength = (((string[])row.Values)[5].Equals("")) ? customField.MaxLength : ((string[])row.Values)[5];
            }

            var customFieldToModify = new CustomFieldsPostData
                {
                    Id = idToModified,
                    Name = name,
                    Type = type,
                    DefaultValue = "",
                    MultiSelect = multiselect,
                    AllowedValues = allowedValues,
                    EntityType = entityType,
                    MaxLength = maxlength
                };

            var PutCustomField = new CustomFieldsService(SessionKey).ModifyCustomFields(idToModified, customFieldToModify);
            PropertyBucket.Remember(PUT_CUSTOMFIELDS_CREATE_RESPONSE_KEY, PutCustomField);
            PropertyBucket.Remember(PUT_CUSTOMFIELDS_DATA, customFieldToModify);
        }

        [Then(@"CustomField endpoint '(.*)' response code should be (.*)")]
        public void ThenEndpointResponseCodeShouldBe(string type, int responseCode)
        {
            IRestResponse<CustomFieldsPostData> response = null;
            if (type.Equals("POST")) {
                response = PropertyBucket.GetProperty<IRestResponse<CustomFieldsPostData>>(POST_CUSTOMFIELDS_CREATE_RESPONSE_KEY);
            }
            if (type.Equals("PUT"))
            {
                response = PropertyBucket.GetProperty<IRestResponse<CustomFieldsPostData>>(PUT_CUSTOMFIELDS_CREATE_RESPONSE_KEY);
            }
            Assert.NotNull(response);
            Assert.AreEqual(responseCode, (int)response.StatusCode, response.Content);
        }

        [When(@"I perform a GET on customfields '(.*)' endpoint")]
        public void WhenIPerformAGETOnCustomfieldsEndpoint(string type)
        {
            var response = new CustomFieldsService(SessionKey).GetAllsCustomFields(type);
            PropertyBucket.Remember(GET_CUSTOMFIELDS_RESPONSE_KEY, response);
        }

        [Then(@"I verify all CustomField data was changed")]
        public void IVerifyAllCustomFieldDataWasChanged() {
            IRestResponse<CustomFieldsPostData> response = PropertyBucket.GetProperty<IRestResponse<CustomFieldsPostData>>(PUT_CUSTOMFIELDS_CREATE_RESPONSE_KEY);
            CustomFieldsPostData customFieldModified = JsonConvert.DeserializeObject<CustomFieldsPostData>(response.Content);
            CustomFieldsPostData data = PropertyBucket.GetProperty<CustomFieldsPostData>(PUT_CUSTOMFIELDS_DATA);
            Assert.AreEqual(data.Name, customFieldModified.Name);
            int index = 0;
            if (!"Date".Equals(data.Type)) {
                 index = customFieldModified.DefaultValue.Length;
            }
            
            if ("Decimal".Equals(data.Type)) {
                index = customFieldModified.DefaultValue.IndexOf('.') == -1 
                    ? customFieldModified.DefaultValue.Length 
                    : customFieldModified.DefaultValue.IndexOf('.');
            }
           
            if (!"Decimal".Equals(data.Type) && !"Date".Equals(data.Type))
            {
                Assert.AreEqual(data.DefaultValue, customFieldModified.DefaultValue.Substring(0, index));
                Assert.AreEqual(data.MaxLength, customFieldModified.MaxLength);
            }           
        }


        [Then(@"Get customfields endpoint response code should be (.*)")]
        public void ThenGetEndpointResponseCodeShouldBe(int responseCode)
        {
            IRestResponse<AllCustomFieldsResponse> responseGet = PropertyBucket.GetProperty<IRestResponse<AllCustomFieldsResponse>>(GET_CUSTOMFIELDS_RESPONSE_KEY);
            Assert.AreEqual(responseCode, (int)responseGet.StatusCode, responseGet.Content);
        }


        [Then(@"Delete just created CustomFields and response code should be (.*)")]
        public void ThenDeleteJustCreatedCustomFields(int responseCode)
        {
            IRestResponse<CustomFieldsPostData> response = PropertyBucket.GetProperty<IRestResponse<CustomFieldsPostData>>(POST_CUSTOMFIELDS_CREATE_RESPONSE_KEY);
            CustomFieldsPostData customFieldToBeDeleted = JsonConvert.DeserializeObject<CustomFieldsPostData>(response.Content);
            int idToBeDeleted = customFieldToBeDeleted.Id;
            var responseDeletion = new CustomFieldsService(SessionKey).deleteCustomFields(idToBeDeleted);            
            Assert.AreEqual(responseCode, (int)responseDeletion.StatusCode, responseDeletion.Content);
            
        }

        [Given(@"custom fields for '(Activity|News)' present:")]
        public void GivenCustomFieldsForPresent(string option, Table table)
        {
            var customFieldsService = new CustomFieldsService(SessionKey);
            var required = table.Rows.Select(r => r.FirstOrError().Value).ToList();
            var availableFields = customFieldsService.EnsureCompanyHasCustomFieldsOfTypes(option, required);
            PropertyBucket.Remember(CUSTOM_FIELDS, availableFields);
            PropertyBucket.Remember(REQUIRED_CUSTOM_FIELDS, required);
        }
    }
}
