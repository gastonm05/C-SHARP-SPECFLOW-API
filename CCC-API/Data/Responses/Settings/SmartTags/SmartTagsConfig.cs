

namespace CCC_API.Data.Responses.Settings.SmartTags
{
    public class SmartTagsConfig
    {
        public int FeatureMentions { get; set; }
        public int FeatureWords { get; set; }
        public int BriefMentions { get; set; }
        public int BriefWords { get; set; }
        public string SearchTerm { get; set; }

        public SmartTagsConfig(){ }
    }    
}
