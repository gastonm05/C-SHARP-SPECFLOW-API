using System.Collections.Generic;
using System.Linq;

namespace CCC_API.Data.Responses.Analytics.Available
{
    public class AvailableWidgetOption
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ParameterName { get; set; }
        public List<AvailableWidgetOptionValue> AvailableWidgetOptionValues { get; set; }
        public List<AvailableWidgetOptionValueGroup> AvailableWidgetOptionValueGroups { get; set; }
        public string TooltipLanguageKey { get; set; }
        public bool IsCustom { get; set; }
        public bool AreOptionValuesGrouped { get; set; }

        public bool IsValid()
        {
            return Id > 0 &&
                    !string.IsNullOrWhiteSpace(Name) &&
                    !string.IsNullOrWhiteSpace(ParameterName) &&
                    AvailableWidgetOptionValues.Count > 0 &&
                    AvailableWidgetOptionValues.All(option => option.IsValid()) &&
                    AvailableWidgetOptionValueGroups.All(g => g.IsValid());
                    // Tooltip intentionally skipped
                    // IsCustom and AreGrouped intentially skipped
        }
    }
}