

namespace CCC_API.Data.Responses.Settings.FormManagement
{
    public class Meta
    {
        public bool IsSystem { get; set; }
        public bool IsCustomActivityEnabled { get; set; }
    }

    public class FieldMeta
    {
        public string Label { get; set; }
        public bool IsAlwaysRequired { get; set; }
        public bool IsAlwaysEnabled { get; set; }
    }
}
