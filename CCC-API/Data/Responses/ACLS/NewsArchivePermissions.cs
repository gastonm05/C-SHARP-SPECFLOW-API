namespace CCC_API.Data.Responses.ACLS
{
    public class NewsArchivePermissions
    {
        public bool CanExportToNews { get; set; }
        public bool CanView { get; set; }
        public bool CanCreate { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
        public bool CanPreview { get; set; }
    }
}