using CCC_API.Data.Responses.Analytics;
using System;

namespace CCC_API.Services.Analytics
{
    public class DMAWidgetServices : AuthApiService
    {
        public static string DmaWidgetUri = "news/analytics/dma";
        public const string DATE_TIME_FORMAT = "yyyy-MM-ddT00:00:00.000Z";

        public DMAWidgetServices(string sessionKey) : base(sessionKey) { }

        /// <summary>
        /// Gets DMA widget data 
        /// </summary>
        /// <returns DMAWidget></returns>
        public DMAWidget GetDMA(string type)
        {
            var endDateTimeFormatted = DateTime.Now.ToString(DATE_TIME_FORMAT);
            var startDateTimeFormatted = DateTime.Today.AddDays(-30).ToString(DATE_TIME_FORMAT);

            Common.TypeId typeId = (Common.TypeId)(Enum.Parse(typeof(Common.TypeId), type));

            string endpoint = $"{DmaWidgetUri}?dataSet={36}&endDate={endDateTimeFormatted}&startDate={startDateTimeFormatted}&typeId={(int)typeId}";
            return Request().Get().ToEndPoint(endpoint).ExecContentCheck<DMAWidget>();
        }
    }
}
