namespace CCC_API.Data.Responses.Analytics.Available
{
    public class AvailableWidgetOptionValueGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public bool IsValid()
        {
            return Id > 0 &&
                    !string.IsNullOrWhiteSpace(Name);
        }
    }
}