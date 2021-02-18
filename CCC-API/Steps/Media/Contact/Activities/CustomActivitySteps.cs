using BoDi;
using CCC_API.Data.PostData.Settings.CustomFields;
using CCC_API.Data.Responses.Activities;
using CCC_API.Data.TestDataObjects;
using CCC_API.Data.TestDataObjects.Activities;
using CCC_API.Services.Activities;
using CCC_API.Services.Media.Contact;
using CCC_API.Services.Media.Outlet;
using CCC_API.Steps.Common;
using CCC_API.Steps.Settings.FormManagement;
using CCC_API.Steps.Settings.UserManagement;
using CCC_Infrastructure.Utils;
using CCC_API.Utils.Assertion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Is = NUnit.Framework.Is;

namespace CCC_API.Steps.Activities
{
    [Binding]
    public sealed class CustomActivitySteps : AuthApiSteps
    {
        private readonly CustomActivityService _customActivityService;

        private readonly IObjectContainer _objectContainer;
        public CustomActivitySteps(IObjectContainer objectContainer) : base(objectContainer)
        {
            _customActivityService = new CustomActivityService(SessionKey);
            _objectContainer = objectContainer;
        }

        [Given(@"custom activity combinations:")]
        public void GivenCustomActivityCombinations(Table table)
        {
            var activities = table.CreateSet<CustomActivity>();
            PropertyBucket.Remember("CustomActivities", activities);
        }

        [Given(@"I take combination of '(.*)'")]
        public void GivenITakeCombinationOf(string type)
        {
            var activities = PropertyBucket.GetProperty<IEnumerable<CustomActivity>>("CustomActivities");
            var act = activities.FirstOrError(it => it.Type == type, $"Type '{type}' not found.");
            PropertyBucket.Remember("CustomActivity", act);
        }
        
        [When(@"I POST custom activity of '(.*)' '(.*)' '(.*)'")]
        public void WhenIPostCustomActivityOfTypeInWithTimezone(string type, string contact, string outlet)
        {
            var acc = PrepareCustomActivity(type, contact, outlet);
            _customActivityService.PostActivity(acc);
        }

        [When(@"I POST custom activity with type '(.*)'$")]
        public void WhenIPostCustomActivityOfTypeInWithTimezone(string type)
        {
            WhenIPostCustomActivityOfTypeInWithTimezone(type, null, null);
        }

        public CustomActivity PrepareCustomActivity(string type, string contact, string outlet)
        {
            var customAc = PropertyBucket.GetProperty<CustomActivity>("CustomActivity");
            var shift = Convert.ToInt32(customAc.ScheduleTime);
            var time = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow.AddHours(shift), customAc.TimeZoneIdentifier);

            int id = 0;
            PublishActivityType enumType;
            if (Enum.TryParse<PublishActivityType>(type, out enumType)) // If not system type.
            {
                id = (int) enumType;
            }
            else
            {
                if (! PropertyBucket.ContainsKey(FormManagementSteps.CUSTOM_TYPE_ID))
                    throw new ArgumentException(Err.Msg($"Provided type {type} is neigher system type nor remembered custom form type."));
                id = PropertyBucket.GetProperty<int>(FormManagementSteps.CUSTOM_TYPE_ID);
            }
            
            var acc = new CustomActivity
            {
                Notes = !string.IsNullOrEmpty(customAc.Notes) ? StringUtils.RandomAlphaNumericString(Convert.ToInt32(customAc.Notes)) : null,
                Title = !string.IsNullOrEmpty(customAc.Title) ? StringUtils.RandomAlphaNumericString(Convert.ToInt32(customAc.Title)) : null,

                Type = id.ToString(),
                
                TimeZoneIdentifier = customAc.TimeZoneIdentifier ?? null,
                ScheduleTime = $"{time:s}"
            };

            if (!string.IsNullOrEmpty(contact))
            {
                var cont = new ContactsService(SessionKey)
                    .FindContacts(ContactsService.ContactsSearchCriteria.Contact_Name, contact);
                acc.Contact = cont.Items[0];
            }

            if (!string.IsNullOrEmpty(outlet))
            {
                var outlets = new OutletsService(SessionKey)
                    .FindOutlets(OutletsService.OutletSearchCriteria.OutletName, outlet);
                acc.Outlet = outlets.Items[0];
            }

            var act = new PublishActivity { Title = acc.Title };
            PropertyBucket.Remember(type, act, true);
            PropertyBucket.Remember("Exp" + type, acc);

            //Create empty campaignsidlist
            acc.CampaignIds = new List<int>();

            return acc;
        }

        [When(@"I POST custom activity of type '(SendMailing|Callback|Appointment|Inquiry|Other)' with Custom Fields set to default values")]
        public void WhenIPOSTCustomActivityOfTypeWithCustomFieldsSet(string type)
        {
            var act = PrepareCustomActivity(type, "", "");
            var availableFields = PropertyBucket.GetProperty<List<CustomFieldsPostData>>(SettingsCustomFieldsNewsSteps.CUSTOM_FIELDS);

            var customFields = availableFields.Select(f => new AllowValue {Id = f.Id, Value = f.DefaultValue}).ToList();
            act.CustomFields = customFields;
            _customActivityService.PostActivity(act);
        }

        [When(@"I POST custom activity of type '(.*)' with Custom Fields having each MAX allowed value")]
        public void WhenIpostCustomActivityOfTypeWithCustomFieldsHavingEachMaxAllowedValue(string type)
        {
            var act = PrepareCustomActivity(type, "", "");
            var availableFields = PropertyBucket.GetProperty<List<CustomFieldsPostData>>(SettingsCustomFieldsNewsSteps.CUSTOM_FIELDS);

            var customFields = availableFields.Select(f =>
            {
                var value = f.DefaultValue;
                var max = Convert.ToInt16(f.MaxLength ?? "0");
                var fieldType = f.EvaluateCustomFieldType();

                switch (fieldType)
                {
                    case "Memo":
                    case "String":
                        value = StringUtils.RandomAlphaNumericString(max);
                        break;
                    case "Decimal":
                        value = "9" + StringUtils.RandomString(StringUtils.NumericChars, max - 1);
                        break;
                    case "Boolean":
                        value = (! Convert.ToBoolean(f.DefaultValue)).ToString();
                        break;
                    case "Date":
                        value = DateTime.Parse(DateTime.Now.AddYears(1).AddDays(1).ToShortDateString()).ToString("O");
                        break;
                    case "SingleSelect":
                        value = f.AllowedValues.RandomFirst("No allowed values present").Value.ToString();
                        break;
                    case "MultiSelect":
                        value = string.Join("; ", f.AllowedValues.Select(v => v.Value));
                        break;
                    default:
                        throw new ArgumentException(Err.Msg("Unkown type: " + fieldType));
                }
                return new AllowValue {Id = f.Id, Value = value};
            }).ToList();

            act.CustomFields = customFields;
            _customActivityService.PostActivity(act);
        }

        [Given(@"I create custom activity of '(SendMailing|Callback|Appointment|Inquiry|Other)' '(.*)' '(.*)' '(.*)'")]
        public void WhenICreateCustomActivityOfTypeInWithTimezone(string type, string state, string contact, string outlet)
        {
            WhenIPostCustomActivityOfTypeInWithTimezone(type, contact, outlet);
            new PublishActivitySteps(_objectContainer) // We need Custom Activity ID
                .ThenICanFindActivityListedInPublishActivities("can", type, state);
        }

        [Then(@"I (can|cannot) DELETE custom activity of type '(SendMailing|Callback|Appointment|Inquiry|Other)'")]
        public void WhenICanDeleteCustomActivity(string choise, string type)
        {
            var acc = PropertyBucket.GetProperty<PublishActivity>(type);
            // If we change a user, which may be the case for the deletion, we need separate instance with fresh session key
            var resp = new CustomActivityService(ResolveSessionKey()).DeleteActivity(acc.EntityId);
            var expCode = choise.Contains("not") ? HttpStatusCode.Forbidden : HttpStatusCode.OK;
            Assert.AreEqual(expCode, resp.StatusCode, 
                $"Wrong code on delete {CustomActivityService.CustomActivityUri}. Message: {resp.ErrorMessage}");
        }

        [Then(@"can GET information about Custom Activity of '(.*)'")]
        public CustomActivity ThenCanGetInformationAboutCustomActivityOf(string type)
        {
            var exp    = PropertyBucket.GetProperty<CustomActivity>("Exp" + type);
            var pubAct = PropertyBucket.GetProperty<PublishActivity>(type);

            var act = _customActivityService.GetActivity(pubAct.EntityId);
            Assert.AreEqual(exp.Type,               act.Type,               "Type not saved");
            Assert.AreEqual(exp.Title,              act.Title,              "Title not saved");
           

            var expSt = DateTime.Parse(exp.ScheduleTime);
            var actSt = DateTime.Parse(act.ScheduleTime);

            Assert.That(expSt, Is.EqualTo(actSt).Within(TimeSpan.FromMinutes(1)), "Time not saved");
            Assert.AreEqual(exp.TimeZoneIdentifier, act.TimeZoneIdentifier, "Timezone not saved");
            Assert.Contains(exp.Notes,        act.Notes,              "Notes not saved");
            Assert.AreEqual(exp.Contact?.Id,        act.Contact?.Id,        "Contact not saved");
            Assert.AreEqual(exp.Outlet?.Id,         act.Outlet?.Id,         "Outlet not saved");
            return act;
        }

        [Then(@"can GET information about Custom Activity of '(.*)' with custom fields values")]
        public void ThenCanGETInformationAboutCustomActivityOfWithCustomFieldsValues(string type)
        {
            var exp = PropertyBucket.GetProperty<CustomActivity>("Exp" + type);
            var pubAct = PropertyBucket.GetProperty<PublishActivity>(type);

            var act = ThenCanGetInformationAboutCustomActivityOf(type);
            Assert.That(act.CustomFields, Is.EqualTo(exp.CustomFields), "Custom fields information is wrong");
        }
    }
}
