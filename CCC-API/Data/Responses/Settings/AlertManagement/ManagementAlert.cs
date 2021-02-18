using System;
using System.Collections.Generic;

namespace CCC_API.Data.Responses.Settings.AlertManagement
{
    public class ManagementAlert
    {
        public int Id { get; set; }
        public string EndDate { get; set; }
        public int OwnerId { get; set; }
        public string OwnerName { get; set; }
        public string SearchName { get; set; }
    }

    public class AnalyticsAlert
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public int OwnerId { get; set; }
        public string OwnerName { get; set; }
        public List<string> Recipients { get; set; }
        public DateTime StartDate { get; set; }
        public int DaysOfWeek { get; set; }
        public string Time { get; set; }
        public DateTime? EndDate { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public int Status { get; set; }
    }
}