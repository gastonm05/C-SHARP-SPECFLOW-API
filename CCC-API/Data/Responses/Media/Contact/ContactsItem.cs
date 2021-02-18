using CCC_API.Data.PostData.Media.Contact;
using System.Collections.Generic;

namespace CCC_API.Data.Responses.Media.Contact
{
    public class ContactsItem : IMediaListItem
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Id { get; set; }
        public bool IsProprietaryContact { get; set; }
        public string DisplayName { get; set; }
		public int OutletId { get; set; }
        public string OutletName { get; set; }
        public string PitchingProfile { get; set; }
        public ProprietaryDataItem ProprietaryData { get; set; }
        public List<SocialProfile> SocialProfiles { get; set; }
        public string SortName { get; set; }       
        public List<string> Subjects { get; set; }
        public List<ContactListData> Lists { get; set; }
        public bool HasMultipleOutlets { get; set; }
        public List<WorkingLanguages> WorkingLanguages { get; set; }
        public bool IsOptedOut { get; set; }
        public string Title { get; set; }
        public int UniqueVisitorsPerMonth { get; set; }
        public int CirculationAudience { get; set; }
        public string CountryName { get; set; }
        public string FullName { get; set; }
    }
}
