using Newtonsoft.Json;
using System.Collections.Generic;

namespace CCC_API.Data.Responses.Analytics
{
    /// <summary>
    /// Analytics widget.
    /// </summary>
    public class Widget
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? SortOrder { get; set; }
        public string Size { get; set; }
        public int? AvailableWidgetId { get; set; }
        public List<object> WidgetOptionValues { get; set; }
        public object Data { get; set; }
        public object StartDate { get; set; }
        public object EndDate { get; set; }
        public int SystemId { get; set; }
        public object Notes { get; set; }
        public string Tooltip { get; set; }


        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? AnalyticsSectionId { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool IsTooltipTranslatable { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Icon { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool IsNameTranslatable { get; set; }
    }
}
