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
namespace CCC_API.Features.Common
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.3.2.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("DataGroup")]
    public partial class DataGroupFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "DataGroup.feature"
#line hidden
        
        [NUnit.Framework.OneTimeSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "DataGroup", "\tIn order to separate data in my account\r\n\tAs a standard CCC User\r\n\tI want to man" +
                    "age datagroups", ProgrammingLanguage.CSharp, ((string[])(null)));
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
        [NUnit.Framework.DescriptionAttribute("Change to valid data group")]
        [NUnit.Framework.CategoryAttribute("configuration")]
        public virtual void ChangeToValidDataGroup()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Change to valid data group", new string[] {
                        "acl"});
#line 7
this.ScenarioSetup(scenarioInfo);
#line 8
 testRunner.Given("I login as \'basic\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 9
 testRunner.When("I perform a PUT to change the datagroup", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 10
 testRunner.Then("the active datagroup should be changed", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Change to invalid data group returns 404")]
        [NUnit.Framework.CategoryAttribute("configuration")]
        public virtual void ChangeToInvalidDataGroupReturns404()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Change to invalid data group returns 404", new string[] {
                        "acl"});
#line 13
this.ScenarioSetup(scenarioInfo);
#line 14
 testRunner.Given("shared session for \'standard\' user with edition \'basic\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 15
 testRunner.When("I perform a PUT to change to datagroup \'-1\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 16
 testRunner.Then("the Accounts endpoint response should be \'404\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 17
 testRunner.And("the Accounts endpoint content should be \'{\"IncidentId\":null,\"Message\":\"The reques" +
                    "ted resource (-1) was not found.\",\"ErrorCode\":null,\"ErrorData\":null}\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Change to existing invalid data group returns 404")]
        [NUnit.Framework.CategoryAttribute("configuration")]
        public virtual void ChangeToExistingInvalidDataGroupReturns404()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Change to existing invalid data group returns 404", new string[] {
                        "acl"});
#line 20
this.ScenarioSetup(scenarioInfo);
#line 21
 testRunner.Given("shared session for \'standard\' user with edition \'basic\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 22
 testRunner.When("I perform a PUT to change to datagroup \'13979293\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 23
 testRunner.Then("the Accounts endpoint response should be \'404\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 24
 testRunner.And("the Accounts endpoint content should be \'{\"IncidentId\":null,\"Message\":\"The reques" +
                    "ted resource (13979293) was not found.\",\"ErrorCode\":null,\"ErrorData\":null}\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Data groups are returned in alphabetical order")]
        [NUnit.Framework.CategoryAttribute("configuration")]
        public virtual void DataGroupsAreReturnedInAlphabeticalOrder()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Data groups are returned in alphabetical order", new string[] {
                        "acl"});
#line 27
this.ScenarioSetup(scenarioInfo);
#line 28
 testRunner.Given("I login as \'basic\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 29
 testRunner.When("I get all datagroups for the user", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 30
 testRunner.Then("the datagroups should be in alphabetical order", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Get data groups with invalid company id returns 403")]
        [NUnit.Framework.CategoryAttribute("configuration")]
        public virtual void GetDataGroupsWithInvalidCompanyIdReturns403()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Get data groups with invalid company id returns 403", new string[] {
                        "herdOfGnus"});
#line 33
this.ScenarioSetup(scenarioInfo);
#line 34
 testRunner.Given("shared session for \'standard\' user with edition \'basic\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 35
 testRunner.When("I get all datagroups for the user with company id \'99999999\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 36
 testRunner.Then("the Accounts endpoint response should be \'403\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("ALL datagroup is not returned as a valid datagroup")]
        [NUnit.Framework.CategoryAttribute("configuration")]
        public virtual void ALLDatagroupIsNotReturnedAsAValidDatagroup()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("ALL datagroup is not returned as a valid datagroup", new string[] {
                        "acl"});
#line 39
this.ScenarioSetup(scenarioInfo);
#line 40
 testRunner.Given("I login as \'ESAManager\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 41
 testRunner.Then("the \'(ALL)\' datagroup is not returned in list of datagroups", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion

