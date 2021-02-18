

namespace CCC_API.Data.Responses.ACLS
{
    /// <summary>
    /// response from endpoint api/v1/acls: 200 - OK
    /// News Tags Section should include bool values for: 
    /// CanView, CanEdit, CanCreate and CanDelete based on user's permissions
    /// </summary>
    public class NewsTagsPermissions
    {
        public bool CanView { get; set; }
        public bool CanCreate { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
    }
}
