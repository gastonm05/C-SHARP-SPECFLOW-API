using System;
using System.Linq;
using System.Reflection;

namespace CCC_API.Services.Analytics
{
    /// <summary>
    /// Provides convenient way to send requests with widget configuration.
    /// </summary>
    public class AnalyticsWidgetSettings
    {
        public const string DATE_TIME_FORMAT = "yyyy-MM-ddT00:00:00.000Z";
        public string EndPoint = "news/analytics";

        public string Chart { get; set; }
        public Common.TypeId TypeId { get; set; }
        public Common.ToneId? Tones { get; set; }

        public Common.Frequency? Frequency { get; set; }
        public Common.DataLabel? DataLabel { get; set; }

        public int? Maxseries { get; set; }
        public Common.Calculation? Calculation { get; set; }
        public Common.Annotations? Annotations { get; set; }

        public bool CreateScratchTable { get; set; }
        public DateTime EndDate { get; set; } = DateTime.Now;
        public DateTime StartDate { get; set; } = DateTime.Now.AddYears(-3);

        public Common.YAxisMetric? Yaxismetric { get; set; }

        /// <summary>
        /// Converts settings to url string.
        /// </summary>
        /// <returns>URL suffix</returns>
        public string AsRequestString()
        {
            var excluded = "Chart";
            var par = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(prop => ! excluded.Contains(prop.Name) && prop.GetValue(this) != null)
                .ToDictionary(prop => prop.Name, prop =>
                {
                    var value = prop.GetValue(this);
                    // Is date - apply formating
                    if (value is DateTime)
                    {
                        return ((DateTime) value).ToString(DATE_TIME_FORMAT);
                    }
                    // Is int enum ? - get enum value, not name
                    if (value is Enum && Enum.GetUnderlyingType(value.GetType()).IsAssignableFrom(typeof(int)))
                    {
                        int v = (int) value;
                        return v.ToString();
                    }
                    return value.ToString();
                });

            var pars = string.Join("&", par.Select(kvp => $"{kvp.Key}={kvp.Value}"));
            var url = $"{EndPoint}/{Chart}?{pars}";
            return url;
        }
    }
}
