using CCC_API.Data.Responses.Analytics;
using CCC_API.Data.Responses.News;
using CCC_Infrastructure.API.Utils;
using CCC_Infrastructure.Utils;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using static CCC_API.Services.Analytics.Common;

namespace CCC_API.Services.Analytics.Mentions
{
    public abstract class BaseMentionsService : AuthApiService
    {
        public const string DATE_TIME_FORMAT = "yyyy-MM-ddT00:00:00.000Z";
        public BaseMentionsService(string sessionKey) : base(sessionKey)
        {
        }

        /// <summary>
        /// Each class that inherits from BaseMentionsService should specify their EndPoint
        /// </summary>
        protected abstract string EndPoint { get; }

        /// <summary>
        /// Gets the basic mentions request for Analytics Data Series end points.
        /// </summary>
        /// <param name="typeId">The type identifier (line=1, donut=2, etc).</param>
        /// <param name="frequency">The frequency (daily=365, weekly=52, monthly=12 or yearly=1).</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns>basic RestBuilder that should be completed and executed by classes that inherit from BaseMentionsService</returns>
        protected RestBuilder GetMentionsRequest(TypeId typeId, Frequency frequency, DateTime? startDate = null, DateTime? endDate = null)
        {
            var request = Request().Get().ToEndPoint(EndPoint);
            request.AddUrlQueryParam("CreateScratchTable", "true");
            request.AddUrlQueryParam("TypeId", ((int)typeId).ToString());
            if (frequency != Frequency.None) // optional
            {
                request.AddUrlQueryParam("frequency", ((int)frequency).ToString());
            }
            string dateTimeFormatted;
            if (startDate == null)
            {
                // default to 30 day window
                dateTimeFormatted = DateTime.Now.AddDays(-30).ToString(DATE_TIME_FORMAT);
            }
            else
            {
                dateTimeFormatted = startDate.Value.ToString(DATE_TIME_FORMAT);
            }
            request.AddUrlQueryParam("StartDate", dateTimeFormatted);
            if (endDate == null)
            {
                // default to today
                dateTimeFormatted = DateTime.Now.ToString(DATE_TIME_FORMAT);
            }
            else
            {
                dateTimeFormatted = endDate.Value.ToString(DATE_TIME_FORMAT);
            }
            request.AddUrlQueryParam("EndDate", dateTimeFormatted);
            return request;
        }

        /// <summary>
        /// Executes the request and will retry the get for mentions if
        /// the first attempt times out.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        protected WidgetData ExecuteRequest(RestBuilder request)
        {
            try
            {
                return request.ExecContentCheck<WidgetData>();
            }
            catch (Exception e) // operation has timed out
            {
                if (e.Message.Contains("The operation has timed out"))
                {
                    // try again
                    return request.ExecContentCheck<WidgetData>();
                }
                else
                {
                    throw e;
                }
            }
        }

        /// <summary>
        /// Gets widget with specified settings.
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public IRestResponse<WidgetData> GetWidgetWithSettings(AnalyticsWidgetSettings settings)
        {
            var endpoint = settings.AsRequestString();
            return Request().Get().ToEndPoint(endpoint).Exec<WidgetData>();
        }
        
        /// <summary>
        /// Retrieves start date based on frequency. 
        /// </summary>
        /// <param name="frequency"></param>
        /// <returns></returns>
        public Func<DateTime, DateTime> GetStartDateLogicBasedOnFrequency(Frequency frequency) =>
            date =>
            {
                switch (frequency)
                {
                    case Frequency.None:
                        return date;
                    case Frequency.Daily:
                        return date;
                    case Frequency.Weekly:
                        return date.FirstDayOfWeek(CultureInfo.InvariantCulture);
                    case Frequency.Monthly:
                        return date.FirstDayOfMonth();
                    case Frequency.Yearly:
                        return date.FirstDayOfYear();
                    default:
                        throw new ArgumentOutOfRangeException(nameof(frequency), frequency, Err.Msg("Unknown frequency"));
                }
            };

        /// <summary>
        /// Series grouping logic based on series name.
        /// </summary>
        /// <param name="series"></param>
        /// <returns></returns>
        public Func<List<NewsItem>, decimal> GetSeriesLogic(string series)
        {
            var caseInv = series.ToLower();
            switch (caseInv)
            {
                case "mentions":
                    return listOfNews => listOfNews.Count;
                case "publicity value":
                    return listOfNews => (decimal) listOfNews.Sum(news => news.PublicityValue);
                case "reach":
                    return listOfNews => (decimal) listOfNews.Sum(news => news.CirculationAudience);
                case "uvpm":
                    return listOfNews => listOfNews.Sum(news => news.UniqueVisitors);
                case "positive":
                    return listOfNews => listOfNews.Count(news => news.Tone?.Id == (int) ToneId.Positive);
                case "negative":
                    return listOfNews => listOfNews.Count(news => news.Tone?.Id == (int) ToneId.Negative);
                case "neutral":
                    return listOfNews => listOfNews.Count(news => news.Tone?.Id == (int) ToneId.Neutral);
                default:
                    throw new ArgumentOutOfRangeException(nameof(series), series, Err.Msg("Unknown series. Please add implementation for those"));
            }
        }

    }
}
