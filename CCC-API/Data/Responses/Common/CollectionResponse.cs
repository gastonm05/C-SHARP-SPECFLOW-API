using System.Collections.Generic;

namespace CCC_API.Data.Responses.Common
{
    /// <summary>
    /// A response body for strongly typed collection content.
    /// </summary>
    public class CollectionResponse<T>
    {
        public CollectionResponse() : base() { }

        public int ItemCount { get; set; }
        public List<T> Items { get; set; }
    }
}