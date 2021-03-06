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
namespace CCC_API.Features.Impact
{
    using TechTalk.SpecFlow;


    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.3.2.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("EarnedAnalytics")]
    [NUnit.Framework.CategoryAttribute("impact")]
    public partial class EarnedAnalyticsFeature
    {

        private TechTalk.SpecFlow.ITestRunner testRunner;

#line 1 "EarnedAnalytics.feature"
#line hidden

        [NUnit.Framework.OneTimeSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "EarnedAnalytics", "\tTo verify that earned analytics retrieve data per chart\r\n\tAs a valid CCC user\r\n\t" +
                    "I want to call the earned-analytics endpoints", ProgrammingLanguage.CSharp, new string[] {
                        "impact"});
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
        [NUnit.Framework.DescriptionAttribute("Earned Views data is correct on analytics view")]
        [NUnit.Framework.TestCaseAttribute("90", null)]
        [NUnit.Framework.TestCaseAttribute("30", null)]
        [NUnit.Framework.TestCaseAttribute("7", null)]
        public virtual void EarnedViewsDataIsCorrectOnAnalyticsView(string days, string[] exampleTags)
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Earned Views data is correct on analytics view", exampleTags);
#line 7
            this.ScenarioSetup(scenarioInfo);
#line 8
            testRunner.Given("I login as \'Impact Enabled Company\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 9
            testRunner.And("I call the search endpoint in order to get the search id", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 10
            testRunner.When(string.Format("I call the earned views endpoint on the last {0} days", days), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 11
            testRunner.Then("the earned views endpoint has the correct response", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }

        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Earned WebEvents data is correct on analytics view")]
        [NUnit.Framework.TestCaseAttribute("90", null)]
        [NUnit.Framework.TestCaseAttribute("30", null)]
        [NUnit.Framework.TestCaseAttribute("7", null)]
        public virtual void EarnedWebEventsDataIsCorrectOnAnalyticsView(string days, string[] exampleTags)
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Earned WebEvents data is correct on analytics view", exampleTags);
#line 19
            this.ScenarioSetup(scenarioInfo);
#line 20
            testRunner.Given("I login as \'Impact Enabled Company\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 21
            testRunner.And("I call the search endpoint in order to get the search id", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 22
            testRunner.When(string.Format("I call the earned WebEvents endpoint on the last {0} days", days), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 23
            testRunner.Then("the earned WebEvents endpoint has the correct response", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }

        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Earned Audience data is correct on analytics view")]
        [NUnit.Framework.TestCaseAttribute("90", null)]
        [NUnit.Framework.TestCaseAttribute("30", null)]
        [NUnit.Framework.TestCaseAttribute("7", null)]
        public virtual void EarnedAudienceDataIsCorrectOnAnalyticsView(string days, string[] exampleTags)
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Earned Audience data is correct on analytics view", exampleTags);
#line 31
            this.ScenarioSetup(scenarioInfo);
#line 32
            testRunner.Given("I login as \'Impact Enabled Company\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 33
            testRunner.And("I call the search endpoint in order to get the search id", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 34
            testRunner.When(string.Format("I call the earned Audience endpoint on the last {0} days", days), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 35
            testRunner.Then("the earned Audience endpoint has the correct response", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }

        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Earned TopOutlet data is correct on analytics view")]
        [NUnit.Framework.TestCaseAttribute("90", null)]
        [NUnit.Framework.TestCaseAttribute("30", null)]
        [NUnit.Framework.TestCaseAttribute("7", null)]
        public virtual void EarnedTopOutletDataIsCorrectOnAnalyticsView(string days, string[] exampleTags)
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Earned TopOutlet data is correct on analytics view", exampleTags);
#line 43
            this.ScenarioSetup(scenarioInfo);
#line 44
            testRunner.Given("I login as \'Impact Enabled Company\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 45
            testRunner.And("I call the search endpoint in order to get the search id", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 46
            testRunner.When(string.Format("I call the earned TopOutlet endpoint on the last {0} days", days), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 47
            testRunner.Then("the earned TopOutlet endpoint has the correct response", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
