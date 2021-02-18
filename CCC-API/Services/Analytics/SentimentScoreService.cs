using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CCC_API.Data.Responses.Analytics;

namespace CCC_API.Services.Analytics
{
    public class SentimentScoreService : AuthApiService
    {
        public static string SentimentScoreuri = "news/analytics/company_sentiment_score";
        public const string DATE_TIME_FORMAT = "yyyy-MM-ddT00:00:00.000Z";

        public SentimentScoreService(string sessionKey) : base(sessionKey) { }

        /// <summary>
        /// Gets Sentiment Score widget data 
        /// </summary>
        /// <returns DMAWidget></returns>
        /// 
        public SentimentScoreWidget GetSentimentScore(string type)
        {
            var endDateTimeFormatted = DateTime.Now.ToString(DATE_TIME_FORMAT);
            var startDateTimeFormatted = DateTime.Today.AddDays(-30).ToString(DATE_TIME_FORMAT);

            Common.TypeId typeId = (Common.TypeId)(Enum.Parse(typeof(Common.TypeId), type));

            string endpoint = $"{SentimentScoreuri}?calculation=2&criteria=0&dataSet=92&datalabel=0&endDate={endDateTimeFormatted}&frequency=1&maxseries=10&startDate={startDateTimeFormatted}&typeId={(int)typeId}";
            return Request().Get().ToEndPoint(endpoint).ExecContentCheck<SentimentScoreWidget>();
        }
    }
}
