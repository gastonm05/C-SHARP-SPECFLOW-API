using System.Collections.Generic;

namespace CCC_API.Data.Responses.Analytics.Available
{
    public class AvailableWidget
    {
        public int Id { get; set; }
        public string LanguageKey { get; set; }
        public string TooltipLanguageKey { get; set; }
        public string[] AxisNames { get; set; }
        public bool ShowLegendTotals { get; set; }
        public AvailableDataSet AvailableDataSet { get; set; }
        public AvailableWidgetType AvailableWidgetType { get; set; }
        public AnalyticsArea AnalyticsArea { get; set; }
        public List<AvailableWidgetOption> AvailableWidgetOptions { get; set; }
    }
}
