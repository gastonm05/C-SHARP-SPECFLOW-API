using System;
using System.Collections.Generic;
using System.Linq;
using CCC_API.Data.Responses.Analytics;
using RestSharp;
using CCC_Infrastructure.Utils;

namespace CCC_API.Services.Analytics
{
    public class ShareAnalyticsReportService : AuthApiService
    {
        public const string EmailReportUri = "analytics/report/email";
        public const string EmailRecurringReportUri = "analytics/report/email/recurring";
        
        public ShareAnalyticsReportService(string sessionKey) : base(sessionKey){}

        /// <summary>
        /// Shares analytics email.
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        public IRestResponse ShareEmail(SharedEmailConfig payload)
        {
            return Request().ToEndPoint(EmailReportUri).Post()
                .Data(payload)
                .ExecCheck();
        }

        /// <summary>
        /// Schedules periodic analytics email.
        /// </summary>
        /// <param name="configRecurringEmail"></param>
        /// <returns></returns>
        public IRestResponse SchedulePeriodicEmail(ShareRecurringEmail configRecurringEmail)
        {
            return Request().ToEndPoint(EmailRecurringReportUri).Post().Data(configRecurringEmail).ExecCheck();
        }
        
        /// <summary>
        /// Provides sum for scheduled days.
        /// </summary>
        /// <param name="days"></param>
        /// <returns></returns>
        public static int EncodeScheduledDays(string[] days)
        {
            var codes = new Dictionary<string, int>
            {
                { "Mon", 2 }, { "Tue", 4 }, { "Wed", 8 }, { "Thu",  16 }, { "Fri", 32 }, { "Sat", 64 }, { "Sun", 1 }
            };

            return days.Sum(day =>
            {
                var key = day.Trim();
                if (!codes.ContainsKey(key)) throw new ArgumentException(Err.Msg("Unknown day: " + day));
                var dayCode = codes[key];
                return dayCode;
            });
        }
    }
}
