using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CCC_API.Services.Impact.ImpactService;

namespace CCC_API.Data.Request
{
    public class AudienceRequest
    {
        public static string AnalyticsAudienceUri = "/impact/analytics/audience";
        public static string ALL_ACCOUNTS = "allAccounts";
        public static string END_DATE = "endDate";
        public static string START_DATE = "startDate";
        public static string LANGUAGE_CODE = "languageCode";
        public static string PRESS_RELEASES_ID = "pressReleaseId";

        public bool AllAccounts { get; set; }
        public string EndDate { get; set; }
        public string StartDate { get; set; }
        public string LanguageCode { get; set; }
        public string ReleaseId { get; set; }

        public override string ToString()
        {
            return $"{AnalyticsAudienceUri}?{ALL_ACCOUNTS}={AllAccounts}&{END_DATE}={EndDate}&{LANGUAGE_CODE}={LanguageCode}&{PRESS_RELEASES_ID}={ReleaseId}&{START_DATE}={StartDate}";
        }
    }
}
