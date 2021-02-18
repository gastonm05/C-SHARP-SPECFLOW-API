using BoDi;
using CCC_API.Data.Responses.Settings.FormManagement;
using CCC_API.Services.Settings.FormManagement;
using CCC_API.Steps.Common;
using CCC_Infrastructure.API.Utils;
using CCC_Infrastructure.Utils;
using CCC_API.Utils.Assertion;
using System;
using System.Linq;
using TechTalk.SpecFlow;
using Is = NUnit.Framework.Is;

namespace CCC_API.Steps.Settings.FormManagement
{
    [Binding]
    public class FormManagementSteps : AuthApiSteps
    {
        private readonly IObjectContainer _objectContainer;
        private readonly FormManagementService _managementService;
        private const string CUSTOM_FIELDS_KEY = "custom fields";
        private const string ACTIVITY_FORM_KEY = "activity form";
        public const string CUSTOM_TYPE_ID = "activity form id";

        public FormManagementSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
            _objectContainer = objectContainer;
            _managementService = new FormManagementService(SessionKey);
        }

        [When(@"I GET activities fields")]
        public FormsActivity WhenIgetFromActivitiesFields()
        {
            var fields = _managementService.GetFormsActivities();
            PropertyBucket.Remember(CUSTOM_FIELDS_KEY, fields, true);
            return fields;
        }

        [When(@"I PUT New Form with color '([^']+)', icon '([^']+)'")]
        public void WhenICreateNewFormWithColorIcon(string color, string icon)
        {
            var existingFields = PropertyBucket.GetProperty<FormsActivity>(CUSTOM_FIELDS_KEY);
            var newField = new FormsActivity
            {
                Color = color,
                Icon = icon,
                Fields = existingFields.Fields,
                Id = 0,
                Name = "Api " + StringUtils.RandomAlphaNumericString(45),
                _meta = existingFields._meta
            };

            PropertyBucket.Remember(ACTIVITY_FORM_KEY, newField);
            var form = _managementService.PutNewForm(newField);
            newField.Id = form.Id;
        }

        [When(@"I PUT New Form with color '([^']+)', icon '([^']+)', disabling:")]
        public void WhenIPUTNewFormWithColorIconDisabling(string color, string icon, Table table)
        {
            var existingFields = PropertyBucket.GetProperty<FormsActivity>(CUSTOM_FIELDS_KEY);
            var newFields = _managementService.UpdateFields(existingFields.Fields, table);
            var newField = new FormsActivity
            {
                Color = color,
                Icon = icon,
                Fields = newFields,
                Id = 0,
                Name = "Api " + StringUtils.RandomAlphaNumericString(45),
                _meta = existingFields._meta
            };

            PropertyBucket.Remember(ACTIVITY_FORM_KEY, newField);
            var form = _managementService.PutNewForm(newField);
            newField.Id = form.Id;
        }

        [Then(@"new form activity is created in Activities Forms \(GET\) with color and icon")]
        public void ThenNewFormActivityIsCreatedWithSpecifiedColorAndIcon()
        {
            var forms = _managementService.GetActivitiesTypes();
            var expFormsConf = PropertyBucket.GetProperty<FormsActivity>(ACTIVITY_FORM_KEY);
            var customFormCreated = forms.Items.FirstOrError(item => item.Id == expFormsConf.Id, "Form was not created in the system");

            Assert.Multiple(() =>
            {
                Assert.That(customFormCreated.Name, Is.EqualTo(expFormsConf.Name), Err.Line("Wrong name"));
                Assert.That(customFormCreated.Color, Is.EqualTo(expFormsConf.Color), Err.Line("Wrong color"));
                Assert.That(customFormCreated.Icon, Is.EqualTo(expFormsConf.Icon), Err.Line("Wrong icon"));
            });
        }

        [Then(@"I DELETE activity form")]
        [When(@"I DELETE activity form")]
        public void WhenIdeleteActivityForm()
        {
            var expFormsConf = PropertyBucket.GetProperty<FormsActivity>(ACTIVITY_FORM_KEY);
            var resp = _managementService.DeleteFormsActivities(expFormsConf.Id);
            resp.CheckCode();
        }

        [Then(@"activity form is not among Activities Forms \(GET\)")]
        public void ThenActivityFormIsNotAmoungActivitiesFormsGet()
        {
            var forms = _managementService.GetActivitiesTypes();
            var expFormsConf = PropertyBucket.GetProperty<FormsActivity>(ACTIVITY_FORM_KEY);
            var isPresent = forms.Items.Any(item => item.Id == expFormsConf.Id);
            Assert.IsFalse(isPresent, "Activity form is not expected to be present");
        }

        [When(@"I edit \(PUT\) activity form name, color '(.*)', icon '(.*)'")]
        public void WhenIEditPutActivityFormNameColorIcon(string color, string icon)
        {
            var expFormsConf = PropertyBucket.GetProperty<FormsActivity>(ACTIVITY_FORM_KEY);

            expFormsConf.Color = color;
            expFormsConf.Icon = icon;
            expFormsConf.Name = "Edited api " + Guid.NewGuid();

            PropertyBucket.Remember(ACTIVITY_FORM_KEY, expFormsConf, true);
            var form = _managementService.PutNewForm(expFormsConf);
        }

        [Given(@"activity form with name '(.*)', color '(.*)', icon '(.*)' exist \(Settings > Form Management\), edition '(.*)'")]
        public FormsActivity GivenActivityFormWithNameColorIconExistSettingsFormManagementEdition(string typeName, string color, string icon, string edition)
        {
            var adminSessionKey = new LoginSteps(_objectContainer, FeatureContext, ScenarioContext).GivenILoginAsSharedUser(edition);
            var newType = new FormManagementService(adminSessionKey).CreateIfNotPresent(typeName, color, icon);
            PropertyBucket.Remember(CUSTOM_TYPE_ID, newType.Id);
            return newType;
        }

        [Then(@"new form activity is created in Activities Forms \(GET\) with color, icon and fields")]
        public void ThenNewFormActivityIsCreatedInActivitiesFormsGETWithColorIconAndFields()
        {
            var forms = _managementService.GetActivitiesTypes();
            var expFormsConf = PropertyBucket.GetProperty<FormsActivity>(ACTIVITY_FORM_KEY);
            var customFormCreated = forms.Items.FirstOrError(item => item.Id == expFormsConf.Id, Err.Msg("Form was not created in the system"));

            Assert.Multiple(() =>
            {
                Assert.That(customFormCreated.Name, Is.EqualTo(expFormsConf.Name), Err.Line("Wrong name"));
                Assert.That(customFormCreated.Color, Is.EqualTo(expFormsConf.Color), Err.Line("Wrong color"));
                Assert.That(customFormCreated.Icon, Is.EqualTo(expFormsConf.Icon), Err.Line("Wrong icon"));
                Assert.That(customFormCreated.Fields, Is.Not.Empty, Err.Msg("Empty result"));
                foreach (var field in customFormCreated.Fields.Select((value, i) => new { i, value }))
                {
                    Assert.That(field.value.IsEnabled, Is.EqualTo(expFormsConf.Fields[field.i].IsEnabled), Err.Line("Wrong field status"));
                }

            });
        }

    }
}
