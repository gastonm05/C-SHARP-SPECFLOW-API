using System;
using System.Collections.Generic;
using CCC_API.Data.TestDataObjects;
using CCC_API.Data.TestDataObjects.Activities;

namespace CCC_API.Data.Responses.Activities
{
    /// <summary>
    /// Activitis as it appears in PublishActivity response.
    /// </summary>
    public class Activity
    {
        public object Subject { get; set; }
        public int ActivityTypeId { get; set; }
        public object ActivityTypeName { get; set; }
        public List<object> ActivityCategoryNames { get; set; }
        public string ActivityDate { get; set; }
        public object ContentSnippet { get; set; }
    }

    /// <summary>
    /// Activities as how it appears in the reports.
    /// </summary>
    public class ExportActivity : IEquatable<ExportActivity>
    {
        public string Title { get; set; }
        public string Campaign { get; set; }
        public int Status { get; set; }
        public PublicationsStatus StatusName => (PublicationsStatus) Status;
        public string Type { get; set; }
        public DateTime DateTime { get; set; }

        public bool Equals(ExportActivity other)
        {
            return
                string.Equals(Campaign, other.Campaign)
                && (DateTime - other.DateTime).TotalMinutes <= 1
                && Status == other.Status
                && string.Equals(Title, other.Title);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ExportActivity) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Campaign != null ? Campaign.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ DateTime.GetHashCode();
                hashCode = (hashCode * 397) ^ Status;
                hashCode = (hashCode * 397) ^ (Title != null ? Title.GetHashCode() : 0);
                return hashCode;
            }
        }

        public override string ToString()
        {
            return $"{Title} {Type} {Status}";
        }
    }
}
