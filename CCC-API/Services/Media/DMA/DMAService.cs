
using CCC_API.Data.Responses.Common;
using RestSharp;
using CCC_API.Data.Responses.Media;

namespace CCC_API.Services.Media.DMA
{
    public class DMAService : AuthApiService
    {
        public static string DMAsEndPoint = "media/DMAs";
        public DMAService(string sessionKey) : base(sessionKey) {  }

        /// <summary>
        /// Perfor a Get to Dma endpoint to get the results by a sorted applied
        /// </summary>
        /// <param name="sort"></param>
        /// <returns></returns>
        public IRestResponse<CollectionResponse<DmaResponse>> GetDmaSorted(string sort)
        {
          return Get<CollectionResponse<DmaResponse>>($"{DMAsEndPoint}?sort={sort}");
        }
    }
}
