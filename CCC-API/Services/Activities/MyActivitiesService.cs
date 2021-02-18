using CCC_API.Data.PostData.Activities;
using CCC_API.Data.Responses.Activities;
using CCC_API.Data.Responses.Common;
using CCC_API.Data.TestDataObjects.Activities;
using CCC_Infrastructure.API.Utils;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using static CCC_API.Services.Analytics.Common;

namespace CCC_API.Services.Activities
{
    public class MyActivitiesService : AuthApiService
    {
        public MyActivitiesService(string sessionKey) : base(sessionKey) { }

        public static string PublishActivityEndPoint = "PublishActivity";
        public static string DistributionPrivilegeEndPoint = "acls";
        public static string AddCampaignEndPoint = $"{PublishActivityEndPoint}/";
        public static string DelCampaignEndPoint = $"{PublishActivityEndPoint}/";
        public static string CountsEndPoint = $"{PublishActivityEndPoint}/counts";
        public static string ActivitiesExport = $"{PublishActivityEndPoint}/export";
        public static readonly string ActivitiesGrid = "ui/grid/activitiesgrid";
        public static string BulkAddToCampaign = $"{PublishActivityEndPoint}/bulk/campaigns";

        /// <summary>
        /// This method get the list of items int he activity grid.
        /// </summary>
        /// <param name="RowCount"></param>
        /// <param name="RowOffset"></param>
        /// <param name="UpperBound"></param>
        /// <returns></returns>
        public IRestResponse<PublishActivitiesResponse> GetActivities(
            string RowCount = "40", string RowOffset = "0", string UpperBound = "0")
        {
            string resource = string.Format(PublishActivityEndPoint +
                "?RowCount={0}&RowOffset={1}&UpperBound={2}",
                RowCount, RowOffset, UpperBound);
            return Get<PublishActivitiesResponse>(resource);
        }

        /// <summary>
        /// Recent activities by states and types.
        /// </summary>
        /// <param name="types"></param>
        /// <param name="publicationStates"></param>
        /// <param name="rowCount"></param>
        /// <param name="upperBound"></param>
        /// <param name="sortDirection"></param>
        /// <param name="page"></param>
        /// <param name="sortedField"></param>
        /// <param name="sortOnCustomFieldId"></param>
        /// <returns></returns>
        public PublishActivitiesResponse GetRecentActivities(
            string types, 
            string publicationStates = "",
            string rowCount = "50",
            string upperBound = "999999", 
            string sortDirection = "descending", 
            string page = "1", 
            string sortedField = "Time", 
            int sortOnCustomFieldId = 0)
        {
            var pubStates = string.IsNullOrEmpty(publicationStates) ? "" : $"PublicationStates={publicationStates}&";

            int typeId = 0;
            PublishActivityType systemType;
            if (PublishActivityType.TryParse(types, out systemType))
            {
                typeId = (int) systemType;
            }

            var endPoint = $"{PublishActivityEndPoint}?{pubStates}" +
                           $"Page={page}&SortField={sortedField}&RowCount={rowCount}&SortDirection={sortDirection}" +
                           $"&Types={typeId}" +
                           $"&UpperBound={upperBound}" +
                           $"&SortOnCustomFieldId={sortOnCustomFieldId}";

            return Request().Get().ToEndPoint(endPoint).ExecCheck<PublishActivitiesResponse>();
        }


        /// <summary>
        /// This method return the privilege of the Company for the Activities distribution
        /// </summary>
        /// <returns></returns>
        public IRestResponse<DistributionPrivilegeResponse> GetDistributionPrivilege()
        {
            return Get<DistributionPrivilegeResponse>(DistributionPrivilegeEndPoint);
        }

        /// <summary>
        /// Assign activity to a campaign.
        /// </summary>
        /// <param name="campaignId"></param>
        /// <param name="entityId"></param>
        /// <param name="type"></param>
        public void AssignToACampaign(List<int> data, string campaignId, int entityId, string type, HttpStatusCode expHttpStatusCode = HttpStatusCode.NoContent) =>
            Request().Put().ToEndPoint($"{AddCampaignEndPoint}{type}_{entityId}/campaigns").Data(data)
               .ExecCheck(expHttpStatusCode);

        /// <summary>
        /// Assign activity to a campaign.
        /// </summary>
        /// <param name="campaignId"></param>
        /// <param name="entityId"></param>
        /// <param name="type"></param>
        public void RemoveFromACampaign(List<int> values,string campaignId, int entityId, string type, HttpStatusCode expHttpStatusCode = HttpStatusCode.NoContent) =>
           Request().Put()
               .ToEndPoint($"{DelCampaignEndPoint}{type}_{entityId}/campaigns").Data(values)
               .ExecCheck(expHttpStatusCode);

        /// <summary>
        /// Filters activities by campaign.
        /// </summary>
        /// <param name="campaignIds"></param>
        /// <param name="rowCount"></param>
        /// <param name="rowOffset"></param>
        /// <param name="sortDirection"></param>
        /// <param name="sortField"></param>
        /// <returns></returns>
        public PublishActivitiesResponse GetActivitiesByCampaign(string campaignIds, string rowCount = "40",
                                                                 string rowOffset = "0",
                                                                 string sortDirection = "descending",
                                                                 string sortField = "Time") =>
            Request().Get()
                .ToEndPoint($"{PublishActivityEndPoint}" +
                            $"?CampaignIds={campaignIds}&RowCount={rowCount}&RowOffset={rowOffset}&SortDirection={sortDirection}&sortField={sortField}")
                .ExecCheck<PublishActivitiesResponse>();

        /// <summary>
        /// Gets the activity counts as an array.
        /// </summary>
        /// <param name="frequency">The frequency (daily=365, weekly=52, monthly=12 or yearly=1).</param>
        /// <param name="publicationStates">The publication states.</param>
        /// <returns></returns>
        public ActivityCounts[] GetCounts(Frequency frequency, string publicationStates = "")
        {
            var pubStates = string.IsNullOrEmpty(publicationStates) ? "" : $"PublicationStates={publicationStates}";
            return Request().Get()
            .ToEndPoint($"{CountsEndPoint}?frequency={(int)frequency}&{pubStates}")
            .ExecCheck().ContentAsEnumerable<ActivityCounts>().ToArray();
        }

        /// <summary>
        /// Get activities by campaign and grouped by a givel field
        /// </summary>
        /// <param name="campaignIds"></param>
        /// <param name="groupByField">PublicationState or Type</param>
        /// <returns></returns>
        public ActivityCounts[] GetActivitiesByCampaignGroupedBy(string groupByField, string campaignIds) =>
            Request().Get()
                .ToEndPoint($"{CountsEndPoint}?GroupByField={groupByField}&CampaignIds={campaignIds}")
                .ExecCheck().ContentAsEnumerable<ActivityCounts>().ToArray();

        /// <summary>
        /// Exports activities as xlxs file.
        /// </summary>
        /// <param name="payload"></param>
        /// <returns>JobResponse</returns>
        public JobResponse ExportActivitiesXlxs(ExportFilterData payload) =>
            Request().Post()
                .ToEndPoint(ActivitiesExport)
                .Data(payload)
                .ExecCheck<JobResponse>(HttpStatusCode.Accepted);

        /// <summary>
        /// Activities grid columns.
        /// </summary>
        /// <returns>ActivitiesGridView</returns>
        public ActivitiesGridView GetActivitiesGridColumns() => 
            Request().Get().ToEndPoint(ActivitiesGrid).ExecCheck<ActivitiesGridView>();

        /// <summary>
        /// Specifies activities grid view order.
        /// </summary>
        /// <param name="settings"></param>
        /// <returns>ActivitiesGridView</returns>
        public ActivitiesGridView PostActivitiesGridColumns(ActivitiesGridView settings) =>
            Request().Post().ToEndPoint(ActivitiesGrid).Data(settings).ExecCheck<ActivitiesGridView>(HttpStatusCode.Created);

        /// <summary>
        /// Assign multiples activities to campaign
        /// </summary>
        /// <param name="campaignId"></param>
        /// <param name="payload"></param>
        /// <returns></returns>
        public IRestResponse BulkAddActivitiesToCampaign(BulkAddToCampaignData payload, string op)
        {
            payload.Operation = op;
            return Request().Put().ToEndPoint(BulkAddToCampaign).Data(payload).ExecCheck(HttpStatusCode.NoContent);
        }
    }
}
