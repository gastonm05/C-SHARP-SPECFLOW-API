using System.Collections.Generic;

namespace CCC_API.Data.PostData.News
{
    public class NewsArchiveImportPostData
    {
        public string Key { get; set; }
        public List<string> Delta { get; set; }
        public bool SelectAll { get; set; }
    }
}
