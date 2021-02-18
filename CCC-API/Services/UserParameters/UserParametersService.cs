using CCC_API.Data.PostData.UserParameter;
using CCC_API.Data.Responses.UserParameters;
using RestSharp;
using System.Collections.Generic;

namespace CCC_API.Services.UserParameters
{
    public class UserParametersService : AuthApiService
    {
        public UserParametersService(string sessionKey) : base(sessionKey) { }

        public const string UserParameterEndpoint = "parameters/user/";

        /// <summary>
        /// Creates a user parameter.
        /// </summary>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="paramValue">The parameter value.</param>
        public IRestResponse<List<UserParameter>> CreateUserParameter(string paramName, string paramValue)
        {
            var data = new UserParameterPostData()
            {
                Name = paramName,
                Value = paramValue
            };
            return Request().Post().ToEndPoint(UserParameterEndpoint).Data(data).Exec<List<UserParameter>>();
        }

        /// <summary>
        /// Gets a complete user parameter (both name and value)
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public IRestResponse<List<UserParameter>> GetUserParameter(string name)
        {
            return Get<List<UserParameter>>($"{UserParameterEndpoint}?names={name}");
        }
        
        /// <summary>
        /// Gets a user parameter value for a given parameter name
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public IRestResponse<UserParameter> GetUserParameterValue(string name)
        {
            return Get<UserParameter>($"{UserParameterEndpoint}?name={name}");
        }
    }
}
