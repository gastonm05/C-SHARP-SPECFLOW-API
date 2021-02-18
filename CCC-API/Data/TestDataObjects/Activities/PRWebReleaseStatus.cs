namespace CCC_API.Data.TestDataObjects.Activities
{
    public enum PRWebReleaseStatus : int
    {
        NotSent = -2,
        Deleted = -1,
        PendingEditorialReview = 0,
        DraftMode = 1,
        OnHold = 2,
        PendingDistribution = 3,
        Published = 4,
        InEditorialReview = 5,
        OnHoldLocked = 6,
        OnHoldLimitedDistribution = 7,
        PendingDistributionUponUserApproval = 8,
        PendingSeniorReview = 9,
        Ignore = -99,
        NotFound = -999
    }

    public enum PublicationsStatus : int
    {
        Scheduled,
        Draft,
        Sent,
        InProgress,
        InReview,
        OnHold
    }
}
