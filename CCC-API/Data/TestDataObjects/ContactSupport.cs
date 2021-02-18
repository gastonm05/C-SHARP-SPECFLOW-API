
using System.ComponentModel;

namespace CCC_API.Data.TestDataObjects
{
    public class ContactSupport
    {
        public int ContactSupportDetailsID { get; set; }
        public string Name { get; set; }
        public string LanguageKey { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ChatUrl { get; set; }
        public string FeedbackUrl { get; set; }
        public string Hours { get; set; }



        public ContactSupport(int contactSupportDetailsID, string languageKey, string email, string phoneNumber, string chatUrl, string feedbackUrl, string hours)
        {
            this.ContactSupportDetailsID = contactSupportDetailsID;
            this.LanguageKey = languageKey;
            this.Email = email;
            this.PhoneNumber = phoneNumber;
            this.ChatUrl = chatUrl;
            this.FeedbackUrl = feedbackUrl;
            this.Hours = hours;
        }
        /// <summary>
        /// This method Verifies a get contact support response for the given language key
        /// </summary>
        /// <param name="key"></param>
        /// <returns>boolean based on if the current data is a valid response for the given language key</returns>
        public bool VerifyResponse(string description, LanguageKeys languageKey)
        {
            
            switch (languageKey)
            {
                case LanguageKeys.EN_GB:
                    return !(ContactSupportDetailsID != 2 || !LanguageKey.Equals(description) || Email == null ||
                            PhoneNumber == null || ChatUrl == null || FeedbackUrl == null || Hours == null);
                case LanguageKeys.EN_CA:
                    return !(ContactSupportDetailsID != 3 || !LanguageKey.Equals(description) || Email == null ||
                            PhoneNumber == null || ChatUrl == null || FeedbackUrl == null || Hours == null);
                case LanguageKeys.DE_DE:
                    return !(ContactSupportDetailsID != 4 || !LanguageKey.Equals(description) || Email == null ||
                            PhoneNumber == null || ChatUrl == null || FeedbackUrl == null || Hours == null);
                case LanguageKeys.FR_FR:
                    return !(ContactSupportDetailsID != 5 || !LanguageKey.Equals(description) || Email == null ||
                            PhoneNumber == null || ChatUrl == null || FeedbackUrl == null || Hours == null);
                case LanguageKeys.IT_IT:
                    return !(ContactSupportDetailsID != 6 || !LanguageKey.Equals(description) || Email == null ||
                            PhoneNumber == null || ChatUrl == null || FeedbackUrl == null || Hours == null);
                case LanguageKeys.ES_ES:
                    return !(ContactSupportDetailsID != 7 || !LanguageKey.Equals(description) || Email == null ||
                            PhoneNumber == null || ChatUrl == null || FeedbackUrl == null || Hours == null);
                case LanguageKeys.FR_CA:
                    return !(ContactSupportDetailsID != 8 || !LanguageKey.Equals(description) || Email == null ||
                            PhoneNumber == null || ChatUrl == null || FeedbackUrl == null || Hours == null);
                case LanguageKeys.NL_NL:
                    return !(ContactSupportDetailsID != 10 || !LanguageKey.Equals(description) || Email == null ||
                           PhoneNumber == null || ChatUrl == null || FeedbackUrl == null || Hours == null);
                case LanguageKeys.FI_FI:
                    return !(ContactSupportDetailsID != 11 || !LanguageKey.Equals(description) || Email == null ||
                           PhoneNumber == null || ChatUrl == null || FeedbackUrl == null || Hours == null);
                case LanguageKeys.SV_SE:
                    return !(ContactSupportDetailsID != 12 || !LanguageKey.Equals(description) || Email == null ||
                          PhoneNumber == null || ChatUrl == null || FeedbackUrl == null || Hours == null);
                default:                    
                    return !(ContactSupportDetailsID != 1 || !LanguageKey.Equals("en-us") || Email == null ||
                           PhoneNumber == null || ChatUrl == null || FeedbackUrl == null || Hours == null);
            }
        }
    }
}