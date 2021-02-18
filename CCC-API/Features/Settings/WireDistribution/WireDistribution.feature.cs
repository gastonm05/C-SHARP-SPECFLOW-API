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
namespace CCC_API.Features.Settings.WireDistribution
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.3.2.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("WireDistribution")]
    public partial class WireDistributionFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "WireDistribution.feature"
#line hidden
        
        [NUnit.Framework.OneTimeSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "WireDistribution", @"	To verify that WireDistribution Settings can be retrieved and modified
	As a valid C3 user from a company with parameter PressReleaseImpactEnabled set to true
	I want to call the WireDisribution management endpoint - api/v1/management/wiredistributionaccount", ProgrammingLanguage.CSharp, ((string[])(null)));
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
        [NUnit.Framework.DescriptionAttribute("a Manager C3 user get current WireDistribution configuration for a Impact enabled" +
            " company")]
        [NUnit.Framework.IgnoreAttribute("Ignored scenario")]
        [NUnit.Framework.CategoryAttribute("acl")]
        [NUnit.Framework.CategoryAttribute("WireDistribution")]
        public virtual void AManagerC3UserGetCurrentWireDistributionConfigurationForAImpactEnabledCompany()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("a Manager C3 user get current WireDistribution configuration for a Impact enabled" +
                    " company", new string[] {
                        "acl",
                        "WireDistribution",
                        "Ignore"});
#line 7
this.ScenarioSetup(scenarioInfo);
#line 8
 testRunner.Given("I login as \'Press Release Impact Enabled Company - Manager\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 9
 testRunner.When("I get current WireDistribution configuration", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 10
 testRunner.Then("the response returns a valid WireDistribution configuration", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("a AE C3 user get current WireDistribution configuration for a Impact enabled comp" +
            "any")]
        [NUnit.Framework.IgnoreAttribute("Ignored scenario")]
        [NUnit.Framework.CategoryAttribute("acl")]
        [NUnit.Framework.CategoryAttribute("WireDistribution")]
        public virtual void AAEC3UserGetCurrentWireDistributionConfigurationForAImpactEnabledCompany()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("a AE C3 user get current WireDistribution configuration for a Impact enabled comp" +
                    "any", new string[] {
                        "acl",
                        "WireDistribution",
                        "Ignore"});
#line 13
this.ScenarioSetup(scenarioInfo);
#line 14
 testRunner.Given("I login as \'Press Release Impact Enabled Company - AE\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 15
 testRunner.When("I get current WireDistribution configuration", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 16
 testRunner.Then("the response returns a valid WireDistribution configuration", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("a Manager C3 user get current WireDistribution configuration for a Impact disable" +
            "d company")]
        [NUnit.Framework.IgnoreAttribute("Ignored scenario")]
        [NUnit.Framework.CategoryAttribute("acl")]
        [NUnit.Framework.CategoryAttribute("WireDistribution")]
        public virtual void AManagerC3UserGetCurrentWireDistributionConfigurationForAImpactDisabledCompany()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("a Manager C3 user get current WireDistribution configuration for a Impact disable" +
                    "d company", new string[] {
                        "acl",
                        "WireDistribution",
                        "Ignore"});
#line 19
this.ScenarioSetup(scenarioInfo);
#line 20
 testRunner.Given("I login as \'Smart Tag ON Company\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 21
 testRunner.When("I get current WireDistribution configuration", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 22
 testRunner.Then("the response returns a \'Access Denied\' message", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("a Standard C3 user get current WireDistribution configuration for a Impact enable" +
            "d company")]
        [NUnit.Framework.IgnoreAttribute("Ignored scenario")]
        [NUnit.Framework.CategoryAttribute("acl")]
        [NUnit.Framework.CategoryAttribute("WireDistribution")]
        public virtual void AStandardC3UserGetCurrentWireDistributionConfigurationForAImpactEnabledCompany()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("a Standard C3 user get current WireDistribution configuration for a Impact enable" +
                    "d company", new string[] {
                        "acl",
                        "WireDistribution",
                        "Ignore"});
#line 25
this.ScenarioSetup(scenarioInfo);
#line 26
 testRunner.Given("I login as \'Press Release Impact Enabled Company - Standard\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 27
 testRunner.When("I get current WireDistribution configuration", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 28
 testRunner.Then("the response returns a \'Access Denied\' message", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("a ReadOnly C3 user get current WireDistribution configuration for a Impact enable" +
            "d company")]
        [NUnit.Framework.IgnoreAttribute("Ignored scenario")]
        [NUnit.Framework.CategoryAttribute("acl")]
        [NUnit.Framework.CategoryAttribute("WireDistribution")]
        public virtual void AReadOnlyC3UserGetCurrentWireDistributionConfigurationForAImpactEnabledCompany()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("a ReadOnly C3 user get current WireDistribution configuration for a Impact enable" +
                    "d company", new string[] {
                        "acl",
                        "WireDistribution",
                        "Ignore"});
#line 31
this.ScenarioSetup(scenarioInfo);
#line 32
 testRunner.Given("I login as \'Press Release Impact Enabled Company - ReadOnly\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 33
 testRunner.When("I get current WireDistribution configuration", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 34
 testRunner.Then("the response returns a \'Access Denied\' message", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("a SysAdmin C3 user get current WireDistribution configuration for a Impact enable" +
            "d company")]
        [NUnit.Framework.IgnoreAttribute("Ignored scenario")]
        [NUnit.Framework.CategoryAttribute("acl")]
        [NUnit.Framework.CategoryAttribute("WireDistribution")]
        public virtual void ASysAdminC3UserGetCurrentWireDistributionConfigurationForAImpactEnabledCompany()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("a SysAdmin C3 user get current WireDistribution configuration for a Impact enable" +
                    "d company", new string[] {
                        "acl",
                        "WireDistribution",
                        "Ignore"});
#line 36
this.ScenarioSetup(scenarioInfo);
#line 37
 testRunner.Given("I login as \'Press Release Impact Enabled Company - SysAdmin\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 38
 testRunner.When("I get current WireDistribution configuration", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 39
 testRunner.Then("the response returns a \'Access Denied\' message", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("a Manager C3 user set multiple Oracle IDs, one for each DataGroup for his WireDis" +
            "tribution configuration for a Impact enabled company")]
        [NUnit.Framework.IgnoreAttribute("Ignored scenario")]
        [NUnit.Framework.CategoryAttribute("acl")]
        [NUnit.Framework.CategoryAttribute("WireDistribution")]
        [NUnit.Framework.CategoryAttribute("NeedsCleanupWireDistribution")]
        public virtual void AManagerC3UserSetMultipleOracleIDsOneForEachDataGroupForHisWireDistributionConfigurationForAImpactEnabledCompany()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("a Manager C3 user set multiple Oracle IDs, one for each DataGroup for his WireDis" +
                    "tribution configuration for a Impact enabled company", new string[] {
                        "acl",
                        "WireDistribution",
                        "NeedsCleanupWireDistribution",
                        "Ignore"});
#line 42
this.ScenarioSetup(scenarioInfo);
#line 43
 testRunner.Given("I login as \'Press Release Impact Enabled Company - Manager\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 44
 testRunner.When("I get current WireDistribution configuration", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 45
 testRunner.And("I set WireDistribution configuration with multiple Oracle IDs, one for each DataG" +
                    "roup", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 46
 testRunner.Then("I should see the proper WireDistribution response", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("a Manager C3 user set only one Oracle ID for all DataGroup for his WireDistributi" +
            "on configuration for a Impact enabled company")]
        [NUnit.Framework.IgnoreAttribute("Ignored scenario")]
        [NUnit.Framework.CategoryAttribute("acl")]
        [NUnit.Framework.CategoryAttribute("WireDistribution")]
        public virtual void AManagerC3UserSetOnlyOneOracleIDForAllDataGroupForHisWireDistributionConfigurationForAImpactEnabledCompany()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("a Manager C3 user set only one Oracle ID for all DataGroup for his WireDistributi" +
                    "on configuration for a Impact enabled company", new string[] {
                        "acl",
                        "WireDistribution",
                        "Ignore"});
#line 49
this.ScenarioSetup(scenarioInfo);
#line 50
 testRunner.Given("I login as \'Press Release Impact Enabled Company - Manager\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 51
 testRunner.When("I get current WireDistribution configuration", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 52
 testRunner.And("I set WireDistribution configuration with multiple Oracle IDs, one for each DataG" +
                    "roup", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 53
 testRunner.And("I set WireDistribution configuration with only one Oracle ID for all DataGroup", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 54
 testRunner.Then("I should see the proper WireDistribution response", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Manager C3 user set only one Oracle ID for all DataGroup for his WireDistribution" +
            " configuration for a Impact enabled company with View all for all Datagroup")]
        [NUnit.Framework.IgnoreAttribute("Ignored scenario")]
        [NUnit.Framework.CategoryAttribute("acl")]
        [NUnit.Framework.CategoryAttribute("WireDistribution")]
        public virtual void ManagerC3UserSetOnlyOneOracleIDForAllDataGroupForHisWireDistributionConfigurationForAImpactEnabledCompanyWithViewAllForAllDatagroup()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Manager C3 user set only one Oracle ID for all DataGroup for his WireDistribution" +
                    " configuration for a Impact enabled company with View all for all Datagroup", new string[] {
                        "acl",
                        "WireDistribution",
                        "Ignore"});
#line 57
this.ScenarioSetup(scenarioInfo);
#line 58
 testRunner.Given("I login as \'Press Release Impact Enabled Company - Manager\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 59
 testRunner.When("I get current WireDistribution configuration", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 60
 testRunner.And("I set WireDistribution configuration with only one Oracle ID for all DataGroup wi" +
                    "th View All for all data groups", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 61
 testRunner.Then("I should see the proper WireDistribution response", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("a Manager C3 user set Default(today) date as Impact Start Date for his WireDistri" +
            "bution configuration for a Impact enabled company")]
        [NUnit.Framework.IgnoreAttribute("Ignored scenario")]
        [NUnit.Framework.CategoryAttribute("acl")]
        [NUnit.Framework.CategoryAttribute("WireDistribution")]
        [NUnit.Framework.CategoryAttribute("NeedsCleanupWireDistribution")]
        public virtual void AManagerC3UserSetDefaultTodayDateAsImpactStartDateForHisWireDistributionConfigurationForAImpactEnabledCompany()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("a Manager C3 user set Default(today) date as Impact Start Date for his WireDistri" +
                    "bution configuration for a Impact enabled company", new string[] {
                        "acl",
                        "WireDistribution",
                        "NeedsCleanupWireDistribution",
                        "Ignore"});
#line 64
this.ScenarioSetup(scenarioInfo);
#line 65
 testRunner.Given("I login as \'Press Release Impact Enabled Company - Manager\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 66
 testRunner.When("I get current WireDistribution configuration", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 67
 testRunner.And("I set WireDistribution configuration with only one Oracle ID for all DataGroup wi" +
                    "th View All for all data groups and \'DEFAULT\' date as Impact Start Date", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 68
 testRunner.Then("I should see the proper WireDistribution response for ImpactStartDateDefaultEnabl" +
                    "ed property which should be \'true\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("a Manager C3 user set 90 Days prior date as Impact Start Date for his WireDistrib" +
            "ution configuration for a Impact enabled company")]
        [NUnit.Framework.IgnoreAttribute("Ignored scenario")]
        [NUnit.Framework.CategoryAttribute("acl")]
        [NUnit.Framework.CategoryAttribute("WireDistribution")]
        [NUnit.Framework.CategoryAttribute("NeedsCleanupWireDistribution")]
        public virtual void AManagerC3UserSet90DaysPriorDateAsImpactStartDateForHisWireDistributionConfigurationForAImpactEnabledCompany()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("a Manager C3 user set 90 Days prior date as Impact Start Date for his WireDistrib" +
                    "ution configuration for a Impact enabled company", new string[] {
                        "acl",
                        "WireDistribution",
                        "NeedsCleanupWireDistribution",
                        "Ignore"});
#line 71
this.ScenarioSetup(scenarioInfo);
#line 72
 testRunner.Given("I login as \'Press Release Impact Enabled Company - Manager\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 73
 testRunner.When("I get current WireDistribution configuration", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 74
 testRunner.And("I set WireDistribution configuration with only one Oracle ID for all DataGroup wi" +
                    "th View All for all data groups and \'NINETYDAYS\' date as Impact Start Date", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 75
 testRunner.Then("I should see the proper WireDistribution response for ImpactStartDateDefaultEnabl" +
                    "ed property which should be \'false\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("a Manager C3 user with WireDistribution configuration set to no Company Wire Acco" +
            "unt ID and has a Impact enabled DG set to use Data Group ID should have OMC SSO " +
            "if that DG is selected.")]
        [NUnit.Framework.IgnoreAttribute("Ignored scenario")]
        [NUnit.Framework.CategoryAttribute("acl")]
        [NUnit.Framework.CategoryAttribute("WireDistribution")]
        public virtual void AManagerC3UserWithWireDistributionConfigurationSetToNoCompanyWireAccountIDAndHasAImpactEnabledDGSetToUseDataGroupIDShouldHaveOMCSSOIfThatDGIsSelected_()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("a Manager C3 user with WireDistribution configuration set to no Company Wire Acco" +
                    "unt ID and has a Impact enabled DG set to use Data Group ID should have OMC SSO " +
                    "if that DG is selected.", new string[] {
                        "acl",
                        "WireDistribution",
                        "Ignore"});
#line 78
this.ScenarioSetup(scenarioInfo);
#line 79
 testRunner.Given("I login as \'Impact Enabled Company with No Company id or DataGroup id set - Manag" +
                    "er - DGLevelAccountID DG Selected\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 80
 testRunner.When("I perform a GET to verify the token", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 81
 testRunner.Then("the token should be valid and it shouldn\'t return an empty AccountID", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("a Manager C3 user with WireDistribution configuration set to no Company Wire Acco" +
            "unt ID and has a Impact enabled DG set to use Data Group ID should NOT have OMC " +
            "SSO if that DG is not selected.")]
        [NUnit.Framework.IgnoreAttribute("Ignored scenario")]
        [NUnit.Framework.CategoryAttribute("acl")]
        [NUnit.Framework.CategoryAttribute("WireDistribution")]
        public virtual void AManagerC3UserWithWireDistributionConfigurationSetToNoCompanyWireAccountIDAndHasAImpactEnabledDGSetToUseDataGroupIDShouldNOTHaveOMCSSOIfThatDGIsNotSelected_()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("a Manager C3 user with WireDistribution configuration set to no Company Wire Acco" +
                    "unt ID and has a Impact enabled DG set to use Data Group ID should NOT have OMC " +
                    "SSO if that DG is not selected.", new string[] {
                        "acl",
                        "WireDistribution",
                        "Ignore"});
#line 84
this.ScenarioSetup(scenarioInfo);
#line 85
 testRunner.Given("I login as \'Impact Enabled Company with No Company id or DataGroup id set - Manag" +
                    "er\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 86
 testRunner.When("I perform a GET to verify the token", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 87
 testRunner.Then("the token should be valid and return an empty AccountID", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion

