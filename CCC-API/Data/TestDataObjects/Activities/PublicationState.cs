namespace CCC_API.Data.TestDataObjects.Activities
{
    public enum PublicationState : int
    {
        Scheduled = 0, // scheduled or for non-distribution it's just the date it occurs on
        Draft = 1, // WIP stuff
        Sent = 2, // distributed/sent/disseminated/time passed for non-distribution
        InProgress = 3, // temporary short lived status, we're prepping stuff
        InReview = 4, // awaiting editorial approval/processing
        OnHold = 5, // distribution is waiting for user action before it can be sent (i.e. Approval from user)
        Deleted = 6, // shouldn't be used
        Cancelled = 7, // system has cancelled the distribution for whatever reason
        ReadyForApproval = 8, // PR Newswire specific state that requires user's attention after IRIS has prepared distribution release
        Failed = 9 // The publication failed i.e. a social post via the social publishing API failed
    }
}
