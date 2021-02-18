using CCC_API.Data.TestDataObjects.Analytics;
using System.Collections.Generic;

namespace CCC_API.Data.Responses.Activities
{
    class PRWebOnlinePickupResponse
    {
        public List<PRWebOnlinePickupSource> Sources { get; set; }
        public int TotalOnlinePickup { get; set; }
        public float PotentialAudience { get; set; }
    }
}
