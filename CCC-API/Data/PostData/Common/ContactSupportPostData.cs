

using CCC_API.Data.TestDataObjects;

namespace CCC_API.Data.PostData.Common
{
    public class ContactSupportPostData
    {
        public string FromName { get; set; }
        public string FromEmail { get; set; }
        public string Message { get; set; }
        public string ToEmail { get; set; }
        public LanguageKeys LanguageKey { get; set; }


        public ContactSupportPostData() { }

        public ContactSupportPostData(string fromName, string fromEmail, string message, string toEmail, LanguageKeys languageKey)
        {
            this.FromName = fromName;
            this.FromEmail = fromEmail;
            this.Message = message;
            this.ToEmail = toEmail;
            this.LanguageKey = languageKey;
        }
    }
}