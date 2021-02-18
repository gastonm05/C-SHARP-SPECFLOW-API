using CCC_API.Data.Responses.Analytics;
using System;
using static CCC_API.Services.Analytics.Common;

namespace CCC_API.Services.Analytics.Mentions
{
    /// <summary>
    /// Service that calls the company mindshare (company mentions) api endpoints.
    /// </summary>
    /// <seealso cref="CCC_API.Services.Analytics.BaseMentionsService" />
    public class CompanyMindshareService : BaseMentionsService
    {
        protected override string EndPoint => "news/analytics/company_mindshare";

        public CompanyMindshareService(string sessionKey) : base(sessionKey)
        {
        }

        /// <summary>
        /// Gets the company mindshare as a WidgetData response object
        /// </summary>
        /// <param name="typeId">The type identifier (line=1, donut=2, etc).</param>
        /// <param name="frequency">The frequency (daily=365, weekly=52, monthly=12 or yearly=1).</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns>WidgetData response</returns>
        public WidgetData GetCompanyMentions(TypeId typeId, Frequency frequency = Frequency.None, DateTime? startDate = null, DateTime? endDate = null)
        {
            var request = GetMentionsRequest(typeId, frequency, startDate, endDate);
            return ExecuteRequest(request);
        }
    }
}
