namespace CCC_API.Services.Analytics
{
    /// <summary>
    /// Common constants for Analytics Data Series endpoints
    /// </summary>
    public class Common
    {
        public enum Frequency : int { None = 0, Daily = 365, Weekly = 52, Monthly = 12, Yearly = 1 }

        public enum TypeId : int { Line = 1, Donut = 2, Map = 3, WordCloud = 6, Table = 7, Bar = 9, StackedBar = 10, StackedArea = 11, HorizontalBar = 12, MapBubble = 14 }

        public enum YAxisMetric : int { Average = 0, Total = 1 }

        public enum ToneId : int
        {
           None = 0, Negative = -1, Neutral = -2, Positive = -3
        }

        public enum DataLabel : int
        {
            Hide = 0, Show = 1
        }

        public enum Calculation : int
        {
            Count = 0, YearOverYear = 2
        }

        public enum Annotations : int
        {
            Hide = 0, Show = 1
        }
    }
}
