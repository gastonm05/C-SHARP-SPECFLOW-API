using System;
using System.Collections.Generic;
using CCC_API.Data.Responses.Campaigns;

namespace CCC_API.Data.Responses.News
{
    public class NewsItem
    {
        public float PublicityValue { get; set; }
        public float CirculationAudience { get; set; }
        public string FileUrl { get; set; }
        public Contact Contact { get; set; }
        public Outlet Outlet { get; set; }
        public int Id { get; set; }
        public string Headline { get; set; }
        public string Text { get; set; }
        public DateTime NewsDate { get; set; }
        public string CreationDate { get; set; }
        public Tone Tone { get; set; }
        public string ArticleUrl { get; set; }
        public string Clip { get; set; }
        public NewsItemFeed Feed { get; set; }
        public string ExternalId { get; set; }
        public List<NewsClipCustomFields> CustomFields { get; set; }
        public List<Campaign> Campaigns { get; set; }
        public List<NewsTag> Tags { get; set; }
        public NewsType Type { get; set; }
        public int UniqueVisitors { get; set; }
        public NewsItemAnalytics Analytics { get; set; }
        public NewsMeta _meta { get; set; }
        public long SocialAnalyticsLikes { get; set; }
        public long SocialAnalyticsShares { get; set; }
        public long SocialAnalyticsComments { get; set; }
        public long SocialAnalyticsFollowers { get; set; }
        public NewsItemLinks _links { get; set; }
        public string SocialCountry { get; set; }
        public string Notes { get; set; }
        public string VTKey { get; set; }
        public TVEyesPlayerProperties TVEyesPlayerProperties { get; set; }
    }

    public class NewsItemPayload
    {
        public string NewsDate { get; set; }
        public string Headline { get; set; }
        public string Text { get; set; }
        public string ArticleUrl { get; set; }
        public int? MediaOutletId { get; set; }

        public void SetNewsDate(DateTime time)
        {
            NewsDate = time.ToString("MM-dd-yy");
        }
    }

    public class NewsMeta
    {
        public bool IsDeleted { get; set; }
        public bool HasAnalyticsCapabilities { get; set; }
        public CanEditProperty CanEditProperty { get; set; }
        public KeywordOffsets KeywordOffsets { get; set; }
    }

    public class NewsItemFeed
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class NewsItemLinks
    {
        public string self { get; set; }
        public string clip { get; set; }
        public string segments { get; set; }
        public string mediaDownload { get; set; }
        public string fullClip { get; set; }
        public string fielddefinitions { get; set; }
        public string tveyesOrderSubmitCallback { get; set; }
        public string tveyesOrderPlacedCallback { get; set;}
        public string tveyesOrderCompleteCallback { get; set;}
        public string analytics_adobe { get; set;}
    }

    public class CanEditProperty
    {
        public bool Headline { get; set; }
        public bool Text { get; set; }
        public bool Type { get; set; }
        public bool Tags { get; set; }
        public bool CustomFields { get; set;}
        public bool Analytics { get; set; }
        public bool Notes { get; set; }
        public bool ArticleUrl { get; set; }
    }

    public class KeywordOffsets
    {
        List<Dictionary<int, int>> Text { get; set; }
        List<Dictionary<int, int>> Headline { get; set; }
    }

    public class TVEyesPlayerProperties
    {
        string StartDateTime { get; set; }
        string EndDateTime { get; set; }
        int Station { get; set; }
    }
}
