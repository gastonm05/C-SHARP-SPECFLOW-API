using CCC_API.Data.TestDataObjects.Activities;

namespace CCC_API.Data.Responses.Activities
{
    public class DistroResponseStatus
    {
        public PRWebDistributionResponse ApiData { get; set; }
        public PublicationsStatus PublicationStatus { get; set; }
        public string PublicationStatusDescription { get; set; }
        public PRWebReleaseStatus PRWebReleaseStatus { get; set; }

        public DistroResponseStatus(PublicationsStatus publicationStatus, string publicationStatusDescription, PRWebReleaseStatus prwebReleaseStatus)
        {
            PublicationStatus = publicationStatus;
            PublicationStatusDescription = publicationStatusDescription;
            PRWebReleaseStatus = prwebReleaseStatus;
        }
    }
}
