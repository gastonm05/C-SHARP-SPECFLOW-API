
using System.Collections.Generic;

namespace CCC_API.Data.Responses.Analytics
{
    /// <summary>
    /// Response template for Sentiment Score responses that contain series and data
    /// </summary>
        
   public class SentimentScoreWidget
    {
            public bool showTotalsInLegend { set; get; }
            public string[] yAxis { set; get; }
            public string type { set; get; }
            public List<AnalyticsSeries> Series { get; set; }

    }
}
