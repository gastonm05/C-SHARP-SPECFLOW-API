using CCC_API.Data.PostData.News;
using CCC_API.Data.Responses.News;
using CCC_Infrastructure.Utils;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CCC_API.Services.News
{
    public class NewsForwardService : AuthApiService
    {
        public static string NewsForwardsEndpoint = "news/forwards";

        public NewsForwardService(string sessionKey) : base(sessionKey) { }

        /// <summary>
        /// Performs a POST to news/forwards endpoint
        /// </summary>
        /// <param name="key"></param>
        /// <param name="selectAll"></param>
        /// <param name="delta"></param>
        /// <param name="recipient"></param>
        /// <param name="templateId"></param>
        /// <param name="subject"></param>
        /// <param name="grouping"></param>
        /// <param name="message"></param>
        /// <param name="endDate"></param>
        /// <param name="items"></param>
        /// <returns>IRestResponse</returns>
        public IRestResponse PostNewsForward(string key, bool selectAll, List<int> delta, 
            List<string> recipients, string template, string subject, string groupBy, 
            string message, string endDate, IEnumerable<string> items, string senderName, string senderEmail)
        {
            var postData = new NewsForwardPostData()
            {
                Key = key,
                SelectAll = selectAll,
                Delta = delta,
                Recipients = recipients,
                Template = template,
                Subject = subject,
                GroupBy = groupBy,
                Message = message,
                EndDate = endDate,
                Items = items,
                SenderName = senderName,
                SenderEmail = senderEmail
            };

            var response = Post<Forwards>(NewsForwardsEndpoint, GetAuthorizationHeader(), postData);
            return response;
        }

        /// <summary>
        /// Performs a POST to news/forwards endpoint
        /// </summary>
        /// <param name="key"></param>
        /// <param name="items"></param>
        /// <returns>IRestResponse</returns>
        public IRestResponse PostNewsForward(string key, List<string> items) {
            List<int> delta = new List<int>();
            delta.Add(1);
            List<string> recipients = new List<string>();
            recipients.Add("somemail@mailinator.com");
            var postData = new NewsForwardPostData()
            {
                Key = key,
                SelectAll = false,
                Delta = delta,
                Recipients = recipients,
                Template = "NT-2",
                Subject = "Automation Subject",
                GroupBy = "medium",
                Message = "Automation Message",
                EndDate = GetNewsForwardEndDate(),
                Items = items,
                SenderName = "Sender Name",
                SenderEmail = "sender@mailinator.com"
            };

            var response = Post<Forwards>(NewsForwardsEndpoint, GetAuthorizationHeader(), postData);
            return response;
        }

        /// <summary>
        /// Returns a List<string> of News Clips Ids from a given search result
        /// </summary>
        /// <param name="response"></param>
        /// <returns>List<string></returns>
        public IEnumerable<string> GetNewsItemsIds(IRestResponse<NewsView> response) =>
            response == null ? new List<string>() : response.Data.Items.Select(i => i.Id.ToString()).Some(100);

        /// <summary>
        /// Helper method to get News Forward End Date,
        /// in this case, tomorrow's date as string for News Forward payload
        /// </summary>
        /// <returns>string</returns>
        public string GetNewsForwardEndDate() {
            return DateTime.Today.AddDays(1).ToString("MM/dd/yyyy");
        }

        /// <summary>
        /// Returns a list of recipient's emails for the News Forwards payload
        /// </summary>
        /// <returns>List<string></returns>
        public List<string> GetRecipientsList() {
            var recipientsList = new List<string>();
            recipientsList.Add("jj@mailinator.com");
            recipientsList.Add("jpc@mailinator.com");

            return recipientsList;
        }

        /// <summary>
        /// Returns a list of int values to represent Delta on News Forwards payload
        /// </summary>
        /// <returns></returns>
        public List<int> GetDelta() {
            var deltas = new List<int>();
            deltas.Add(1);
            return deltas;
        }
    }
}
