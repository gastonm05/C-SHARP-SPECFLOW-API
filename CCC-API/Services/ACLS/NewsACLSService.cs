using CCC_API.Data.Responses.ACLS;
using RestSharp;

namespace CCC_API.Services.ACLS
{
    public class NewsACLSService : AuthApiService
    {
        public static string NewsACLSEndPoint = "acls/news";

        private const string GET_RESPONSE_KEY = "GetResponse";

        public NewsACLSService(string sessionKey) : base(sessionKey) { }

        public IRestResponse<NewsACLS> GetNewsACLS()
        {
            return Get<NewsACLS>(NewsACLSEndPoint);
        }

        /// <summary>
        /// Returns a bool value for a given ACL property from a News section
        /// </summary>
        /// <param name="property"></param>
        /// <param name="section"></param>
        /// <returns>bool</returns>
        public static bool getPropertyValue(IRestResponse<NewsACLS> response, string property, string section)
        {
            bool ret = false;
            //TODO: Find a way, other than nested switches, to resolve this
            switch (section)
            {
                case "HasAccess": ret = response.Data.HasAccess; break;
                case "Archive":
                    switch (property)
                    {
                        case "CanExportToNews": ret = response.Data.Archive.CanExportToNews; break;
                        case "CanView": ret = response.Data.Archive.CanView; break;
                        case "CanCreate": ret = response.Data.Archive.CanCreate; break;
                        case "CanEdit": ret = response.Data.Archive.CanEdit; break;
                        case "CanDelete": ret = response.Data.Archive.CanDelete; break;
                        default: break;
                    }; break;
                case "Items":
                    switch (property)
                    {
                        case "CanForward": ret = response.Data.Items.CanForward; break;
                        case "CanExport": ret = response.Data.Items.CanExport; break;
                        case "CanView": ret = response.Data.Items.CanView; break;
                        case "CanCreate": ret = response.Data.Items.CanCreate; break;
                        case "CanEdit": ret = response.Data.Items.CanEdit; break;
                        case "CanDelete": ret = response.Data.Items.CanDelete; break;
                        default: break;
                    }; break;
                case "Searches":
                    switch (property)
                    {
                        case "CanView": ret = response.Data.Searches.CanView; break;
                        case "CanCreate": ret = response.Data.Searches.CanCreate; break;
                        case "CanEdit": ret = response.Data.Searches.CanEdit; break;
                        case "CanDelete": ret = response.Data.Searches.CanDelete; break;
                        default: break;
                    }; break;
                case "Templates":
                    switch (property)
                    {
                        case "CanView": ret = response.Data.Templates.CanView; break;
                        case "CanCreate": ret = response.Data.Templates.CanCreate; break;
                        case "CanEdit": ret = response.Data.Templates.CanEdit; break;
                        case "CanDelete": ret = response.Data.Templates.CanDelete; break;
                        default: break;
                    }; break;
                case "Tags":
                    switch (property)
                    {
                        case "CanView": ret = response.Data.Tags.CanView; break;
                        case "CanCreate": ret = response.Data.Tags.CanCreate; break;
                        case "CanEdit": ret = response.Data.Tags.CanEdit; break;
                        case "CanDelete": ret = response.Data.Tags.CanDelete; break;
                        default: break;
                    }; break;
                case "Analytics":
                    switch (property)
                    {
                        case "HasScoring": ret = response.Data.Analytics.HasScoring; break;
                        case "HasToning": ret = response.Data.Analytics.HasToning; break;
                        case "HasToningOverride": ret = response.Data.Analytics.HasToningOverride; break;
                        case "HasFullAdvancedAnalytics": ret = response.Data.Analytics.HasFullAdvancedAnalytics; break;
                        case "CanView": ret = response.Data.Analytics.CanView; break;
                        case "CanCreate": ret = response.Data.Analytics.CanCreate; break;
                        case "CanEdit": ret = response.Data.Analytics.CanEdit; break;
                        case "CanDelete": ret = response.Data.Analytics.CanDelete; break;
                        default: break;
                    }; break;
                case "ForwardWizard":
                    switch (property)
                    {
                        case "IsGranted": ret = response.Data.ForwardWizard.IsGranted; break;
                        default: break;
                    }; break;
            }
            return ret;
        }
    }
}
