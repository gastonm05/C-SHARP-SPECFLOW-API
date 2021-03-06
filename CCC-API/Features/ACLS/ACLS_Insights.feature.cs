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
namespace CCC_API.Features.ACLS
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.3.2.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("Insights - ACLS Endpoint")]
    public partial class Insights_ACLSEndpointFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "ACLS_Insights.feature"
#line hidden
        
        [NUnit.Framework.OneTimeSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Insights - ACLS Endpoint", "\tTo verify that a list of permissions retrieved for Insights\r\n\tAs a valid CCC use" +
                    "r\r\n\tI want to call the ACLS endpoint", ProgrammingLanguage.CSharp, ((string[])(null)));
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
        [NUnit.Framework.DescriptionAttribute("A company with ImageIQNavEnabled param false cannot access Insights - ImageIQ")]
        [NUnit.Framework.IgnoreAttribute("Ignored scenario")]
        [NUnit.Framework.CategoryAttribute("acl")]
        public virtual void ACompanyWithImageIQNavEnabledParamFalseCannotAccessInsights_ImageIQ()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("A company with ImageIQNavEnabled param false cannot access Insights - ImageIQ", new string[] {
                        "acl",
                        "Ignore"});
#line 7
this.ScenarioSetup(scenarioInfo);
#line 8
 testRunner.Given("I login as \'PRWebElysiumCompany4\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 9
 testRunner.When("I perform a GET ACLS permissions", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 10
 testRunner.Then("ACLS Endpoint response should be \'200\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                        "property",
                        "subProperty",
                        "subPropertyOther",
                        "permission",
                        "value"});
            table1.AddRow(new string[] {
                        "ImageIQ",
                        "Access",
                        "",
                        "IsGranted",
                        "False"});
            table1.AddRow(new string[] {
                        "ImageIQ",
                        "Access",
                        "",
                        "Status",
                        "Feature Not Enabled"});
            table1.AddRow(new string[] {
                        "ImageIQ",
                        "Access",
                        "",
                        "StatusCode",
                        "2"});
#line 11
 testRunner.And("ACLS permissions for Insights should be:", ((string)(null)), table1, "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("A company with ImageIQNavEnabled param true can access Insights - ImageIQ")]
        [NUnit.Framework.IgnoreAttribute("Ignored scenario")]
        [NUnit.Framework.CategoryAttribute("acl")]
        public virtual void ACompanyWithImageIQNavEnabledParamTrueCanAccessInsights_ImageIQ()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("A company with ImageIQNavEnabled param true can access Insights - ImageIQ", new string[] {
                        "acl",
                        "Ignore"});
#line 18
this.ScenarioSetup(scenarioInfo);
#line 19
 testRunner.Given("I login as \'ImageIQNavEnabled param True - Manager\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 20
 testRunner.When("I perform a GET ACLS permissions", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 21
 testRunner.Then("ACLS Endpoint response should be \'200\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                        "property",
                        "subProperty",
                        "subPropertyOther",
                        "permission",
                        "value"});
            table2.AddRow(new string[] {
                        "ImageIQ",
                        "Access",
                        "",
                        "IsGranted",
                        "True"});
            table2.AddRow(new string[] {
                        "ImageIQ",
                        "Access",
                        "",
                        "Status",
                        "Access Granted"});
            table2.AddRow(new string[] {
                        "ImageIQ",
                        "Access",
                        "",
                        "StatusCode",
                        "0"});
#line 22
 testRunner.And("ACLS permissions for Insights should be:", ((string)(null)), table2, "And ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion

