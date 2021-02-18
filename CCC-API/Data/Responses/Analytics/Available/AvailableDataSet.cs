using System.Collections.Generic;
using System.Linq;

namespace CCC_API.Data.Responses.Analytics.Available
{
    public class AvailableDataSet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Endpoint { get; set; }
        public List<AvailableDataSeries> AvailableDataSeries { get; set; }

        public bool IsValid()
        {
            return Id > 0 &&
                    !string.IsNullOrWhiteSpace(Name) &&
                    Endpoint != null &&
                    AvailableDataSeries.All(a => a.IsValid());
        }
    }
}