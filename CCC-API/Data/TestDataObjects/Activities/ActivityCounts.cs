using System;

namespace CCC_API.Data.TestDataObjects.Activities
{
    public class ActivityCounts
    {
        public DateTime Date { get; set; }
        public long Timestamp { get; set; } 
        public int ActivityCount { get; set; }
        public string Type { get; set; }
        public int PublicationState { get; set; }
    }
}

