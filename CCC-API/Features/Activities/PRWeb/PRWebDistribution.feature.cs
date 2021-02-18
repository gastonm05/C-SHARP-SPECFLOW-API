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
namespace CCC_API.Features.Activities.PRWeb
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.3.2.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("PRWebDistribution")]
    [NUnit.Framework.CategoryAttribute("PRWeb")]
    public partial class PRWebDistributionFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "PRWebDistribution.feature"
#line hidden
        
        [NUnit.Framework.OneTimeSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "PRWebDistribution", "\tIn order to use the CCC application\r\n\tAs a PRWeb user\r\n\tI need to verify the end" +
                    "points works", ProgrammingLanguage.CSharp, new string[] {
                        "PRWeb"});
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
        [NUnit.Framework.DescriptionAttribute("Save Draft Distribution Test")]
        public virtual void SaveDraftDistributionTest()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Save Draft Distribution Test", ((string[])(null)));
#line 7
this.ScenarioSetup(scenarioInfo);
#line 8
 testRunner.Given("the API test data \'PRWebDistribution.json\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 9
 testRunner.And("I login as \'prweb\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 10
 testRunner.When("I call save draft distribution", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 11
 testRunner.Then("The distribution is in the listing data", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 12
 testRunner.Then("Delete created distribution from Databases", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Is Limited Distribution Approval Test")]
        public virtual void IsLimitedDistributionApprovalTest()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Is Limited Distribution Approval Test", ((string[])(null)));
#line 14
this.ScenarioSetup(scenarioInfo);
#line 15
 testRunner.Given("the API test data \'PRWebDistribution.json\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 16
 testRunner.And("I login as \'prweb\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 17
 testRunner.When("I call send distribution and set status to \'PendingDistributionUponUserApproval\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 18
 testRunner.Then("The distribution status is Pending Distribution Upon User Approval", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 19
 testRunner.Then("Delete created distribution from Databases", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("On Hold Reasons Test")]
        public virtual void OnHoldReasonsTest()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("On Hold Reasons Test", ((string[])(null)));
#line 21
this.ScenarioSetup(scenarioInfo);
#line 22
 testRunner.Given("the API test data \'OnHoldReason.json\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 23
 testRunner.And("I login as \'PRWebOnHoldReasonsTest\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 24
 testRunner.When("I call the On hold Reason Endpoint", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 25
 testRunner.Then("The on hold reason match as expected", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("PRWeb Create Distribution Test With Video")]
        [NUnit.Framework.TestCaseAttribute("https://youtu.be/EzKImzjwGyM", null)]
        [NUnit.Framework.TestCaseAttribute("https://vimeo.com/6370469", null)]
        public virtual void PRWebCreateDistributionTestWithVideo(string videoUrl, string[] exampleTags)
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("PRWeb Create Distribution Test With Video", exampleTags);
#line 27
this.ScenarioSetup(scenarioInfo);
#line 28
 testRunner.Given("the API test data \'PRWebDistributionWithVideo.json\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 29
 testRunner.And("I login as \'prweb\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 30
 testRunner.When(string.Format("I call send new distribution with video \'{0}\'", videoUrl), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 31
 testRunner.Then("The distribution is in the listing data", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 32
 testRunner.Then("Delete created distribution from Databases", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Verify endpoint prweb distribution preview returns the proper info")]
        public virtual void VerifyEndpointPrwebDistributionPreviewReturnsTheProperInfo()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Verify endpoint prweb distribution preview returns the proper info", ((string[])(null)));
#line 39
this.ScenarioSetup(scenarioInfo);
#line 40
 testRunner.Given("the API test data \'PRWebDistribution.json\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 41
 testRunner.And("I login as \'prweb\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 42
 testRunner.When("I call get distribution preview", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 43
 testRunner.Then("The preview info match the provided data", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("PRWeb Verify the IDL get added in to the distribution and storage in the DB")]
        public virtual void PRWebVerifyTheIDLGetAddedInToTheDistributionAndStorageInTheDB()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("PRWeb Verify the IDL get added in to the distribution and storage in the DB", ((string[])(null)));
#line 45
this.ScenarioSetup(scenarioInfo);
#line 46
 testRunner.Given("the API test data \'PRWebDistributionWithIdl.json\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 47
 testRunner.And("I login as \'getDistributionIdl\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 48
 testRunner.When("I call send distribution", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 49
 testRunner.Then("The IDL is in the response and added to the DB", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 50
 testRunner.Then("Delete created distribution from Databases", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("PRWeb Get Chart Impression Test")]
        public virtual void PRWebGetChartImpressionTest()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("PRWeb Get Chart Impression Test", ((string[])(null)));
#line 52
this.ScenarioSetup(scenarioInfo);
#line 53
 testRunner.Given("the API test data \'AnalyticSentDistribution.json\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 54
 testRunner.And("I login as \'PRWebElysiumCompany1\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 55
 testRunner.When("I clean and update count for release", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 56
 testRunner.And("I call to API headlines impressions", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 57
 testRunner.And("I call to API full release reads", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 58
 testRunner.Then("The news Aggregator and prweb impressions match to headlines impressions", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 59
 testRunner.And("The count read match the full release reads", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("PRWeb Back To Draft Test")]
        public virtual void PRWebBackToDraftTest()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("PRWeb Back To Draft Test", ((string[])(null)));
#line 61
this.ScenarioSetup(scenarioInfo);
#line 62
 testRunner.Given("the API test data \'PRWebDistribution.json\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 63
 testRunner.And("I login as \'prweb\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 64
 testRunner.When("I call send distribution and set status to \'InEditorialReview\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 65
 testRunner.Then("I call distribution back to draft", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 66
 testRunner.And("Delete created distribution from Databases", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("PRWeb Get Analytics Online Pickup Totals")]
        public virtual void PRWebGetAnalyticsOnlinePickupTotals()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("PRWeb Get Analytics Online Pickup Totals", ((string[])(null)));
#line 68
this.ScenarioSetup(scenarioInfo);
#line 69
 testRunner.Given("the API test data \'AnalyticsSentDistributionOnlinepickup.json\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 70
 testRunner.And("I login as \'PRWebElysiumCompany1\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 71
 testRunner.When("I call online pickup analitycs", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 72
 testRunner.Then("I get all the online pickup and potencial audience in a descending order", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("PRWeb Verify the attachments get added in to the distribution")]
        public virtual void PRWebVerifyTheAttachmentsGetAddedInToTheDistribution()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("PRWeb Verify the attachments get added in to the distribution", ((string[])(null)));
#line 74
this.ScenarioSetup(scenarioInfo);
#line 75
 testRunner.Given("the API test data \'PRWebDistributionImageAttach.json\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 76
 testRunner.And("I login as \'prweb\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 77
 testRunner.When("I call send distribution", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 78
 testRunner.Then("The values of the attachments are in the reponse", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 79
 testRunner.And("Delete created distribution from Databases", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("PRWeb Create Distribution Test")]
        public virtual void PRWebCreateDistributionTest()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("PRWeb Create Distribution Test", ((string[])(null)));
#line 81
this.ScenarioSetup(scenarioInfo);
#line 82
 testRunner.Given("the API test data \'PRWebDistribution.json\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 83
 testRunner.And("I login as \'prweb\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 84
 testRunner.When("I call send distribution", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 85
 testRunner.Then("I see all the values in the response", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 86
 testRunner.And("Delete created distribution from Databases", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("PRWeb Create Distributions And Check Sort Order For Activities Grid")]
        public virtual void PRWebCreateDistributionsAndCheckSortOrderForActivitiesGrid()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("PRWeb Create Distributions And Check Sort Order For Activities Grid", ((string[])(null)));
#line 88
this.ScenarioSetup(scenarioInfo);
#line 89
 testRunner.Given("the API test data \'PRWebDistribution.json\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 90
 testRunner.And("I login as \'prweb10\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 91
 testRunner.When("I send multiple distribution and set publish activity status", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 92
 testRunner.Then("I see the distribution are listed in order", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 93
 testRunner.And("Delete multiple distributions from database", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Save Distribution With OAuths Test")]
        public virtual void SaveDistributionWithOAuthsTest()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Save Distribution With OAuths Test", ((string[])(null)));
#line 95
this.ScenarioSetup(scenarioInfo);
#line 96
 testRunner.Given("the API test data \'PRWebDistribution.json\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 97
 testRunner.And("I login as \'prweb\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 98
 testRunner.When("I call save draft distribution", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 99
 testRunner.Then("The OAuth values are in the response", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 100
 testRunner.Then("Delete created distribution from Databases", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Save Draft Distribution and persist data")]
        public virtual void SaveDraftDistributionAndPersistData()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Save Draft Distribution and persist data", ((string[])(null)));
#line 102
this.ScenarioSetup(scenarioInfo);
#line 103
 testRunner.Given("the API test data \'PRWebDistribution.json\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 104
 testRunner.And("I login as \'prweb\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 105
 testRunner.When("I call save draft distribution", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 106
 testRunner.Then("I see all the values in the response", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 107
 testRunner.And("All data is saved in the release", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 108
 testRunner.And("Delete created distribution from Databases", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("PRWeb Create Distribution With RetireEcomOptions2017 param Test")]
        public virtual void PRWebCreateDistributionWithRetireEcomOptions2017ParamTest()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("PRWeb Create Distribution With RetireEcomOptions2017 param Test", ((string[])(null)));
#line 110
this.ScenarioSetup(scenarioInfo);
#line 111
 testRunner.Given("the API test data \'PRWebDistribution.json\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 112
 testRunner.And("I login as \'prweb with RetireEcomOptions2017 enabled\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 113
 testRunner.When("I call send distribution", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 114
 testRunner.Then("I see all the values in the response", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 115
 testRunner.And("Delete created distribution from Databases", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("PRWeb Create Distribution With PullOutQuote")]
        public virtual void PRWebCreateDistributionWithPullOutQuote()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("PRWeb Create Distribution With PullOutQuote", ((string[])(null)));
#line 117
this.ScenarioSetup(scenarioInfo);
#line 118
 testRunner.Given("the API test data \'PRWebDistributionWithPullOutQuote.json\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 119
 testRunner.And("I login as \'prweb\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 120
 testRunner.When("I call send distribution", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 121
 testRunner.Then("Then Pull Out Quote Will Have Been Saved", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 122
 testRunner.Then("Delete created distribution from Databases", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("PRWeb Verify SendToIris returns true value for Distribution")]
        public virtual void PRWebVerifySendToIrisReturnsTrueValueForDistribution()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("PRWeb Verify SendToIris returns true value for Distribution", ((string[])(null)));
#line 124
this.ScenarioSetup(scenarioInfo);
#line 125
 testRunner.Given("the API test data \'PRWebDistributionIdWithSendToIris.json\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 126
 testRunner.And("I login as \'prweb21\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 127
 testRunner.When("I call is send to Iris", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 128
 testRunner.Then("I see a true response", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("PRWeb Verify SendToIris returns false value for Distribution")]
        public virtual void PRWebVerifySendToIrisReturnsFalseValueForDistribution()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("PRWeb Verify SendToIris returns false value for Distribution", ((string[])(null)));
#line 130
this.ScenarioSetup(scenarioInfo);
#line 131
 testRunner.Given("the API test data \'PRWebDistributionIdWithoutSendToIris.json\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 132
 testRunner.And("I login as \'prweb21\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 133
 testRunner.When("I call is send to Iris", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 134
 testRunner.Then("I see a false response", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("PRWeb Verify Subscription is SendToIris")]
        public virtual void PRWebVerifySubscriptionIsSendToIris()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("PRWeb Verify Subscription is SendToIris", ((string[])(null)));
#line 136
this.ScenarioSetup(scenarioInfo);
#line 137
 testRunner.Given("I login as \'PRWebSendToIrisAccount\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 138
 testRunner.When("I call get valid subscription endpoint", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 139
 testRunner.Then("I see a subscription where SendToIris is true", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("PRWeb Create Distribution Test set back to draft and resubmmit with different sub" +
            "scription id")]
        public virtual void PRWebCreateDistributionTestSetBackToDraftAndResubmmitWithDifferentSubscriptionId()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("PRWeb Create Distribution Test set back to draft and resubmmit with different sub" +
                    "scription id", ((string[])(null)));
#line 141
this.ScenarioSetup(scenarioInfo);
#line 142
 testRunner.Given("the API test data \'PRWebDistribution.json\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 143
 testRunner.And("I login as \'prweb\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 144
 testRunner.When("I call send distribution", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 145
 testRunner.Then("I call distribution back to draft", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 146
 testRunner.And("I resubmit distribution with a different subscription to error validation", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 147
 testRunner.And("Delete created distribution from Databases", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("PRWeb 30 day re-submit rule enforced")]
        public virtual void PRWeb30DayRe_SubmitRuleEnforced()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("PRWeb 30 day re-submit rule enforced", ((string[])(null)));
#line 149
this.ScenarioSetup(scenarioInfo);
#line 150
 testRunner.Given("the API test data \'PRWebDistributionPublished30plusDaysAgo.json\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 151
 testRunner.And("I login as \'PRWebElysiumCompany4\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 152
 testRunner.When("I call Resubmit distribution", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 153
 testRunner.Then("I get the validation issue about the 30 day re-edit rule", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("PRWeb Get selected addons for a Distribution")]
        public virtual void PRWebGetSelectedAddonsForADistribution()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("PRWeb Get selected addons for a Distribution", ((string[])(null)));
#line 155
this.ScenarioSetup(scenarioInfo);
#line 156
 testRunner.Given("the API test data \'PRWebDistributionWithAddons.json\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 157
 testRunner.When("I call get selected addons", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 158
 testRunner.Then("I get the selected addons for the Distribution", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("PRWeb Update an addon subscription quantity used")]
        public virtual void PRWebUpdateAnAddonSubscriptionQuantityUsed()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("PRWeb Update an addon subscription quantity used", ((string[])(null)));
#line 160
this.ScenarioSetup(scenarioInfo);
#line 161
 testRunner.Given("the API test data \'PRWebAddonSubscriptionWithSession.json\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 162
 testRunner.When("I call update addons", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 163
 testRunner.Then("the addon quantity is updated", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 164
 testRunner.And("I reset the addon quantity in the DB", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("PRWeb The cpre phone extension default value should not be used.")]
        public virtual void PRWebTheCprePhoneExtensionDefaultValueShouldNotBeUsed_()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("PRWeb The cpre phone extension default value should not be used.", ((string[])(null)));
#line 166
this.ScenarioSetup(scenarioInfo);
#line 167
 testRunner.Given("the API test data \'PRWebDistribution.json\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 168
 testRunner.And("I login as \'PRWebElysiumCompany4\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 169
 testRunner.And("the press contact phone extension userparameter has a value", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 170
 testRunner.When("I call save draft distribution", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 171
 testRunner.Then("the distribution press contact phone extension is blank", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("CAP Account and Tweet OauthXRef and OAuthJSON data confirmation")]
        public virtual void CAPAccountAndTweetOauthXRefAndOAuthJSONDataConfirmation()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("CAP Account and Tweet OauthXRef and OAuthJSON data confirmation", ((string[])(null)));
#line 173
this.ScenarioSetup(scenarioInfo);
#line 174
 testRunner.Given("the API test data \'PRWebDistributionWithCAP.json\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 175
 testRunner.And("I login as \'PRWebElysiumCompany4\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 176
 testRunner.When("I call send distribution", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 177
 testRunner.Then("I see TwitterID and Message values in the response", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 178
 testRunner.And("Delete created distribution from Databases", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion