namespace CCC_API.Data.Responses.Analytics.Available
{
    public class LanguageArgs
    {
        public string company { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(company);
        }
    }
}