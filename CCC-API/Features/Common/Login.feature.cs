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
    [NUnit.Framework.DescriptionAttribute("Login")]
    public partial class LoginFeature
    {

        private TechTalk.SpecFlow.ITestRunner testRunner;

#line 1 "Login.feature"
#line hidden

        [NUnit.Framework.OneTimeSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Login", "\tIn order to verify custom and non-custom logins\r\n\tthe session key and user store" +
                    "d (or not stored) in the PropertyBucket\r\n\tshould contain the expected values", ProgrammingLanguage.CSharp, ((string[])(null)));
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
        [NUnit.Framework.DescriptionAttribute("Login with custom user")]
        [NUnit.Framework.CategoryAttribute("Login")]
        [NUnit.Framework.CategoryAttribute("smokeProd")]
        public virtual void LoginWithCustomUser()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Login with custom user", new string[] {
                        "HeartsAndCharts"});
#line 7
            this.ScenarioSetup(scenarioInfo);
#line 8
            testRunner.Given("I login as \'analytics manager\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 9
            testRunner.Then("the session key exists in the PropertyBucket", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 10
            testRunner.Then("the user exists in the PropertyBucket", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }

        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Attempt to login with invalid credentials")]
        [NUnit.Framework.IgnoreAttribute("Ignored scenario")]
        [NUnit.Framework.CategoryAttribute("Login")]
        [NUnit.Framework.TestCaseAttribute("ShakeDownAutomation", "apitest", "p", "ShakeDownAutomation is not an Elysium company", "2", null)]
        [NUnit.Framework.TestCaseAttribute("ShakeDownAutomation", "a", "1", "Error creating session (Code: InvalidCompanyLogin).", "1", null)]
        [NUnit.Framework.TestCaseAttribute("ShakeDown", "apitest", "1", "Error creating session (Code: InvalidCompanyLogin).", "1", null)]
        [NUnit.Framework.TestCaseAttribute("AdvancePwdCompany", "sysadminNew", "33", "User requires a more secure password.", "6", null)]
        [NUnit.Framework.TestCaseAttribute("AdvancePwdCompany", "AutomationLockedUser", "Verano2018$", "Account is Locked", "8", null)]
        public virtual void AttemptToLoginWithInvalidCredentials(string company, string username, string password, string message, string status, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "Login",
                    "ignore"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Attempt to login with invalid credentials", @__tags);
#line 13
            this.ScenarioSetup(scenarioInfo);
#line 14
            testRunner.Given(string.Format("I login to Company {0} with {1} and {2}", company, username, password), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 15
            testRunner.Then(string.Format("I should see the message {0}", message), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 16
            testRunner.And("the session key should be empty", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 17
            testRunner.And(string.Format("the connect status code should be {0}", status), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }

        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Complete MSA login flow for a user without/with MSA token")]
        [NUnit.Framework.CategoryAttribute("MSA")]
        [NUnit.Framework.CategoryAttribute("Login")]
        [NUnit.Framework.TestCaseAttribute("0", "Msaenabledcompany", "FirstTimeUserSysadmin", "33", "en-us", null)]
        [NUnit.Framework.TestCaseAttribute("1", "Msaenabledcompany", "FirstTimeUserStandard", "33", "en-us", null)]
        [NUnit.Framework.TestCaseAttribute("2", "Msaenabledcompany", "FirstTimeUserReadOnly", "33", "en-us", null)]
        public virtual void CompleteMSALoginFlowForAUserWithoutWithMSAToken(string index, string company, string username, string password, string languagekey, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "MSA",
                    "Login"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Complete MSA login flow for a user without/with MSA token", @__tags);
#line 28
            this.ScenarioSetup(scenarioInfo);
#line 29
            testRunner.Given("the API test data \'MSAloginInfo.json\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 30
            testRunner.When(string.Format("I login \'without\' existing MSA TOKEN to Company {0} with {1} and {2} and <languag" +
                                   "eKey>", company, username, password), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 31
            testRunner.Then("the connect status code should be 5", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 32
            testRunner.And(string.Format("I verify Activation code it\'s stored in userparameter table for {0}", index), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 33
            testRunner.And("Login content response has a valid value", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 34
            testRunner.When("I send a POST to msa endpoint with loginInfo and activation code just obtained", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 35
            testRunner.Then("Verify response code is \'0\' and response has both valid session and save msa toke" +
                               "n to verify I can login with an existing MSA TOKEN", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 36
            testRunner.When(string.Format("I login \'with\' existing MSA TOKEN to Company {0} with {1} and {2} and <languageKe" +
                                   "y>", company, username, password), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 37
            testRunner.Then("Verify MSA existing response code is \'0\' and response has a valid session", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }

        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Verify invalid activation code validation for MSA login")]
        [NUnit.Framework.CategoryAttribute("MSA")]
        [NUnit.Framework.CategoryAttribute("Login")]
        [NUnit.Framework.TestCaseAttribute("1111111", "InvalidCode", "1", null)]
        [NUnit.Framework.TestCaseAttribute("0", "Invalid MSA Login Request", "1", null)]
        public virtual void VerifyInvalidActivationCodeValidationForMSALogin(string activationCode, string message, string status, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "MSA",
                    "Login"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Verify invalid activation code validation for MSA login", @__tags);
#line 46
            this.ScenarioSetup(scenarioInfo);
#line 47
            testRunner.Given("the API test data \'MSAloginInfo.json\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 48
            testRunner.When("I login \'without\' existing MSA TOKEN to Company Msaenabledcompany with FirstTimeU" +
                               "serSysadmin and 33 and en-us", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 49
            testRunner.Then("Login content response has a valid value", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 50
            testRunner.When(string.Format("I send a POST to msa endpoint with login info and {0}", activationCode), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 51
            testRunner.Then(string.Format("I verify response code it\'s {0} OK and response has {1}", status, message), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }

        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Verify autologin endpoint is working fine to send login event to ChurnZero")]
        [NUnit.Framework.CategoryAttribute("Login")]
        [NUnit.Framework.CategoryAttribute("ChurnZero")]
        [NUnit.Framework.CategoryAttribute("AutoLogin")]
        [NUnit.Framework.TestCaseAttribute("gnus3", "AutoLoginAdmin", "1", null)]
        [NUnit.Framework.TestCaseAttribute("gnus3", "AutoLoginStandard", "1", null)]
        [NUnit.Framework.TestCaseAttribute("gnus3", "AutoLoginReadOnly", "1", null)]
        public virtual void VerifyAutologinEndpointIsWorkingFineToSendLoginEventToChurnZero(string company, string username, string password, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "ChurnZero",
                    "AutoLogin"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Verify autologin endpoint is working fine to send login event to ChurnZero", @__tags);
#line 59
            this.ScenarioSetup(scenarioInfo);
#line 60
            testRunner.Given(string.Format("I login to Company {0} with {1} and {2}", company, username, password), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 61
            testRunner.When("I sent a POST to AutoLogin endpoint using user credentials", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 62
            testRunner.Then("I should see the proper AutoLogin response", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }

        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Verify Successful response for new SSO autologin endpoint using a valid SSO compa" +
            "ny")]
        [NUnit.Framework.IgnoreAttribute("Ignored scenario")]
        [NUnit.Framework.CategoryAttribute("Login")]
        [NUnit.Framework.CategoryAttribute("SSOAutoLogin")]
        [NUnit.Framework.CategoryAttribute("SSO")]
        [NUnit.Framework.TestCaseAttribute("OnpointCompany", "4", "okta-jt", "", null)]
        [NUnit.Framework.TestCaseAttribute("NoExistent", "1", "", "", null)]
        [NUnit.Framework.TestCaseAttribute("ACL", "9", "", "Single Sign On is not set up for this company", null)]
        public virtual void VerifySuccessfulResponseForNewSSOAutologinEndpointUsingAValidSSOCompany(string company, string statusCode, string identityProvider, string message, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "SSOAutoLogin",
                    "SSO",
                    "Ignore"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Verify Successful response for new SSO autologin endpoint using a valid SSO compa" +
                    "ny", @__tags);
#line 71
            this.ScenarioSetup(scenarioInfo);
#line 72
            testRunner.When(string.Format("I sent a POST to SSO AutoLogin endpoint using this {0}", company), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 73
            testRunner.Then(string.Format("I should see the proper SSO AutoLogin {0} - {1} - {2} response", statusCode, identityProvider, message), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }

        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Verify Resend Code flow for MSA login are working as expected for Success scenari" +
            "o")]
        [NUnit.Framework.IgnoreAttribute("Ignored scenario")]
        [NUnit.Framework.CategoryAttribute("MSA")]
        [NUnit.Framework.CategoryAttribute("NeedsCleanupSuccess")]
        [NUnit.Framework.CategoryAttribute("Login")]
        public virtual void VerifyResendCodeFlowForMSALoginAreWorkingAsExpectedForSuccessScenario()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Verify Resend Code flow for MSA login are working as expected for Success scenari" +
                    "o", new string[] {
                         "MSA",
                        "NeedsCleanupSuccess",
                        "Login",
                        "Ignore"});
#line 85
            this.ScenarioSetup(scenarioInfo);
#line 86
            testRunner.Given("the API test data \'MSAloginInfo.json\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 87
            testRunner.When("I login \'without\' existing MSA TOKEN to Company Msaenabledcompany with MSACodeSuc" +
                               "cessUser and 33 and en-us", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 88
            testRunner.Then("Login content response has a valid value", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 89
            testRunner.When("I send a POST to msa code endpoint using this login info for this scenario", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 90
            testRunner.Then("I verify Msa code endpoint status code is \'0\' OK and message is \'Success\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }

        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Verify Resend Code flow for MSA login are working as expected for Maximum Code Re" +
            "sends resend limit is Reached")]
        [NUnit.Framework.IgnoreAttribute("Ignored scenario")]
        [NUnit.Framework.CategoryAttribute("MSA")]
        [NUnit.Framework.CategoryAttribute("NeedsCleanupMaxCodeResendsReached")]
        [NUnit.Framework.CategoryAttribute("Login")]
        public virtual void VerifyResendCodeFlowForMSALoginAreWorkingAsExpectedForMaximumCodeResendsResendLimitIsReached()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Verify Resend Code flow for MSA login are working as expected for Maximum Code Re" +
                    "sends resend limit is Reached", new string[] {
                        "MSA",
                        "NeedsCleanupMaxCodeResendsReached",
                        "Login",
                        "Ignore"});
#line 93
            this.ScenarioSetup(scenarioInfo);
#line 94
            testRunner.Given("the API test data \'MSAloginInfo.json\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 95
            testRunner.When("I login \'without\' existing MSA TOKEN to Company Msaenabledcompany with MSACodeMax" +
                               "CodeResendsReachedUser and 33 and en-us", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 96
            testRunner.Then("Login content response has a valid value", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 97
            testRunner.When("I send a POST to msa code endpoint using this login info for this scenario", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 98
            testRunner.Then("I verify Msa code endpoint status code is \'2\' OK and message is \'MaxCodeResendsRe" +
                               "ached\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }

        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Verify Resend Code flow for MSA login are working as expected for Maximum Code Re" +
            "sends resend limit is Reached 10 times")]
        [NUnit.Framework.IgnoreAttribute("Ignored scenario")]
        [NUnit.Framework.CategoryAttribute("MSA")]
        [NUnit.Framework.CategoryAttribute("NeedsCleanupAccountLocked")]
        [NUnit.Framework.CategoryAttribute("Login")]
        public virtual void VerifyResendCodeFlowForMSALoginAreWorkingAsExpectedForMaximumCodeResendsResendLimitIsReached10Times()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Verify Resend Code flow for MSA login are working as expected for Maximum Code Re" +
                    "sends resend limit is Reached 10 times", new string[] {
                        "MSA",
                        "NeedsCleanupAccountLocked",
                        "Login",
                        "Ignore"});
#line 101
            this.ScenarioSetup(scenarioInfo);
#line 102
            testRunner.Given("the API test data \'MSAloginInfo.json\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 103
            testRunner.When("I login \'without\' existing MSA TOKEN to Company Msaenabledcompany with MSACodeAcc" +
                               "ountLockedUser and 33 and en-us", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 104
            testRunner.Then("Login content response has a valid value", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 105
            testRunner.When("I send a POST to msa code endpoint using this login info for this scenario", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 106
            testRunner.Then("I verify Msa code endpoint status code is \'3\' OK and message is \'AccountLocked\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }

        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Verify LCID user parameter gets created once user is logged in.")]
        [NUnit.Framework.CategoryAttribute("LCID")]
        [NUnit.Framework.CategoryAttribute("Language")]
        [NUnit.Framework.CategoryAttribute("Login")]
        [NUnit.Framework.TestCaseAttribute("Churnzerocompany", "manager", "33", "1031", null)]
        [NUnit.Framework.TestCaseAttribute("Churnzerocompany", "manager", "33", "1033", null)]
        [NUnit.Framework.TestCaseAttribute("Churnzerocompany", "manager", "33", "1036", null)]
        [NUnit.Framework.TestCaseAttribute("Churnzerocompany", "manager", "33", "1043", null)]
        [NUnit.Framework.TestCaseAttribute("Churnzerocompany", "manager", "33", "2057", null)]
        [NUnit.Framework.TestCaseAttribute("Churnzerocompany", "manager", "33", "4105", null)]
        public virtual void VerifyLCIDUserParameterGetsCreatedOnceUserIsLoggedIn_(string company, string username, string password, string lCID, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "LCID",
                    "Language",
                    "Login"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Verify LCID user parameter gets created once user is logged in.", @__tags);
#line 109
            this.ScenarioSetup(scenarioInfo);
#line 110
            testRunner.Given("the API test data \'LCIDLoginInfo.json\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 111
            testRunner.When(string.Format("I login to Company {0} with {1} and {2} and {3}", company, username, password, lCID), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 112
            testRunner.Then("Verify response code is \'0\' and response has a valid session", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 113
            testRunner.And(string.Format("User parameter {0} is created for this user with correct value.", lCID), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
