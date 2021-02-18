namespace CCC_API.Data.TestDataObjects.Activities
{
    /// <summary>
    /// Common publish activity types.
    /// </summary>
    public enum PublishActivityType : int
    {
        Email = -28,
        Twitter = -15,
        Facebook = -47, /*-19*/
        LinkedIn = -21, /*-23 / -25 */
        PRWebRelease = -29,
        PRWeb = -29,
        Appointment = -2,
        Inquiry = -1,
        SendMailing = -4,
        Callback = -3,
        Other = -71,
        Pinterest = -72,
        GlobalPortal = -73,
        /// <summary>
        /// This is the releases created in the CPRE PRN wizard going through Order API
        /// </summary>
        CprePrNewswire = -74,
        BCCEmail = -75
    }
}
