using CCC_API.Services;
using CCC_Infrastructure.API.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCC_API.MyGorkana
{
    public class MyGorkanaBaseService : AuthApiService
    {
        public MyGorkanaBaseService(string sessionKey) : base(sessionKey)
        {
        }

    }
}
