using CCC_API.Data.Responses.Analytics;
using System;
using static CCC_API.Services.Analytics.Common;

namespace CCC_API.Services.Analytics.Mentions
{
    /// <summary>
    /// Service that calls the company prominence and impact api endpoints.
    /// </summary>
    /// <seealso cref="CCC_API.Services.Analytics.BaseMentionsService" />
    public class MentionsOverTimeService : BaseMentionsService
    {
        protected override string EndPoint => "news/analytics/mentions_over_time";

        public MentionsOverTimeService(string sessionKey) : base(sessionKey)
        {
        }

        public enum MentionsCalculation { None = 0, Momentum = 1 }

        /// <summary>
        /// Gets the mentions momentum as a WidgetData response object
        /// </summary>
        /// <param name="typeId">The type identifier (line=1, donut=2, etc).</param>
        /// <param name="calculation">The calculation to use (none or momentum).</param>
        /// <param name="frequency">The frequency (daily=365, weekly=52, monthly=12 or yearly=1).</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns>WidgetData response</returns>
        public WidgetData GetMentionsMomentum(TypeId typeId, MentionsCalculation calculation, Frequency frequency, DateTime? startDate = null, DateTime? endDate = null)
        {
            var request = GetMentionsRequest(typeId, frequency, startDate, endDate);
            request.AddUrlQueryParam("calculation", ((int)calculation).ToString());
            return ExecuteRequest(request);
        }
    }
}
