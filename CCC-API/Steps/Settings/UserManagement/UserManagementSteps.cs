using BoDi;
using CCC_API.Data.PostData.Settings.UserManagement;
using CCC_API.Data.Responses.Settings.UserManagement;
using CCC_API.Services;
using CCC_API.Services.Common.db;
using CCC_API.Steps.Common;
using CCC_API.Steps.PrNewswire;
using CCC_Infrastructure.Utils;
using Newtonsoft.Json;
using CCC_API.Utils.Assertion;
using RestSharp;
using System;
using System.Net;
using TechTalk.SpecFlow;
using Zukini;



namespace CCC_API.Steps.Settings.UserManagement
{
    [Binding]
    public class UserManagementSteps : AuthApiSteps
    {

        private const string COMPANY_ID_FORGOT_PASSWORD_KEY = "CompanyId";
        private const string FORGOT_PASSWORD_USER_EMAIL_ADDRESS_KEY = "EmailAddress";
        private const string FORGOT_PASSWORD_LCID_KEY = "Lcid"; 
        private const string GET_USER_RESPONSE_KEY = "GetUserResponse";
        private const string POST_RESPONSE_FORGOT_PASSWORD_KEY = "PostResponseForgotPassword";
        private const string POST_RESPONSE_REQUEST_AUTHORIZATION_SENDER_KEY ="PostRequestAuthorizationSenderResponse";
        private const string POST_RESPONSE_RESET_PASSWORD_KEY = "PostResetPasswordResponse";
        private const string POST_USERMANAGER_SAVEUSER_RESPONSE_KEY = "SaveUserResponse";
        private const string PUT_USERMANAGER_UPDATEUSER_RESPONSE_KEY = "UpdateResponse";
        private const string USER_ACCOUNT_ID_FORGOT_PASSWORD_KEY = "UserAccountId";
        private const string USER_ACCOUNT_ID_REQUEST_AUTHORIZATION_SENDER_KEY = "UserAccountIdRequestAuthorizationSender";
        private const string USER_ACCOUNT_RESET_PASSWORD_KEY = "UserAccountResetPassowrdId";
        private const string USER_ID_KEY = "UserAccountID";

        private readonly AccountsService _accountsService = new AccountsService();
        public enum SecurityTypePassword {Advanced, Regular, NoSpecialChars, AnyNumberChar, AnyLetterChar, SamePassword }

        public UserManagementSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
        }

        [When(@"I perform a POST for management/user/management endpoint")]
        public void WhenIPerformAPOSTForManagementUserManagementEndpoint()
        {
            IRestResponse<UserManagementPostData> getOMCuserResponse = PropertyBucket.GetProperty<IRestResponse<UserManagementPostData>>(GET_USER_RESPONSE_KEY);
            UserManagementPostData user = JsonConvert.DeserializeObject<UserManagementPostData>(getOMCuserResponse.Content);
            user.Id = 0;
            user.Password = "33";
            user.PasswordConfirm = "33";
            user.LoginId = "Repetead OMC User ID User";
            var saveUserResponse = new UserManagementService(SessionKey).SaveUser(user);
            PropertyBucket.Remember(POST_USERMANAGER_SAVEUSER_RESPONSE_KEY, saveUserResponse);
        }

        [Then(@"Endpoint response code should be (.*)")]
        public void ThenEndpointResponseCodeShouldBe(int responseCode)
        {
            IRestResponse<UserManagementPostData> response = PropertyBucket.GetProperty<IRestResponse<UserManagementPostData>>(POST_USERMANAGER_SAVEUSER_RESPONSE_KEY);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode, response.Content);
        }

        [Then(@"the response Message should be ""(.*)""")]
        public void ThenTheResponseMessageShouldBe(string validationMessage)
        {
            IRestResponse<UserManagementPostData> badRequestResponse = PropertyBucket.GetProperty<IRestResponse<UserManagementPostData>>(POST_USERMANAGER_SAVEUSER_RESPONSE_KEY);
            Assert.IsTrue(badRequestResponse.Content.Contains(validationMessage), "Message differed from requested");
        }

        [When(@"I perform a GET for management/user/management  endpoint to get existing OMC user")]
        public void WhenIPerformAGETForManagementUserManagementEndpointToGetExistingOMCUser()
        {
            var userAccountId= Int32.Parse(PropertyBucket.GetProperty<string>(USER_ID_KEY));
            var response = new UserManagementService(SessionKey).GetUserByUserAccountId(userAccountId);
            PropertyBucket.Remember(GET_USER_RESPONSE_KEY, response);

        }
        [When(@"I perform a PUT for management/user/management endpoint to set '(.*)' as default page")]
        public void WhenIPerformAPUTForManagementUserManagementEndpointToSetAsDefaultPage(string defaultPage)
        {
            IRestResponse<UserManagementPostData> getResponse = PropertyBucket.GetProperty<IRestResponse<UserManagementPostData>>(GET_USER_RESPONSE_KEY);
            UserManagementPostData user = JsonConvert.DeserializeObject<UserManagementPostData>(getResponse.Content);
            user.CurrentPassword = null;
            user.Password = "";
            user.PasswordConfirm = "";
            user.DefaultSection = defaultPage;
            var userManagement = new UserManagementService(SessionKey);
            PropertyBucket.Remember(PUT_USERMANAGER_UPDATEUSER_RESPONSE_KEY, userManagement.UpdateUserCheck(user));
        }

        [Then(@"I verify PUT transaction was successfully completed")]
        public void ThenIVerifyPUTTransctionWasSuccessfullyCompleted()
        {
            Assert.IsTrue(PropertyBucket.GetProperty<bool>(PUT_USERMANAGER_UPDATEUSER_RESPONSE_KEY), Err.Msg("There was a problem with update transaction"));
        }
        [When(@"I perform a POST for management/user/management/RequestAuthorizationSender endpoint to send a Sender Request Authorization")]
        public void WhenIPerformAPOSTForManagementUserManagementRequestAuthorizationSenderEndpointToSendASenderRequestAuthorizationForLanguage()
        {
            var userAccountId = Int32.Parse(PropertyBucket.GetProperty<string>(USER_ACCOUNT_ID_REQUEST_AUTHORIZATION_SENDER_KEY));
            RequestAutorizationSenderPostData requestAutorizationSenderPostData = new RequestAutorizationSenderPostData(userAccountId);
            var postResponse = new UserManagementService(SessionKey).SendRequestAutorization(requestAutorizationSenderPostData);
            PropertyBucket.Remember(POST_RESPONSE_REQUEST_AUTHORIZATION_SENDER_KEY, postResponse);
        }

        /// <summary>
        /// Validates the Status Code Response from an endpoint
        /// </summary>
        [Then(@"Request Authorization Sender Endpoint response should be '(.*)'")]
        public void ThenRequestAuthorizationSenderEndpointResponseShouldBe(int responseCode)
        {
            IRestResponse<Object> response = PropertyBucket.GetProperty<IRestResponse<Object>>(POST_RESPONSE_REQUEST_AUTHORIZATION_SENDER_KEY);
            Assert.AreEqual(responseCode, Services.BaseApiService.GetNumericStatusCode(response), response.Content);
        }

        [Then(@"Request Authorization Sender Endpoint response message should be '(.*)'")]
        public void ThenRequestAuthorizationSenderEndpointResponseMessageShouldBe(string responseMessage)
        {
            IRestResponse<Object> response = PropertyBucket.GetProperty<IRestResponse<Object>>(POST_RESPONSE_REQUEST_AUTHORIZATION_SENDER_KEY);
            Assert.IsTrue(response.Content.Contains(responseMessage), "Message '{response.Content}' differ from expected '{responseMessage}'");
        }

        [Given(@"I perform a POST to api/v1/management/user/password/forgot endpoint to start reset password flow")]
        public void GivenIPerformAPOSTToApiVManagementUserPasswordForgotEndpointToStartResetPasswordFlow()
        {
            var emailaddress = PropertyBucket.GetProperty<string>(FORGOT_PASSWORD_USER_EMAIL_ADDRESS_KEY);
            var lcid = PropertyBucket.GetProperty<string>(FORGOT_PASSWORD_LCID_KEY);
            ForgotPasswordPostData forgotPasswordPostData = new ForgotPasswordPostData();
            forgotPasswordPostData.Email = emailaddress;
            forgotPasswordPostData.Lcid = lcid;
            var postResponse = new UserManagementService(SessionKey).StartForgotPasswordFlow(forgotPasswordPostData);
            PropertyBucket.Remember(POST_RESPONSE_FORGOT_PASSWORD_KEY, postResponse);
        }

        [Then(@"Forgot Password Endpoint response should be '(.*)'")]
        public void ThenForgotPassowrdEndpointResponseShouldBe(int responseCode)
        {
            
            IRestResponse response = PropertyBucket.GetProperty<IRestResponse>(POST_RESPONSE_FORGOT_PASSWORD_KEY);
            Assert.AreEqual(responseCode, Services.BaseApiService.GetNumericStatusCode(response), response.Content);
        }

        [Then(@"I get from DB recently created UserAccountResetPasswordid for this user")]
        public void ThenIGetFromDBRecenltyCreatedUserAccountResetPasswordidForThisUser()
        {
            var companyId = Int32.Parse(PropertyBucket.GetProperty<string>(COMPANY_ID_FORGOT_PASSWORD_KEY));
            var userAccountId = Int32.Parse(PropertyBucket.GetProperty<string>(USER_ACCOUNT_ID_FORGOT_PASSWORD_KEY));
            using (var _accountsDbService = new AccountsDbService(companyId))
            {
                var userAccountResetPasswordId= _accountsDbService.GetUserAccountResetPasswordid(userAccountId);
                PropertyBucket.Remember(USER_ACCOUNT_RESET_PASSWORD_KEY, userAccountResetPasswordId, true);
            }
        }

        [When(@"I perform a POST on Reset Password Endpoint using '(.*)' password security")]
        public void WhenIPerformAPOSTOnResetPasswordEndpointUsingPasswordSecurity(SecurityTypePassword typePassword)
        {
            string newPassword = "";
            switch (typePassword)
            {
                case SecurityTypePassword.Advanced:
                    {
                        newPassword = Guid.NewGuid().ToString();
                        break;
                    }
                case SecurityTypePassword.Regular:
                    {
                        newPassword = Guid.NewGuid().ToString().Substring(0, 3);
                        break;
                    }
                case SecurityTypePassword.NoSpecialChars:
                    {
                        newPassword = "NoSpecialChar2000";
                        break;
                    }
                case SecurityTypePassword.AnyNumberChar:
                    {
                        newPassword = "VeranoAAAAAA";
                        break;
                    }
                case SecurityTypePassword.AnyLetterChar:
                    {
                        newPassword = "123456789++$$";
                        break;
                    }
                case SecurityTypePassword.SamePassword:
                    {
                        newPassword = "Verano2018$";
                        break;
                    }
            }
            var companyId = Int32.Parse(PropertyBucket.GetProperty<string>(COMPANY_ID_FORGOT_PASSWORD_KEY));
            var userAccountResetPasswordId = PropertyBucket.GetProperty<string>(USER_ACCOUNT_RESET_PASSWORD_KEY);
            ResetPasswordPostData resetPasswordPostData = new ResetPasswordPostData(companyId, userAccountResetPasswordId, newPassword.ToString());
            var postResponse = new UserManagementService(SessionKey).ResetPassword(resetPasswordPostData);
            PropertyBucket.Remember(POST_RESPONSE_RESET_PASSWORD_KEY, postResponse);
        }

        [Then(@"Reset Password Endpoint response should be '(.*)' and status code be '(.*)'")]
        public void ThenResetPasswordEndpointResponseShouldBeAndStatusCodeBe(int responseCode, int statusCode)
        {
            IRestResponse<ResetPasswordResponse> response = PropertyBucket.GetProperty<IRestResponse<ResetPasswordResponse>>(POST_RESPONSE_RESET_PASSWORD_KEY);
            Assert.AreEqual(responseCode, Services.BaseApiService.GetNumericStatusCode(response), response.Content);
            Assert.AreEqual(statusCode, response.Data.StatusCode, response.Content);
        }
    }
}
