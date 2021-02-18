using System;
using RestSharp;
using CCC_API.Data.PostData.Settings.CustomFields;
using CCC_API.Data.Responses.Settings.CustomFields;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using CCC_API.Utils.Assertion;
using CCC_Infrastructure.Utils;
using Is = NUnit.Framework.Is;

namespace CCC_API.Services.Settings.CustomFields
{
    public class CustomFieldsService : AuthApiService
    {
        public static string customFieldsEndPoint = "customfields";
        
        public CustomFieldsService(string sessionKey) : base(sessionKey) { }

        /// <summary>
        /// Performs a POST to customFields endpoint
        /// </summary>
        /// <param name="customFieldsPostData"></param>
        /// <returns>IRestResponse</returns>
        public IRestResponse<CustomFieldsPostData> CreateCustomFields(CustomFieldsPostData customFieldsPostData)
            => Request().Post().Data(customFieldsPostData).ToEndPoint(customFieldsEndPoint).Exec<CustomFieldsPostData>();

        /// <summary>
        /// Creates custom field in the system or otherwise throws an exception.
        /// </summary>
        /// <param name="customFieldsPostData"></param>
        /// <returns></returns>
        public CustomFieldsPostData CreateCustomField(CustomFieldsPostData customFieldsPostData)
        {
            return Request().Post().ToEndPoint(customFieldsEndPoint)
                .Data(customFieldsPostData)
                .ExecCheck<CustomFieldsPostData>(HttpStatusCode.Created);
        }

        /// <summary>
        /// Prepares common valid data for common custom fields.
        /// </summary>
        /// <param name="name">name of custom field</param>
        /// <param name="type">number, memo, string, yes / no, date, multi-select, single-select</param>
        /// <param name="concept">news, activities</param>
        /// <returns>CustomFieldsPostData</returns>
        public CustomFieldsPostData PrepareCustomFieldsPostData(string name, string type, string concept)
        {
            var maxLength = "250";
            var defaultValue = "";
            var allowedValues = new List<AllowValue>();
            var multiselect = false;

            switch (type.ToLower())
            {
                case "number":
                    type = "Decimal";
                    break;
                case "memo":
                    type = "String";
                    maxLength = "2000";
                    break;
                case "string":
                case "text":
                    type = "String";
                    maxLength = "255";
                    break;
                case "yes / no":
                    type = "Boolean";
                    defaultValue = "true";
                    break;
                case "date":
                    maxLength = "255";
                    break;
                case "multi-select":
                    multiselect = true;
                    type = "String";
                    allowedValues.Add(new AllowValue("first"));
                    allowedValues.Add(new AllowValue("second"));
                    allowedValues.Add(new AllowValue("third"));
                    break;
                case "single-select":
                    type = "String";
                    allowedValues.Add(new AllowValue("first"));
                    allowedValues.Add(new AllowValue("second"));
                    allowedValues.Add(new AllowValue("third"));
                    break;
                default:
                    throw new ArgumentException(Err.Msg("Uknown type: " + type));
            }

            var customFieldToCreate = new CustomFieldsPostData
            {
                Name = name,
                Type = type,
                DefaultValue = defaultValue,
                MultiSelect = multiselect,
                AllowedValues = allowedValues,
                EntityType = concept,
                MaxLength = maxLength
            };

            return customFieldToCreate;
        }

        /// <summary>
        /// Performs a PUT to customFields endpoint
        /// </summary>
        /// <param name="customFieldsId"></param>
        /// <param name="modifiedCustomFieldsData"></param>
        /// <returns>IRestResponse</returns>
        public IRestResponse<CustomFieldsPostData> ModifyCustomFields(int customFieldsId, CustomFieldsPostData modifiedCustomFieldsData)
        {
            string resource = string.Format(customFieldsEndPoint + "/{0}", customFieldsId);
            return Put<CustomFieldsPostData>(resource, modifiedCustomFieldsData);
        }

        /// <summary>
        /// Performs a GET to customFields endpoint to get all current available CustomFields
        ///<param name="type">this can be news/Activity</param>
        /// </summary>               
        /// <returns> IRestResponse<AllCustomFieldsResponse> </returns>
        public IRestResponse<AllCustomFieldsResponse> GetAllsCustomFields(string entityType)
        {  
            return Get<AllCustomFieldsResponse>(customFieldsEndPoint+"/"+entityType);
        }

        /// <summary>
        /// Retrieves avaliable custom fields for a company.
        /// </summary>
        /// <param name="entityType"></param>
        /// <returns>AvaliableCustomFields</returns>
        public AvaliableCustomFields GetCustomFields(string entityType)
            => Request().ToEndPoint(customFieldsEndPoint + "/" + entityType).ExecCheck<AvaliableCustomFields>();

        /// <summary>
        /// Creates required custom fields types if those not present.
        /// </summary>
        /// <param name="concept"></param>
        /// <param name="requiredFieldsTypes"></param>
        /// <param name="namePrefix"></param>
        /// <returns></returns>
        public List<CustomFieldsPostData> EnsureCompanyHasCustomFieldsOfTypes(
            string concept, IEnumerable<string> requiredFieldsTypes, string namePrefix = "Auto ")
        {
            var availableFields = GetCustomFields(concept).Items;
            var fields = availableFields.Select(_ => _.Name).ToList();

            var any = false;
            var created = requiredFieldsTypes
                .Select(expField =>
                {
                    var name = namePrefix + expField;
                    if (! fields.Contains(name))
                    {
                        // Lets add settings to a company if those are not present in the system.
                        var field = PrepareCustomFieldsPostData(name, expField, concept);
                        CreateCustomField(field);
                        any = true;
                    }
                    return name;
                })
                .ToList();

            if (any)
                availableFields = GetCustomFields(concept).Items;

            var names = availableFields.Select(_ => _.Name).ToList();
            Assert.That(created, Is.SubsetOf(names), "Not all requeired fields were present in the system");
            return availableFields;
        }

        /// <summary>
        /// Performs a DELETE to customFields endpoint for specified CustomFields
        /// </summary>               
        /// <param name="customFieldsId"></param>        
        /// <returns> IRestResponse<AllCustomFieldsResponse> </returns>
        public IRestResponse<Dictionary<string, string>> deleteCustomFields(int customFieldsId)
        {
            string resource = string.Format(customFieldsEndPoint + "/{0}", customFieldsId);
            return Delete<Dictionary<string, string>>(resource);
        }
    }
}