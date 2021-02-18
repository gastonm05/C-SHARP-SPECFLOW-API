using CCC_API.Data.Responses.Mojo;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCC_API.Services.Mojo
{
    public class MojoBaseService : AuthApiService 
    {

        public MojoBaseService(string sessionKey) : base(sessionKey)
        {
        }

        public MojoBaseService() : base("")
        {
        }


    }
}
