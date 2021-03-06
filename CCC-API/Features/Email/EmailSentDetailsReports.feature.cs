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
namespace CCC_API.Features.Email
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.3.2.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("EmailSentDetailsReports")]
    public partial class EmailSentDetailsReportsFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "EmailSentDetailsReports.feature"
#line hidden
        
        [NUnit.Framework.OneTimeSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "EmailSentDetailsReports", "\tIn order to share information about send email I can generate reports", ProgrammingLanguage.CSharp, ((string[])(null)));
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
        
        public virtual void FeatureBackground()
        {
#line 4
#line 5
 testRunner.Given("I remember expected data from \'Distributions.json\' file", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Generate PDF report")]
        [NUnit.Framework.CategoryAttribute("publishers")]
        [NUnit.Framework.CategoryAttribute("email_sent_details")]
        [NUnit.Framework.CategoryAttribute("email_report")]
        [NUnit.Framework.TestCaseAttribute("contacts", "standard", "Publishers manager user", null)]
        [NUnit.Framework.TestCaseAttribute("outlets", "read_only", "Publishers manager user", null)]
        [NUnit.Framework.TestCaseAttribute("individuals", "system_admin", "Publishers manager user", null)]
        [NUnit.Framework.TestCaseAttribute("organizations", "standard", "Publishers manager user", null)]
        public virtual void GeneratePDFReport(string type, string permission, string edition, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "publishers",
                    "email_sent_details",
                    "email_report"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Generate PDF report", @__tags);
#line 8
this.ScenarioSetup(scenarioInfo);
#line 4
this.FeatureBackground();
#line 9
  testRunner.Given(string.Format("session for \'{0}\' user with edition \'{1}\'", permission, edition), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 10
     testRunner.When(string.Format("I request PDF export for a distribution with {0}", type), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 11
  testRunner.Then(string.Format("I should be given the pending job url for future report of {0}", type), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 12
  testRunner.And(string.Format("I can download PDF report of type {0}", type), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 13
  testRunner.Then("I can take necessary distribution details to compare with", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 14
      testRunner.And("check cover page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 15
   testRunner.And(string.Format("check analytics page with clicks, opens and bounces for {0}", type), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 16
   testRunner.And("check clicks summary section for a report of type PDF", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 17
   testRunner.And("check copy of email page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 18
   testRunner.And("check email details page to have times in user settings timezone for type of repo" +
                    "rt PDF", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 19
   testRunner.And("check recipients lists", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 20
   testRunner.And(string.Format("check list of recipients for the report of type PDF and recipients {0}", type), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 21
   testRunner.And("check section of email opens", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 22
   testRunner.And("check section of email clicks", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Generate PDF report with excluded sections")]
        [NUnit.Framework.IgnoreAttribute("Ignored scenario")]
        [NUnit.Framework.CategoryAttribute("publishers")]
        [NUnit.Framework.CategoryAttribute("email_sent_details")]
        [NUnit.Framework.CategoryAttribute("email_report")]
        [NUnit.Framework.TestCaseAttribute("contacts", null)]
        [NUnit.Framework.TestCaseAttribute("outlets", null)]
        public virtual void GeneratePDFReportWithExcludedSections(string type, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "publishers",
                    "email_sent_details",
                    "email_report",
                    "ignore"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Generate PDF report with excluded sections", @__tags);
#line 32
this.ScenarioSetup(scenarioInfo);
#line 4
this.FeatureBackground();
#line 33
 testRunner.Given("shared session for \'standard\' user with edition \'Publishers manager user\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 34
 testRunner.When(string.Format("I request export for a PDF report distribution of {0} and excluded sections <all>" +
                        "", type), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 35
 testRunner.Then(string.Format("I should be given the pending job url for future report of {0}", type), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 36
 testRunner.And(string.Format("I can download PDF report of type {0}", type), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 37
 testRunner.And("I can take necessary distribution details to compare with", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 38
 testRunner.Then("I should see \'1\' page with title and page number", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Generate DOCX report")]
        [NUnit.Framework.CategoryAttribute("publishers")]
        [NUnit.Framework.CategoryAttribute("email_sent_details")]
        [NUnit.Framework.CategoryAttribute("email_report")]
        [NUnit.Framework.TestCaseAttribute("contacts", "standard", null)]
        [NUnit.Framework.TestCaseAttribute("outlets", "read_only", null)]
        public virtual void GenerateDOCXReport(string type, string permission, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "publishers",
                    "email_sent_details",
                    "email_report"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Generate DOCX report", @__tags);
#line 46
this.ScenarioSetup(scenarioInfo);
#line 4
this.FeatureBackground();
#line 47
     testRunner.Given(string.Format("session for \'{0}\' user with edition \'Publishers manager user\'", permission), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 48
     testRunner.When(string.Format("I request DOCX export for a distribution with {0}", type), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 49
  testRunner.Then(string.Format("I should be given the pending job url for future report of {0}", type), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 50
  testRunner.And(string.Format("I can download DOCX report of type {0}", type), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 51
  testRunner.Then("I can take necessary distribution details to compare with", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 52
      testRunner.And("check cover page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 53
   testRunner.And(string.Format("check analytics page with clicks, opens and bounces for {0}", type), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 54
   testRunner.And("check clicks summary section for a report of type DOCX", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 55
   testRunner.And("check copy of email page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 56
   testRunner.And("check email details page to have times in user settings timezone for type of repo" +
                    "rt DOCX", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 57
   testRunner.And("check recipients lists", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 58
   testRunner.And(string.Format("check list of recipients for the report of type DOCX and recipients {0}", type), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 59
   testRunner.And("check section of email opens", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 60
   testRunner.And("check section of email clicks", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Generate DOCX report with excluded sections")]
        [NUnit.Framework.IgnoreAttribute("Ignored scenario")]
        [NUnit.Framework.CategoryAttribute("publishers")]
        [NUnit.Framework.CategoryAttribute("email_sent_details")]
        [NUnit.Framework.CategoryAttribute("email_report")]
        [NUnit.Framework.TestCaseAttribute("individuals", "standard", null)]
        [NUnit.Framework.TestCaseAttribute("outlets", "read_only", null)]
        public virtual void GenerateDOCXReportWithExcludedSections(string type, string permission, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "publishers",
                    "email_sent_details",
                    "email_report",
                    "ignore"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Generate DOCX report with excluded sections", @__tags);
#line 67
this.ScenarioSetup(scenarioInfo);
#line 4
this.FeatureBackground();
#line 68
    testRunner.Given(string.Format("shared session for \'{0}\' user with edition \'Publishers manager user\'", permission), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 69
 testRunner.When(string.Format("I request export for a DOCX report distribution of {0} and excluded sections <all" +
                        ">", type), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 70
 testRunner.Then(string.Format("I should be given the pending job url for future report of {0}", type), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 71
 testRunner.And(string.Format("I can download DOCX report of type {0}", type), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 72
 testRunner.And("I can take necessary distribution details to compare with", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 73
 testRunner.Then("I should see \'1\' page with title and page number", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion

