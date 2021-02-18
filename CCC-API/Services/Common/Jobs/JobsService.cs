using CCC_API.Data.Responses.Common;
using CCC_Infrastructure.API.Utils;
using RestSharp;
using System.Net;

namespace CCC_API.Services.Common.Jobs
{
    public class JobsService : AuthApiService
    {
        public JobsService(string sessionKey) : base(sessionKey) { }
                
        public static string JobsEndPoint = "jobs/";

        /// <summary>
        /// Gets a specific job
        /// </summary>
        /// <param name="id">Job id</param>
        /// <returns></returns>
        public IRestResponse<JobResponse> GetJobs(int id)
        {
            return Request().ToEndPoint($"{JobsEndPoint}{id}").Get().ExecUntil<JobResponse>(
                new Poller(), r => r.StatusCode == HttpStatusCode.OK);
        }

    }
}
