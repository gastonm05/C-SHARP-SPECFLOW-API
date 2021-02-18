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
namespace CCC_API.Features.News
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.3.2.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("NewsCustomFields")]
    public partial class NewsCustomFieldsFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "NewsCustomFields.feature"
#line hidden
        
        [NUnit.Framework.OneTimeSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "NewsCustomFields", "\tIn order to manage News\r\n\tAs a C3 User\r\n\tI want to be able to CRUD Custom Fields" +
                    "", ProgrammingLanguage.CSharp, ((string[])(null)));
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
        [NUnit.Framework.DescriptionAttribute("News - Expose News Custom Fields")]
        [NUnit.Framework.IgnoreAttribute("Ignored scenario")]
        [NUnit.Framework.CategoryAttribute("herdOfGnus")]
        [NUnit.Framework.CategoryAttribute("news")]
        public virtual void News_ExposeNewsCustomFields()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("News - Expose News Custom Fields", new string[] {
                        "herdOfGnus",
                        "ignore"});
#line 7
this.ScenarioSetup(scenarioInfo);
#line 8
 testRunner.Given("shared session for \'standard\' user with edition \'basic\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 9
 testRunner.When("I search for news by \'Keywords\' with a value of \'owls\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 10
 testRunner.Then("the News endpoint has the correct response", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 11
 testRunner.And("the News endpoint has news with value \'owls\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 12
 testRunner.When("I perform GET to a single news item from search", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 13
 testRunner.Then("I should see that the News Clip contains Custom Fields", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("PATCH - News Custom Fields")]
        [NUnit.Framework.IgnoreAttribute("Ignored scenario")]
        [NUnit.Framework.CategoryAttribute("herdOfGnus")]
        [NUnit.Framework.CategoryAttribute("news")]
        public virtual void PATCH_NewsCustomFields()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("PATCH - News Custom Fields", new string[] {
                        "herdOfGnus",
                        "ignore"});
#line 16
this.ScenarioSetup(scenarioInfo);
#line 17
 testRunner.Given("I login as \'analytics manager\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 18
 testRunner.When("I perform a GET for all news", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 19
 testRunner.And("I perform GET to a single news item from search", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 20
 testRunner.And("I perform a GET for all available custom fields", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 21
 testRunner.And("I perform a PATCH to News Custom Field with type \'String\' from Single News", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 22
 testRunner.Then("the News Custom Field Endpoint response should be \'200\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("PATCH - News Custom Fields Null out Date values")]
        [NUnit.Framework.IgnoreAttribute("Ignored scenario")]
        [NUnit.Framework.CategoryAttribute("herdOfGnus")]
        [NUnit.Framework.CategoryAttribute("news")]
        public virtual void PATCH_NewsCustomFieldsNullOutDateValues()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("PATCH - News Custom Fields Null out Date values", new string[] {
                        "herdOfGnus",
                        "ignore"});
#line 25
this.ScenarioSetup(scenarioInfo);
#line 26
 testRunner.Given("I login as \'analytics manager\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 27
 testRunner.When("I perform a GET for all news", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 28
 testRunner.And("I perform GET to a single news item from search", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 29
 testRunner.And("I perform a GET for all available custom fields", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 30
 testRunner.And("I perform a PATCH to null out News Custom Field with type \'Date\' from Single News" +
                    "", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 31
 testRunner.Then("the News Custom Field Endpoint response should be \'200\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("PATCH - Bulk Edit Text Custom Field")]
        [NUnit.Framework.IgnoreAttribute("Ignored scenario")]
        [NUnit.Framework.CategoryAttribute("herdOfGnus")]
        [NUnit.Framework.CategoryAttribute("news")]
        public virtual void PATCH_BulkEditTextCustomField()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("PATCH - Bulk Edit Text Custom Field", new string[] {
                        "herdOfGnus",
                        "ignore"});
#line 34
this.ScenarioSetup(scenarioInfo);
#line 35
 testRunner.Given("I login as \'ESAManager - Custom Fields\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 36
 testRunner.When("I search for news by \'Keywords\' with a value of \'automation testing\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 37
 testRunner.And("I perform a PATCH to all the results to update \'Text UDF\' custom field", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 38
 testRunner.Then("the News Custom Field Bulk Edit Endpoint response should be \'204\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("PATCH - Bulk Edit Decimal Custom Field")]
        [NUnit.Framework.IgnoreAttribute("Ignored scenario")]
        [NUnit.Framework.CategoryAttribute("herdOfGnus")]
        [NUnit.Framework.CategoryAttribute("news")]
        public virtual void PATCH_BulkEditDecimalCustomField()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("PATCH - Bulk Edit Decimal Custom Field", new string[] {
                        "herdOfGnus",
                        "ignore"});
#line 41
this.ScenarioSetup(scenarioInfo);
#line 42
 testRunner.Given("I login as \'ESAManager - Custom Fields\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 43
 testRunner.When("I search for news by \'Keywords\' with a value of \'automation testing\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 44
 testRunner.And("I perform a PATCH to all the results to update \'Decimal UDF\' custom field", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 45
 testRunner.Then("the News Custom Field Bulk Edit Endpoint response should be \'204\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("PATCH - Bulk Edit Date Custom Field")]
        [NUnit.Framework.IgnoreAttribute("Ignored scenario")]
        [NUnit.Framework.CategoryAttribute("herdOfGnus")]
        [NUnit.Framework.CategoryAttribute("news")]
        public virtual void PATCH_BulkEditDateCustomField()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("PATCH - Bulk Edit Date Custom Field", new string[] {
                        "herdOfGnus",
                        "ignore"});
#line 48
this.ScenarioSetup(scenarioInfo);
#line 49
 testRunner.Given("I login as \'ESAManager - Custom Fields\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 50
 testRunner.When("I search for news by \'Keywords\' with a value of \'automation testing\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 51
 testRunner.And("I perform a PATCH to all the results to update \'Date UDF\' custom field", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 52
 testRunner.Then("the News Custom Field Bulk Edit Endpoint response should be \'204\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("PATCH - Bulk Edit Boolean Custom Field")]
        [NUnit.Framework.IgnoreAttribute("Ignored scenario")]
        [NUnit.Framework.CategoryAttribute("herdOfGnus")]
        [NUnit.Framework.CategoryAttribute("news")]
        public virtual void PATCH_BulkEditBooleanCustomField()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("PATCH - Bulk Edit Boolean Custom Field", new string[] {
                        "herdOfGnus",
                        "ignore"});
#line 55
this.ScenarioSetup(scenarioInfo);
#line 56
 testRunner.Given("I login as \'ESAManager - Custom Fields\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 57
 testRunner.When("I search for news by \'Keywords\' with a value of \'automation testing\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 58
 testRunner.And("I perform a PATCH to all the results to update \'Bool UDF\' custom field", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 59
 testRunner.Then("the News Custom Field Bulk Edit Endpoint response should be \'204\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("PATCH - Bulk Edit Memo Custom Field")]
        [NUnit.Framework.IgnoreAttribute("Ignored scenario")]
        [NUnit.Framework.CategoryAttribute("herdOfGnus")]
        [NUnit.Framework.CategoryAttribute("news")]
        public virtual void PATCH_BulkEditMemoCustomField()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("PATCH - Bulk Edit Memo Custom Field", new string[] {
                        "herdOfGnus",
                        "ignore"});
#line 62
this.ScenarioSetup(scenarioInfo);
#line 63
 testRunner.Given("I login as \'ESAManager - Custom Fields\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 64
 testRunner.When("I search for news by \'Keywords\' with a value of \'automation testing\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 65
 testRunner.And("I perform a PATCH to all the results to update \'Memo UDF\' custom field", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 66
 testRunner.Then("the News Custom Field Bulk Edit Endpoint response should be \'204\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Order by Custom Fields")]
        [NUnit.Framework.IgnoreAttribute("Ignored scenario")]
        [NUnit.Framework.CategoryAttribute("herdOfGnus")]
        [NUnit.Framework.CategoryAttribute("news")]
        public virtual void OrderByCustomFields()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Order by Custom Fields", new string[] {
                        "herdOfGnus",
                        "ignore"});
#line 69
this.ScenarioSetup(scenarioInfo);
#line 70
 testRunner.Given("I login as \'ESAManager\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 71
 testRunner.When("I perform a GET for all news", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 72
 testRunner.And("I perform a GET for all available custom fields", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 73
 testRunner.And("I perform a GET for news ordered by allowed custom field", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 74
 testRunner.Then("the News endpoint has the correct response for ordered news by custom field", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("News Search by Include or Exclude Custom Field")]
        [NUnit.Framework.CategoryAttribute("herdOfGnus")]
        [NUnit.Framework.CategoryAttribute("news")]
        public virtual void NewsSearchByIncludeOrExcludeCustomField()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("News Search by Include or Exclude Custom Field", new string[] {
                        "herdOfGnus"});
#line 77
this.ScenarioSetup(scenarioInfo);
#line 78
 testRunner.Given("I login as \'ESAManager\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 79
 testRunner.When("I perform a GET for all available custom fields", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 80
 testRunner.And("I select the first with Type \'string\' and a MultiSelect value of \'true\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 81
 testRunner.And("I perform a GET for news by selected Custom Field using the \'Include\' operator", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 82
 testRunner.Then("the News Endpoint responds with a \'200\' for search by Include field", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 83
 testRunner.And("all items should be from the included custom field", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 84
 testRunner.When("I perform a GET for news by selected Custom Field using the \'Exclude\' operator", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 85
 testRunner.Then("the News Endpoint responds with a \'200\' for search by Exclude field", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 86
 testRunner.And("none of the items should be from the excluded custom field", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
