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
namespace CCC_API.Features.Campaigns
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.3.2.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("Campaigns")]
    public partial class CampaignsFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "Campaigns.feature"
#line hidden
        
        [NUnit.Framework.OneTimeSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Campaigns", "\tIn order to group my activities\r\n\tAs a valid CCC user with system parameter Camp" +
                    "aign-Enabled set to true\r\n\tI can assign assign activities to a campaign", ProgrammingLanguage.CSharp, ((string[])(null)));
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
        [NUnit.Framework.DescriptionAttribute("Campaign > Create > Code")]
        [NUnit.Framework.IgnoreAttribute("Ignored scenario")]
        [NUnit.Framework.CategoryAttribute("publishers")]
        [NUnit.Framework.CategoryAttribute("campaign")]
        [NUnit.Framework.TestCaseAttribute("some", "", "200", null)]
        [NUnit.Framework.TestCaseAttribute("50 chars", "250 chars", "200", null)]
        [NUnit.Framework.TestCaseAttribute("", "", "400", null)]
        [NUnit.Framework.TestCaseAttribute("", "some", "400", null)]
        [NUnit.Framework.TestCaseAttribute("51 chars", "some", "400", null)]
        [NUnit.Framework.TestCaseAttribute("some", "251 chars", "400", null)]
        public virtual void CampaignCreateCode(string name, string description, string result, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "publishers",
                    "campaign",
                    "ignore"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Campaign > Create > Code", @__tags);
#line 7
this.ScenarioSetup(scenarioInfo);
#line 8
 testRunner.Given("I login as shared user \'Manager user Campaign-Enabled company\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 9
 testRunner.When(string.Format("I POST campaign with \'{0}\' and \'{1}\'", name, description), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 10
 testRunner.Then(string.Format("I have to be given \'{0}\'", result), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Campaign > Get Id")]
        [NUnit.Framework.IgnoreAttribute("Ignored scenario")]
        [NUnit.Framework.CategoryAttribute("publishers")]
        [NUnit.Framework.CategoryAttribute("campaign")]
        public virtual void CampaignGetId()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Campaign > Get Id", new string[] {
                        "publishers",
                        "campaign",
                        "ignore"});
#line 22
this.ScenarioSetup(scenarioInfo);
#line 23
 testRunner.Given("I login as shared user \'Manager user Campaign-Enabled company\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 24
 testRunner.When("I POST campaign \'1\' times", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 25
 testRunner.Then("I have to be given id", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 26
 testRunner.And("I can GET campaign by id", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Campaign > List of campaigns")]
        [NUnit.Framework.IgnoreAttribute("Ignored scenario")]
        [NUnit.Framework.CategoryAttribute("publishers")]
        [NUnit.Framework.CategoryAttribute("campaign")]
        public virtual void CampaignListOfCampaigns()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Campaign > List of campaigns", new string[] {
                        "publishers",
                        "campaign",
                        "ignore"});
#line 29
this.ScenarioSetup(scenarioInfo);
#line 30
 testRunner.Given("I login as shared user \'Manager user Campaign-Enabled company\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 31
 testRunner.When("I POST campaign \'2\' times", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 32
 testRunner.Then("I have to be given ids", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 33
 testRunner.And("I can find \'2\' created campaigns in campaigns list", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Campaign > Edit")]
        [NUnit.Framework.IgnoreAttribute("Ignored scenario")]
        [NUnit.Framework.CategoryAttribute("publishers")]
        [NUnit.Framework.CategoryAttribute("campaign")]
        public virtual void CampaignEdit()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Campaign > Edit", new string[] {
                        "publishers",
                        "campaign",
                        "ignore"});
#line 36
this.ScenarioSetup(scenarioInfo);
#line 37
 testRunner.Given("I login as shared user \'Manager user Campaign-Enabled company\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 38
 testRunner.When("I POST campaign \'1\' times", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 39
 testRunner.Then("I can edit campaign", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 40
 testRunner.And("I can find \'1\' created campaigns in campaigns list", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Campaign > Delete")]
        [NUnit.Framework.CategoryAttribute("publishers")]
        [NUnit.Framework.CategoryAttribute("campaign")]
        public virtual void CampaignDelete()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Campaign > Delete", new string[] {
                        "publishers",
                        "campaign"});
#line 43
this.ScenarioSetup(scenarioInfo);
#line 44
 testRunner.Given("I login as shared user \'Manager user Campaign-Enabled company\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 45
 testRunner.When("I POST campaign \'1\' times", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 46
 testRunner.Then("I can delete campaign", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 47
 testRunner.And("campaign is not in campaigns list", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Campaign > Assign Email Draft > Remove from Campaign")]
        [NUnit.Framework.CategoryAttribute("publishers")]
        [NUnit.Framework.CategoryAttribute("campaign")]
        public virtual void CampaignAssignEmailDraftRemoveFromCampaign()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Campaign > Assign Email Draft > Remove from Campaign", new string[] {
                        "publishers",
                        "campaign"});
#line 50
this.ScenarioSetup(scenarioInfo);
#line 51
 testRunner.Given("I login as shared user \'Manager user Campaign-Enabled company\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 52
 testRunner.Given("I create email \'draft\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 53
 testRunner.When("I POST campaign \'1\' times", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 54
 testRunner.Then("I can assign \'draft\' to campaign", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 55
 testRunner.And("can find \'draft\' in publish activities by campaign", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 56
 testRunner.When("I delete \'draft\' from campaign", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 57
 testRunner.Then("cannot find \'draft\' in publish activities by campaign", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Campaign > Assign Custom Activity > Remove from Campaign")]
        [NUnit.Framework.CategoryAttribute("publishers")]
        [NUnit.Framework.CategoryAttribute("campaign")]
        [NUnit.Framework.TestCaseAttribute("SendMailing", "scheduled", "kevin", "", null)]
        [NUnit.Framework.TestCaseAttribute("Other", "sent", "", "twitter", null)]
        [NUnit.Framework.TestCaseAttribute("Callback", "sent", "", "", null)]
        public virtual void CampaignAssignCustomActivityRemoveFromCampaign(string type, string state, string contact, string outlet, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "publishers",
                    "campaign"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Campaign > Assign Custom Activity > Remove from Campaign", @__tags);
#line 60
this.ScenarioSetup(scenarioInfo);
#line 61
 testRunner.Given("I login as shared user \'Manager user Campaign-Enabled company\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
            TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                        "Title",
                        "Type",
                        "Notes",
                        "TimeZoneIdentifier",
                        "ScheduleTime"});
            table1.AddRow(new string[] {
                        "30",
                        "SendMailing",
                        "0",
                        "Central Standard Time",
                        "6"});
            table1.AddRow(new string[] {
                        "15",
                        "Other",
                        "50",
                        "AUS Eastern Standard Time",
                        "-8"});
            table1.AddRow(new string[] {
                        "5",
                        "Callback",
                        "32",
                        "Pacific Standard Time",
                        "-2"});
#line 62
 testRunner.Given("custom activity combinations:", ((string)(null)), table1, "Given ");
#line 67
 testRunner.Given(string.Format("I take combination of \'{0}\'", type), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 68
 testRunner.Given(string.Format("I create custom activity of \'{0}\' \'{1}\' \'{2}\' \'{3}\'", type, state, contact, outlet), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 69
 testRunner.When("I POST campaign \'1\' times", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 70
 testRunner.Then(string.Format("I can assign \'{0}\' to campaign", type), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 71
 testRunner.And(string.Format("can find \'{0}\' in publish activities by campaign", type), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 72
 testRunner.When(string.Format("I delete \'{0}\' from campaign", type), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 73
 testRunner.Then(string.Format("cannot find \'{0}\' in publish activities by campaign", type), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Campaign > Assign News Article > Remove from Campaign")]
        [NUnit.Framework.CategoryAttribute("publishers")]
        [NUnit.Framework.CategoryAttribute("campaign")]
        public virtual void CampaignAssignNewsArticleRemoveFromCampaign()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Campaign > Assign News Article > Remove from Campaign", new string[] {
                        "publishers",
                        "campaign"});
#line 82
this.ScenarioSetup(scenarioInfo);
#line 83
 testRunner.Given("shared session for \'standard\' user with edition \'Manager user Campaign-Enabled co" +
                    "mpany\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 84
 testRunner.And("news article exist", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 85
 testRunner.And("campaign \'Assign\' present (to create: \'system_admin\' for \'Manager user Campaign-E" +
                    "nabled company\')", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 86
    testRunner.And("campaign \'Assign 2\' present (to create: \'system_admin\' for \'Manager user Campaign" +
                    "-Enabled company\')", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 87
    testRunner.And("campaign \'Assign 3\' present (to create: \'system_admin\' for \'Manager user Campaign" +
                    "-Enabled company\')", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 88
 testRunner.When("I assign news article to campaign", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 89
 testRunner.Then("I see campaign \'Assign\' in news article", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 90
    testRunner.When("I add an additional campaign", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 91
    testRunner.Then("news article campaigns are \'Assign\' and \'Assign 2\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 92
    testRunner.When("I add and remove campaigns on news article", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 93
    testRunner.Then("news article campaigns are \'Assign\' and \'Assign 3\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 94
 testRunner.When("I remove all campaigns from news article", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 95
 testRunner.Then("I cannot see any campaigns in news article", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Campaign > Assign Social Post > Remove from Campaign")]
        [NUnit.Framework.CategoryAttribute("publishers")]
        [NUnit.Framework.CategoryAttribute("campaign")]
        [NUnit.Framework.TestCaseAttribute("Twitter", "tomorrow", "standard", null)]
        [NUnit.Framework.TestCaseAttribute("Twitter", "past", "system_admin", null)]
        public virtual void CampaignAssignSocialPostRemoveFromCampaign(string platform, string time, string permissions, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "publishers",
                    "campaign"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Campaign > Assign Social Post > Remove from Campaign", @__tags);
#line 98
this.ScenarioSetup(scenarioInfo);
#line 100
 testRunner.Given(string.Format("session for \'{0}\' user with edition \'Manager user Campaign-Enabled company\'", permissions), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 101
 testRunner.And("campaign \'Assign\' present (to create: \'system_admin\' for \'Manager user Campaign-E" +
                    "nabled company\')", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 102
 testRunner.And(string.Format("a social post activity to \'{0}\' with time \'{1}\', timezone \'W. Europe Standard Tim" +
                        "e\'", platform, time), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 103
 testRunner.Then(string.Format("I can assign \'{0}\' to campaign", platform), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 104
 testRunner.And(string.Format("can find \'{0}\' in publish activities by campaign", platform), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 105
 testRunner.When(string.Format("I delete \'{0}\' from campaign", platform), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 106
 testRunner.Then(string.Format("cannot find \'{0}\' in publish activities by campaign", platform), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion

