namespace CCC_API.Data.Responses.Analytics.Available
{
    public class AvailableDataSeries
    {
        public int Id { get; set; }
        public int AvailableDataSetId { get; set; }
        public string Name { get; set; }
        public int NumberType { get; set; }
        public string Color { get; set; }
        public int Axis { get; set; }

        public bool IsValid()
        {
            return Id > 0 &&
                    AvailableDataSetId > 0 &&
                    !string.IsNullOrWhiteSpace(Name) &&
                    NumberType >= 0 &&
                    !string.IsNullOrWhiteSpace(Color) &&
                    Axis >= 0;
        }
    }
}