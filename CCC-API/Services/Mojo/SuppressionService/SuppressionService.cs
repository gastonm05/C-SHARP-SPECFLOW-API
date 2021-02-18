using CCC_API.Data.Request;
using CCC_API.Data.Request.Mojo;
using CCC_API.Data.Responses.Mojo;
using CCC_API.Utils;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CCC_API.Services.Mojo.SuppressionService
{
    public class SuppressionService : MojoBaseService
    {
       
        public const string SuppresionUATEndPoint = "https://uatblacklist.mygorkana.com/";
        public override Uri BaseDomainUri => new Uri(SuppresionUATEndPoint);
        public const string resourceSeach = "search/email";
        public const string resourceLogin = "login";

        private  string _sessionKey => Login();

        /// <summary>
        /// Generates an Authorization token
        /// Suppression Login EP that Mygorkana app accesses--Only accessible internally
        /// </summary>
        /// <returns></returns>
        public string Login()
        {
            var payload = new SuppressionLoginRequest
            {
                username = "admin",
                password = "password"
            };

            var response = Request().Post()
                            .ToEndPoint(SuppresionUATEndPoint+resourceLogin)
                            .Data(payload)
                            .ExecCheck(HttpStatusCode.OK);

            return response.Headers[0].Value.ToString();
        }
        /// <summary>
        /// Search for an email address to check the suppresses status
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        public SuppressionResponse Search(string emailAddress)
        {
           // var key = Login();

            var payLoad = new SuppressionSearchRequest
            {
                email = emailAddress
            };

            //var response = Request().Post().AddHeader("Authorization",_sessionKey)
            //                .ToEndPoint(BaseDomainUri + resourceSeach)
            //                .Data(payLoad)
            //                .ExecCheck<SuppressionResponse>(HttpStatusCode.OK);

            var restClient = new RestClient(BaseDomainUri);
            var restRequest = new RestRequest(resourceSeach, Method.POST);

            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddHeader("Authorization", _sessionKey);

            restRequest.AddJsonBody(new
            {
                email = emailAddress
            });

            var response = restClient.Execute<SuppressionResponse>(restRequest);

            return response.Data;

        }
    }
}
