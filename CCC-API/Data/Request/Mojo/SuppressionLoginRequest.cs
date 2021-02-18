
using System;

namespace CCC_API.Data.Request.Mojo
{
    [Serializable]
    public class SuppressionLoginRequest
    {
        public string username { get; set;  }

        public string password { get; set; }
    }
}
