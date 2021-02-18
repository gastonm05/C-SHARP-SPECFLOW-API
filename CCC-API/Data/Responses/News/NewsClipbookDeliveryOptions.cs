using System.Collections.Generic;

namespace CCC_API.Data.Responses.News
{
    public class NewsClipbookDeliveryOptions
    {
        public List<string> DeliveryTypes { get; set; }
        public List<string> Recipients { get; set; }
        public string Subject { get; set; }
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
    }
}
