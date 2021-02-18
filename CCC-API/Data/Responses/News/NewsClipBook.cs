using System.Collections.Generic;

namespace CCC_API.Data.Responses.News
{
    public class NewsClipBook
    {
        public int Id { get; set; }
        public int CompanyId{ get; set; }
        public int DataGroupId { get; set; }
        public int UserAccountId { get; set; }
        public string CreatedDateUTC { get; set;}
        public string LastEditedDateUTC { get; set;}
        public string Title { get; set;}
        public string Summary { get; set;}
        public int NewsTemplateID { get; set; }
        public List<int> NewsIds { get; set; }
    }
}
