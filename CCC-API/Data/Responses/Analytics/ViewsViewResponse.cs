using System.Collections.Generic;

namespace CCC_API.Data.Responses.Analytics
{
    /// <summary>
    /// response from endpoint api/v1/analytics/dashboards/views
    /// </summary>
    public class ViewsViewResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? AccessMode { get; set; }
        public bool? IsEditable { get; set; }
        public bool? IsOwner { get; set; }
        public bool? IsSystem { get; set; }
    }

    /// <summary>
    /// Provides view response with sections and widgets
    /// </summary>
    public class AvailableViewSectionsWidgets : ViewsViewResponse
    {
        public int SystemId { get; set; }
        public List<ViewSection> Sections { get; set; }
    }
}
