namespace CCC_API.Data.Responses.Analytics.Available
{
    public class AvailableWidgetOptionValue
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public LanguageArgs LanguageArgs { get; set; }
        public int OptionValueGroupId { get; set; }

        public bool IsValid()
        {
            return Id >= -1 &&
                   !string.IsNullOrWhiteSpace(Name) &&
                   (LanguageArgs == null ? true : LanguageArgs.IsValid());
                   // Value intentionally skipped
                   // OptionValueGroupId intentionally skipped
        }
    }
}