using System;

namespace CCC_API.Data.Responses.Media
{
    public class Update
    {
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string ImgUrl { get; set; }
        public string Link { get; set; }
        public string Title { get; set; }
    }
}