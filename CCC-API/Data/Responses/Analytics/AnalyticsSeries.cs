using CCC_Infrastructure.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CCC_API.Data.Responses.Analytics
{
    /// <summary>
    /// Series part from data endpoints such as company prominence and impact
    /// </summary>
    /// <seealso cref="WidgetData"/>
    public class AnalyticsSeries
    {
        public string Name { get; set; }
        public long Total { get; set; }
        public long? Average { get; set; }
        public long? NumberOfClips { get; set; }
        public int YAxis { get; set; }
        public string Color { get; set; }
        public int NumberType { get; set; }
        public float? Sum { get; set; }
        public long? Percentage { get; set; }


        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public object DataProperty { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Ids { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        /// <summary>
        /// Datapoints for the series. The first index is the datetimestamp and the second index is a value representing total or average.
        /// </summary>
        public List<List<float>> Data { get; set; }

        /// <summary>
        /// Get the data array as a formatted string
        /// </summary>
        /// <returns></returns>
        public string GetDataAsString()
        {
            var s = string.Empty;
            for (int i = 0; i < Data.Count; i++)
            {
                s += $"{Data[i][0]}:{Data[i][1]}, ";
            }
            return s;
        }

        /// <summary>
        /// Calculates the sum of the data values.
        /// </summary>
        /// <returns></returns>
        public float GetDataSum()
        {
            float sum = 0;
            for (int i = 0; i < Data.Count; i++)
            {
                sum += Data[i][1];
            }
            return sum;
        }

        /// <summary>
        /// Calculates the average of the data values.
        /// </summary>
        /// <returns></returns>
        public float GetDataAverage()
        {
            return (Data.Count == 0) ? 0 : GetDataSum() / Data.Count;
        }

        /// <summary>
        /// Converts the 2D array of Data into a Tuple
        /// </summary>
        /// <returns></returns>
        public List<Tuple<float, float>> GetData()
        {
            var list = new List<Tuple<float, float>>();
            for (int i = 0; i < Data.Count; i++)
            {
                var tuple = new Tuple<float, float>(Data[i][0], Data[i][1]);
                list.Add(tuple);
            }
            return list;
        }

        /// <summary>
        /// Analytics series with parsed date times.
        /// </summary>
        /// <returns></returns>
        public List<Tuple<DateTime, long>> GetDataParsedTime()
        {
            var list = GetData()
                    .Select(pair =>
                    {
                        var dateTime = DateTimeUtil.FromEpoch((long) pair.Item1);
                        return new Tuple<DateTime, long>(dateTime,(long) pair.Item2);
                    })
                    .ToList();
            return list;
        }
    }
}