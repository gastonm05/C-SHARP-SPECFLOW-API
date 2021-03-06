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
    [NUnit.Framework.DescriptionAttribute("NewsForwards")]
    public partial class NewsForwardsFeature
    {

        private TechTalk.SpecFlow.ITestRunner testRunner;

#line 1 "NewsForwards.feature"
#line hidden

        [NUnit.Framework.OneTimeSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "NewsForwards", "\tIn order to forward news\r\n\tAs a valid C3 User\r\n\tI want to be able to use themed " +
                    "templates", ProgrammingLanguage.CSharp, ((string[])(null)));
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
        [NUnit.Framework.DescriptionAttribute("Should not be able to forward another company\'s template")]
        [NUnit.Framework.IgnoreAttribute("Ignored scenario")]
        [NUnit.Framework.CategoryAttribute("herdOfGnus")]
        [NUnit.Framework.CategoryAttribute("news")]
        public virtual void ShouldNotBeAbleToForwardAnotherCompanysTemplate()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Should not be able to forward another company\'s template", new string[] {
                        "herdOfGnus",
                        "ignore"});
#line 7
            this.ScenarioSetup(scenarioInfo);
#line 8
            testRunner.Given("I login as \'CCC1481\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 9
            testRunner.When("I perform a GET for all news", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 10
            testRunner.And("I POST to News Forwards endpoint with another Company template", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 11
            testRunner.Then("the News Forward endpoint should respond with a \'400\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 12
            testRunner.And("the response message should be \'does not exist\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }

        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("User can forward news")]
        [NUnit.Framework.IgnoreAttribute("Ignored scenario")]
        [NUnit.Framework.CategoryAttribute("herdOfGnus")]
        [NUnit.Framework.CategoryAttribute("news")]
        [NUnit.Framework.CategoryAttribute("smokeProd")]
        public virtual void UserCanForwardNews()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("User can forward news", new string[] {
                        "herdOfGnus",
                        "ignore"});
#line 15
            this.ScenarioSetup(scenarioInfo);
#line 16
            testRunner.Given("I login as \'CCC1481\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 17
            testRunner.When("I perform a GET for all news", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 18
            testRunner.And("I POST to News Forwards endpoint with all available fields", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 19
            testRunner.Then("the News Forward endpoint should respond with a \'202\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion

