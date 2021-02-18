using CCC_API.Data.PostData.Streams;
using CCC_API.Data.Responses.Streams;
using CCC_API.Data.Responses.Streams.Groups;
using RestSharp;

namespace CCC_API.Services.Streams
{
    public class StreamsService : AuthApiService
    {
        public static string StreamsEndPoint = "streams";
        public static string GroupsEndPoint = "streams/groups";

        public StreamsService(string sessionKey) : base(sessionKey) { }


        /// <summary>
        /// This method creates the body and post the message for a Stream creation.
        /// </summary>
        /// <param name="type"> Social Media Network Type (1001 for Twitter, 2001 for Instagram</param>
        /// <param name="stream_name">name of the stream to create</param>
        /// <param name="Data">Stream Data object that includes the list to use</param>
        /// <param name="group">group to relate to the stream </param>
        /// <returns> Response of a Stream creation request</returns> 
        public IRestResponse CreateStream(int type, string stream_name, StreamData data, int group)
        {
            var postData = new StreamPostData()
            {
                Type = type,
                Group = group,
                Name = stream_name,
                Data = data

            };
            return Post<StreamResponse>(StreamsEndPoint, GetAuthorizationHeader(), postData);

        }

        /// <summary>
        /// Populate SocialPostInfo object
        /// </summary>
        /// <param name="list_name"> Populates Stream Data object with a list name</param>
        /// <returns>StreamData class populated</returns> 
        public StreamData ListName(string list_name)
        {
            var response = new StreamData()
            {
                listname = list_name
            };
            return response;
        }

        /// <summary>
        ///Get company groups 
        /// </summary>
        /// <returns>Returns the list of a company groups</returns> 
        public int GetGroups()
        {
            var response = Get<GroupsResponse>(GroupsEndPoint);
            int group = response.Data.Items[0].Id;
            return group;
        }

        /// <summary>
        ///Delete a specific stream
        /// </summary>
        /// <param name="streamId">Id of a social network Streamy</param>
        /// <returns>Response of a stream delete request</returns> 
        public IRestResponse DeleteStream(int streamId)
        {
            string stream = $"{StreamsEndPoint}/{streamId}";
            return Request().Delete().ToEndPoint(stream).Exec();
        }

        /// <summary>
        /// Gets the streams for a user
        /// </summary>
        /// <returns></returns>
        public IRestResponse<StreamResponse> GetStreams()
        {
            return Request().Get().ToEndPoint(StreamsEndPoint).Exec<StreamResponse>();
        }
    }
}
