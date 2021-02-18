using System.Linq;
using CCC_Infrastructure.UserSupport;

namespace CCC_API.Data.PostData.Settings.UserManagement
{
    /// <summary>
    /// User for testing purposes.
    /// </summary>
    public class DynamicUser : UserManagementPostData
    {
        public string SessionKey { get; set; }
        public string CompanyEdition { get; set; }
        public string CompanyId { get; set; }
        public string ExpirationStamp { get; set; }
        public PermissionType RequestedPermissions { get; set; }
        public string RequestedDataGroupsCsv { get; set; }

        public User AsTestUser()
        {
            return new User
            {
                CompanyEdition = CompanyEdition,
                CompanyID = CompanyId,
                Password = Password,
                Username = LoginName,
                DataGroup = DataGroups?
                      .Select(it => it.name).Aggregate((one, two) => one + "," + two)
            };
        }

        public string BearerSessionKey() => "Bearer " + SessionKey;
        
        public enum PermissionType
        {
            standard, read_only, system_admin, data_admin
        }

    }
}
