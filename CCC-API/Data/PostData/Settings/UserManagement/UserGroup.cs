
namespace CCC_API.Data.PostData.Settings.UserManagement
{
    /// <summary>
    /// Permissions groups for user management. 
    /// </summary>
    public class UserGroup
    {
        public int UserGroupId { get; set; }
        public string Name { get; set; }
        public object Description { get; set; }
        public int CompanyId { get; set; }
        public int Version { get; set; }
        public int PermissionAccessType { get; set; }
    }
}
