using CCC_API.Data.Responses.Analytics;
using System;
using static CCC_API.Services.Analytics.Common;

namespace CCC_API.Services.Analytics.Mentions
{
    /// <summary>
    /// Service that calls the company impact api endpoints.
    /// </summary>
    /// <seealso cref="CCC_API.Services.Analytics.BaseMentionsService" />
    public class ImpactService : BaseMentionsService
    {
        protected override string EndPoint => "news/analytics/company_impact";

        public ImpactService(string sessionKey) : base(sessionKey)
        {
        }

        /// <summary>
        /// Gets the company impact as a WidgetData response object
        /// </summary>
        /// <param name="typeId">The type identifier (line=1, donut=2, etc).</param>
        /// <param name="yAxisMetric">The y axis metric (total or average).</param>
        /// <param name="frequency">The frequency (daily=365, weekly=52, monthly=12 or yearly=1).</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns>WidgetData response</returns>
        public WidgetData GetCompanyImpact(TypeId typeId, YAxisMetric yAxisMetric, Frequency frequency = Frequency.None, DateTime? startDate = null, DateTime? endDate = null)
        {
            var request = GetMentionsRequest(typeId, frequency, startDate, endDate);
            request.AddUrlQueryParam("yaxismetric", ((int)yAxisMetric).ToString());
            return ExecuteRequest(request);
        }
    }
}
