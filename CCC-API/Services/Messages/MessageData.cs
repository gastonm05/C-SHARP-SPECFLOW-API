using CCC_API.Data.PostData.Messages;
using CCC_API.Data.Responses.Messages;
using CCC_API.Data.TestDataObjects.Activities;
using CCC_Infrastructure.Utils;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CCC_API.Services.Messages
{
    public class MessageService : AuthApiService
    {
        public static string MessageEndPoint = "social/posts";
        public static string ActivityEndPoint = "activity/custom";
        public static string AccountsEndPoint = "social/accounts";
        public static string PinterestsEndPoint = "social/pinterest/";
        public static readonly string PinterestBoardEndpoint = "/boards";

        public MessageService(string sessionKey) : base(sessionKey) { }

        /// <summary>
        /// This method creates a body message as a social media post.
        /// </summary>
        /// <param name="socialMediaList">Social media platform to publish to (FacebookFanPage, Twitter, LinkedInCompany, Pinterest</param>
        /// <param name="text">Required length of the string to generate</param>
        /// <param name="imageUrl">Address of the image to attach/link</param>
        /// <param name="linked">if true message will contain shared link object </param>
        /// <param name="sharedLink">Shared Link Object</param>
        /// <param name="shortenUrls">if true, url shorten service will be enabled</param>
        /// <returns> client posts the message</returns> 
        public MessageData PrepareMessage(SocialPostInfo socialMediaList, string text, string imageUrl, bool linked, SharedLink sharedLink, PublishTime publishTime, bool shortenUrls)
        {
            if (linked == true)
            {
                imageUrl = null;
            }

            var postData = new MessageData()
            {
                Content = text,
                Title = "",
                SocialPostInfos = new List<SocialPostInfo> { socialMediaList },
                ShortenUrls = shortenUrls,
                PublishTime = publishTime,
                SharedLink = sharedLink,
                SharedImageUrl = imageUrl,

            };

            return postData;
        }

        /// <summary>
        /// This method creates a body message for a New Activity.
        /// </summary>
        /// <param name="time">Date when the activity should be published</param>
        /// <param name="timezone">Chosen timezone to publish the activit</param>
        /// <param name="titleText">Text for the title</param>
        /// <param name="notesText">Text for the notes section</param>
        /// <param name="activityType">Type of the new activity</param>
        /// <returns> body of the message to publish</returns> 
        public Activity PrepareActivity(int type, string time, string timezone, string titleText, string notesText)
        {
            var postData = new Activity
            {
                Type = type,
                Notes = notesText,
                ScheduleTime = time,
                TimeZoneIdentifier = timezone,
                Title = titleText,
                Contact = null,
                Outlet = null,
                CustomFields = null
            };

            return postData;
        }

        /// <summary>
        /// Creates an activity with given message data.
        /// </summary>
        /// <param name="postData">post configuration</param>
        /// <returns>IRestResponse</returns>
        public IRestResponse PostActivity(object postData) =>

            Request().Post().Data(postData).ToEndPoint(ActivityEndPoint).Exec<MessageResponse>();

        /// <summary>
        /// Creates a social post with given message data.
        /// </summary>
        /// <param name="postData">post configuration</param>
        /// <returns>IRestResponse</returns>
        public IRestResponse PostMessage(object postData) =>

            Request().Post().Data(postData).ToEndPoint(MessageEndPoint).Exec<MessageResponse>();

        /// <summary>
        /// Generates a random string with the required length.
        /// </summary>
        /// <param name="Size">Required length of the string to generate</param>
        /// <returns>new string</returns> - String generated
        public string GetRandomString(int Size)
        {
            Random random = new System.Random();
            string input = "abcdefghijklmnopqrstuvwxyz0123456789";
            var chars = Enumerable.Range(0, Size)
                                   .Select(x => input[random.Next(0, input.Length)]);
            return new string(chars.ToArray());
        }

        /// <summary>
        /// Populate SocialPostInfo object
        /// </summary>
        /// <param name="platform">Social media platform where to publish</param>
        /// <returns>response</returns> 
        public SocialPostInfo PlatformAsProperty(string platform)
        {
            string pinterestBoard = null;
            if (platform == "Pinterest")
            {
                PinterestBoardResponse board = GetPinterestBoard();
                pinterestBoard = board?.Id;
            }

            var response = new SocialPostInfo()
            {
                SocialMediaAccountType = platform,
                Attributes = pinterestBoard
            };
            return response;
        }

        /// <summary>
        /// Populate SharedLink object
        /// </summary>
        /// <param name="imageUrl">Image address</param>
        /// <param name="pageUrl">url of the website that host the image</param>
        /// <param name="linked">if true message will contain shared link object </param>
        /// <returns>response</returns> 
        public SharedLink SharedLink(string imageUrl, string pageUrl, bool linked)
        {
            dynamic response = null;
            if (linked == true)
            {
                response = new SharedLink()
                {
                    url = pageUrl,
                    imageUrl = imageUrl,
                    caption = GetRandomString(6),
                    description = GetRandomString(10),
                };
            }
            else
            {
                response = null;
            }
            return response;
        }

        /// <summary>
        ///Get Pinterest Board id 
        /// </summary>
        /// <param name="PinterestBoardResponse">List of the Pinterest boards connected to the company</param>
        /// <returns>PinterestBoardResponse</returns> 
        public PinterestBoardResponse GetPinterestBoard()
        {
            var response = GetAccount("Pinterest");

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return null;

            int source_external_id = response.Data.ExternalApplicationId;
            var boardsResponse = Get<List<PinterestBoardResponse>>(PinterestsEndPoint + source_external_id + PinterestBoardEndpoint);
            return boardsResponse.Data[0];
        }

        /// <summary>
        ///Get a specific social media accounts connected to a Company 
        /// </summary>
        /// <param name="Accounts">List of the social media accounts  connected to the company</param>
        /// <returns>Data of an specific social network connected account to a company</returns> 
        public IRestResponse<Accounts> GetAccount(string type)
        {

            var response = Get<Accounts>($"{AccountsEndPoint}?type={type}");
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return null;
            return response;
        }

        /// <summary>
        /// Populate PublishTime object
        /// </summary>
        /// <param name="time">String should be formated in SortableDateTimePattern</param>
        /// <param name="timezone">Time zones IDs</param>
        /// <returns>response</returns> 
        public PublishTime PublishTime(string timePeriod, string timezone)
        {
            var time = DateTime.UtcNow;
            PublishTime pubTime = null;

            switch (timePeriod)
            {
                case "now":
                    return pubTime;
                case "tomorrow":
                    time = time.AddDays(1);
                    break;
                case "nextYear":
                    time = time.AddYears(1);
                    break;
                case "lastYear":
                    time = time.AddYears(-1);
                    break;
                default:
                    throw new ArgumentException("Only now, tomorrow, nextYear or lastYear arguments are accepted");
            }

            pubTime = new PublishTime()

            {
                Time = time.ToString("s"),
                TimeZoneIdentifier = timezone,
            };

            return pubTime;
        }
        /// <summary>
        /// Form time string for present an future time
        /// </summary>
        /// <param name="time">String should be formated in SortableDateTimePattern</param>
        /// <returns>response</returns> 
        public string formatTime(string time)
        {
            DateTime date = DateTime.Now;
            string response = date.ToString("yyyy-MM-dd hh:mm:ss.ss");
            if (time == "now")
                return response;
            if (time.ToLower().Contains("tomorrow"))
                response = String.Format("{0:s}", DateTime.Now.AddDays(2));
            return response;
        }
    }
}
