using CCC_API.Data.PostData.Media.Outlet;
using CCC_API.Data.Responses.Media.Contact;
using System.Collections.Generic;

namespace CCC_API.Data.Responses.Media.Outlet
{
  public  class OutletsItem : IMediaListItem
    {
        public string SortName { get; set; }
        public string FullName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Email { get; set; }
        public string CountryName { get; set; }
        public string TypeName { get; set; }
        public List<string> Subjects { get; set; }
        public string DMAName { get; set; }
        public int Id { get; set; }
        public int UniqueVisitorsPerMonth { get; set; }
        public string Medium { get; set; }
        public string RegionalFocus { get; set; }
        public string Frequency { get; set; }
        public int CirculationAudience { get; set; }
        public bool IsProprietaryOutlet { get; set; }
        public bool IsAffiliatedMediaOutlet { get; set; }
        public bool IsNODOutlet { get; set; }
        public string Location { get; set; }
        public string CountyName { get; set; }
        public bool IsOptedOut { get; set; }
        public List<OutletListData> Lists { get; set; }
        public List<WorkingLanguages> WorkingLanguages { get; set; }
    }
}
