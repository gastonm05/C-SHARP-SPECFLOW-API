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
namespace CCC_API.Features.Analytics
{
    using TechTalk.SpecFlow;


    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.3.2.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("Analytics - Searches End Point")]
    [NUnit.Framework.CategoryAttribute("HeartsAndCharts")]
    public partial class Analytics_SearchesEndPointFeature
    {

        private TechTalk.SpecFlow.ITestRunner testRunner;

#line 1 "AnalyticsSearchesEndPoint.feature"
#line hidden

        [NUnit.Framework.OneTimeSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Analytics - Searches End Point", "\tTo verify that analytics searches and groups can be created, modified and retrie" +
                    "ved\r\n\tAs a valid CCC user with system parameter Analytics-SearchGroups-Enabled s" +
                    "et to true\r\n\tI want to call the analytics searches endpoint", ProgrammingLanguage.CSharp, new string[] {
                        "HeartsAndCharts"});
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
        [NUnit.Framework.CategoryAttribute("analytics")]
        [NUnit.Framework.CategoryAttribute("smokeProd")]
        [NUnit.Framework.DescriptionAttribute("Get Searches")]
        public virtual void GetSearches()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Get Searches", ((string[])(null)));
#line 7
            this.ScenarioSetup(scenarioInfo);
#line 8
            testRunner.Given("I login as \'analytics user\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 9
            testRunner.When("I perform a GET for analytics searches", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 10
            testRunner.Then("there are analytics searches", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }

        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.CategoryAttribute("analytics")]
        [NUnit.Framework.DescriptionAttribute("Create Search")]
        public virtual void CreateSearch()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Create Search", ((string[])(null)));
#line 12
            this.ScenarioSetup(scenarioInfo);
#line 13
            testRunner.Given("I login as \'analytics manager\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 14
            testRunner.When("I create a new analytics search \'test\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 15
            testRunner.Then("the new analytics search \'test\' exists", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 16
            testRunner.And("I can GET the analytics search \'test\' by id", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 17
            testRunner.And("I delete the analytics search \'test\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }

        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.CategoryAttribute("analytics")]
        [NUnit.Framework.DescriptionAttribute("Get Search Groups")]
        [NUnit.Framework.IgnoreAttribute("Ignored scenario")]
        public virtual void GetSearchGroups()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Get Search Groups", new string[] {
                        "ignore"});
#line 20
            this.ScenarioSetup(scenarioInfo);
#line 21
            testRunner.Given("I login as \'analytics manager\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 22
            testRunner.When("I perform a GET for analytics search groups", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 23
            testRunner.Then("there are analytics group searches", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }

        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.CategoryAttribute("analytics")]
        [NUnit.Framework.DescriptionAttribute("Create Search Group")]
        [NUnit.Framework.IgnoreAttribute("Ignored scenario")]
        public virtual void CreateSearchGroup()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Create Search Group", new string[] {
                        "ignore"});
#line 26
            this.ScenarioSetup(scenarioInfo);
#line 27
            testRunner.Given("I login as \'analytics manager\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 28
            testRunner.When("I create a new analytics search group", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 29
            testRunner.Then("the new analytics search group exists", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 30
            testRunner.And("I delete the analytics search group", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }

        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.CategoryAttribute("analytics")]
        [NUnit.Framework.DescriptionAttribute("Cannot Create duplicate Search Group")]
        [NUnit.Framework.IgnoreAttribute("Ignored scenario")]
        public virtual void CannotCreateDuplicateSearchGroup()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Cannot Create duplicate Search Group", new string[] {
                        "ignore"});
#line 33
            this.ScenarioSetup(scenarioInfo);
#line 34
            testRunner.Given("I login as \'analytics manager\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 35
            testRunner.When("I create a new analytics search group", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 36
            testRunner.Then("the new analytics search group exists", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 37
            testRunner.And("I cannot create a duplicate analytics search group", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 38
            testRunner.And("the new analytics search group exists", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 39
            testRunner.And("I delete the analytics search group", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }

        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.CategoryAttribute("analytics")]
        [NUnit.Framework.DescriptionAttribute("Add Search to Search Group")]
        [NUnit.Framework.IgnoreAttribute("Ignored scenario")]
        public virtual void AddSearchToSearchGroup()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Add Search to Search Group", new string[] {
                        "ignore"});
#line 43
            this.ScenarioSetup(scenarioInfo);
#line 44
            testRunner.Given("I login as \'analytics manager\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 45
            testRunner.When("I create a new analytics search \'test\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 46
            testRunner.And("I create a new analytics search group", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 47
            testRunner.And("I add the analytics search \'test\' to the search group", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 48
            testRunner.Then("the new analytics search \'test\' exists", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 49
            testRunner.And("the new analytics search group exists", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 50
            testRunner.And("I delete the analytics search \'test\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 51
            testRunner.And("I delete the analytics search group", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }

        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.CategoryAttribute("analytics")]
        [NUnit.Framework.DescriptionAttribute("Add duplicate Search to Search Group")]
        [NUnit.Framework.IgnoreAttribute("Ignored scenario")]
        public virtual void AddDuplicateSearchToSearchGroup()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Add duplicate Search to Search Group", new string[] {
                        "ignore"});
#line 54
            this.ScenarioSetup(scenarioInfo);
#line 55
            testRunner.Given("I login as \'analytics manager\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 56
            testRunner.When("I create a new analytics search \'test\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 57
            testRunner.And("I create a new analytics search group", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 58
            testRunner.And("I add the analytics search \'test\' to the search group", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 59
            testRunner.And("I add the analytics search \'test\' to the search group", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 60
            testRunner.Then("the new analytics search \'test\' exists", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 61
            testRunner.And("the new analytics search group exists", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 62
            testRunner.And("I delete the analytics search \'test\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 63
            testRunner.And("I delete the analytics search group", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }

        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.CategoryAttribute("analytics")]
        [NUnit.Framework.DescriptionAttribute("Remove single Search from Search Group")]
        [NUnit.Framework.IgnoreAttribute("Ignored scenario")]
        public virtual void RemoveSingleSearchFromSearchGroup()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Remove single Search from Search Group", new string[] {
                        "ignore"});
#line 66
            this.ScenarioSetup(scenarioInfo);
#line 67
            testRunner.Given("I login as \'analytics manager\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 68
            testRunner.When("I create a new analytics search group", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 69
            testRunner.And("I create a new analytics search \'test\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 70
            testRunner.And("I add the analytics search \'test\' to the search group", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 71
            testRunner.And("I remove the analytics search \'test\' from the search group", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 72
            testRunner.Then("the new analytics search group exists", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 73
            testRunner.And("the new analytics search \'test\' exists", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 74
            testRunner.And("I delete the analytics search group", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 75
            testRunner.And("I delete the analytics search \'test\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }

        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.CategoryAttribute("analytics")]
        [NUnit.Framework.DescriptionAttribute("Modify Search Group multiple times")]
        public virtual void ModifySearchGroupMultipleTimes()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Modify Search Group multiple times", ((string[])(null)));
#line 78
            this.ScenarioSetup(scenarioInfo);
#line 79
            testRunner.Given("I login as \'analytics manager\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 80
            testRunner.When("I create a new analytics search group", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 81
            testRunner.And("I create a new analytics search \'test\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 82
            testRunner.And("I add the analytics search \'test\' to the search group", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 83
            testRunner.And("I remove the analytics search \'test\' from the search group", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 84
            testRunner.And("I add the analytics search \'test\' to the search group", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 85
            testRunner.And("I remove the analytics search \'test\' from the search group", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 86
            testRunner.Then("the new analytics search group exists", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 87
            testRunner.And("the new analytics search \'test\' exists", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 88
            testRunner.And("I delete the analytics search \'test\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 89
            testRunner.And("I delete the analytics search group", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }

        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.CategoryAttribute("analytics")]
        [NUnit.Framework.DescriptionAttribute("Add multiple Searches to Search Group")]
        public virtual void AddMultipleSearchesToSearchGroup()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Add multiple Searches to Search Group", ((string[])(null)));
#line 91
            this.ScenarioSetup(scenarioInfo);
#line 92
            testRunner.Given("I login as \'analytics manager\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 93
            testRunner.When("I create a new analytics search group", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 94
            testRunner.And("I create a new analytics search \'#1\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 95
            testRunner.And("I add the analytics search \'#1\' to the search group", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 96
            testRunner.And("I create a new analytics search \'#2\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 97
            testRunner.And("I add the analytics search \'#2\' to the search group", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 98
            testRunner.Then("the new analytics search group exists", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 99
            testRunner.And("the new analytics search \'#1\' exists", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 100
            testRunner.And("the new analytics search \'#2\' exists", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 101
            testRunner.And("I delete the analytics search group", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 102
            testRunner.And("I delete the analytics search \'#1\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 103
            testRunner.And("I delete the analytics search \'#2\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }

        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.CategoryAttribute("analytics")]
        [NUnit.Framework.DescriptionAttribute("Remove multiple Searches from Search Group")]
        public virtual void RemoveMultipleSearchesFromSearchGroup()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Remove multiple Searches from Search Group", ((string[])(null)));
#line 105
            this.ScenarioSetup(scenarioInfo);
#line 106
            testRunner.Given("I login as \'analytics manager\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 107
            testRunner.When("I create a new analytics search group", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 108
            testRunner.And("I create a new analytics search \'#1\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 109
            testRunner.And("I add the analytics search \'#1\' to the search group", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 110
            testRunner.And("I create a new analytics search \'#2\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 111
            testRunner.And("I add the analytics search \'#2\' to the search group", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 112
            testRunner.And("I remove the analytics search \'#1\' from the search group", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 113
            testRunner.And("I remove the analytics search \'#2\' from the search group", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 114
            testRunner.Then("the new analytics search group exists", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 115
            testRunner.And("the new analytics search \'#1\' exists", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 116
            testRunner.And("the new analytics search \'#2\' exists", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 117
            testRunner.And("I delete the analytics search group", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 118
            testRunner.And("I delete the analytics search \'#1\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 119
            testRunner.And("I delete the analytics search \'#2\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion

