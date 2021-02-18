using System.Collections.Generic;

namespace CCC_API.Data.Responses.Settings.FormManagement
{
    public class ListFormsActivities
    {
        public int ItemCount { get; set; }
        public List<FormsActivity> Items { get; set; }
        public Links _links { get; set; }
        public object _meta { get; set; }
    }
}
