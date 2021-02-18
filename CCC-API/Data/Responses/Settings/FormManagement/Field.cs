

namespace CCC_API.Data.Responses.Settings.FormManagement
{
    public class Field
    {
        public int Id { get; set; }
        public bool IsRequired { get; set; }
        public bool IsEnabled { get; set; }
        public object SortOrder { get; set; }
        public FieldMeta _meta { get; set; }
    }
}
