﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:2.3.2.0
//      SpecFlow Generator Version:2.3.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace CCC_API.Features.Activities.NewActivity
{
    using TechTalk.SpecFlow;


    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.3.2.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("CustomActivity")]
    public partial class CustomActivityFeature
    {

        private TechTalk.SpecFlow.ITestRunner testRunner;

#line 1 "CustomActivity.feature"
#line hidden

        [NUnit.Framework.OneTimeSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "CustomActivity", "\tIn order to organize my things\r\n\tI can create custom activity", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }

        [NUnit.Framework.OneTimeTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }

        [NUnit.Framework.SetUpAttribute()]
        public virtual void TestInitialize()
        {
        }

        [NUnit.Framework.TearDownAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }

        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }

        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }

        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Custom activity > Create (Post) > Activities > Delete")]
        [NUnit.Framework.CategoryAttribute("publishers")]
        [NUnit.Framework.CategoryAttribute("custom_activity")]
        [NUnit.Framework.CategoryAttribute("activities")]
        [NUnit.Framework.CategoryAttribute("smokeProd")]
        [NUnit.Framework.TestCaseAttribute("Callback", "sent", "", "ab", null)]
        [NUnit.Framework.TestCaseAttribute("Appointment", "", "", "sw", null)]
        [NUnit.Framework.TestCaseAttribute("Inquiry", "scheduled", "", "", null)]
        [NUnit.Framework.TestCaseAttribute("Other", "sent", "test", "", null)]
        public virtual void CustomActivityCreatePostActivitiesDelete(string type, string state, string contact, string outlet, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "publishers",
                    "custom_activity"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Custom activity > Create (Post) > Activities > Delete", @__tags);
#line 6
            this.ScenarioSetup(scenarioInfo);
#line 7
            testRunner.Given("I login as \'analytics manager\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");

#line hidden
            TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                        "Title",
                        "Type",
                        "Notes",
                        "TimeZoneIdentifier",
                        "ScheduleTime"});
            table1.AddRow(new string[] {
                        "4",
                        "Callback",
                        "50",
                        "GMT Standard Time",
                        "-1"});
            table1.AddRow(new string[] {
                        "15",
                        "Appointment",
                        "100",
                        "Central Standard Time",
                        "0"});
            table1.AddRow(new string[] {
                        "30",
                        "Inquiry",
                        "10",
                        "W. Europe Standard Time",
                        "25"});
            table1.AddRow(new string[] {
                        "20",
                        "Other",
                        "1",
                        "UTC",
                        "-24"});
#line 8
            testRunner.And("custom activity combinations:", ((string)(null)), table1, "And ");
#line 15
            testRunner.Given(string.Format("I take combination of \'{0}\'", type), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 16
            testRunner.When(string.Format("I POST custom activity of \'{0}\' \'{1}\' \'{2}\'", type, contact, outlet), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 17
            testRunner.Then(string.Format("can find activity of \'{0}\' and \'{1}\' activity listed among published activities", type, state), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 18
            testRunner.And(string.Format("can GET information about Custom Activity of \'{0}\'", type), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 20
            testRunner.Given("shared session for \'system_admin\' user with edition \'ESAManager\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 21
            testRunner.Then(string.Format("I can DELETE custom activity of type \'{0}\'", type), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 22
            testRunner.And(string.Format("cannot find activity of \'{0}\' and \'{1}\' activity listed among published activitie" +
                                   "s", type, state), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }

        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Custom activity > Create (Post) With DEFAULT Custom Fields")]
        [NUnit.Framework.IgnoreAttribute("Ignored scenario")]
        [NUnit.Framework.CategoryAttribute("publishers")]
        [NUnit.Framework.CategoryAttribute("custom_activity")]
        [NUnit.Framework.CategoryAttribute("CustomFields")]
        [NUnit.Framework.CategoryAttribute("activities")]
        public virtual void CustomActivityCreatePostWithDEFAULTCustomFields()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Custom activity > Create (Post) With DEFAULT Custom Fields", new string[] {
                        "publishers",
                        "custom_activity",
                        "CustomFields",
                        "ignore"});
#line 32
            this.ScenarioSetup(scenarioInfo);
#line 33
            testRunner.Given("shared session for \'system_admin\' user with edition \'Publishers social company, c" +
                               "ustom fields\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
            TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                        "Title",
                        "Type",
                        "Notes",
                        "TimeZoneIdentifier",
                        "ScheduleTime"});
            table2.AddRow(new string[] {
                        "100",
                        "SendMailing",
                        "5",
                        "Pacific Standard Time",
                        "9"});
#line 34
            testRunner.And("custom activity combinations:", ((string)(null)), table2, "And ");
#line hidden
            TechTalk.SpecFlow.Table table3 = new TechTalk.SpecFlow.Table(new string[] {
                        "Custom Field Type"});
            table3.AddRow(new string[] {
                        "String"});
            table3.AddRow(new string[] {
                        "Memo"});
            table3.AddRow(new string[] {
                        "Number"});
            table3.AddRow(new string[] {
                        "Yes / No"});
            table3.AddRow(new string[] {
                        "Date"});
            table3.AddRow(new string[] {
                        "Single-Select"});
            table3.AddRow(new string[] {
                        "Multi-Select"});
#line 37
            testRunner.And("custom fields for \'Activity\' present:", ((string)(null)), table3, "And ");
#line 46
            testRunner.And("I take combination of \'SendMailing\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 47
            testRunner.When("I POST custom activity of type \'SendMailing\' with Custom Fields set to default va" +
                               "lues", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 48
            testRunner.Then("can find activity of \'SendMailing\' and \'scheduled\' activity listed among publishe" +
                               "d activities", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 49
            testRunner.And("can GET information about Custom Activity of \'SendMailing\' with custom fields val" +
                               "ues", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }

        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Custom activity > Create (Post) With Valid Custom Fields MAX values")]
        [NUnit.Framework.IgnoreAttribute("Ignored scenario")]
        [NUnit.Framework.CategoryAttribute("publishers")]
        [NUnit.Framework.CategoryAttribute("custom_activity")]
        [NUnit.Framework.CategoryAttribute("CustomFields")]
        [NUnit.Framework.CategoryAttribute("activities")]
        public virtual void CustomActivityCreatePostWithValidCustomFieldsMAXValues()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Custom activity > Create (Post) With Valid Custom Fields MAX values", new string[] {
                        "publishers",
                        "custom_activity",
                        "CustomFields",
                        "ignore"});
#line 52
            this.ScenarioSetup(scenarioInfo);
#line 53
            testRunner.Given("shared session for \'standard\' user with edition \'Publishers social company, custo" +
                               "m fields\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
            TechTalk.SpecFlow.Table table4 = new TechTalk.SpecFlow.Table(new string[] {
                        "Title",
                        "Type",
                        "Notes",
                        "TimeZoneIdentifier",
                        "ScheduleTime"});
            table4.AddRow(new string[] {
                        "50",
                        "Inquiry",
                        "0",
                        "Romance Standard Time",
                        "-13"});
#line 54
            testRunner.And("custom activity combinations:", ((string)(null)), table4, "And ");
#line hidden
            TechTalk.SpecFlow.Table table5 = new TechTalk.SpecFlow.Table(new string[] {
                        "Custom Field Type"});
            table5.AddRow(new string[] {
                        "String"});
            table5.AddRow(new string[] {
                        "Memo"});
            table5.AddRow(new string[] {
                        "Number"});
            table5.AddRow(new string[] {
                        "Yes / No"});
            table5.AddRow(new string[] {
                        "Date"});
            table5.AddRow(new string[] {
                        "Single-Select"});
            table5.AddRow(new string[] {
                        "Multi-Select"});
#line 57
            testRunner.And("custom fields for \'Activity\' present:", ((string)(null)), table5, "And ");
#line 66
            testRunner.And("I take combination of \'Inquiry\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 67
            testRunner.When("I POST custom activity of type \'Inquiry\' with Custom Fields having each MAX allow" +
                               "ed value", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 68
            testRunner.Then("can find activity of \'Inquiry\' and \'sent\' activity listed among published activit" +
                               "ies", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 69
            testRunner.And("can GET information about Custom Activity of \'Inquiry\' with custom fields values", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }

        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Custom activity > Create Custom Activity with Type from Form Management")]
        [NUnit.Framework.IgnoreAttribute("Ignored scenario")]
        [NUnit.Framework.CategoryAttribute("publishers")]
        [NUnit.Framework.CategoryAttribute("custom_activity")]
        [NUnit.Framework.CategoryAttribute("activities")]
        public virtual void CustomActivityCreateCustomActivityWithTypeFromFormManagement()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Custom activity > Create Custom Activity with Type from Form Management", new string[] {
                        "publishers",
                        "custom_activity",
                        "ignore"});
#line 72
            this.ScenarioSetup(scenarioInfo);
#line 73
            testRunner.Given("session for edition \'ESAManager\', permission: \'standard\', datagroup: \'group2\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 74
            testRunner.And("activity form with name \'Automation activity type\', color \'#FFBB00\', icon \'fa-gif" +
                               "t\' exist (Settings > Form Management), edition \'ESAManager\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table6 = new TechTalk.SpecFlow.Table(new string[] {
                        "Title",
                        "Type",
                        "Notes",
                        "TimeZoneIdentifier",
                        "ScheduleTime"});
            table6.AddRow(new string[] {
                        "45",
                        "Automation activity type",
                        "10",
                        "GMT Standard Time",
                        "24"});
#line 75
            testRunner.And("custom activity combinations:", ((string)(null)), table6, "And ");
#line 78
            testRunner.And("I take combination of \'Automation activity type\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 79
            testRunner.When("I POST custom activity with type \'Automation activity type\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 80
            testRunner.Then("can find activity of \'Automation activity type\' and \'Scheduled\' activity listed a" +
                               "mong published activities", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 81
            testRunner.And("can GET information about Custom Activity of \'Automation activity type\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion

