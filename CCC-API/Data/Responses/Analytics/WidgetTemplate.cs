using System.Collections.Generic;

namespace CCC_API.Data.Responses.Analytics
{
    public class WidgetTemplate
    {
        public Category Category { get; set; }
        public List<Widget> Widgets { get; set; }
    }

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}