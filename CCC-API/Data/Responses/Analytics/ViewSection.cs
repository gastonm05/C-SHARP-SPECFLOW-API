using System.Collections.Generic;

namespace CCC_API.Data.Responses.Analytics
{
    /// <summary>
    /// Analytics view widget. Part of analytics/dashboards/views.
    /// </summary>
    public class ViewSection
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SortOrder { get; set; }
        public int SystemId { get; set; }
        public List<Widget> Widgets { get; set; }
    }
}