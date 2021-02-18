
namespace CCC_API.Data.TestDataObjects
{
    public class Account
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string LoginName { get; set; }
        public Areas Areas { get; set; }
        public string LanguageId { get; set; }
        public TimeZonesResponse TimeZone { get; set; }
        public string CurrencySymbol { get; set; }
    }
}
