using Newtonsoft.Json;

namespace CCC_API.Data.Responses.Common
{
    /// <summary>
    /// Use this response when you just want to get the Total property
    /// and don't care about the rest of the content
    /// </summary>
    public class GenericResponseWithTotal
    {
        [JsonRequired]
        public int Total { get; set; }
    }
}
