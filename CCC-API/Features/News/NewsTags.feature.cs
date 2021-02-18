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
    [NUnit.Framework.DescriptionAttribute("NewsTags")]
    public partial class NewsTagsFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "NewsTags.feature"
#line hidden
        
        [NUnit.Framework.OneTimeSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "NewsTags", "\tIn order to categorize news items\r\n\tAs a standard CCC user\r\n\tI want to be able t" +
                    "o tag items", ProgrammingLanguage.CSharp, ((string[])(null)));
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
        [NUnit.Framework.DescriptionAttribute("Tags endpoint returns items")]
        [NUnit.Framework.IgnoreAttribute("Ignored scenario")]
        [NUnit.Framework.CategoryAttribute("herdOfGnus")]
        [NUnit.Framework.CategoryAttribute("news")]
        public virtual void TagsEndpointReturnsItems()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Tags endpoint returns items", new string[] {
                        "herdOfGnus",
                        "ignore"});
#line 7
this.ScenarioSetup(scenarioInfo);
#line 8
 testRunner.Given("shared session for \'standard\' user with edition \'basic\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 9
 testRunner.When("I perform a GET to the tags endpoint", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 10
 testRunner.Then("there should be tags returned", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Validate Tag Rename Endpoint")]
        [NUnit.Framework.IgnoreAttribute("Ignored scenario")]
        [NUnit.Framework.CategoryAttribute("herdOfGnus")]
        [NUnit.Framework.CategoryAttribute("news")]
        public virtual void ValidateTagRenameEndpoint()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Validate Tag Rename Endpoint", new string[] {
                        "herdOfGnus",
                        "ignore"});
#line 13
this.ScenarioSetup(scenarioInfo);
#line 14
 testRunner.Given("I login as \'ESAManager\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 15
 testRunner.When("I perform a POST to create a new tag with name \'NewsTagTest7\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 16
 testRunner.And("I perform a PATCH to update tag name", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 17
 testRunner.Then("the Single Tag Endpoint response should be \'200\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 18
 testRunner.And("I should see that the tag name was updated", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 19
 testRunner.And("I perform a DELETE to eliminate recently created tag", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Create/Duplicate/Delete New Tag")]
        [NUnit.Framework.IgnoreAttribute("Ignored scenario")]
        [NUnit.Framework.CategoryAttribute("herdOfGnus")]
        [NUnit.Framework.CategoryAttribute("news")]
        public virtual void CreateDuplicateDeleteNewTag()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Create/Duplicate/Delete New Tag", new string[] {
                        "herdOfGnus",
                        "ignore"});
#line 22
this.ScenarioSetup(scenarioInfo);
#line 23
 testRunner.Given("I login as \'ESAManager\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 24
 testRunner.When("I perform a POST to create a new tag with name \'NewsTagTest\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 25
 testRunner.Then("I should see the tags endpoint has the correct response for new tag", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 26
 testRunner.When("I perform a POST to create a duplicate tag with name \'NewsTagTest\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 27
 testRunner.Then("I should see the tags endpoint has the correct response for creating duplicate ta" +
                    "gs", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 28
 testRunner.And("I perform a DELETE to eliminate recently created tag", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 29
 testRunner.And("I should see that the delete tag endpoint has the correct response", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Validate Tags are added to News Search results")]
        [NUnit.Framework.IgnoreAttribute("Ignored scenario")]
        [NUnit.Framework.CategoryAttribute("herdOfGnus")]
        [NUnit.Framework.CategoryAttribute("news")]
        public virtual void ValidateTagsAreAddedToNewsSearchResults()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Validate Tags are added to News Search results", new string[] {
                        "herdOfGnus",
                        "ignore"});
#line 32
this.ScenarioSetup(scenarioInfo);
#line 33
 testRunner.Given("I login as \'ESA Standard User with Tags\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 34
 testRunner.When("I search for news by \'Tags\' with a value of \'Cision\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 35
 testRunner.Then("the News endpoint has the correct response for News Tags search", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 36
 testRunner.And("all the News Clips are tagged with \'Cision\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Attempting to add tag with existing tag name returns 400")]
        [NUnit.Framework.IgnoreAttribute("Ignored scenario")]
        [NUnit.Framework.CategoryAttribute("herdOfGnus")]
        [NUnit.Framework.CategoryAttribute("news")]
        public virtual void AttemptingToAddTagWithExistingTagNameReturns400()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Attempting to add tag with existing tag name returns 400", new string[] {
                        "herdOfGnus",
                        "ignore"});
#line 39
this.ScenarioSetup(scenarioInfo);
#line 40
 testRunner.Given("I login as \'ESAManager\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 41
 testRunner.When("I create the tag \'QA Test Tag\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 42
 testRunner.When("I perform a POST to create a new tag with name \'QA Test Tag\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 43
 testRunner.Then("the Create News Tags endpoint response status code should be \'400\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Bulk Tag News Items")]
        [NUnit.Framework.CategoryAttribute("herdOfGnus")]
        [NUnit.Framework.CategoryAttribute("news")]
        public virtual void BulkTagNewsItems()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Bulk Tag News Items", new string[] {
                        "herdOfGnus"});
#line 46
this.ScenarioSetup(scenarioInfo);
#line 47
 testRunner.Given("I login as \'ESAManager\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 48
 testRunner.When("I create the tag \'Bulk Tag Test\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 49
 testRunner.And("I search for news by start date with a value of \'Today minus 10 days\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 50
 testRunner.And("I bulk add the tag \'Bulk Tag Test\' to the first \'10\' news items", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 51
 testRunner.Then("I should see that the news endpoint has the correct response for appending tags", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Validate Tag Name max length is 50 characters")]
        [NUnit.Framework.IgnoreAttribute("Ignored scenario")]
        [NUnit.Framework.CategoryAttribute("herdOfGnus")]
        [NUnit.Framework.CategoryAttribute("news")]
        public virtual void ValidateTagNameMaxLengthIs50Characters()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Validate Tag Name max length is 50 characters", new string[] {
                        "herdOfGnus",
                        "ignore"});
#line 54
this.ScenarioSetup(scenarioInfo);
#line 55
 testRunner.Given("I login as \'ESAManager\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 56
 testRunner.When("I perform GET for all available tags", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 57
 testRunner.Then("I should see the max length for a tag name is \'50\' characters", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("News Search by Include Tags")]
        [NUnit.Framework.IgnoreAttribute("Ignored scenario")]
        [NUnit.Framework.CategoryAttribute("herdOfGnus")]
        [NUnit.Framework.CategoryAttribute("news")]
        public virtual void NewsSearchByIncludeTags()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("News Search by Include Tags", new string[] {
                        "herdOfGnus",
                        "ignore"});
#line 60
this.ScenarioSetup(scenarioInfo);
#line 61
 testRunner.Given("I login as \'ESAManager\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 62
 testRunner.When("I perform a GET for news by \'Cision\' Tag using the \'Include\' operator", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 63
 testRunner.Then("the News Endpoint responds with a \'200\' for search by Include field", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 64
 testRunner.And("all items should have the \'Cision\' tag", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("News Search by Exclude Tags")]
        [NUnit.Framework.IgnoreAttribute("Ignored scenario")]
        [NUnit.Framework.CategoryAttribute("herdOfGnus")]
        [NUnit.Framework.CategoryAttribute("news")]
        public virtual void NewsSearchByExcludeTags()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("News Search by Exclude Tags", new string[] {
                        "herdOfGnus",
                        "ignore"});
#line 67
this.ScenarioSetup(scenarioInfo);
#line 68
 testRunner.Given("I login as \'ESAManager\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 69
 testRunner.When("I perform a GET for news by \'Cision\' Tag using the \'Exclude\' operator", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 70
 testRunner.Then("the News Endpoint responds with a \'200\' for search by Exclude field", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 71
 testRunner.And("none of the items should have the \'Cision\' tag", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Validate PATCH to Bulk Replace & Remove Tags")]
        [NUnit.Framework.IgnoreAttribute("Ignored scenario")]
        [NUnit.Framework.CategoryAttribute("herdOfGnus")]
        [NUnit.Framework.CategoryAttribute("news")]
        public virtual void ValidatePATCHToBulkReplaceRemoveTags()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Validate PATCH to Bulk Replace & Remove Tags", new string[] {
                        "herdOfGnus",
                        "ignore"});
#line 74
this.ScenarioSetup(scenarioInfo);
#line 75
 testRunner.Given("I login as \'ESAManager\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 76
 testRunner.When("I create the tag \'Bulk Tag\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 77
 testRunner.And("I search for news by start date with a value of \'Today minus 10 days\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 78
 testRunner.And("I perform a PATCH to bulk replace with tag named \'Bulk Tag\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 79
 testRunner.Then("I should see that the news endpoint has the correct response for replacing tags", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 80
 testRunner.When("I perform a PATCH to bulk remove tag with name \'Bulk Tag\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 81
 testRunner.Then("I should see that the news endpoint has the correct response for removing tags", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion

