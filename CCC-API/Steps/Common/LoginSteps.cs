using BoDi;
using CCC_API.Data.PostData.Settings.UserManagement;
using CCC_API.Services;
using CCC_API.Services.Common;
using CCC_API.Services.Common.db;
using CCC_API.Utils;
using CCC_Infrastructure.API.Utils;
using CCC_Infrastructure.UserSupport;
using CCC_Infrastructure.Utils;
using Newtonsoft.Json;
using CCC_API.Utils.Assertion;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using ZukiniWrap;

namespace CCC_API.Steps.Common
{
    [Binding]
    public class LoginSteps : ApiSteps
    {

        private const string ACTIVATION_CODE_KEY = "ActivationCode";
        private const string COMPANY_ID_KEY = "CompanyId";
        private const string COMPANY_ID_LCID_KEY = "CompanyIdLCID";
        private const string CONNECT_RESPONSE_KEY = "ConnectResponse";
        private const string MSA_CODE_RESPONSE_KEY = "MSACodeConnectResponse";
        private const string MSA_CONNECT_RESPONSE_KEY = "MSAConnectResponse";
        private const string MSA_EXISTING_CONNECT_RESPONSE_KEY = "MSAExistingConnectResponse";
        private const string MSA_TOKEN_KEY = "MSAToken";
        private const string LOGIN = "Login";
        private const string LOGIN_INFO_KEY = "LoginInfo";                
        private const string USER_ACCOUNT_ID_KEY = "UserAccountId";
        private const string USER_ACCOUNT_ID_ACCOUNT_LOCKED_KEY = "UserAccountIdAccountLocked";
        private const string USER_ACCOUNT_LCID_KEY = "UserAccountIdLCID";
        private const string USER_ACCOUNT_ID_MAX_CODE_RESENDS_REACHED_KEY = "UserAccountIdMaxCodeResendsReached";
        private const string USER_ACCOUNT_ID_SUCCESS_KEY = "UserAccountIdSuccess";
        private const string AUTO_LOGIN_RESPONSE = "AutoLoginResponse";
        private const string AUTO_LOGIN_SSO_RESPONSE = "AutoLoginSSOResponse";
        public static readonly TimeSpan LOGIN_TIMEOUT = TimeSpan.FromMinutes(2);

        private readonly AccountsService _accountsService = new AccountsService();

        private readonly C3UserFactory _userFactory;

        public const string USER_KEY = "user";        

        public LoginSteps(IObjectContainer objectContainer, string featureName) : base(objectContainer)
        {
            _userFactory = new C3UserFactory(ApiHooks.APIUserFile, "http://docker1.qwestcolo.local:8080/", featureName, "NoScenario");
        }

        public LoginSteps(IObjectContainer objectContainer, FeatureContext featureContext, ScenarioContext scenarioContext) : base(objectContainer) {
            _userFactory = new C3UserFactory(ApiHooks.APIUserFile, "http://docker1.qwestcolo.local:8080/", featureContext.FeatureInfo.Title, scenarioContext.ScenarioInfo.Title);
        }

        #region Given Steps

        [Given(@"I login as '(.*)'")]
        [Given(@"I log into the API as '(.*)'")]
        public void GivenILoginAs(string key)
        {
            var user = UserList.GetUser(key);
            PropertyBucket.Remember(ApiHooks.ScenarioUserKey, user);
            // login the user
            var sessionKey = _accountsService.LoginWithRetry(user);
            PropertyBucket.Remember(AccountsService.SessionKey, sessionKey);
            // change datagroup
            if (user.CompanyEdition == "basic")
            {
                user.DataGroup = "(Default)";
            }
            if (!string.IsNullOrEmpty(user.DataGroup))
            {
                new AccountInfoService(sessionKey).ChangeDataGroup(user.DataGroup);
            }
        }

        [Given(@"I login as shared user '(.*)'")]
        [When(@"I login as shared user '(.*)'")]
        public string GivenILoginAsSharedUser(string edition)
        {
            // Seems like I cannot hijack static logic of the UserList class without releasing new version ... 
            // So here I perform caching of the session key in order to reuse it between tests
            // Seems like Zukini.Remember is Test local bucket
            var sessionKey = SessionKeyCache.Instance().GetOrAdd(edition, key =>
            {
                var user = UserList.GetUser(edition);
                var localKey =
                    new Poller(LOGIN_TIMEOUT)
                        .TryUntil(() => _accountsService.AsExceptional().ThenExecute(_ => _.Login(user)),
                            result => result.IsSuccessful)
                        .Value;
                // change datagroup
                if (!string.IsNullOrEmpty(user.DataGroup))
                {
                    new AccountInfoService(localKey).ChangeDataGroup(user.DataGroup);
                }
                return localKey;
            });
            PropertyBucket.Remember(AccountsService.SessionKey, sessionKey, true);
            return sessionKey;
        }

        [Given(@"I login to Company (.*) with (.*) and (.*)")]
        public void GivenILoginToCompanyWithCompanyUserAndPassword(string company, string username, string password)
        {
            var response = _accountsService.Login(company, username, password);
            PropertyBucket.Remember(CONNECT_RESPONSE_KEY, response);
        }


        [Given(@"I login '(.*)' existing MSA TOKEN to Company (.*) with (.*) and (.*) and (.*)")]
        [When(@"I login '(.*)' existing MSA TOKEN to Company (.*) with (.*) and (.*) and (.*)")]        
        public void WhenILoginExistingMSATOKENToCompanyWithUserAndPasswordAndLanguageKey(string msaTypeLogin, string company, string username, string password, string languageKey)
        {
            string msaToken = "";
            if (msaTypeLogin == "without")
            {
                msaToken = "";
                var response = _accountsService.Login(company, username, password, languageKey, msaToken);
                PropertyBucket.Remember(CONNECT_RESPONSE_KEY, response);
            }
            else
            {
                msaToken = PropertyBucket.GetProperty<string>(MSA_TOKEN_KEY);
                var response = _accountsService.Login(company, username, password, languageKey, msaToken);
                PropertyBucket.Remember(MSA_EXISTING_CONNECT_RESPONSE_KEY, response);
            }
        }
        
        [Given(@"session for '(.*)' user with edition '(.*)'")]
        public void GivenSessionForUserWithEdition(DynamicUser.PermissionType permissions, string edition)
        {
            var user = _userFactory.CreateUserWithKey(edition, permissions);

            PropertyBucket.Remember(AccountsService.SessionKey, user.SessionKey, true);
            PropertyBucket.Remember(USER_KEY, user, true);
        }

        [Given(@"session for edition '(.*)', permission: '(.*)', datagroup: '(.*)'")]
        public void GivenSessionForUserWithEditionDataGroup(string edition, DynamicUser.PermissionType permissions, string dataGroup)
        {
            var user = _userFactory.CreateUserWithKey(edition, permissions, dataGroup);
            PropertyBucket.Remember(AccountsService.SessionKey, user.SessionKey, true);
            PropertyBucket.Remember(USER_KEY, user, true);
        }

        [Given(@"shared session for '(.*)' user with edition '(.*)'")]
        public string GivenSharedSessionForUserWithEdition(DynamicUser.PermissionType permissions, string edition)
        {
            var userString = SessionKeyCache.Instance()
                .GetOrAdd(edition + permissions, k =>
                    new Poller(LOGIN_TIMEOUT)
                        .TryUntil(() =>
                                _userFactory.AsExceptional()
                                    .ThenExecute(
                                        _ => JsonConvert.SerializeObject(_.CreateUserWithKey(edition, permissions))),
                            result => result.IsSuccessful)
                        .Value);

            var user = JsonConvert.DeserializeObject<DynamicUser>(userString);
            var key  = user.SessionKey;

            PropertyBucket.Remember(AccountsService.SessionKey, key, true);
            PropertyBucket.Remember(USER_KEY, user, true);
            return key;
        }

        [Given(@"shared session for '(.*)' user with edition '(.*)', datagroup '(.*)'")]
        public string GivenSharedSessionForUserWithEditionAndDg(DynamicUser.PermissionType permissions, string edition, string datagroup)
        {
            var key = SessionKeyCache.Instance()
                .GetOrAdd(edition + permissions + datagroup, k =>
                    new Poller(LOGIN_TIMEOUT)
                        .TryUntil(() =>
                                _userFactory.AsExceptional()
                                    .ThenExecute(_ => _.CreateUserWithKey(edition, permissions, dataGroupsCsv: datagroup).SessionKey),
                            result => result.IsSuccessful)
                        .Value);

            PropertyBucket.Remember(AccountsService.SessionKey, key, true);
            return key;
        }

        [When(@"I login to Company (.*) with (.*) and (.*) and (.*)")]
        [Given(@"I login to Company (.*) with (.*) and (.*) and (.*)")]
        public void WhenILoginToCompanyWithUserAndPassowrdAndLCID(string companyId, string username, string password, string lcid)
        {

            var response = _accountsService.Login(companyId, username, password, lcid);
            PropertyBucket.Remember(CONNECT_RESPONSE_KEY, response);
        }

        #endregion

        #region Then Steps

        [Then(@"the session key does not exist in the PropertyBucket")]
        public void ThenTheSessionKeyDoesNotExistInThePropertyBucket()
        {
            NUnit.Framework.Assert.Throws<Zukini.PropertyNotFoundException>(() => PropertyBucket.GetProperty<string>(AccountsService.SessionKey),
                StackTraceErrorAppender.AddMultipleLines("Session key not in Property Bucket"));
        }

        [Then(@"the user does not exist in the PropertyBucket")]
        public void ThenTheUserDoesNotExistInThePropertyBucket()
        {
            NUnit.Framework.Assert.Throws<Zukini.PropertyNotFoundException>(() => PropertyBucket.GetProperty<User>(ApiHooks.ScenarioUserKey),
                StackTraceErrorAppender.AddMultipleLines("User not in Property Bucket"));
        }

        [Then(@"the session key exists in the PropertyBucket")]
        public void ThenTheSessionKeyExistsInThePropertyBucket()
        {
            var sessionKey = PropertyBucket.GetProperty<string>(AccountsService.SessionKey);
            Assert.IsNotNull(sessionKey, "Session key is null");
            Assert.IsNotEmpty(sessionKey, "Session key is empty");
        }

        [Then(@"the user exists in the PropertyBucket")]
        public void ThenTheUserExistsInThePropertyBucket()
        {
            var user = PropertyBucket.GetProperty<User>(ApiHooks.ScenarioUserKey);
            Assert.IsNotNull(user, "User is null");
            Assert.IsNotNull(user.CompanyID, "Company id is null");
            Assert.IsNotNull(user.Username, "Username null");
            Assert.IsNotNull(user.Password, "Password null");
        }

        [Then(@"I should see the message (.*)")]
        public void ThenIShouldSeeTheMessage(string message)
        {
            string m = "";
            Dictionary<string, string> response = PropertyBucket.GetProperty<Dictionary<string, string>>(CONNECT_RESPONSE_KEY);
            response.TryGetValue("Message", out m);
            Assert.AreEqual(message, m);
        }

        [Then(@"I should see the proper AutoLogin response")]
        public void ThenIShouldSeeTheProperAutoLoginResponse()
        {            
            IRestResponse response = PropertyBucket.GetProperty<IRestResponse>(AUTO_LOGIN_RESPONSE);                        
            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [Then(@"I should see the proper SSO AutoLogin (.*) - (.*) - (.*) response")]
        public void ThenIShouldSeeTheProperSSOAutoLoginOkta_JtSingleSignOnIsNotSetUpForThisCompanyResponse(string expectedStatusCode, string expectedIdentityProvider, string expectedMessage)
        {
            Dictionary<string, string> response = PropertyBucket.GetProperty<Dictionary<string, string>>(AUTO_LOGIN_SSO_RESPONSE);            
            string statusCode = "";
            string message = "";
            string identityProvider = "";
            response.TryGetValue("StatusCode", out statusCode);
            response.TryGetValue("Message", out message);
            response.TryGetValue("IdentityProvider", out identityProvider);
            Assert.AreEqual(expectedStatusCode, statusCode);
            Assert.AreEqual(expectedIdentityProvider, identityProvider);
            Assert.AreEqual(expectedMessage , message);
        }


        [Then(@"the session key should be empty")]
        public void ThenTheSessionKeyShouldBeEmpty()
        {
            string sessionKey = "";
            Dictionary<string, string> response = PropertyBucket.GetProperty<Dictionary<string, string>>(CONNECT_RESPONSE_KEY);
            response.TryGetValue("SessionKey", out sessionKey);
            Assert.AreEqual(string.Empty, sessionKey);
        }

        [Then(@"the connect status code should be (.*)")]
        public void ThenTheConnectStatusCodeShouldBe(string status)
        {
            string statusCode = "";
            Dictionary<string, string> response = PropertyBucket.GetProperty<Dictionary<string, string>>(CONNECT_RESPONSE_KEY);
            response.TryGetValue("StatusCode", out statusCode);
            Assert.AreEqual(status, statusCode);
        }

        [Then(@"I verify Activation code it's stored in userparameter table for (.*)")]
        public void ThenIVerifyActivationCodeItSStoredInUserparameterTable(int index)
        {
            var companyId = Int32.Parse(PropertyBucket.GetProperty<string>(COMPANY_ID_KEY));
            List<object> userAccountId = PropertyBucket.GetProperty<List<object>>(USER_ACCOUNT_ID_KEY);
            var activationCode = 0;
            using (var _accountsDbService = new AccountsDbService(companyId))
            {
                activationCode = _accountsDbService.getActivationCode(Int32.Parse(userAccountId[index].ToString()));
                PropertyBucket.Remember(ACTIVATION_CODE_KEY, activationCode, true);
            }            
        }

        [Then(@"Login content response has a valid value")]
        public void ThenLoginContentResponseHasAValidValue()
        {
            string loginInfo = "";
            Dictionary<string, string> response = PropertyBucket.GetProperty<Dictionary<string, string>>(CONNECT_RESPONSE_KEY);
            response.TryGetValue("Content", out loginInfo);
            Assert.IsNotNull(loginInfo, "login info is null");
            PropertyBucket.Remember(LOGIN_INFO_KEY, loginInfo);
        }
        [Then(@"I verify response code it's (.*) OK and response has (.*)")]
        public void ThenIVerifyResponseCodeItSOKAndResponseHasInvalidCode(string expectedStatusCode, string expectedMessage)
        {
            string message = "";
            string statusCode = "";
            Dictionary<string, string> response = PropertyBucket.GetProperty<Dictionary<string, string>>(MSA_CONNECT_RESPONSE_KEY);
            response.TryGetValue("Message", out message);
            Assert.IsNotNull(message, "Message is null");
            Assert.AreEqual(expectedMessage, message);
            response.TryGetValue("StatusCode", out statusCode);
            if (!string.IsNullOrEmpty(statusCode))
            {
                Assert.IsNotNull(statusCode, "Status code is null");
                Assert.AreEqual(expectedStatusCode, statusCode);
            }
        }
        [Then(@"Verify response code is '(.*)' and response has both valid session and save msa token to verify I can login with an existing MSA TOKEN")]        
        public void ThenVerifyResponseCodeIsAndResponseHasBothValidSessionAndMsaToken_(string expectedStatusCode)
        {
            string sessionKey = "";
            string msaToken = "";
            string statusCode = "";
            Dictionary<string, string> response = PropertyBucket.GetProperty<Dictionary<string, string>>(MSA_CONNECT_RESPONSE_KEY);
            response.TryGetValue("StatusCode", out statusCode);
            Assert.IsNotNull(statusCode, "Status code is null");
            Assert.AreEqual(expectedStatusCode, statusCode);
            response.TryGetValue("SessionKey", out sessionKey);
            Assert.IsNotNull(sessionKey, "Session key is null");
            response.TryGetValue("MSAToken", out msaToken);
            PropertyBucket.Remember(MSA_TOKEN_KEY, msaToken);            
            Assert.IsNotNull(msaToken, "msaToken is null");
        }
        [Then(@"Verify MSA existing response code is '(.*)' and response has a valid session")]        
        public void ThenVerifyResponseCodeIsAndResponseHasBothValidSession_(string expectedStatusCode)
        {
            string sessionKey = "";
            string statusCode = "";
            Dictionary<string, string> response = PropertyBucket.GetProperty<Dictionary<string, string>>(MSA_EXISTING_CONNECT_RESPONSE_KEY);
            response.TryGetValue("StatusCode", out statusCode);
            Assert.IsNotNull(statusCode, "Status code is null");
            Assert.AreEqual(expectedStatusCode, statusCode);
            response.TryGetValue("SessionKey", out sessionKey);
            Assert.IsNotNull(sessionKey, "Session key is null");
        }
        [Then(@"Verify response code is '(.*)' and response has a valid session")]
        public void ThenVerifyResponseCodeIsAndResponseHasBothValidSession(string expectedStatusCode)
        {
            string sessionKey = "";
            string statusCode = "";
            Dictionary<string, string> response = PropertyBucket.GetProperty<Dictionary<string, string>>(CONNECT_RESPONSE_KEY);
            response.TryGetValue("StatusCode", out statusCode);
            Assert.IsNotNull(statusCode, "Status code is null");
            Assert.AreEqual(expectedStatusCode, statusCode);
            response.TryGetValue("SessionKey", out sessionKey);
            Assert.IsNotNull(sessionKey, "Session key is null");
        }

        [Then(@"I verify Msa code endpoint status code is '(.*)' OK and message is '(.*)'")]
        public void ThenIVerifyMsaCodeEndpointResponseCodeIsOKAndResponseHas(string expectedStatusCode, string expectedMessage)
        {
            string message = "";
            string statusCode = "";
            Dictionary<string, string> response = PropertyBucket.GetProperty<Dictionary<string, string>>(MSA_CODE_RESPONSE_KEY);
            response.TryGetValue("StatusCode", out statusCode);
            Assert.IsNotNull(statusCode, "Status code is null");
            Assert.AreEqual(expectedStatusCode, statusCode);
            response.TryGetValue("Message", out message);
            Assert.IsNotNull(message, "Message is null");
            Assert.AreEqual(expectedMessage, message);
        }

        [Then(@"User parameter (.*) is created for this user with correct value\.")]
        public void ThenUserParameterIsCreatedForThisUserWithCorrectValue_(int lcid)
        {
            var companyId = Int32.Parse(PropertyBucket.GetProperty<string>(COMPANY_ID_LCID_KEY));
            var userAccountId = Int32.Parse(PropertyBucket.GetProperty<string>(USER_ACCOUNT_LCID_KEY));
            using (var _accountsDbService = new AccountsDbService(companyId))
            {
                var userLcid = _accountsDbService.GetLCID(userAccountId);
                Assert.AreEqual (lcid, userLcid, $"LCID was different than expected: {lcid}");
            }
        }



        #region When Steps

        [When(@"I send a POST to msa endpoint with loginInfo and activation code just obtained")]
        public void WhenISendAPOSTToMsaEndpointWithLoginInfoAndActivationCodeJustObtained()
        {
            int activationCode = PropertyBucket.GetProperty<Int32>(ACTIVATION_CODE_KEY);
            var loginInfo = PropertyBucket.GetProperty<string>(LOGIN_INFO_KEY);
            var response = _accountsService.MSALogin(activationCode, loginInfo);
            PropertyBucket.Remember(MSA_CONNECT_RESPONSE_KEY, response);
        }
        [When(@"I send a POST to msa endpoint with login info and (.*)")]
        public void WhenISendAPOSTToMsaEndpointWithAnd(int activationCode)
        {
            string loginInfo = PropertyBucket.GetProperty<string>(LOGIN_INFO_KEY);
            var response = _accountsService.MSALogin(activationCode, loginInfo);
            PropertyBucket.Remember(MSA_CONNECT_RESPONSE_KEY, response);
        }

        [When(@"I sent a POST to AutoLogin endpoint using user credentials")]
        public void WhenISendAPOSTToAutoLoginEndPointUsingUserCredentials()
        {
            string sessionKey = "";
            Dictionary<string, string> response = PropertyBucket.GetProperty<Dictionary<string, string>>(CONNECT_RESPONSE_KEY);
            response.TryGetValue("SessionKey", out sessionKey);
            Assert.IsNotEmpty( sessionKey,"Session shouldn't be empty");
            var responseAutoLogin = _accountsService.autoLogin(sessionKey);
            PropertyBucket.Remember(AUTO_LOGIN_RESPONSE, responseAutoLogin);

        }
        [When(@"I sent a POST to SSO AutoLogin endpoint using this (.*)")]
        public void WhenISentAPOSTToSSOAutoLoginEndpointUsingThisOnpointCompany(string company)
        {
            var responseSSOAutoLogin = _accountsService.SSOAutoLogin(company);
            PropertyBucket.Remember(AUTO_LOGIN_SSO_RESPONSE, responseSSOAutoLogin);
        }

        [When(@"I send a POST to msa code endpoint using this login info for this scenario")]
        public void WhenISendAPOSTToMsaCodeEndpointUsingThisLoginInfoForScenario()
        {
                           
            var loginInfo = PropertyBucket.GetProperty<string>(LOGIN_INFO_KEY);
            var response = _accountsService.ReSendMSACode(loginInfo);
            PropertyBucket.Remember(MSA_CODE_RESPONSE_KEY, response);

        }

        #endregion
        /// <summary>
        /// Clean up for created Keyword searches.
        /// </summary>
        [AfterScenario, Scope(Feature = LOGIN, Tag = "NeedsCleanupSuccess")]
        public void ResetUserMSALoginInfoSuccessForNextTest()
        {
            var companyId = Int32.Parse(PropertyBucket.GetProperty<string>(COMPANY_ID_KEY));
            var userAccountId = Int32.Parse(PropertyBucket.GetProperty<string>(USER_ACCOUNT_ID_SUCCESS_KEY));
            using (var _accountsDbService = new AccountsDbService(companyId))
            {
                _accountsDbService.ResetMSATokenInfo(userAccountId, "1", "MSATokenResent");
                _accountsDbService.ResetMSATokenInfo(userAccountId, "9/19/2050 3:26:15 AM", "MSATokenTimeout");
                _accountsDbService.ResetMSATokenInfo(userAccountId, "5785158", "MSAToken");
            }             
        }
        [AfterScenario, Scope(Feature = LOGIN, Tag = "NeedsCleanupMaxCodeResendsReached")]
        public void ResetUserMSALoginInfoMaxCodeResendsReachedForNextTest()
        {
            var companyId = Int32.Parse(PropertyBucket.GetProperty<string>(COMPANY_ID_KEY));
            var userAccountId = Int32.Parse(PropertyBucket.GetProperty<string>(USER_ACCOUNT_ID_MAX_CODE_RESENDS_REACHED_KEY));
            using (var _accountsDbService = new AccountsDbService(companyId))
            {
                _accountsDbService.ResetMSATokenInfo(userAccountId, "5", "MSATokenResent");
                _accountsDbService.ResetMSATokenInfo(userAccountId, "9/19/2050 3:26:15 AM", "MSATokenTimeout");
                _accountsDbService.ResetMSATokenInfo(userAccountId, "1", "MSAFailedTokenAttempt");
                _accountsDbService.ResetMSATokenInfo(userAccountId, "5785158", "MSAToken");
            }
        }

        [AfterScenario, Scope(Feature = LOGIN, Tag = "NeedsCleanupAccountLocked")]
        public void ResetUserMSALoginInfoAccountLockedForNextTest()
        {
            var companyId = Int32.Parse(PropertyBucket.GetProperty<string>(COMPANY_ID_KEY));
            var userAccountId = Int32.Parse(PropertyBucket.GetProperty<string>(USER_ACCOUNT_ID_ACCOUNT_LOCKED_KEY));
            using (var _accountsDbService = new AccountsDbService(companyId))
            {
                _accountsDbService.ResetMSATokenInfo(userAccountId, "5", "MSATokenResent");
                _accountsDbService.ResetMSATokenInfo(userAccountId, "9/19/2050 3:26:15 AM", "MSATokenTimeout");
                _accountsDbService.ResetMSATokenInfo(userAccountId, "10", "MSAFailedTokenAttempt");
                _accountsDbService.ResetMSATokenInfo(userAccountId, "5785158", "MSAToken");
                _accountsDbService.ResetUserAccountIsLocked(userAccountId, "false");
                _accountsDbService.ResetUserFailedAttempts(userAccountId, "0");
            }
        }

        [AfterScenario]
        public void MakeSureRegistrationTasksComplete()
        {
            // Registration of user deletion is implemented via using Task conception 
            // in order to start test execution immediately after user is created and 
            // to save a save on test run. But there is no guarantee Task will be completed if 
            // Test execution time is less than registration task time.
            // So here we explicitly make sure to await all registered tasks.
            if (_userFactory.RegistrationTasks.Any())
                Task.WaitAll(_userFactory.RegistrationTasks.ToArray());
        }
        #endregion
    }
}
