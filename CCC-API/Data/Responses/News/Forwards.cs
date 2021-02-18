using CCC_API.Data.Responses.Common;

namespace CCC_API.Data.Responses.News
{
    public class Forwards
    {
        public int Id { get; set; }
        public Status Status {get; set;}
        public FW_Links Links { get; set; }
    }
}
