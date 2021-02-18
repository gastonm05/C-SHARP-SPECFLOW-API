using CCC_API.Data.PostData.Settings.UserManagement;
using CCC_API.Data.Responses.Settings.UserManagement;
using CCC_API.Services;
using CCC_API.Utils;
using RestSharp;
using System;
using System.Collections.Generic;

namespace CCC_API.Steps.PrNewswire
{
    public class UserManagementService : AuthApiService
    {
        public static string UserManagementEndPoint = "management/user/management";
        public const string UserManagementGroupsUri = "management/user/management/groups";
        public const string UserManagementUsersList = "management/user/management/list";
        public const string UserManagementRequestAuthorizationSenderEndPoint = "management/user/management/RequestAuthorizationSender";
        public const string UserManagementForgotPasswordEndPoint = "management/user/password/forgot";
        public const string UserManagementResetPasswordEndPoint = "management/user/password/reset"; 

        public UserManagementService(string sessionKey) : base(sessionKey){}

        /// <summary>
        /// Performs a POST to management/user/management endpoint
        /// </summary>
        /// <param name="userManagementPostData"></param>   
        /// <returns>IRestResponse</returns>
        public IRestResponse SaveUser(UserManagementPostData userManagementPostData)
        {
            return Post<UserManagementPostData>(UserManagementEndPoint, GetAuthorizationHeader(), userManagementPostData);
        }

        /// <summary>
        /// Sends data to save a new user.
        /// </summary>
        /// <param name="userManagementPostData">User data - expected to be valid</param>
        public void SaveUserCheck(UserManagementPostData userManagementPostData)
        {
            Request().Post()
                .ToEndPoint(UserManagementEndPoint)
                .Data(userManagementPostData).ExecCheck();
        }

        /// <summary>
        /// Performs a PUT to management/user/management endpoint
        /// </summary>
        /// <param name="userManagementPutData"></param>   
        /// <returns>IRestResponse</returns>
        public IRestResponse UpdateUser(UserManagementPostData userManagementPutData)
        {
            return Put<UserManagementPostData>(UserManagementEndPoint, GetAuthorizationHeader(), userManagementPutData);

        }
        /// <summary>
        /// Sends data to update an existing user.
        /// </summary>
        /// <param name="userManagementPostData">User data - expected to be valid</param>
        public Boolean UpdateUserCheck(UserManagementPostData userManagementPostData)
        {
            try
            {
                   Request().Put().ToEndPoint(UserManagementEndPoint)
                   .Data(userManagementPostData).ExecCheck();
            }
            catch (Exception exception)
            {
                throw exception;                

            }
            return true;
        }

        /// <summary>
        /// Performs a GET to management/user/management endpoint to get specified user
        /// </summary>
        /// <param name="userAccountId"></param>        
        /// <returns> IRestResponse<UserManagementPostData> </returns>
        public IRestResponse<UserManagementPostData> GetUserByUserAccountId(int userAccountId)
        {
            string resource = string.Format(UserManagementEndPoint +
                "/{0}",
             userAccountId);
            return Get<UserManagementPostData>(resource);            
        }

        /// <summary>
        /// Data groups for a user.
        /// </summary>
        /// <returns>List</returns>
        public List<UserGroup> GetUserGroups() => 
            Request().Get()
            .ToEndPoint(UserManagementGroupsUri)
            .ExecCheck<List<UserGroup>>();

        /// <summary>
        /// List of users for a company.
        /// </summary>
        /// <returns>List</returns>
        public List<UserManagementPostData> GetUsers() =>
            Request().Get().ToEndPoint(UserManagementUsersList)
            .ExecCheck<List<UserManagementPostData>>();

        /// <summary>
        /// Performs a POST to management/user/management/RequestAuthorizationSender endpoint
        /// </summary>
        /// <param name="requestAutorizationSenderPostData"></param>   
        /// <returns>IRestResponse</returns>
        public IRestResponse SendRequestAutorization(RequestAutorizationSenderPostData requestAutorizationSenderPostData)
        {
            
            return Post<object>(UserManagementRequestAuthorizationSenderEndPoint, GetAuthorizationHeader(), requestAutorizationSenderPostData);
        }
        /// <summary>
        /// Performs a POST to api/v1/management/user/password/forgot endpoint to start reset password flow
        /// </summary>
        /// <param name="ForgotPasswordPostData"></param>   
        /// <returns>IRestResponse</returns>
        public IRestResponse<object> StartForgotPasswordFlow(ForgotPasswordPostData forgotPasswordPostData)
        {
            var header = new Dictionary<string, string>()
            {
                { "referer", ApiEndpoints.UriBaseDomain.ToString() }
            };
            return Post<object>(UserManagementForgotPasswordEndPoint, header, forgotPasswordPostData);
        }
        /// <summary>
        /// Performs a POST to api/v1/management/user/password/reset endpoint to reset current password
        /// </summary>
        /// <param name="ResetPasswordPostData"></param>   
        /// <returns>IRestResponse</returns>
        public IRestResponse<ResetPasswordResponse> ResetPassword(ResetPasswordPostData resetPasswordPostData)
        {
            var header = new Dictionary<string, string>()
            {
                { "referer", ApiEndpoints.UriBaseDomain.ToString() }
            };

            return Post<ResetPasswordResponse>(UserManagementResetPasswordEndPoint, header, resetPasswordPostData);
        }
    }
}