namespace CCC_API.Data.Responses.Analytics
{
    /// <summary>
    /// Response template for Analytics responses that contain series and data
    /// </summary>
    /// <typeparam name="SeriesObject">The type of the series object, usually AnalyticsSeries.</typeparam>
    /// <typeparam name="DataObject">The type of the data object.</typeparam>
    public class AnalyticsSeriesDataResponse<SeriesObject, DataObject>
    {
        public SeriesObject[] Series { get; set; }
        public int Total { get; set; }
        public DataObject[] Data { get; set; }
        public string Key { get; set; }
    }
}
