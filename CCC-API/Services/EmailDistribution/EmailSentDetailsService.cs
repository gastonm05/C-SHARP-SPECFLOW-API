using CCC_API.Data.PostData;
using CCC_API.Data.Responses;
using CCC_API.Data.Responses.Common;
using CCC_API.Data.Responses.Email;
using CCC_API.Utils;
using CCC_Infrastructure.API.Utils;
using CCC_Infrastructure.Utils;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using RestSharp;
using RestSharp.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace CCC_API.Services.EmailDistribution
{
    public class EmailSentDetailsService : AuthApiService
    {
        public string EmailDistExportUri => "email/distribution/details/{0}/export";
        public string EmailDetailsUri => "email/distribution/details/";
        public string EmailDistUri => "/email/distribution";

        public const string DETAILS_KEY = "distribution details";

        private readonly TimeSpan DOWNLOAD_REPORT_TIMEOUT;
        private readonly TimeSpan DOWNLOAD_REPORT_DELAY;

        public enum ReportType { PDF, XLSX, DOCX }

        public EmailSentDetailsService(string sessionKey) : base(sessionKey){}

        public EmailSentDetailsService(string sessionKey, TimeSpan reportTimeout, TimeSpan reportDelay) : base(sessionKey) {

            DOWNLOAD_REPORT_TIMEOUT = reportTimeout;
            DOWNLOAD_REPORT_DELAY = reportDelay;
        }

        /// <summary>
        /// Distribution information by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>EmailDistUri</returns>
        public EmailDist GetDistribution(int id) => Request().Get().ToEndPoint(EmailDistUri + "/" + id).ExecCheck<EmailDist>();

        /// <summary>
        /// Basic configuration for PDF report.
        /// </summary>
        /// <param name="texts">texts to appear in the report</param>
        /// <returns>GenerateReport</returns>
        public GenerateReport GetDefaultReportRequest(Texts texts)
        {
            GenerateReport reportRequest = new GenerateReport()
            {
                Author = "test author",
                Bounced = true,
                Clicked = true,
                ClientCulture = "en-GB",
                FileType = "csv",
                IncludeContactRecipients = true,
                IncludeCoverPage = true,
                IncludeDetails = true,
                IncludeEmailImage = true,
                IncludeIndividualRecipients = true,
                IncludeLinkSummary = true,
                IncludeLinksMetrics = true,
                IncludeOrganizationRecipients = true,
                IncludeOutletRecipients = true,
                IncludeRecipientMetrics = true,
                IncludeRecipientSummary = true,
                IncludeWhoClicked = true,
                IncludeWhoOpened = true,
                Opened = true,
                ReportType = "pdf",
                Title = "test title",
                TrackingEnabled = true                
            };
            
            reportRequest.Texts = texts;            
            return reportRequest;
        }

        /// <summary>
        /// Text settings for the report.
        /// </summary>
        /// <returns>Text</returns>
        public Texts GetDefaultExportTexts() => new Texts {
                Author = "Created by",
                Bounced = "Bounced",
                Click = "Click",
                ClickCount = "clicks",
                Clicked = "Clicked",
                Clicks = "Number of clicks",
                Contacts = "Contacts",
                CopyOfEmail = "Copy of Email",
                Created = "Created",
                DateTimeSent = "Date/Time sent",
                DistributionName = "Distribution Name",
                EmailAddress = "Email Address",
                EmailDetails = "Email Details",
                EmailSubject = "Email Subject",
                HasOpened = "Opened Email",
                Immediate = "Immediate",
                Individual = "Individual",
                IndividualListNames = "COMP_ACTIVITIES_EMAIL_DETAILS_EXPORT_INDIVIDUAL_LIST_NAMES", // NOT USED
                Individuals = "Individuals",
                Link = "Link text",
                LinkSummary = "Link Summary",
                LinksClicked = "Links Clicked",
                LinksClickedSectionHeader = "Link Clicks",
                LinksMetricsHeader = "Click Summary",
                ListName = "List Name",
                ListOfContactsRecipients = "List of Media Contact Recipients",
                ListOfIndividualRecipients = "List of Individual Recipients",
                ListOfOrganizationRecipients = "List of Organization Recipients",
                ListOfOutletsRecipients = "List of Media Outlet Recipients",
                MediaContact = "Media Contact",
                MediaOutlet = "Media Outlet",
                Name = "Name",
                No = "No",
                NoContacts = "This email was not sent to any Contacts.",
                NoLinksClicked = "There are no known clicked links in the email.",
                NoLinksOnJob = "This email contained no links.",
                NoOpens = "There are no known recipients that has opened the email.",
                NoOutlets = "This email was not sent to any Outlets.",
                NotOpened = "Not Opened",
                Opened = "Opened",
                Organization = "Organization",
                OrganizationListNames = "COMP_ACTIVITIES_EMAIL_DETAILS_EXPORT_ORGANIZATION_LIST_NAMES", // NOT USED
                OrganizationName = "Organization Name",
                Organizations = "Organizations",
                Outlet = "Outlet",
                Outlets = "Outlets",
                RecipientMetricsHeader = "Analytics",
                Recipients = "Recipients",
                RecipientsList = "Recipient Lists",
                Sent = "Sent",
                Status = "Status",
                Subject = "Subject",
                Submitted = "Submitted",
                Title = "Title",
                WhoOpenedSectionHeader = "Opened",
                WhoBouncedSectionHeader = "Bounced",
                Yes = "Yes"
            };
                
        /// <summary>
        /// Sends request for a pdf report for provided distribution id.
        /// </summary>
        /// <param name="distId"></param>
        /// <returns></returns>
        public IRestResponse<JobResponse> ExportAsReport(int distId, ReportType type = ReportType.PDF)
        {
            var reportSettings = GetDefaultReportRequest(GetDefaultExportTexts());
            reportSettings.ReportType = type.ToString().ToLower();
            return ExportAsReport(distId, reportSettings);
        }

        /// <summary>
        /// Sends request for a pdf report for provided distribution id.
        /// </summary>
        /// <param name="distId"></param>
        /// <param name="reportReq"></param>
        /// <returns></returns>
        public IRestResponse<JobResponse> ExportAsReport(int distId, GenerateReport reportReq)
        {
            var url = string.Format(EmailDistExportUri, distId);
            return Request()
                .Post()
                .Data(reportReq)
                .ToEndPoint(url)
                .Exec<JobResponse>();
        }

        /// <summary>
        /// Waits for report to be generated be provided link and provides report object.
        /// </summary>
        /// <param name="response">JobResponse from Export pdf report endpoint</param>
        /// <returns>List</returns>
        public List<string> DownloadReport(JobResponse response, ReportType type)
        {
            if (response?._links?.file == null)
                throw new ArgumentException(Err.Msg("Wrong link specified"));

            switch(type)
            {
                case ReportType.PDF:
                    return DowloadPdfReport(response);
                case ReportType.DOCX:
                    return DownloadDocxReport(response);
                default:
                    throw new NotImplementedException(Err.Msg($"Report type {type} download has not been implement yet"));
            }            
        }

        /// <summary>
        /// Downloads Docx report.
        /// </summary>
        /// <param name="response"></param>
        /// <returns>List<string></returns>
        public List<string> DownloadDocxReport(JobResponse response)
        {
            var tempFile = System.IO.Path.GetTempFileName();
            var docxText = new Poller(DOWNLOAD_REPORT_TIMEOUT, DOWNLOAD_REPORT_DELAY).TryUntil(() =>     
                    new RestClient(response._links.file)
                     .AsExceptional()
                     .ThenExecute(_ =>
                      {
                         _.DownloadData(new RestRequest()).SaveAs(tempFile);
                         return Office2007Reader.ParseDocx(tempFile);
                      }),                  
                      result => result.IsSuccessful, 
                      tearDown: () => {
                          if (File.Exists(tempFile))
                              File.Delete(tempFile);
                      });
            return docxText.Value;
        }

        /// <summary>
        /// Cleans up nasty Unicode Character 'ZERO WIDTH SPACE' in the reports.
        /// </summary>
        /// <param name="s"></param>
        /// <returns>string</returns>
        public static string CleanUpReportString(string s) => Regex.Replace(s, @"\s+|\u200B+|\d", "");

        /// <summary>
        /// Downloads PDF report.
        /// </summary>
        /// <param name="response"></param>
        /// <returns>List<String></returns>
        public List<String> DowloadPdfReport(JobResponse response)
        {
            // Wait until it is downloadable, so the report is generated on server side
            new RestBuilder(new Uri(response._links.file)).Get()
                .ExecUntil(new Poller(DOWNLOAD_REPORT_TIMEOUT, DOWNLOAD_REPORT_DELAY), r => r.StatusCode == HttpStatusCode.OK);

            var report = new List<string>();
            using (var reader = new PdfReader(response._links.file))
            {
                for (var i = 1; i <= reader.NumberOfPages; i++)
                {
                    report.Add(PdfTextExtractor.GetTextFromPage(reader, i));
                }                    
                return report;
            }
        }

        /// <summary>
        /// Gets recipients of email distribution by given options. 
        /// </summary>
        /// <param name="id">distribution id</param>
        /// <param name="recipientType">contacts, outlets</param>
        /// <param name="option">specifies option, like hasOpenedEmail, hasClickedLink</param>
        /// <param name="offset">specifies offset param</param>
        /// <returns>IRestResponse</returns> - list of recipients
        public IRestResponse<RecipientsResponse> GetRecipients(string id, string recipientType, string option, int offset)
        {
            // hasNotClicked to hasClicked, false
            var isTrue = !option.Contains("Not");
            var op = isTrue ? option : option.Replace("Not", "");

            return Request()
                    .Get()
                    .ToEndPoint(EmailDetailsUri + id + "/recipients/" + recipientType)
                    .AddUrlQueryParam(op, isTrue.ToString())
                    .AddUrlQueryParam("offset", offset.ToString())
                    .Exec<RecipientsResponse>();
        }

        /// <summary>
        /// Provides Email distribution details by id. 
        /// </summary>
        /// <param name="distId">should be present in the system. Previous distribution.</param>
        /// <param name="width"></param>
        /// <returns></returns>
        public EmailDistDetails GetDistDetails(int distId, string width = "600") =>
            Request().Get()
            .ToEndPoint(EmailDetailsUri + distId)
            .AddUrlQueryParam("w", width)
            .ExecCheck<EmailDistDetails>();

        /// <summary>
        /// Converts links to shorter, elipsised version, as those appear in PDF report.
        /// Image - https://upload.wikimedia.org/wikipedia/commons/thumb/b/bc/Wii.svg/110px-Wii.svg.png
        /// Image - https://upload.wikimedia.org/wikipedia/commons/thumb/b/bc/Wii.svg/110px-W…
        /// </summary>
        /// <param name="it"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public string ShortifyLinkForPdfReport(string it, int length)
        {
            var trimS = it.Replace(Environment.NewLine, "").Trim();
            return trimS.Length <= length ? trimS : trimS.Substring(0, length) + "…"; 
            // Please note, those three dots some custom dots chars as those appears in PDF report ^
        }
    }
}
