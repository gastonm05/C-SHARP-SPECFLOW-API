using BoDi;
using CCC_API.Data.PostData;
using CCC_API.Data.PostData.Settings.UserManagement;
using CCC_API.Data.Responses;
using CCC_API.Data.Responses.Common;
using CCC_API.Data.Responses.Email;
using CCC_API.Data.Responses.Media;
using CCC_API.Data.TestDataObjects;
using CCC_API.Services.EmailDistribution;
using CCC_API.Services.EmailDistribution.DB;
using CCC_API.Services.Media;
using CCC_API.Steps.Activities;
using CCC_API.Steps.Common;
using CCC_Infrastructure.Utils;
using CCC_API.Utils.Assertion;
using RestSharp;
using RestSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using TechTalk.SpecFlow;
using static CCC_API.Services.EmailDistribution.EmailSentDetailsService;
using Has = NUnit.Framework.Has;
using System.Text.RegularExpressions;

namespace CCC_API.Steps.EmailDistribution
{
    [Binding]
    public class EmailDetailsSteps : AuthApiSteps
    {
        private readonly EmailSentDetailsService _emailSentDetailsService;

        public const string REPORT_KEY = "email report";
        public const string GENERAL_REPORT_KEY = "Report";
        public const string DISTRIBUTION_KEY = "dist";
        public const string REPORT_LANG = "Report language";
        public const string FULL_REPORT_TEXT = "Full report text";
        public const string EXP_DIST_KEY = "Distribution";
        public const string LINKS_KEY = "Links";
        public const string EXP_DIST_DETAILS_KEY = "Distribution details";
        public const string DISTRIBUTION_RECIPIENTS = DistributionTestsSetupSteps.DISTRIBUTION_RECIPIENTS;

        public EmailDetailsSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
            _emailSentDetailsService = new EmailSentDetailsService(SessionKey);
        }

        [When(@"I perform a filter on (.*) which (.*) and (.*)")]
        public void WhenIPerformAFilterOnWhichAndOffset(string recipientType, string filterOption, int offset)
        {
            var dist =
                PropertyBucket.GetProperty<RecipientsResponse[]>(DISTRIBUTION_RECIPIENTS)
                    .FirstOrError(it => it.Type.Equals(recipientType), $"No recipient type {recipientType} found.");

            var response = _emailSentDetailsService.GetRecipients(dist.DistId, recipientType, filterOption, offset);
            PropertyBucket.Remember("filter", response);
        }
        
        [Then(@"the email distribution details endpoint should return the list of (.*) who (.*) skipping (.*)")]
        public void ThenTheEmailDistributionDetailsEndpointShouldReturnTheListOfWho(string recipientType,
            string filterOption, int offset)
        {
            var resp = PropertyBucket.GetProperty<IRestResponse<RecipientsResponse>>("filter");
            Assert.AreEqual(HttpStatusCode.OK, resp.StatusCode, "Wrong response from recipients endpoint");
            
            var allExpRecipients = PropertyBucket.GetProperty<RecipientsResponse[]>(DISTRIBUTION_RECIPIENTS)
                .FirstOrError(it => it.Type.Equals(recipientType), $"No recipient type {recipientType} found.");
            
            // Filtering logic
            Func<Item, bool> predicate = recipient =>
            {
                switch (filterOption)
                {
                    case "hasClickedLink":
                        return recipient.ClickedLinks != null;
                    case "hasOpened":
                        return recipient.HasOpened == true;
                    case "hasNotOpened":
                        return recipient.HasOpened == false;
                    case "hasNotClickedLink":
                        return recipient.ClickedLinks == null;
                }
                return true; // By default filter disabled
            };

            var expRec = allExpRecipients.Items.Where(predicate).Skip(offset).ToList();
            var response = resp.Data;

            Assert.AreEqual(expRec.Count, response.Items?.Count, 
                $"Filter by option {filterOption} clicked recipients {recipientType} does not work.");

            foreach (var recipient in expRec)
            {
                Assert.Contains(recipient, response.Items, 
                    $"Expected recipient {recipient} is not present in the response.");
            }
        }              

        [When(@"I request (PDF|XLSL|DOCX) export for a distribution with (.*)")]
        public void WhenIRequestExportForADistributionWith(ReportType reportType, string recipientType)
        {
            var dist =
                PropertyBucket.GetProperty<RecipientsResponse[]>(DISTRIBUTION_RECIPIENTS)
                    .FirstOrError(it => it.Type.Equals(recipientType), $"No recipient type {recipientType} found.");


            var page = new EmailSentDetailsService(SessionKey, TimeSpan.FromMinutes(2), TimeSpan.FromSeconds(10));
            var response = page.ExportAsReport(Convert.ToInt32(dist.DistId), reportType);
            PropertyBucket.Remember(GENERAL_REPORT_KEY, response);
            PropertyBucket.Remember(DISTRIBUTION_KEY,   dist);
        }

        [When(@"I request export for a (PDF|XLSL|DOCX) report distribution of (.*) and excluded sections (.*)")]
        public void WhenIRequestExportForADistributionOfAndExcludedSections(ReportType type, string recipientType, string excludes)
        {
            var reportReq = 
                _emailSentDetailsService.GetDefaultReportRequest(
                    _emailSentDetailsService.GetDefaultExportTexts());

            if (excludes.ToLower().Contains("all"))
            {
                reportReq.Bounced = false;
                reportReq.Clicked = false;
                reportReq.IncludeContactRecipients = false;
                reportReq.IncludeCoverPage = false;
                reportReq.IncludeDetails = false;
                reportReq.IncludeEmailImage = false;
                reportReq.IncludeIndividualRecipients = false;
                reportReq.IncludeLinkSummary = false;
                reportReq.IncludeLinksMetrics = false;
                reportReq.IncludeOrganizationRecipients = false;
                reportReq.IncludeOutletRecipients = false;
                reportReq.IncludeRecipientMetrics = false;
                reportReq.IncludeRecipientSummary = false;
                reportReq.IncludeWhoClicked = false;
                reportReq.IncludeWhoOpened = false;
                reportReq.Opened = false;
                reportReq.TrackingEnabled = false;
                reportReq.ReportType = type.ToString().ToLower();
            }

            var dist =
                PropertyBucket.GetProperty<RecipientsResponse[]>(DISTRIBUTION_RECIPIENTS)
                    .FirstOrError(it => it.Type.Equals(recipientType), $"No recipient type {recipientType} found.");

            var page = new EmailSentDetailsService(SessionKey, TimeSpan.FromMinutes(2), TimeSpan.FromSeconds(10));
            var response = page.ExportAsReport(Convert.ToInt32(dist.DistId), reportReq);
            PropertyBucket.Remember(GENERAL_REPORT_KEY, response);
            PropertyBucket.Remember(DISTRIBUTION_KEY,   dist);
        }

        [Then(@"I should be given the pending job url for future report of (.*)")]
        public void ThenIShouldBeGivenThePendingJobUrlForFutureReportOf(string typeOfRecipients)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<JobResponse>>(GENERAL_REPORT_KEY);
            Assert.AreEqual(HttpStatusCode.Accepted, response.StatusCode,
                $"Reponse failed from {_emailSentDetailsService.EmailDistExportUri}");

            var data = response.Data;
            Assert.AreEqual("Pending", data.Status.State,     "Job status wrong");
            Assert.NotNull(data._links.file,                  "Generated link is null");
            Assert.True(data._links.self.Contains("jobs/"),   "Job was not assigned");
            PropertyBucket.Remember("PDF" + typeOfRecipients, response.Data);
        }

        [Then(@"I can download (PDF|XLSL|DOCX) report of type (.*)")]
        public void ThenICanDownloadIt(ReportType type, string typeOfRecipients)
        {
            var rep = PropertyBucket.GetProperty<JobResponse>("PDF" + typeOfRecipients);
            var page = new EmailSentDetailsService(SessionKey, TimeSpan.FromMinutes(2), TimeSpan.FromSeconds(10));
            var d = page.DownloadReport(rep, type);
            PropertyBucket.Remember(REPORT_KEY, d);
        }
        
        [Then(@"I can take necessary distribution details to compare with")]
        public void TakeExpectedData()
        {
            var report = PropertyBucket.GetProperty<List<string>>(REPORT_KEY);

            var expText =
                _emailSentDetailsService.GetDefaultReportRequest(_emailSentDetailsService.GetDefaultExportTexts());
            PropertyBucket.Remember(REPORT_LANG, expText);

            var dist    = PropertyBucket.GetProperty<RecipientsResponse>(DISTRIBUTION_KEY);

            var details = _emailSentDetailsService.GetDistDetails(Convert.ToInt32(dist.DistId));
            PropertyBucket.Remember(EXP_DIST_DETAILS_KEY, details);

        
            var wholeReport = string.Join("\n", report);
            PropertyBucket.Remember(FULL_REPORT_TEXT, wholeReport);
        }

        [Then(@"I should see '(\d+)' page(?:|s) with title and page number")]
        public void ThenIShouldSeePagesWithTitleAndPageNumber(int pages)
        {
            var report = PropertyBucket.GetProperty<List<string>>(REPORT_KEY);
            Assert.AreEqual(pages, report.Count, $"Only {pages} expected");

            var page = CleanUpReportString(PropertyBucket.GetProperty<string>(FULL_REPORT_TEXT));
            var expText = PropertyBucket.GetProperty<GenerateReport>(REPORT_LANG);
            var dist = PropertyBucket.GetProperty<EmailDistDetails>(EXP_DIST_DETAILS_KEY);
            var items = new List<string> { expText.Title, dist.Subject, pages.ToString() };

            var exp = CleanUpReportString(string.Join("", items));
            Assert.Contains(exp, page, "Displayed page wrong");
        }
        
        [Then(@"check cover page")]
        public void ThenCoverPage()
        {
            var expText   = PropertyBucket.GetProperty<GenerateReport>(REPORT_LANG);
            var distData  = PropertyBucket.GetProperty<EmailDistDetails>(EXP_DIST_DETAILS_KEY);
            var report    = PropertyBucket.GetProperty<List<string>>(REPORT_KEY);
            var coverPage = report.FirstOrError();

            Assert.Contains(expText.Author,   coverPage, "Author");
            Assert.Contains(expText.Title,    coverPage, "Title");
            Assert.Contains(distData.Subject, coverPage, "Subject");
        }

        [Then(@"check analytics page with clicks, opens and bounces for (.*)")]
        public void ThenAnalyticsPageWithClicksOpensAndBounces(string type)
        {
            var expText   = PropertyBucket.GetProperty<GenerateReport>(REPORT_LANG);
            var distData  = PropertyBucket.GetProperty<EmailDistDetails>(EXP_DIST_DETAILS_KEY);
            var firstPage = PropertyBucket.GetProperty<List<string>>(REPORT_KEY)[1];
            var allDist   = PropertyBucket.GetProperty<RecipientsResponse[]>(DISTRIBUTION_RECIPIENTS);
            var dist      = PropertyBucket.GetProperty<RecipientsResponse>(DISTRIBUTION_KEY);
            
            Assert.Contains(expText.Title,                        firstPage, "Title");
            Assert.Contains(distData.Subject,                     firstPage, "Subject");
            Assert.Contains(expText.Texts.RecipientMetricsHeader, firstPage, "Analytics label");
            Assert.Contains(expText.Texts.Opened.ToUpper(),       firstPage, "Opened label");
            Assert.Contains(expText.Texts.Clicked.ToUpper(),      firstPage, "Clicked label");
            Assert.Contains(expText.Texts.Bounced.ToUpper(),      firstPage, "Bounced label");
            Assert.Contains(expText.Texts.LinksMetricsHeader,     firstPage, "Click Summary label");

            // % of analytics
            var items = allDist.Where(it => it.DistId == dist.DistId).SelectMany(it => it.Items);
            var enumerable = items as Item[] ?? items.ToArray();
            var all = enumerable.Count();
            // var bounced... Not supported atm on Test env ...

            var o = Math.Round(distData.RecipientsSummary.OpenedRate * 100);
            var c = Math.Round(distData.RecipientsSummary.ClickThroughRate * 100);

            var openedPer  = Convert.ToInt32(o);
            var clickedPer = Convert.ToInt32(c);

            if (type != "individuals") // TODO unmute, once CCC-4359 is fixed
            {
                Assert.Contains(openedPer.ToString(), firstPage, "Total percent of opens");
                Assert.Contains(clickedPer.ToString(), firstPage, "Total percent of clicks");
            }
            Assert.Contains(expText.Texts.LinksMetricsHeader, firstPage,   "Click summary label");
            Assert.Contains(expText.Texts.Link.ToUpper(),     firstPage,   "Link summary label");
            Assert.Contains(expText.Texts.Clicks.ToUpper(),   firstPage,   "Number of clicks");
        }

        [Then(@"check clicks summary section for a report of type (PDF|XLSL|DOCX)")]
        public void ThenClicksSummarySection(string type)
        {
            var wholeReport = PropertyBucket.GetProperty<string>(FULL_REPORT_TEXT);
            var details = PropertyBucket.GetProperty<EmailDistDetails>("Distribution details");
            var links = details.Links.ToDictionary(x =>
                    _emailSentDetailsService.ShortifyLinkForPdfReport(x.Name ?? x.Href, 81), x => x.ClickCount);

            Assert.IsNotEmpty(links, "Testing data is wrong. Need links for assert");
            foreach (var linkDict in links)
            {
                if (type.Equals("DOCX")) // Docx report has rectangles of text that are cumbersome to check as a single line.
                {
                    Assert.Contains(linkDict.Key, wholeReport, "PDF does not contain link");
                    Assert.Contains(linkDict.Value.ToString(), wholeReport, "PDF does not contain link");
                } else
                {
                    Assert.Contains(linkDict.Key + " " + linkDict.Value, wholeReport, "PDF does not contain link");
                }            
            }
        }

        [Then(@"check copy of email page")]
        public void ThenCopyOfEmailPage()
        {
            var report = PropertyBucket.GetProperty<List<string>>(REPORT_KEY);
            var expText = PropertyBucket.GetProperty<GenerateReport>(REPORT_LANG);

            var copyOfEmailPage = report.Count(it => it.Contains(expText.Texts.CopyOfEmail));
            Assert.AreEqual(copyOfEmailPage, 1, "Copy of Email page was no in the report");
        }
                
        private void ThenEmailDetailsPage(Func<string, string> fmt)
        {
            var report   = PropertyBucket.GetProperty<List<string>>(REPORT_KEY);
            var expText  = PropertyBucket.GetProperty<GenerateReport>(REPORT_LANG);
            //var distData = PropertyBucket.GetProperty<EmailDist>(EXP_DIST_KEY);
            var details  = PropertyBucket.GetProperty<EmailDistDetails>("Distribution details");
            var user     = PropertyBucket.GetProperty<DynamicUser>(LoginSteps.USER_KEY);
            var timezone = user.TimeZone.Id;                      

            var emailDetailsPage = report.FirstOrError(it => it.Contains(expText.Texts.EmailDetails), $"Text not found: {expText.Texts.EmailDetails}");
            Assert.Contains(expText.Texts.Subject, emailDetailsPage, "Subject label");
            Assert.Contains(details.Subject,      emailDetailsPage, "Subject of Email");

            var submitted = ParseReportDateTime(details.SubmittedDate, timezone);
            Assert.Contains(submitted.ToString(fmt.Invoke(expText.Texts.Submitted)), emailDetailsPage, "Submitted date");
            
            bool containsTimeZoneInfo = Regex.IsMatch(details.SendDate, ".*T.*[+-].*");
            var sent = containsTimeZoneInfo
                ? ParseReportDateTime(details.SendDate, timezone)
                : DateTimeOffset.Parse(details.SendDate);

            Assert.Contains(sent.ToString(fmt.Invoke(expText.Texts.Sent)), emailDetailsPage, "Sent label");

            var created = ParseReportDateTime(details.CreatedDate, timezone);
            Assert.Contains(created.ToString(fmt.Invoke(expText.Texts.Created)), emailDetailsPage, "Created label");
        }

        [Then(@"check email details page to have times in user settings timezone for type of report (PDF|XLSL|DOCX)")]
        public void ThenEmailDetailsPage(ReportType type)
        {
            Func<string, string> fmt1 = label => $"'{label}': \nddd, dd MMM yyyy HH':'mm':'ss";
            Func<string, string> fmt2 = label => $"'{label}':ddd, dd MMM yyyy HH':'mm':'ss";
            ThenEmailDetailsPage(type == ReportType.PDF ? fmt1 : fmt2);
        }

        private DateTimeOffset ParseReportDateTime(string date, string timezone) =>
            DateTime.Parse(date).ConvertIntoTimezone(timezone);

        [Then(@"check recipients lists")]
        public void ThenRecipientsLists()
        {
            // Recipients lists
            var expText = PropertyBucket.GetProperty<GenerateReport>(REPORT_LANG);
            var details = PropertyBucket.GetProperty<EmailDistDetails>(EXP_DIST_DETAILS_KEY);
            var report  = PropertyBucket.GetProperty<List<string>>(REPORT_KEY);
            var recipients = PropertyBucket.GetProperty<RecipientsResponse[]>(DISTRIBUTION_RECIPIENTS);
            var emailDetailsPage = report.FirstOrError(it => it.Contains(expText.Texts.EmailDetails), $"Text not found: {expText.Texts.EmailDetails}");

            Assert.Contains(expText.Texts.RecipientsList, emailDetailsPage, "Recipient Lists label");
            var lists = new List<EntityList>()
                .Concat(details.MediaContactLists)
                .Concat(details.MediaOutletLists)
                .Select(it => it.Name)
                .ToList();

            //Double check recipients 
            //Individuals and Organizations are not coming anymore
            //in Details from SENT distributions
            Assert.IsNotEmpty(recipients.ToList(), Err.Msg("List contains empty values"));

            Assert.IsNotEmpty(lists, Err.Msg("Response contained 0 valid lists for report assertion."));
            lists.ForEach(list =>
                    Assert.Contains(list, emailDetailsPage, Err.Msg("List was not present")));
        }

        [Then(@"check list of recipients for the report of type (.*) and recipients (.*)")]
        public void ThenListOfRecipients(ReportType reportype, string rectype)
        {
            /*
            List of Media Contact Recipients
            Name                               Outlet      Opened Email         Links Clicked     
            Oleh Ilnytskyi                      Test         Yes / No                5            
            */
            var wholeReport = PropertyBucket.GetProperty<string>(FULL_REPORT_TEXT);
            var expText     = PropertyBucket.GetProperty<GenerateReport>(REPORT_LANG);
            var rec         = PropertyBucket.GetProperty<RecipientsResponse>(DISTRIBUTION_KEY).Items;

            // Table headers
            var name = expText.Texts.GetListOfRecipientsHeading(rectype);
            Assert.Contains(name, wholeReport, "Report didn't contain recipients table");

            var table  = CleanUpReportString(wholeReport.Substring(wholeReport.LastIndexOf(name)));
            var headers =
                expText.Texts.GetPdfReportRecipientsTableHeaders(rectype) // extraction tool don't understand blocks of text in the PDF table, no time to teach it
                .Select(it => it.Contains(" ") ? it.Split(' ').LastOrError() : it) //  but this should be enough for for the BA
                .ToList();

            if (reportype == ReportType.PDF)
            {
                var headText = headers.Aggregate((a, b) => a + b);
                Assert.Contains(headText, table, "Table headers were wrong");
            } else
                headers.ForEach(header =>
                             Assert.Contains(header, table, "Table headers were wrong"));

            // Rows of the table
            rec.ForEach(r =>
            {
                var parts = new List<string>
                {
                    r.Name, r.Info2,
                    r.OpenCount > 0 ? expText.Texts.Opened : expText.Texts.NotOpened,
                    r.ClickCount.ToString(),
                    r.Info1
                }
                .Where(it => it != null)
                .Select(CleanUpReportString)
                .ToList();
                
                if (reportype == ReportType.PDF)
                {
                    if (parts.Remove("NotOpened")) // In the PDF it goes in a rectagnle something as Not \n Opened. 
                    {
                        parts.Add("Not");
                        parts.Add("Opened");
                    }
                }

                foreach (var part in parts)
                {
                    Assert.Contains(part, table, $"Report {reportype} does not contain recipient info");
                }
            });
        }

        [Then(@"check section of email opens")]
        public void ThenCheckSectionOfEmailOpens()
        {
            var wholeReport      = PropertyBucket.GetProperty<string>(FULL_REPORT_TEXT);
            var expText          = PropertyBucket.GetProperty<GenerateReport>(REPORT_LANG);
            var allExpRecipients = PropertyBucket.GetProperty<RecipientsResponse[]>(DISTRIBUTION_RECIPIENTS);
            var details          = PropertyBucket.GetProperty<EmailDistDetails>("Distribution details");
            var openedRecipients = allExpRecipients
                .Where(it => it.DistId == details.DistributionId.ToString())
                .SelectMany(it => it.Items)
                .Where(recipient => recipient.OpenCount > 0);

            // Cut text from Opens ... Links clicks.
            Assert.Contains(expText.Texts.Opened, wholeReport, $"Report does not contain {expText.Texts.Opened} section");
            var table = wholeReport.Substring(wholeReport.LastIndexOf(expText.Texts.Opened));
            var till = table.IndexOf(expText.Texts.LinksClickedSectionHeader);
            var opensSection = till > 0 ? table.Substring(0, till) : table;

            openedRecipients.Select(it => it.Name).ToList()
                .ForEach(name => Assert.Contains(name, opensSection, "Opens were wrong"));
        }

        [Then(@"check section of email clicks")]
        public void ThenCheckSectionOfEmailClicks()
        {
            var expText = PropertyBucket.GetProperty<GenerateReport>(REPORT_LANG);
            var wholeReport = PropertyBucket.GetProperty<string>(FULL_REPORT_TEXT);

            var allExpRecipients = PropertyBucket.GetProperty<RecipientsResponse[]>(DISTRIBUTION_RECIPIENTS);
            var details = PropertyBucket.GetProperty<EmailDistDetails>("Distribution details");

            var recs = allExpRecipients
                .Where(it => it.DistId == details.DistributionId.ToString())
                .SelectMany(it => it.Items).ToList();

            var links = recs
                .Where(it => it.ClickedLinks != null && it.ClickedLinks.Any())
                .SelectMany(recipient => recipient.ClickedLinks).Distinct().ToList();

            Assert.Contains(expText.Texts.Opened, wholeReport,
                $"Report does not contain {expText.Texts.LinksClickedSectionHeader} section");

            var table = wholeReport.Substring(wholeReport.LastIndexOf(expText.Texts.LinksClickedSectionHeader));
            foreach (var link in links)
            {
                var linkText = string.IsNullOrEmpty(link.Name) ? link.Href : link.Name;
                Assert.Contains(linkText, table, "Link was not present in the clicked links section");

                var expRec = recs.Where(it => it.ClickedLinks != null && it.ClickedLinks.Contains(link)).ToList();
                expRec.ForEach(r =>
                    Assert.Contains(r.Name, table, "Recipient was not present in  clicks section"));            
            }
        }
        
        [When(@"I request sent email distribution details")]
        public EmailDistDetails WhenIRequestDistributionDetails()
        {
            var distAcc = PropertyBucket.GetProperty<PublishActivity>(PublishActivitySteps.ACTIVITY_KEY);
            var emailDistDetails = _emailSentDetailsService.GetDistDetails(distAcc.EntityId);
            PropertyBucket.Remember(DETAILS_KEY, emailDistDetails, true);
            return emailDistDetails;
        }

        [Given(@"request distribution recipients")]
        public List<IMediaListItem> GivenRequestDistributionRecipients()
        {
            var dist = PropertyBucket.GetProperty<EmailDist>(EmailDistributionWizardSteps.DISTRIBUTION_KEY);
            var total = new EntityListService(SessionKey)
                .GetEmailDistributionRecipientListItems(dist, PropertyBucket);
            PropertyBucket.Remember(DISTRIBUTION_RECIPIENTS, total, true);
            return total;
        }

        [Then(@"email sent details analytics request shows '(.*)' opens, '(.*)' clicks, '(.*)' bounces")]
        public void ThenEmailSentDetailsAnalyticsRequestShowsClicksOpensBounces(int opens, int clicks, int bounces)
        {
            var emailDistDetails = PropertyBucket.GetProperty<EmailDistDetails>(DETAILS_KEY);
            var details = emailDistDetails.RecipientsSummary;
            var mediaListItems = PropertyBucket.GetProperty<List<IMediaListItem>>(DISTRIBUTION_RECIPIENTS);
            var total = mediaListItems
                .GroupBy(g => g.GetType())
                .SelectMany(g => g.DistinctBy(_ => _.Id).ToList())
                .ToList();

            Assert.AreEqual(opens,   details.KnownOpenedCount,                "Opens count");
            Assert.AreEqual(clicks,  details.RecipientsWithClickThroughCount, "Clicks count");
            Assert.AreEqual(bounces, details.BouncedCount,                    "Bounces count");
            Assert.AreEqual(total.Count, details.TotalRecipientCount,       "Total count");
        }

        [Then(@"each link parsed correctly")]
        public void ThenEachLinkParsedCorrectly()
        {
            var emailDistDetails = PropertyBucket.GetProperty<EmailDistDetails>(DETAILS_KEY);
            var expLinks = PropertyBucket.GetProperty<List<Link>>(LINKS_KEY);
            var actLinks = emailDistDetails.Links;

            foreach (var link in expLinks)
            {
                Assert.That(actLinks, Has.Exactly(1).EqualTo(link), "Link is not parsed correctly.");
            }
        }

        [When(@"open event present in the system for '(.*)' recipients")]
        public void WhenOpenEvenPresentInTheSystemForRecipients(int num)
        {
            var userInfo = PropertyBucket.GetProperty<DynamicUser>(LoginSteps.USER_KEY);
            var emailDistDetails = PropertyBucket.GetProperty<EmailDistDetails>(DETAILS_KEY);
            var rec = PropertyBucket.GetProperty<List<IMediaListItem>>(DISTRIBUTION_RECIPIENTS).Shuffle().ToList();
            PropertyBucket.Remember(DISTRIBUTION_RECIPIENTS, rec, true);

            var recipients = rec
                .Where(it => ! string.IsNullOrEmpty(it.Email))
                .Some(num)
                .ToList();

            using (var service = new EmailDistributionDbService(userInfo.CompanyId))
            {
                recipients.ForEach(recipient =>
                    service.OpenEmail(emailDistDetails, recipient, num));
            }
        }

        [When(@"click event present in the system for '(\d+)' recipient(?:s|) to click '(\d+)' link(?:s|)")]
        public void WhenClickEvenPresentInTheSystemForRecipientsToClickLink(int numOfClicked, int numOfLinks)
        {
            var dist = PropertyBucket.GetProperty<EmailDist>(EmailDistributionWizardSteps.DISTRIBUTION_KEY);
            var emailDistDetails = PropertyBucket.GetProperty<EmailDistDetails>(DETAILS_KEY);
            var companyId = dist.CompanyId;

            var contacts = PropertyBucket.GetProperty<List<IMediaListItem>>(DISTRIBUTION_RECIPIENTS)
                .Where(it => ! string.IsNullOrEmpty(it.Email))
                .Some(numOfClicked)
                .ToList();

            var expLinks = PropertyBucket.GetProperty<List<Link>>(LINKS_KEY);
            var userInfo = PropertyBucket.GetProperty<DynamicUser>(LoginSteps.USER_KEY);
            using (var service = new EmailDistributionDbService(userInfo.CompanyId))
            {
                contacts.ForEach(recipient =>
                    expLinks.Some(numOfLinks).ToList()
                        .ForEach(link => service.ClickEmailLink(emailDistDetails, recipient, link, numOfClicked)));
            }
        }

        [Then(@"'(.*)' opens appears in email details")]
        public void ThenOpensAppearsInEmailDetails(int num)
        {
            var distAcc = PropertyBucket.GetProperty<PublishActivity>(PublishActivitySteps.ACTIVITY_KEY);
            var details = _emailSentDetailsService.GetDistDetails(distAcc.EntityId);
            Assert.AreEqual(num, details.RecipientsSummary.KnownOpenedCount, "Known opens count wrong");
        }
    }
}