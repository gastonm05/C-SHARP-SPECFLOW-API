namespace CCC_API.Data.Responses.Activities
{
    public class Package
    {
        public int PackageId { get; set; }
        public int MaximumTopicsAllowed { get; set; }
        public int MaximumCitiesAllowed { get; set; }
        public int MaximumMediaDigestsAllowed { get; set; }
        public bool AllowsVideo { get; set; }
    }
}
