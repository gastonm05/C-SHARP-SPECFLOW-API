using Newtonsoft.Json;
using System.Collections.Generic;

namespace CCC_API.Data.PostData.Analytics
{
    public class SectionBody
    {
        public int Id { get; set; }
        public int ViewId { get; set; }
        public string Name { get; set; }
        public bool IsNameTranslatable { get; set; }
        public int SortOrder { get; set; }
        public int SystemId { get; set; }
        public List<Widget> Widgets { get; set; }
        public string Tooltip { get; set; }
        public bool IsTooltipTranslatable { get; set; }
        public string Icon { get; set; }

        public class Widget
        {
            public int Id { get; set; }
            public int AnalyticsSectionId { get; set; }
            public string Name { get; set; }
            public bool IsNameTranslatable { get; set; }
            public int SortOrder { get; set; }
            public string Size { get; set; }
            public int AvailableWidgetId { get; set; }
            public List<WidgetOptionValues> WidgetOptionValues { get; set; }
            public int SystemId { get; set; }
            public bool IsTooltipTranslatable { get; set; }
        }

        public class WidgetOptionValues
        {
            public int availableWidgetOptionId { get; set; }
            public int? availableWidgetOptionValueId { get; set; }
            public string customOptionValue { get; set; }
        }
    }
}
