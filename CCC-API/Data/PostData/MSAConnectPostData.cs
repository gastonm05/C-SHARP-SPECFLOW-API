using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCC_API.Data.PostData
{
    /// <summary>
    /// post data for endpoint api/v1/accounts/connect/msa
    /// </summary>
    public class MSAConnectPostData
    {
        public int MsaToken { get; set; }
        public string LoginInfo { get; set; }
    }
}
