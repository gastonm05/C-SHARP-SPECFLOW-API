using System;
using System.Collections.Generic;
using CCC_API.Data.Responses.Activities;
using CCC_API.Data.Responses.Media;
using RestSharp;

namespace CCC_API.Data.Responses.Email
{
    /// <summary>
    /// Email distribution main object. 
    /// Appears on submitting an email distribution. Activities > Create New > Email Distribution.
    /// </summary>
    public class EmailDist
    {
        public int DistributionId { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public string TextBody { get; set; }
        public string HTMLBody { get; set; }
        public int WizardStep { get; set; }
        public bool OverrideOptOutAddress { get; set; }
        public string OptOutName { get; set; }
        public string OptOutAddressLine1 { get; set; }
        public string OptOutAddressLine2 { get; set; }
        public string OptOutCity { get; set; }
        public string OptOutState { get; set; }
        public string OptOutZip { get; set; }
        public int OptOutCountryId { get; set; }
        public int OptOutWorkingLanguageID { get; set; }
        public int EmailType { get; set; }
        public string EmailSenderName { get; set; }
        public string EmailSenderAddress { get; set; }
        public bool SendEmailCopy { get; set; }
        public int? ScheduleType { get; set; }
        public string TimeZoneIdentifier { get; set; }
        public string SendTime { get; set; }
        public int CompanyId { get; set; }
        public int DataGroupId { get; set; }
        public List<BaseMedia> MediaContacts { get; set; }
        public List<BaseMedia> MediaOutlets { get; set; }
        public List<BaseMedia> Organizations { get; set; }
        public List<BaseMedia> Individuals { get; set; }
        public bool CreateActivity { get; set; }
        public Activity Activity { get; set; }
        public object PublishActivity { get; set; }
        public bool CreatePrAsset { get; set; }
        public int Entity { get; set; }
        public bool HasCustomPlainTextVersion { get; set; }
        public bool GoogleAnalyticsEnabled { get; set; }
        public string GoogleAnalyticsSource { get; set; }
        public string GoogleAnalyticsMedium { get; set; }
        public string GoogleAnalyticsCampaign { get; set; }
        public string GoogleAnalyticsContent { get; set; }
        public string PrimaryAttachmentFileName { get; set; }
        public string PrimaryAttachmentFilePath { get; set; }
        public int? PrimaryAttachmentFileSize { get; set; }
        public int HtmlTemplateId { get; set; }
        public int HtmlTemplateType { get; set; }
        public object CampaignId { get; set; }
        public List<string> AdditionalEmailAddresses { get; set; }
        public object DehydratedUnsavedLegacyDistribution { get; set; }

        public string TrackingParameters { get; set; }
        public string TrackingType { get; set; }

        public void SetScheduleType(ScheduleTypes type) => ScheduleType = (int) type;

        public enum ScheduleTypes { Now = 1, InFuture = 2 }

        public void SetSendTime(DateTime time)
        {
            SendTime = time.AddSeconds(-time.Second).ToString(DateFormat.ISO_8601);
        }

        /// <summary>
        /// Send time property.
        /// </summary>
        /// <returns></returns>
        public DateTime GetSendTime()
        {
            return DateTime.Parse(SendTime);
        }
    }
}
