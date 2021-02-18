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
    [NUnit.Framework.DescriptionAttribute("Analytics - Prominence and Impact End Point")]
    [NUnit.Framework.IgnoreAttribute("Ignored feature")]
    [NUnit.Framework.CategoryAttribute("HeartsAndCharts")]
    public partial class Analytics_ProminenceAndImpactEndPointFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "AnalyticsProminenceAndImpactEndPoint.feature"
#line hidden
        
        [NUnit.Framework.OneTimeSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Analytics - Prominence and Impact End Point", "\tTo verify that prominence and impact scores are correct\r\n\tAs a valid CCC user\r\n\t" +
                    "I want to call the analytics prominence and impact endpoint", ProgrammingLanguage.CSharp, new string[] {
                        "HeartsAndCharts",
                        "ignore"});
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
        [NUnit.Framework.DescriptionAttribute("Prominence series data is correct for different frequencies")]
        [NUnit.Framework.TestCaseAttribute("Line", "Average", "None", null)]
        [NUnit.Framework.TestCaseAttribute("Donut", "Average", "None", null)]
        [NUnit.Framework.TestCaseAttribute("HorizontalBar", "Average", "None", null)]
        [NUnit.Framework.TestCaseAttribute("Line", "Total", "None", null)]
        [NUnit.Framework.TestCaseAttribute("Donut", "Total", "None", null)]
        [NUnit.Framework.TestCaseAttribute("HorizontalBar", "Total", "None", null)]
        [NUnit.Framework.TestCaseAttribute("Line", "Average", "Daily", null)]
        [NUnit.Framework.TestCaseAttribute("Donut", "Average", "Daily", null)]
        [NUnit.Framework.TestCaseAttribute("HorizontalBar", "Average", "Daily", null)]
        [NUnit.Framework.TestCaseAttribute("Line", "Total", "Daily", null)]
        [NUnit.Framework.TestCaseAttribute("Donut", "Total", "Daily", null)]
        [NUnit.Framework.TestCaseAttribute("HorizontalBar", "Total", "Daily", null)]
        [NUnit.Framework.TestCaseAttribute("Line", "Average", "Weekly", null)]
        [NUnit.Framework.TestCaseAttribute("Donut", "Average", "Weekly", null)]
        [NUnit.Framework.TestCaseAttribute("HorizontalBar", "Average", "Weekly", null)]
        [NUnit.Framework.TestCaseAttribute("Line", "Total", "Weekly", null)]
        [NUnit.Framework.TestCaseAttribute("Donut", "Total", "Weekly", null)]
        [NUnit.Framework.TestCaseAttribute("HorizontalBar", "Total", "Weekly", null)]
        [NUnit.Framework.TestCaseAttribute("Line", "Average", "Monthly", null)]
        [NUnit.Framework.TestCaseAttribute("Donut", "Average", "Monthly", null)]
        [NUnit.Framework.TestCaseAttribute("HorizontalBar", "Average", "Monthly", null)]
        [NUnit.Framework.TestCaseAttribute("Line", "Total", "Monthly", null)]
        [NUnit.Framework.TestCaseAttribute("Donut", "Total", "Monthly", null)]
        [NUnit.Framework.TestCaseAttribute("HorizontalBar", "Total", "Monthly", null)]
        [NUnit.Framework.TestCaseAttribute("Line", "Average", "Yearly", null)]
        [NUnit.Framework.TestCaseAttribute("Donut", "Average", "Yearly", null)]
        [NUnit.Framework.TestCaseAttribute("HorizontalBar", "Average", "Yearly", null)]
        [NUnit.Framework.TestCaseAttribute("Line", "Total", "Yearly", null)]
        [NUnit.Framework.TestCaseAttribute("Donut", "Total", "Yearly", null)]
        [NUnit.Framework.TestCaseAttribute("HorizontalBar", "Total", "Yearly", null)]
        public virtual void ProminenceSeriesDataIsCorrectForDifferentFrequencies(string type, string yAxisMetric, string frequency, string[] exampleTags)
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Prominence series data is correct for different frequencies", exampleTags);
#line 7
this.ScenarioSetup(scenarioInfo);
#line 8
 testRunner.Given("I login as \'analytics manager\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 9
 testRunner.When(string.Format("I perform a GET for company prominence with type \'{0}\', y axis metric \'{1}\' and f" +
                        "requency \'{2}\'", type, yAxisMetric, frequency), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 10
 testRunner.Then("the company prominence endpoint has series data with total and average", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.CategoryAttribute("analytics")]
        [NUnit.Framework.DescriptionAttribute("Impact series data is correct for different frequencies")]
        [NUnit.Framework.TestCaseAttribute("Line", "Average", "None", null)]
        [NUnit.Framework.TestCaseAttribute("Donut", "Average", "None", null)]
        [NUnit.Framework.TestCaseAttribute("HorizontalBar", "Average", "None", null)]
        [NUnit.Framework.TestCaseAttribute("Line", "Total", "None", null)]
        [NUnit.Framework.TestCaseAttribute("Donut", "Total", "None", null)]
        [NUnit.Framework.TestCaseAttribute("HorizontalBar", "Total", "None", null)]
        [NUnit.Framework.TestCaseAttribute("Line", "Average", "Daily", null)]
        [NUnit.Framework.TestCaseAttribute("Donut", "Average", "Daily", null)]
        [NUnit.Framework.TestCaseAttribute("HorizontalBar", "Average", "Daily", null)]
        [NUnit.Framework.TestCaseAttribute("Line", "Total", "Daily", null)]
        [NUnit.Framework.TestCaseAttribute("Donut", "Total", "Daily", null)]
        [NUnit.Framework.TestCaseAttribute("HorizontalBar", "Total", "Daily", null)]
        [NUnit.Framework.TestCaseAttribute("Line", "Average", "Weekly", null)]
        [NUnit.Framework.TestCaseAttribute("HorizontalBar", "Average", "Weekly", null)]
        [NUnit.Framework.TestCaseAttribute("Donut", "Average", "Weekly", null)]
        [NUnit.Framework.TestCaseAttribute("Line", "Total", "Weekly", null)]
        [NUnit.Framework.TestCaseAttribute("Donut", "Total", "Weekly", null)]
        [NUnit.Framework.TestCaseAttribute("HorizontalBar", "Total", "Weekly", null)]
        [NUnit.Framework.TestCaseAttribute("Line", "Average", "Monthly", null)]
        [NUnit.Framework.TestCaseAttribute("Donut", "Average", "Monthly", null)]
        [NUnit.Framework.TestCaseAttribute("HorizontalBar", "Average", "Weekly", null)]
        [NUnit.Framework.TestCaseAttribute("Line", "Total", "Monthly", null)]
        [NUnit.Framework.TestCaseAttribute("Donut", "Total", "Monthly", null)]
        [NUnit.Framework.TestCaseAttribute("HorizontalBar", "Total", "Weekly", null)]
        [NUnit.Framework.TestCaseAttribute("Line", "Average", "Yearly", null)]
        [NUnit.Framework.TestCaseAttribute("Donut", "Average", "Yearly", null)]
        [NUnit.Framework.TestCaseAttribute("HorizontalBar", "Average", "Weekly", null)]
        [NUnit.Framework.TestCaseAttribute("Line", "Total", "Yearly", null)]
        [NUnit.Framework.TestCaseAttribute("Donut", "Total", "Yearly", null)]
        [NUnit.Framework.TestCaseAttribute("HorizontalBar", "Total", "Weekly", null)]
        public virtual void ImpactSeriesDataIsCorrectForDifferentFrequencies(string type, string yAxisMetric, string frequency, string[] exampleTags)
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Impact series data is correct for different frequencies", exampleTags);
#line 47
this.ScenarioSetup(scenarioInfo);
#line 48
 testRunner.Given("I login as \'analytics manager\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 49
 testRunner.When(string.Format("I perform a GET for company impact with type \'{0}\', y axis metric \'{1}\' and frequ" +
                        "ency \'{2}\'", type, yAxisMetric, frequency), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 50
 testRunner.Then("the company impact endpoint has series data with total and average", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
