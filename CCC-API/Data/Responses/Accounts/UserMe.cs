using CCC_API.Data.TestDataObjects;

namespace CCC_API.Data.Responses.Accounts
{
    public class UserMe
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
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
        public object ExpirationDate { get; set; }
        public object CurrentPassword { get; set; }
        public object Password { get; set; }
        public object PasswordConfirm { get; set; }
        public object DataGroups { get; set; }
        public int CountryId { get; set; }
        public object LoginId { get; set; }
        public int PermissionId { get; set; }
        public object AccountStatus { get; set; }
        public object PermissionIds { get; set; }
        public string PermissionAccessType { get; set; }
        public bool IsManager { get; set; }
        public bool IsReadOnly { get; set; }
        public string SessionId { get; set; }
        public string OMCUserId { get; set; }
        public object Authorizations { get; set; }
        public object RequestedAuthorizationDate { get; set; }
        public bool IsLocked { get; set; }
        public bool HasSenderAuthorization { get; set; }
        public object ExternalId { get; set; }
    }
}
