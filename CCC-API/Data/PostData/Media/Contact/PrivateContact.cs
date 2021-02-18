using CCC_Infrastructure.Utils;
using System.Collections.Generic;
using System.Linq;

namespace CCC_API.Data.PostData.Media.Contact
{
    public class PrivateContact
    {
        public PrivateContact()
        {
            Lists = new List<ContactListData>();
        }

        public PrivateContact(int outletId, string first = null, string last = null, string email = null, int countryID = 210, IEnumerable<int> subjects = null, IEnumerable<ContactListData> lists = null) : this()
        {
            if (string.IsNullOrEmpty(first))
            {
                first = StringUtils.RandomAlphaNumericString(4);
            }
            if (string.IsNullOrEmpty(last))
            {
                last = StringUtils.RandomAlphaNumericString(8);
            }
            if (string.IsNullOrEmpty(email))
            {
                email = StringUtils.RandomEmail(8);
            }
            if (subjects == null)
            {
                subjects = new List<int>() { 319000 };
            }
            FirstName = first;
            LastName = last;
            Email = email;
            OutletId = outletId;
            CountryId = countryID;
            IsProprietaryContact = true;
            SubjectsIds = subjects.ToList();
            if (lists != null)
            {
                Lists = lists.ToList();
            }
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }         
        public int OutletId { get; set; }
        public int CountryId { get; set; }
        public bool IsProprietaryContact { get; set; }
        public List<int> SubjectsIds { get; set; }
        public List<ContactListData> Lists { get; set; }

        public static PrivateContact CreateNewForOutlet(string outletName = "", string first = null, string last = null, string email = null, IEnumerable<ContactListData> lists = null, int CountryId = 0)
        {
            var outlets = TestData.DeserializedJson<List<Responses.Media.Outlet.OutletsItem>>("Outlets.json", System.Reflection.Assembly.GetExecutingAssembly());
            int outletId = 0;
            if (!string.IsNullOrEmpty(outletName))
            {
                outletId = outlets.FirstOrError(o => o.FullName?.ToLower() == outletName.ToLower(), $"'{outletName}' not found in Outlets.json file.").Id;
            }

            return new PrivateContact(outletId, first: first, last: last, email: email, lists: lists, countryID: CountryId);
        }
    }
}
