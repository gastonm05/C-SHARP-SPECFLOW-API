using System;
using CCC_API.Data.TestDataObjects;

namespace CCC_API.Data.Responses.Settings.UserManagement
{
    public class DataGroupResponse
    {
        public int id { get; set; }
        public int accountId { get; set; }
        public Boolean IsActive { get; set; }
        public string name { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public int countryId { get; set; }
        public string countryName { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
        public Boolean isDefault { get; set; }

        /// <summary>
        /// Profile to data group response.
        /// </summary>
        /// <param name="profile"></param>
        /// <returns>DataGroupResponse</returns>
        public DataGroupResponse FromProfile(Profile profile)
        {
            return new DataGroupResponse
            {
                id = profile.Id,
                IsActive = profile.IsActive,
                name = profile.Name,
                accountId = profile.AccountId
            };
        }
    }
}
