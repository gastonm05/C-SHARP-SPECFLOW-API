using CCC_API.Data.Responses.Impact;
using CCC_API.Data.Responses.Impact.CisionId;
using CCC_API.Data.Responses.Settings.WireDistribution;
using CCC_API.Services.Settings.WireDistribution;
using CCC_Infrastructure.API.Utils;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Configuration;
using static CCC_API.Services.Impact.ImpactService;

namespace CCC_API.Services.Impact
{
    public class ImpactBaseServices : AuthApiService
    {
        public static string BaseCisionIdApiURL = ConfigurationManager.AppSettings["BaseCisionIdApiUrl"];
        public const string EMAIL_LOGIN = "admin@cision.com";
        public const string PASSWORD_LOGIN = "password";

        public static string ImpactLoginCisionId = "login";

        public const string DATE_TIME_FORMAT = "yyyy-MM-ddT00:00:00.000Z";
        public const string DATE_TIME_FORMAT_CID = "yyyy-MM-dd";

        public ImpactBaseServices(string sessionKey) : base(sessionKey) { }

        /// <summary>
        /// Returns a RestBuilder instance for a CisionId request
        /// </summary>
        /// <return RestBuilder=""></param>
        public RestBuilder CisionIdRequest()
        {
            Uri baseUrl = new Uri(BaseCisionIdApiURL);
            RestBuilder request = new RestBuilder(baseUrl);
            return request;
        }

        /// <summary>
        /// Login to CisionId to get the list releases 
        /// </summary>
        /// <param email=""></param>
        /// <param password=""></param>
        /// <return LoginCisionId=""></param>
        public LoginCisionId GetLogin(string email, string password)
        {
            string endpoint = $"{BaseCisionIdApiURL}{ImpactLoginCisionId}?email={email}&password={password}";
            RestBuilder request = new RestBuilder(new Uri(endpoint));
            return request.Get().ExecContentCheck<LoginCisionId>();
        }

        /// <summary>
        /// Gets datebounds
        /// </summary>
        /// <return Datebounds=""></param>
        public Datebounds GetDatebound()
        {
            string endpoint = $"{ImpactDateboundsUri}";
            return Request().Get().ToEndPoint(endpoint).ExecContentCheck<Datebounds>();
        }

        /// <summary>
        /// Gets the parameters for the datebound field
        /// </summary>
        /// <return Tuple<string, string>=""></param>
        protected Tuple<string, string> GetDateboundsParameters()
        {
            Datebounds dateBound = GetDatebound();
            string startDateTimeFormatted = dateBound.MinDate;
            string toDateTimeFormatted = dateBound.MaxDate;

            return new Tuple<string, string>(startDateTimeFormatted, toDateTimeFormatted);
        }

        /// <summary>
        /// Gets the parameters for the dates field, using the datebounds
        /// </summary>
        /// <return Tuple<string, string>=""></param>
        public Tuple<string, string> GetDatesParameterFromDatebound()
        {
            Tuple<string, string> datebounds = GetDateboundsParameters();
            string startDateTime = datebounds.Item1;
            string toDateTime = datebounds.Item2;

            string startDateTimeFormatted = DateTime.ParseExact(startDateTime, "yyyy-MM-dd",
                                       System.Globalization.CultureInfo.InvariantCulture)
                                        .ToString(DATE_TIME_FORMAT);
            string endDateTimeFormatted = DateTime.ParseExact(toDateTime, "yyyy-MM-dd",
                                       System.Globalization.CultureInfo.InvariantCulture)
                                        .ToString(DATE_TIME_FORMAT);

            return new Tuple<string, string>(startDateTimeFormatted, endDateTimeFormatted);
        }

        protected string AccountParameter(AllAccounts allAccounts)
        {
            //Setting default account id or multiple account ids
            bool includeAccounts = (allAccounts == AllAccounts.including) ? true : false;
            WireDistributionAccountService s = new WireDistributionAccountService(SessionKey);
            IRestResponse<WireDistributionConfig> getWireDistributionConfig = s.GetCurrentWireDistributionConfiguration();
            var currentWireDistributionConfig = JsonConvert.DeserializeObject<WireDistributionConfig>(getWireDistributionConfig.Content);

            string accounts = currentWireDistributionConfig.CompanyWireDistributionAccountId;

            if (includeAccounts)
            {
                foreach (DataGroupWireDistributionConfig dataGroupWireDistributionConfig in currentWireDistributionConfig.DataGroupWireDistributionAccounts)
                {
                    if (!string.IsNullOrEmpty(dataGroupWireDistributionConfig.WireDistributionAccountId))
                        accounts = accounts + "%3B" + dataGroupWireDistributionConfig.WireDistributionAccountId;
                        
                }
            }

            return accounts;
        }

    }

}
