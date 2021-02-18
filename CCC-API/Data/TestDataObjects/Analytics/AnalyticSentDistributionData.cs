namespace CCC_API.Data.PostData.Analytics
{
    public class AnalyticSentDistributionData {
        public int CompanyId { get; set; }
        public int ReleaseId { get; set; }
        public string ReleaseDate { get; set; }
    }

    public enum PRWebActivityType
    {
        LIFETIME = 1,
        EMAILFORWARD = 2,
        PODCAST = 3,
        EYECASTER = 4,
        TRACKBACK = 5,
        PINGBACK = 6,
        ROBO = 7,
        AUDIBLE = 8,
        RSSPULL = 9,
        RSSPICKUP = 10,
        PDF = 11,
        JS = 12,
        PRINT = 13,
        VIEWS = 14,
        READS = 15,
        EXTERNAL = 20, // THESE ARE RSS HEADLINES
        EMAILCLICKTHRU = 21,
        YAHOO_ENR = 22,
        SPONSOREDPR = 23,
        RELATEDPR = 24,
        PRWEBXML = 25,
        AHHA = 26,
        INKTOMI = 27,
        FDSE = 28,
        ARRIVENET = 29,
        NEWSPAD = 30,
        TOPIX = 31,
        KMM = 32,
        CFU = 33,
        PHEEDO = 34,
        GOOGLE = 35,
        EBOOK = 36,
        BLOGTHIS = 37,
        FEATUREVID = 38,
        ENT = 39,
        CLICKTHRUS = 42,
        OUTBOUND = 41, // CLICK OUT
        NEWSALERT = 43,
        FPV_IM = 44,
        FPV_CT = 54,
        IFRAME_VIEW = 64,
        IFRAME_CLICK = 74,
        IFRAME_MOUSE = 84,
        FACEBOOK_SHARE = 85,
        TWITTER_SHARE = 86,
        LINKEDIN_SHARE = 87,
        GOOGLEPLUS_SHARE = 88,
        CONTACT_FACEBOOK_CLICK = 89,
        CONTACT_TWITTER_CLICK = 90,
        CONTACT_LINKEDIN_CLICK = 91,
        CONTACT_GOOGLEPLUS_CLICK = 92,
        ATTACHMENT_CLICK = 93,
        PINTEREST_SHARE = 94
    }
}
