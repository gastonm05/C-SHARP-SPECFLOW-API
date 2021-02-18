
namespace CCC_API.Data.Responses.Activities
{
    public class DistributionPrivilegeResponse
    {
        public PriviliegeDistribution Distribution { get; set; }
    }

    public class PriviliegeDistribution
    {
        public bool HasAccess { get; set; }
        public bool HasPRWeb { get; set; }
        public PriviliegePRWeb PRWeb { get; set; }
    }

    public class PriviliegePRWeb
    {
        public bool CanCreate { get; set; }
        public bool CanDelete { get; set; }
        public bool CanEdit { get; set; }
        public bool CanView { get; set; }
    }
}
