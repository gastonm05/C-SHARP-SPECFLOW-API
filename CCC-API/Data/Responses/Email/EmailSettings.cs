namespace CCC_API.Data.Responses.Email
{
    /// <summary>
    /// Email settings like credits... for email distribution.
    /// </summary>
    public class EmailSettings
    {
        public bool HasUnlimitedEmails { get; set; }
        public int MaxRecipientsCount { get; set; }
        public int AvailableSubscriptions { get; set; }
        public bool HasAdditionalEmailAddresses { get; set; }
    }
}
