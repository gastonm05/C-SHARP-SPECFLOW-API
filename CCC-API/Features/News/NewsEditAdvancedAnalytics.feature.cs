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
    [NUnit.Framework.DescriptionAttribute("NewsEditAdvancedAnalytics")]
    public partial class NewsEditAdvancedAnalyticsFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "NewsEditAdvancedAnalytics.feature"
#line hidden
        
        [NUnit.Framework.OneTimeSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "NewsEditAdvancedAnalytics", "\tUser can override default analytics searches for a news article", ProgrammingLanguage.CSharp, ((string[])(null)));
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
        [NUnit.Framework.DescriptionAttribute("News > Edit News Advanced Company Analytics")]
        [NUnit.Framework.CategoryAttribute("publishers")]
        [NUnit.Framework.CategoryAttribute("news")]
        public virtual void NewsEditNewsAdvancedCompanyAnalytics()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("News > Edit News Advanced Company Analytics", new string[] {
                        "publishers"});
#line 5
this.ScenarioSetup(scenarioInfo);
#line 6
 testRunner.Given("session for edition \'Analytics company with features enabled and dynamic news\', p" +
                    "ermission: \'system_admin\', datagroup: \'news\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 7
 testRunner.And("analytics profile \'company\' searches present: \'Reebok, Nike, Martens, Vans\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 8
 testRunner.And("news article exist", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                        "search",
                        "tone",
                        "prominence",
                        "impact"});
            table1.AddRow(new string[] {
                        "Reebok",
                        "Negative",
                        "50",
                        "200"});
            table1.AddRow(new string[] {
                        "Nike",
                        "Positive",
                        "1",
                        "1"});
            table1.AddRow(new string[] {
                        "Martens",
                        "Neutral",
                        "49",
                        "5"});
            table1.AddRow(new string[] {
                        "Vans",
                        "None",
                        "20",
                        "40"});
#line 9
 testRunner.When("I perform a PATCH for news item to Add company searches:", ((string)(null)), table1, "When ");
#line hidden
            TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                        "search",
                        "tone",
                        "prominence",
                        "impact"});
            table2.AddRow(new string[] {
                        "Reebok",
                        "Negative",
                        "50",
                        "200"});
            table2.AddRow(new string[] {
                        "Nike",
                        "Positive",
                        "1",
                        "1"});
            table2.AddRow(new string[] {
                        "Martens",
                        "Neutral",
                        "49",
                        "5"});
            table2.AddRow(new string[] {
                        "Vans",
                        "None",
                        "20",
                        "40"});
#line 15
 testRunner.Then("news item has news analytics searches:", ((string)(null)), table2, "Then ");
#line hidden
            TechTalk.SpecFlow.Table table3 = new TechTalk.SpecFlow.Table(new string[] {
                        "search",
                        "tone",
                        "prominence",
                        "impact"});
            table3.AddRow(new string[] {
                        "Reebok",
                        "Negative",
                        "1",
                        "1"});
#line 21
 testRunner.When("I perform a PATCH for news item to Remove company searches:", ((string)(null)), table3, "When ");
#line hidden
            TechTalk.SpecFlow.Table table4 = new TechTalk.SpecFlow.Table(new string[] {
                        "search",
                        "tone",
                        "prominence",
                        "impact"});
            table4.AddRow(new string[] {
                        "Nike",
                        "Positive",
                        "1",
                        "1"});
            table4.AddRow(new string[] {
                        "Martens",
                        "Neutral",
                        "49",
                        "5"});
            table4.AddRow(new string[] {
                        "Vans",
                        "None",
                        "20",
                        "40"});
#line 24
 testRunner.Then("news item has news analytics searches:", ((string)(null)), table4, "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("News > Edit News Advanced Product Mentions Analytics > Remove")]
        [NUnit.Framework.CategoryAttribute("publishers")]
        [NUnit.Framework.CategoryAttribute("news")]
        public virtual void NewsEditNewsAdvancedProductMentionsAnalyticsRemove()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("News > Edit News Advanced Product Mentions Analytics > Remove", new string[] {
                        "publishers"});
#line 31
this.ScenarioSetup(scenarioInfo);
#line 32
 testRunner.Given("session for edition \'Analytics company with features enabled and dynamic news\', p" +
                    "ermission: \'system_admin\', datagroup: \'news\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 33
 testRunner.And("analytics profile \'product\' searches present: \'Tortilla, Wasabi, Pret, Wahaca\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 34
 testRunner.And("news article exist", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 35
 testRunner.When("I perform a PATCH for news item to Add product searches: \'Wasabi, Wahaca\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 36
 testRunner.Then("news item has news analytics searches: \'Wasabi, Wahaca\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 37
 testRunner.When("I perform a PATCH for news item to Remove product searches: \'Wahaca\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 38
 testRunner.Then("news item has news analytics searches: \'Wasabi\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("News > Edit News Advanced Message Mentions Analytics > Remove")]
        [NUnit.Framework.CategoryAttribute("publishers")]
        [NUnit.Framework.CategoryAttribute("news")]
        public virtual void NewsEditNewsAdvancedMessageMentionsAnalyticsRemove()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("News > Edit News Advanced Message Mentions Analytics > Remove", new string[] {
                        "publishers"});
#line 41
this.ScenarioSetup(scenarioInfo);
#line 42
 testRunner.Given("session for edition \'Analytics company with features enabled and dynamic news\', p" +
                    "ermission: \'system_admin\', datagroup: \'news\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 43
 testRunner.And("analytics profile \'message\' searches present: \'1_good, 2_bad, 3_awesome\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 44
 testRunner.And("news article exist", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 45
 testRunner.When("I perform a PATCH for news item to Add product searches: \'1_good, 2_bad, 3_awesom" +
                    "e\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 46
 testRunner.Then("news item has news analytics searches: \'1_good, 2_bad, 3_awesome\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 47
 testRunner.When("I perform a PATCH for news item to Remove product searches: \'1_good, 2_bad\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 48
 testRunner.Then("news item has news analytics searches: \'3_awesome\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
