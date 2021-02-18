using System;
using System.Collections.Generic;
using CCC_API.Data.TestDataObjects;
using CCC_API.Data.Responses.Settings.UserManagement;

namespace CCC_API.Data.PostData.Settings.UserManagement
{
    public class UserManagementPostData
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public List<object> Authorizations { get; set; } = new List<object>();
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string LoginName { get; set; }
        public TimeZonesResponse TimeZone { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool IsSysAdmin { get; set; }
        public bool IsAEUser { get; set; }
        public bool IsWebUser { get; set; }
        public string DefaultSection { get; set; }
        public string ExpirationDate { get; set; }
        public string CurrentPassword { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        public DataGroupResponse[] DataGroups { get; set; }
        public int CountryId { get; set; }
        public string LoginId { get; set; }
        public int PermissionId { get; set; }
        public string AccountStatus { get; set; }
        public int[] PermissionIds { get; set; }
        public string PermissionAccessType { get; set; }
        public bool IsManager { get; set; }
        public bool IsReadOnly { get; set; }
        public string SessionId { get; set; }
        public string OMCUserId { get; set; }
        public string ExternalId { get; set; }
    }
}
