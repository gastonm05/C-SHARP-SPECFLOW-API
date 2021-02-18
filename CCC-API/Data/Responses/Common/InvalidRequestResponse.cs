using System.Collections.Generic;

namespace CCC_API.Data.Responses.Common
{
    public class InvalidRequestResponse
    {
        public string Message { get; set; }
        
        public Dictionary<string, string[]> ModelState { get; set; }
    }
}
    