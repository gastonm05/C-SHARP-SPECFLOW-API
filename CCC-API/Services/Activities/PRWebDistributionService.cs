using CCC_API.Data.PostData.Activities;
using CCC_API.Data.PostData.Analytics;
using CCC_API.Data.Responses.Activities;
using CCC_API.Data.Responses.Common;
using CCC_API.Data.TestDataObjects;
using CCC_API.Data.TestDataObjects.Activities;
using CCC_API.Services.Activities.DB;
using CCC_Infrastructure.Utils;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using NUnit.Framework;
using System.Threading;

namespace CCC_API.Services.Activities
{
    public class PRWebDistributionService : AuthApiService
    {
        public PRWebDistributionService(string sessionKey) : base(sessionKey)
        {
        }
        public static string PRWebBaseUrl = "distribution/prweb/";

        public static string PRWebSubscriptionEndPoint = PRWebBaseUrl + "subscriptions";
        public static string PRWebSaveDraftEndPoint = PRWebBaseUrl + "item/savedraft";
        public static string PRWebSendDistributoinEndPoint = PRWebBaseUrl + "item/submit";
        public static string PRWebResubmitDistributoinEndPoint = PRWebBaseUrl + "item/resubmit";
        public static string PRWebPreviewDistributoinEndPoint = PRWebBaseUrl + "preview";
        public static string PRWebPreviewEmailDistributionEndPoint = PRWebBaseUrl + "/preview/email?recipientAddress={0}";
        public static string PRWebPreviewDownloadDistributoinEndPoint = PRWebBaseUrl + "/preview/download";
        public static string PRWebGetDistributionEndPoint = PRWebBaseUrl + "item/{0}";
        public static string PRWebAnalyticsHeadlineimpressionsCharts = PRWebBaseUrl + "analytics/item/headlineimpressions?PRWebPressReleaseID={0}";
        public static string PRWebAnalyticsFullReleaseReadsCharts = PRWebBaseUrl + "analytics/item/fullreleasereads?PRWebPressReleaseID={0}";
        public static string PRWebSetDistributionBackToDraftEndPoint = PRWebBaseUrl + "/item/{0}/backToDraft";
        public static string PRWebAnalitycsOnlinePickupEndPoint = PRWebBaseUrl + "/analytics/item/onlinepickup?PRWebPressReleaseID={0}&DistributionID={1}";
        public static string PRWebGetSingleSubscriptionEndPoint = PRWebBaseUrl + "subscription/{0}";
        public static string PRWebGetDistributionSelectedAddOns = PRWebBaseUrl + "item/{0}/selectedaddons?companyId={1}&applicationId={2}&prwebsubscriptionId={3}";
        public static string PRWebDistributionUpdateAddOns = PRWebBaseUrl + "item/{0}/addon";

        //The param is the DistributionId
        public static string PRWebisLimitedDistributionApprovalEndPoint = PRWebBaseUrl + "item/{0}/isLimitedDistributionApproval";
        public static string PRWebOnHoldReasonsEndPoint = PRWebBaseUrl + "item/{0}/onHoldReasons";
        public static string PRWebGetSendToIrisEndPoint = PRWebBaseUrl + "item/{0}/isSendToIris";

        /// <summary>
        /// This method returns all the valid subscription for PRWeb distributions.
        /// </summary>
        /// <returns></returns>
        public IRestResponse<List<PRWebSubscriptionResponse>> GetValidSubscriptions()
        {
            return Get<List<PRWebSubscriptionResponse>>(PRWebSubscriptionEndPoint);
        }


        /// <summary>
        /// This method returns prweb distribution object 
        /// </summary>
        /// <returns></returns>
        public IRestResponse<PRWebDistributionResponse> GetDistribution(int distributionId)
        {
            return Get<PRWebDistributionResponse>( string.Format( PRWebGetDistributionEndPoint, distributionId) );
        }

        /// <summary>
        /// This method returns a JobResponse
        /// </summary>
        /// <returns></returns>
        public IRestResponse<JobResponse> SendPreviewToEmail(string mail, PRWebDistributionData distributionData)
        {
            return Post<JobResponse>(string.Format( PRWebPreviewEmailDistributionEndPoint, mail), distributionData);
        }

        /// <summary>
        /// This method returns a object with a link of the PDF
        /// </summary>
        /// <returns></returns>
        public IRestResponse<JobResponse> DownloadPreview(PRWebDistributionData distributionData )
        {
            var response = Post<JobResponse>(string.Format(PRWebPreviewDownloadDistributoinEndPoint), distributionData);
            return Get<JobResponse>(new Uri(response.Data._links.file), "", new Dictionary<string, string>());
        }

        /// <summary>
        /// This method save a distribution to draft status.
        /// </summary>
        /// <returns></returns>
        public IRestResponse<PRWebDistributionResponse> SaveDraftDistribution(PRWebDistributionData prwebDistributionData)
        {
            return Post<PRWebDistributionResponse>(PRWebSaveDraftEndPoint, SetReleaseDateAndSubscriptionId(prwebDistributionData, SubscriptionType.ADVANCE));
        }

        /// <summary>
        /// This method save a distribution to draft status indication the subscription.
        /// </summary>
        /// <returns></returns>
        public IRestResponse<PRWebDistributionResponse> SaveDraftDistributionForSubscription(PRWebDistributionData prwebDistributionData, string subscriptionType)
        {
            return Post<PRWebDistributionResponse>(PRWebSaveDraftEndPoint, SetReleaseDateAndSubscriptionId(prwebDistributionData, subscriptionType));
        }

        /// <summary>
        /// This method sets the ReleaseDate and SubscriptionID to make it dynamic based on the user selection.
        /// </summary>
        /// <param name="prwebDistributionData"></param>
        /// <returns></returns>
        public PRWebDistributionData SetReleaseDateAndSubscriptionId(PRWebDistributionData prwebDistributionData, string subscriptionType)
        {
            var response = GetValidSubscriptions();
            prwebDistributionData.ReleaseDate = String.Format("{0:s}", DateTime.UtcNow.AddHours(5));
            if (response.Data.Count > 1) {
                for (int i = 0; i < response.Data.Count; i++) {
                    if (response.Data[i].Name.Contains(subscriptionType)) {
                        prwebDistributionData.SubscriptionId = response.Data[i].PRWebSubscriptionID;
                        prwebDistributionData = SelectAddonsForSubscription(response.Data[i].AddOns, prwebDistributionData);
                        break;
                    }
                }
            }else
            {
                prwebDistributionData.SubscriptionId = response.Data[0].PRWebSubscriptionID;
                prwebDistributionData = SelectAddonsForSubscription(response.Data[0].AddOns, prwebDistributionData);
            }
            return prwebDistributionData;
        }

        /// <summary>
        /// This method verify if the subscription contains addons and auto select the first in the list for each type
        /// </summary>
        /// <param name="addons"></param>
        /// <param name="prwebDistributionData"></param>
        /// <returns></returns>
        public PRWebDistributionData SelectAddonsForSubscription(List<AddOns> addons, PRWebDistributionData prwebDistributionData)
        {
            if (addons.Count > 0)
            {
                for (int p = 0; p < addons.Count; p++)
                {
                    if (addons[p].QuantityUsed < addons[p].Quantity)
                    {
                        if (addons[p].PRWebSkuType == 2 && prwebDistributionData.CJLAddOnId == 0)
                        {
                            prwebDistributionData.CJLAddOnId = addons[p].AddOnSubscriptionID;
                        }
                        if (addons[p].PRWebSkuType == 3 && prwebDistributionData.CSPAddOnId == 0)
                        {
                            prwebDistributionData.CSPAddOnId = addons[p].AddOnSubscriptionID;
                        }
                    }
                }
            }
            return prwebDistributionData;
        }

        /// <summary>
        /// This method submit a new distribution.
        /// </summary>
        /// <returns></returns>
        public IRestResponse<PRWebDistributionResponse> SendDistribution(PRWebDistributionData prwebDistributionData, string subscriptionType)
        {
            return Post<PRWebDistributionResponse>(PRWebSendDistributoinEndPoint,
                SetReleaseDateAndSubscriptionId(prwebDistributionData, subscriptionType));
        }

        /// <summary>
        /// This method is to call the send distribution using default subscription type
        /// </summary>
        /// <param name="prwebDistributionData"></param>
        /// <returns></returns>
        public IRestResponse<PRWebDistributionResponse> SendDistribution(PRWebDistributionData prwebDistributionData)
        {
            return SendDistribution(prwebDistributionData, SubscriptionType.ADVANCE);
        }

        /// <summary>
        /// This method modify the status of a distribution in DB base on provided param
        /// </summary>
        /// <param name="distributionID"></param>
        /// <param name="companyId"></param>
        /// <param name="desiredReleaseStatus"></param>
        public void SetPRWebReleaseStatus(int distributionID, int companyId, PRWebReleaseStatus desiredReleaseStatus)
        {
            using (var dbService = new DbDistributionPRWebService(companyId))
            {
                dbService.SetPRWebReleaseStatusExecution(distributionID, desiredReleaseStatus);
            }
        }

        /// <summary>
        /// This method verify if the distribution has been change to LimitedDistributionApproval
        /// </summary>
        /// <param name="distributionId"></param>
        /// <returns></returns>
        public IRestResponse<bool> IsLimitedDistributionApproval(int distributionId)
        {
            return Get<bool>(string.Format(PRWebisLimitedDistributionApprovalEndPoint, distributionId));
        }

        /// <summary>
        /// This method delete all the info related to the Distribution in the DBs
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="distributionId"></param>
        /// <param name="prwebPressReleaseId"></param>
        public void DeletePrwebDistribution(int companyId, int distributionId, int prwebPressReleaseId)
        {
            using (var dbService = new DbDistributionPRWebService(companyId))
            {
                dbService.DeleteDistribution(distributionId, prwebPressReleaseId);
            }
        }

        /// <summary>
        /// This method return the on hold reason of a prweb distribution
        /// </summary>
        /// <param name="distributionID"></param>
        /// <returns></returns>
        public IRestResponse<OnHoldReasonResponse> GetOnHoldReasons(string distributionID)
        {
            return Get<OnHoldReasonResponse>(string.Format(PRWebOnHoldReasonsEndPoint, distributionID));
        }

        /// <summary>
        /// This method update the expiration date of a subscription.
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="date"></param>
        public void SetPRWebSubscriptionExpirationDate(int companyId, string date)
        {
            DbDistributionPRWebService dbService = new DbDistributionPRWebService(companyId);

            dbService.updatePRWebSubscriptionExpirationDate(companyId, date);
            dbService.Dispose();
        }

        /// <summary>
        /// This method show the preview of a new distribution.
        /// </summary>
        /// <returns></returns>
        public IRestResponse<PRWebDistributionResponse> GetPreviewDistribution (PRWebDistributionData prwebDistributionData)
        {
            return Post<PRWebDistributionResponse>(PRWebPreviewDistributoinEndPoint,
                SetReleaseDateAndSubscriptionId(prwebDistributionData, SubscriptionType.ADVANCE));
        }

        /// <summary>
        /// This method connect DB to return ID of the IDL
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="distributionId"></param>
        /// <returns></returns>
        public string GetIdlDistributionPrweb (int companyId, int distributionId)
        {
            using (var dbService = new DbDistributionPRWebService(companyId))
            {
                return dbService.GetIdlDistributionPrweb(distributionId);
            }
        }

        /// <summary>
        /// This method connect to Company DB and Delete count on release
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="releaseId"></param>
        public void DeleteCountsForRelease(int companyId, int releaseId)
        {
            using (var dbService = new DbDistributionPRWebService(companyId))
            {
                dbService.DeleteCountsForRelease(companyId, releaseId);
            }
        }

        /// <summary>
        /// This method connect to Company DB an insert given values.
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="releaseId"></param>
        /// <param name="releaseDate"></param>
        /// <param name="activityType"></param>
        /// <param name="hits"></param>
        public void InsertCountsForRelease(int companyId, int releaseId, string releaseDate, PRWebActivityType activityType, int hits)
        {
            using (var dbService = new DbDistributionPRWebService(companyId))
            {
                dbService.InsertCountsForRelease(companyId, releaseId, releaseDate, activityType, hits);
            }
        }

        /// <summary>
        /// This method retrieves headlines impressions data chart
        /// </summary>
        /// <param name="distributionId"></param>
        /// <returns></returns>
        public IRestResponse<List<ChartResponse>> GetHeadlineImpressions(int distributionId)
        {
            return Get<List<ChartResponse>>(string.Format(PRWebAnalyticsHeadlineimpressionsCharts, distributionId));
        }

        /// <summary>
        /// This method retrieves full release counts data chart
        /// </summary>
        /// <param name="distributionId"></param>
        /// <returns></returns>
        public IRestResponse<List<ChartResponse>> GetFullReleaseReads(int distributionId)
        {
            return Get<List<ChartResponse>>(string.Format(PRWebAnalyticsFullReleaseReadsCharts, distributionId));
        }

        /// <summary>
        /// This method set a distribution back to draft.
        /// </summary>
        /// <param name="distributionId"></param>
        /// <returns></returns>
        public IRestResponse PutBackToDraft (int distributionId)
        {
            return Put<List>(string.Format(PRWebSetDistributionBackToDraftEndPoint, distributionId ), new object());
        }

        /// <summary>
        /// This method return the online pickup of a publish distribution.
        /// </summary>
        /// <param name="prwebPressReleaseId"></param>
        /// <param name="distributionId"></param>
        /// <returns></returns>
        public IRestResponse GetOnlinePickupAnalytics(int prwebPressReleaseId, int distributionId)
        {
            return Get<PRWebOnlinePickupResponse>(string.Format(PRWebAnalitycsOnlinePickupEndPoint, prwebPressReleaseId, distributionId));
        }

        /// <summary>
        /// This method create a list with distributions desired status
        /// </summary>
        /// <returns></returns>
        public List<DistroResponseStatus> CreateTestDistroList()
        {
            return new List<DistroResponseStatus>()
            {
                new DistroResponseStatus(PublicationsStatus.OnHold, Enum.GetName(typeof(PublicationsStatus), (int)PublicationsStatus.OnHold), PRWebReleaseStatus.OnHold),
                new DistroResponseStatus(PublicationsStatus.Draft, Enum.GetName(typeof(PublicationsStatus), (int)PublicationsStatus.Draft), PRWebReleaseStatus.DraftMode),
                new DistroResponseStatus(PublicationsStatus.InReview, Enum.GetName(typeof(PublicationsStatus), (int)PublicationsStatus.InReview), PRWebReleaseStatus.PendingEditorialReview),
                new DistroResponseStatus(PublicationsStatus.Scheduled, Enum.GetName(typeof(PublicationsStatus), (int)PublicationsStatus.Scheduled), PRWebReleaseStatus.PendingDistribution),
                new DistroResponseStatus(PublicationsStatus.Sent, Enum.GetName(typeof(PublicationsStatus), (int)PublicationsStatus.Sent), PRWebReleaseStatus.Published)
            };
        }

        /// <summary>
        /// This method update the publish Activity status in DB
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="publicationStatus"></param>
        /// <param name="entityId"></param>
        public void UpdatePublishActivityStatus(int companyId, PublicationsStatus publicationStatus, int entityId)
        {
            using (var dbService = new DbDistributionPRWebService(companyId))
            {
                dbService.UpdatePublishActivityStatus(publicationStatus, entityId);
            }
        }

        /// <summary>
        /// This method creates multiple distribution and set the status
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="distribution"></param>
        /// <returns></returns>
        public List<DistroResponseStatus> CreateTestDistributions(int companyId, PRWebDistributionData distribution)
        {
            var distroResponseStatusList = CreateTestDistroList();

            var originalHeadline = distribution.Headline;
            var originalDistributionName = distribution.DistributionName;

            foreach (DistroResponseStatus distroResponseStatus in distroResponseStatusList)
            {
                distribution.Headline += " " + distroResponseStatus.PublicationStatusDescription;
                distribution.DistributionName += " " + distroResponseStatus.PublicationStatusDescription;

                var response = SendDistribution(distribution);
                Utils.Assertion.Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

                SetPRWebReleaseStatus(response.Data.DistributionID, companyId, distroResponseStatus.PRWebReleaseStatus);
                UpdatePublishActivityStatus(companyId, distroResponseStatus.PublicationStatus, response.Data.DistributionID);
                
                distroResponseStatus.ApiData = response.Data;

                distribution.Headline = originalHeadline;
                distribution.DistributionName = originalDistributionName;
            }

            return distroResponseStatusList;
        }

        /// <summary>
        /// This method gets the SendToIris flag value on a distribution.
        /// </summary>
        /// <param name="distributionId"></param>
        /// <returns></returns>
        public IRestResponse GetSendToIris(string distributionId)
        {
            return Get<bool>(string.Format(PRWebGetSendToIrisEndPoint, distributionId));
        }

        public IRestResponse ResubmitDistribution(PRWebDistributionResponse distribution)
        {
            return Put<PRWebDistributionResponse>(PRWebResubmitDistributoinEndPoint, distribution);
        }

        /// <summary>
        /// This method return the requestes subscription
        /// </summary>
        /// <param name="subscriptionID"></param>
        /// <returns></returns>
        public IRestResponse<PRWebSubscriptionResponse> GetSingleSubscription(string subscriptionID)
        {
            return Get<PRWebSubscriptionResponse>(string.Format(PRWebGetSingleSubscriptionEndPoint, subscriptionID));
        }

        public IRestResponse<List<PRWebSubscriptionAddonResponse>> GetSelectedAddons(PRWebDistributionSessionWithSubscription distributionSession)
        {
            var url = string.Format(PRWebGetDistributionSelectedAddOns, distributionSession.DistributionId, distributionSession.CompanyId, distributionSession.ApplicationId, distributionSession.PRWebSubscriptionId);

            var header = new Dictionary<string, string>()
            {
                { "Content-Type", "application/json" },
                { "X-API-KEY", distributionSession.XApiKey }
            };

            return Get<List<PRWebSubscriptionAddonResponse>>(new Uri(TestSettings.GetConfigValue("BaseApiUrl")), url, header);
        }

        public IRestResponse UpdateDistributionAddons(PRWebAddonSubscriptionSessionUpdate subscriptionData)
        {
            var url = string.Format(PRWebDistributionUpdateAddOns, subscriptionData.DistributionId);

            var header = new Dictionary<string, string>()
            {
                { "Content-Type", "application/json" },
                { "X-API-KEY", subscriptionData.XApiKey }
            };

            var addonData = new
            {
                CompanyId = subscriptionData.CompanyId,
                ApplicationId = subscriptionData.ApplicationId,
                DistributionPRWebSubscriptionId = subscriptionData.DistributionPRWebSubscriptionId,
                AddOnPRWebSubscriptionId = subscriptionData.AddOnPRWebSubscriptionId,
                QuantityUsedByDistribution = subscriptionData.QuantityUsedByDistribution
            };

            return Put<List<PRWebSubscriptionAddonResponse>>(url, header, addonData);
        }

        public int GetDistributionAddOnsQuantityUsedFromDb(int companyId, int distributionId, int addonSubscriptionId)
        {
            using (var dbService = new DbDistributionPRWebService(companyId))
            {
                return dbService.GetDistributionAddOnsQuantityUsed(distributionId, addonSubscriptionId);
            }
        }

        public void UpdateDistributionAddOnsQuantityUsedInDb(int companyId, int distributionId, int addonSubscriptionId, int quantityUsed)
        {
            using (var dbService = new DbDistributionPRWebService(companyId))
            {
                dbService.UpdateDistributionAddonsQuantityUsed(distributionId, addonSubscriptionId, quantityUsed);
            }
        }

        public List<AddOns> GetAddonsBySubscriptionName(string subscriptionType)
        {
            var response = GetValidSubscriptions();
            List<AddOns> addons = null;
            foreach (PRWebSubscriptionResponse subscription in response.Data)
            {
                if (subscription.Name.Contains(subscriptionType))
                {
                    addons = subscription.AddOns;
                }
            }
            return addons;
        }

        /// <summary>
        /// This method gets the press contact phone extension on a distribution.
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="distributionId"></param>
        public string GetDistributionPressContactPhoneExtension(int companyId, int distributionId)
        {
            var pressReleasePhoneExtension = string.Empty;
            using (var dbService = new DbDistributionPRWebService(companyId))
            {
                pressReleasePhoneExtension = dbService.GetPressContactPhoneExtension(distributionId);
            }

            return pressReleasePhoneExtension;
        }

        /// <summary>
        /// Approves release for PRWeb.com and sends enquiry to IRIS 
        /// Bypasses IRIS approval and WILL publish to PRWeb.com on time without IRIS approval
        /// </summary>
        /// <param name="distributionId"></param>
        /// <param name="vmsUserId"></param>
        /// <param name="companyId"></param>
        /// <param name="applicationId"></param>
        /// <param name="releaseDateTimeUTC"></param>
        /// <param name="maxBypassIRISAttempts">IRIS Approval Bypass request can require multiple attempts depending on system conditions. This will dictate the max number of retries you would like with 3 seconds between each attempt.</param>
        /// <returns>Dictionary with keys "approveResult" and "bypassResult" indicating, respectively, if the approval succeeded and whether the bypass of IRIS approval succeeded</returns>
        public Dictionary<string, bool> ApproveReleaseAndPublishWithoutIRISApproval( int distributionId, int vmsUserId, int companyId, int applicationId, DateTime releaseDateTimeUTC, int maxBypassIRISAttempts = 0 )
        {
            var approveSuccess = ApproveRelease( distributionId, vmsUserId, companyId, applicationId, releaseDateTimeUTC ).IsSuccessful;

            // no need to run bypass call if approval was not successful
            var bypassSuccess = approveSuccess
                && BypassIRISForReleasePublicationWithRetry( distributionId, vmsUserId, companyId, applicationId, maxBypassIRISAttempts );

            return new Dictionary<string, bool>
            {
                // redundant approveSuccess check for readability
                { "overallSuccess", bypassSuccess && approveSuccess },
                { "approveSuccess", approveSuccess },
                { "bypassSuccess", bypassSuccess }
            };
        }

        /// <summary>
        /// Approves release for PRWeb.com and sends enquiry to IRIS -- release is still subject to IRIS approval and will not publish to PRWeb.com without IRIS approval
        /// </summary>
        /// <param name="distributionId"></param>
        /// <param name="vmsUserId"></param>
        /// <param name="companyId"></param>
        /// <param name="applicationId"></param>
        /// <param name="releaseDateTimeUTC"></param>
        /// <returns></returns>
        public IRestResponse ApproveRelease( int distributionId, int vmsUserId, int companyId, int applicationId, DateTime releaseDateTimeUTC )
        {
            var endPoint = $"{TestSettings.GetConfigValue( "BaseApiUrl" )}distribution/prweb/admin/item/{distributionId}/approve";
            var restClient = new RestClient( new Uri( endPoint ) );
            var restRequest = new RestRequest( Method.PUT );

            restRequest.AddHeader( "Content-Type", "application/x-www-form-urlencoded" );
            restRequest.AddHeader( "X-API-KEY", TestSettings.GetConfigValue( "X-API-KEY" ) );
            restRequest.AddParameter("undefined", $"CompanyID={companyId}&ApplicationID={applicationId}&VMSUserID={vmsUserId}&ReleaseDateTimeUTC={releaseDateTimeUTC}&Score=5", ParameterType.RequestBody );

            var response = restClient.Execute(restRequest);
            return response;
        }

        private bool BypassIRISForReleasePublicationWithRetry( int distributionId, int vmsUserId, int companyId, int applicationId, int maxAttempts = 0 )
        {
            var endPoint = $"{TestSettings.GetConfigValue( "BaseApiUrl" )}distribution/prweb/admin/item/{distributionId}/publishwithoutirisapproval";
            var restClient = new RestClient( new Uri( endPoint ) );
            var restRequest = new RestRequest( Method.PUT );
            var attempts = 0;

            restRequest.AddHeader( "Content-Type", "application/x-www-form-urlencoded" );
            restRequest.AddHeader( "X-API-KEY", TestSettings.GetConfigValue( "X-API-KEY" ) );
            restRequest.AddParameter( "undefined", $"CompanyID={companyId}&ApplicationID={applicationId}&VMSUserID={vmsUserId}", ParameterType.RequestBody );

            var response = restClient.Execute( restRequest );


            // possible the release won't be in all the right tables in time for the next "Bypass..." call to have effective impact.
            // give it some time...
            while ( !response.IsSuccessful
                && response.Content.IndexOf( "try again", StringComparison.OrdinalIgnoreCase ) >= 0
                && attempts++ <= maxAttempts )
            {
                Thread.Sleep( TimeSpan.FromSeconds( 3 ) );
                response = restClient.Execute( restRequest );
            }

            return response.IsSuccessful;

        }
    }
}
