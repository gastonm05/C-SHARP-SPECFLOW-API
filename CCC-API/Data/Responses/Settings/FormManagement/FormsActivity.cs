using System.Collections.Generic;

namespace CCC_API.Data.Responses.Settings.FormManagement
{
    public class FormsActivity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public object Icon { get; set; }
        public object Color { get; set; }
        public List<Field> Fields { get; set; }
        public Meta _meta { get; set; }
    }
}
