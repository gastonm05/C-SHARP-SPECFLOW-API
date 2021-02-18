using CCC_Infrastructure.Utils;
using System;
using System.Collections.Generic;
namespace CCC_API.Data.PostData
{   
    /// <summary>
    /// Generate PDF report settings on Email Sent Details page.
    /// </summary>
    public class GenerateReport
    {
        public string ReportType { get; set; }
        public Texts Texts { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string FileType { get; set; }
        public bool IncludeRecipientSummary { get; set; }
        public bool IncludeLinkSummary { get; set; }
        public bool Opened { get; set; }
        public bool Clicked { get; set; }
        public bool Bounced { get; set; }
        public bool IncludeCoverPage { get; set; }
        public bool IncludeDetails { get; set; }
        public bool IncludeEmailImage { get; set; }
        public bool IncludeOutletRecipients { get; set; }
        public bool IncludeContactRecipients { get; set; }
        public bool IncludeIndividualRecipients { get; set; }
        public bool IncludeOrganizationRecipients { get; set; }
        public bool IncludeRecipientMetrics { get; set; }
        public bool IncludeLinksMetrics { get; set; }
        public bool IncludeWhoOpened { get; set; }
        public bool IncludeWhoClicked { get; set; }
        public bool IncludeWhoBounced { get; set; }
        public bool TrackingEnabled { get; set; }
        public string ClientCulture { get; set; }
    }

    public class Texts
    {
        public string Author { get; set; }
        public string RecipientMetricsHeader { get; set; }
        public string Opened { get; set; }
        public string Clicked { get; set; }
        public string Bounced { get; set; }
        public string LinksMetricsHeader { get; set; }
        public string Link { get; set; }
        public string Clicks { get; set; }
        public string NoLinksOnJob { get; set; }
        public string Contacts { get; set; }
        public string Outlets { get; set; }
        public string ListOfContactsRecipients { get; set; }
        public string ListOfOutletsRecipients { get; set; }
        public string ListOfOrganizationRecipients { get; set; }
        public string ListOfIndividualRecipients { get; set; }
        public string NoContacts { get; set; }
        public string NoOutlets { get; set; }
        public string CopyOfEmail { get; set; }
        public string EmailDetails { get; set; }
        public string Subject { get; set; }
        public string Created { get; set; }
        public string Submitted { get; set; }
        public string Sent { get; set; }
        public string RecipientsList { get; set; }
        public string Immediate { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string LinksClicked { get; set; }
        public string Click { get; set; }
        public string HasOpened { get; set; }
        public string Outlet { get; set; }
        public string Yes { get; set; }
        public string No { get; set; }
        public string WhoOpenedSectionHeader { get; set; }
        public string WhoBouncedSectionHeader { get; set; }
        public string NoOpens { get; set; }
        public string LinksClickedSectionHeader { get; set; }
        public string NoLinksClicked { get; set; }
        public string ClickCount { get; set; }
        public string Recipients { get; set; }
        public string DistributionName { get; set; }
        public string DateTimeSent { get; set; }
        public string EmailSubject { get; set; }
        public string MediaContact { get; set; }
        public string MediaOutlet { get; set; }
        public string EmailAddress { get; set; }
        public string Status { get; set; }
        public string LinkSummary { get; set; }
        public string NotOpened { get; set; }
        public string ListName { get; set; }
        public string Individual { get; set; }
        public string Individuals { get; set; }
        public string IndividualListNames { get; set; }
        public string Organization { get; set; }
        public string OrganizationName { get; set; }
        public string Organizations { get; set; }
        public string OrganizationListNames { get; set; }
        
        /// <summary>
        /// List of [type] table headers.
        /// </summary>
        /// <param name="type"></param>
        /// <returns>List</returns>
        public List<string> GetPdfReportRecipientsTableHeaders(string type)
        {
            var headers = new List<string>();
            switch (type.ToLower())
            {
                case "outlets":
                    headers.Add(Outlet);
                    break;
                case "contacts":
                    headers.Add(Name);
                    headers.Add(Outlet);
                    break;
                case "individuals":
                    headers.Add(Name);
                    headers.Add(Organization);
                    break;
                case "organizations":
                    headers.Add(Organization);
                    break;
                default: throw new ArgumentException(Err.Msg($"Uknown type {type}"));
            }

            headers.Add(Status);
            headers.Add(LinksClicked);
            return headers;
        }

        /// <summary>
        /// List of [type] recipients label.
        /// </summary>
        /// <param name="type"></param>
        /// <returns>List</returns>
        public string GetListOfRecipientsHeading(string type)
        {
            var header = "";
            switch (type.ToLower())
            {
                case "outlets":
                    header = MediaOutlet;
                    break;
                case "contacts":
                    header = MediaContact;
                    break;
                case "individuals":
                    header = Individual;
                    break;
                case "organizations":
                    header = Organization;
                    break;
                default: throw new ArgumentException(Err.Msg($"Uknown type {type}"));
            }
            return $"List of {header} Recipients";
        }
    }
}
