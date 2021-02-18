namespace CCC_API.Data.Responses.Settings.KeywordSearches
{
    public class MediaMonitorUserSearchPost
    {
        public MediaMonitorUserSearch MediaMonitorUserSearch { get; set; }

        public MediaMonitorUserSearchPost() { }

        public MediaMonitorUserSearchPost(MediaMonitorUserSearch mediaMonitorUserSearch)
        {
            this.MediaMonitorUserSearch = mediaMonitorUserSearch;
        }
    }
}
