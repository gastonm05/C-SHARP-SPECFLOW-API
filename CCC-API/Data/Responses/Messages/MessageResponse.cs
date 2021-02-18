
namespace CCC_API.Data.Responses.Messages
{
    /// <summary>
    /// response from endpoint api/v1/social/posts
    /// </summary>
    public class MessageResponse
    {
        public string posts { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
    }
}