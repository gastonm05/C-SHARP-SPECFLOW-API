using CCC_API.Data.Responses.Email;
using CCC_API.Data.Responses.Media;
using CCC_API.Services.Media;
using CCC_Infrastructure.API.Utils;
using CCC_Infrastructure.Utils;
using CCC_API.Utils.Assertion;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace CCC_API.Services.EmailDistribution
{
    public class EmailDistributionService : AuthApiService
    {
        public const string WordToHtmlUri = "email/worddoctohtml";
        public const string EmailDistUri     = "email/distribution";
        public const string EmailDistSubmitUri = "email/distribution/submit";
        public const string HtmlTemplatesUri = "htmltemplates/user";
        public const string EmailSettingsUri = "email/distribution/emailsettings";
        public const string UniqueEmailCountUri = "email/distribution/uniqueemailcount";
        public const string UnscheduleEmailUri = "email/distribution/unschedule";
        public const string MergeFieldsUri = "mergefield";

        public EmailDistributionService(string sessionKey) : base(sessionKey){}
        
        /// <summary>
        /// Provides possibility to convert word doc to html.
        /// </summary>
        /// <param name="absFileName">Absolute path to file</param>
        /// <returns>IRestResponse</returns> - html code
        public IRestResponse<object> WordDocToHtml(string absFileName)
        {
            IRestResponse<object> response = Request()
                    .ToEndPoint(WordToHtmlUri)
                    .Post()
                    .AddFile(absFileName)
                    .Exec<object>();
            return response;
        }

        /// <summary>
        /// Saves email distribution.
        /// </summary>
        /// <param name="emailDistSettings"></param>
        /// <returns></returns>
        public void PutSaveDistribution(EmailDist emailDistSettings) =>
            Request().Put().ToEndPoint(EmailDistUri).Data(emailDistSettings).ExecCheck();

        /// <summary>
        /// Initiates new email distribution.
        /// </summary>
        /// <returns></returns>
        public EmailDist PostInitDistribution() =>
            Request().Post().ToEndPoint(EmailDistUri).ExecCheck<EmailDist>();

        /// <summary>
        /// Submits email distribution.
        /// </summary>
        /// <param name="dist">Distribution settings</param>
        public void SubmitDistribution(EmailDist dist) =>
            Request().Post().ToEndPoint(EmailDistSubmitUri).Data(dist).ExecCheck();

        /// <summary>
        /// Saves the result of default email distribution with possible settings.
        /// </summary>
        /// <param name="settings">setting can be applied</param>
        /// <returns></returns>
        public void SaveFromDefaultDistribution(Func<EmailDist, EmailDist> settings) => 
            PutSaveDistribution(settings.Invoke(PostInitDistribution()));

        /// <summary>
        /// Creates email distribution by specified params. Sets possible contacts, outlets.
        /// </summary>
        /// <param name="settings"></param>
        /// <returns>EmailDistUri</returns>
        public EmailDist CreateEmailDistribution(Func<EmailDist, EmailDist> settings)
        {
            var dist = PostInitDistribution();

            if (dist.ScheduleType == null)
                dist.ScheduleType = 1;

            // Set recipients
            var listService = new EntityListService(SessionKey);
            var contacts = listService.GetAvaliableMediaContactsLists();
            var outlets  = listService.GetAvaliableMediaOutletsLists();

            dist.MediaContacts = contacts.ToList();
            dist.MediaOutlets = outlets.ToList();

            var conf = settings.Invoke(dist);
            SubmitDistribution(conf);
            return conf;
        }

        /// <summary>
        /// Creates preconfigured email distribution.
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="scheduleType"></param>
        /// <returns>EmailDistUri</returns>
        public EmailDist CreateDefaultDistribution(Func<EmailDist, EmailDist> settings, 
            string scheduleType = "now")
        {
            var distcr = CreateEmailDistribution(settings: dist =>
            {
                dist.Name = "Test " + StringUtils.RandomAlphaNumericString(5);
                dist.Subject = "Subject " + StringUtils.RandomAlphaNumericString(5);
                dist.EmailType = 1;
                dist.HTMLBody = "<p>This is testing email distribution</p>";
                dist.MediaContacts = dist.MediaContacts?.Some(3).ToList();
                dist.MediaOutlets = dist.MediaOutlets?.Some(3).ToList();

                if (! scheduleType.ToLower().Contains("now"))
                {
                    dist.SetScheduleType(EmailDist.ScheduleTypes.InFuture);
                    dist.SetSendTime(DateTime.Now.AddDays(1).AddHours(1));
                }

                return settings.Invoke(dist);
            });
            return distcr;
        }
        
        /// <summary>
        /// Saves the result of default email distribution.
        /// </summary>
        /// <returns></returns>
        public void SaveFromDefaultDistribution() => SaveFromDefaultDistribution(it => it);

        /// <summary>
        /// Saves an html templateReq for a company.
        /// </summary>
        /// <param name="templateReq"></param>
        /// <returns></returns>
        public IRestResponse<HtmlTemplate> SaveHtmlTemplate(HtmlTemplateReq templateReq) =>
            Request().Post().Data(templateReq).ToEndPoint(HtmlTemplatesUri).Exec<HtmlTemplate>();

        /// <summary>
        /// Gets the list of html templates for a company.
        /// </summary>
        /// <returns></returns>
        public List<HtmlTemplate> GetHtmlTemplates() =>
            Request().Get().ToEndPoint(HtmlTemplatesUri).ExecCheck<List<HtmlTemplate>>();

        /// <summary>
        /// Tries to download html template thumbnail.
        /// </summary>
        /// <param name="template"></param>
        /// <returns></returns>
        public IRestResponse GetHtmlTemplateThumbnail(HtmlTemplate template)
        {
            Assert.IsNotEmpty(template.Thumbnail, "Cannot download empty thumbnail");
            return new RestBuilder(new Uri(template.Thumbnail)).Get().Exec();
        }

        /// <summary>
        /// Edits html template.
        /// </summary>
        /// <param name="edited"></param>
        /// <returns></returns>
        public IRestResponse<HtmlTemplate> EditHtmlTemplate(HtmlTemplate edited) =>
            Request().Put().Data(edited).ToEndPoint(HtmlTemplatesUri).Exec<HtmlTemplate>();

        /// <summary>
        /// Deletes html template by id.
        /// </summary>
        /// <param name="templId"></param>
        /// <returns></returns>
        public IRestResponse DeleteHtmlTemplate(int templId) =>
            Request().Delete().ToEndPoint(HtmlTemplatesUri + "/" + templId).Exec();

        /// <summary>
        /// Deletes html template by name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IRestResponse DeleteHtmlTemplate(string name)
        {
            var temp = GetHtmlTemplates().FirstOrDefault(it => it.Name.Equals(name));
            if (temp == null)
            {
                return new RestResponse {StatusCode = HttpStatusCode.BadRequest,
                    ErrorMessage = $"Cannot find email template: {name} to delete"};
            }
            return DeleteHtmlTemplate(temp.Id);
        }

        /// <summary>
        /// Provides email settings for the company.
        /// </summary>
        public EmailSettings GetEmailSettings => Request().Get().ToEndPoint(EmailSettingsUri).ExecCheck<EmailSettings>();

        /// <summary>
        /// Gets the unique email lists.
        /// </summary>
        /// <param name="lists">lists to count</param>
        /// <returns>int</returns>
        public int UniqueEmailCount(UniqueEmailsLists lists)
        {
            return Request().Post().ToEndPoint(UniqueEmailCountUri).Data(lists).ExecCheck<int>();
        }

        /// <summary>
        /// Unschedule email distribution.
        /// </summary>
        /// <param name="distId">distribution id</param>
        public void UnscheduleEmailDistribution(int distId)
            => Request().Post().ToEndPoint(UnscheduleEmailUri + "?distributionId=" + distId)
                .ExecCheck();

        /// <summary>
        /// Provides a list of merge fields by given type.
        /// </summary>
        /// <param name="recipientsType"></param>
        /// <returns></returns>
        public List<MergeField> GetMergeFields(string recipientsType)
        {
            var uri = "?entities=" + string.Join("&entities=", recipientsType.Split(',').Select(item => item.Trim()));
            return Request().Get().ToEndPoint(MergeFieldsUri + uri).ExecCheck<List<MergeField>>();
        }
    }
}
