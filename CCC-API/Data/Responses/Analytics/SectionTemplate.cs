
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CCC_API.Data.Responses.Analytics
{
    public class SectionTemplate : ViewSection
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int ViewId { get; set; }

        public bool IsNameTranslatable { get; set; }
        public string Tooltip { get; set; }
        public bool IsTooltipTranslatable { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Icon { get; set; }

        public List<WidgetTemplate> WidgetTemplate { get; set; }
    }
}
