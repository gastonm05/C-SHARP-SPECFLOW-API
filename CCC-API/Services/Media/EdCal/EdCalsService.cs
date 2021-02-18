using CCC_API.Data.PostData.Media.EdCal;
using CCC_API.Data.Responses.Common;
using CCC_API.Data.Responses.Media.EdCal;
using CCC_Infrastructure.Utils;
using RestSharp;
using System;

namespace CCC_API.Services.Media.EdCal
{
    public class EdCalsService : AuthApiService
    {
        public static string EdCalsEndPoint = "media/edcals";

        public EdCalsService(string sessionKey) : base(sessionKey) { }

        /// <summary>
        /// Represents the criterion by which EdCals can be searched by
        /// </summary>
        public enum EdCalSearchCriteria
        {
            Contact_Name,
            Keyword,
            Outlet_Country,
            Outlet_Name            
        };

        /// <summary>
        /// Get request for EdCals by the passed in criteria. 
        /// This will throw an exception if the criteria argument doesn't match a valid criteria
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public IRestResponse<EdCals> GetEdCals(EdCalSearchCriteria criteria, string parameter)
        {
            switch (criteria)
            {
                case EdCalSearchCriteria.Contact_Name:
                    return Get<EdCals>($"{EdCalsEndPoint}?contactName={parameter}");

                case EdCalSearchCriteria.Keyword:
                    return Get<EdCals>($"{EdCalsEndPoint}?keyword={parameter}");

                case EdCalSearchCriteria.Outlet_Country:
                    return Get<EdCals>($"{EdCalsEndPoint}?countryIds={parameter}");

                case EdCalSearchCriteria.Outlet_Name:
                    return Get<EdCals>($"{EdCalsEndPoint}?outletName={parameter}");

                default:
                    throw new ArgumentException(Err.Msg($"'{criteria}' is not a valid criteria to search for EdCals"));
            }
        }

        /// <summary>
        /// Gets EdCals by issue date.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns></returns>
        public IRestResponse<EdCals> GetEdCalsByIssueDate(DateTime startDate, DateTime endDate)
        {
            var format = "yyyy-MM-dd'T'HH:mm:ss.fff'Z'";
            TimeSpan localZoneOffSet = TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now);
            var localStartDate = startDate.Add(localZoneOffSet.Negate());
            var formattedStartDate = localStartDate.ToString(format);
            var localEndDate = endDate.Add(localZoneOffSet.Negate());
            var formattedEndDate = localEndDate.ToString(format);
            return Get<EdCals>($"{EdCalsEndPoint}?issueDateEndDate={localEndDate}&issueDateStartDate={localStartDate}");
        }

        /// <summary>
        /// Gets EdCals by issue start OR end date.
        /// </summary>
        /// <param name="dateType">Type of the date start|end</param>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public IRestResponse<EdCals> GetEdCalsByIssueStartOrEndDate(string dateType, DateTime date)
        {
            string queryParam;   
            TimeSpan localZoneOffSet = TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now);
            var localDate = date.Add(localZoneOffSet.Negate());
            var formattedDate = localDate.ToString("yyyy-MM-dd'T'HH:mm:ss.fff'Z'");

            switch (dateType)
            {
                case "end":
                case "End":
                    queryParam = $"?issueDateEndDate={formattedDate}";
                    break;

                case "start":
                case "Start":
                    queryParam = $"?issueDateStartDate={formattedDate}";
                    break;

                default:
                    throw new ArgumentException(Err.Msg($"Argument 'dateType' must be either 'start' or 'end'. Found: {dateType}"));
            }
           
            return Get<EdCals>($"{EdCalsEndPoint}{queryParam}");
        }

        /// <summary>
        /// Exports EdCals.
        /// IMPORTANT - This method assumes SelectAll = TRUE & PresentationType = 0
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public IRestResponse<JobResponse> ExportEdCals(string key)
        {
            var postData = new EdCalExportPostData()
            {
                Key = key,
                PresentationType = 0,
                SelectAll = true
            };
            return Post<JobResponse>($"{EdCalsEndPoint}/export",postData);
        }
    }
}
