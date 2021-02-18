using System.Collections.Generic;
using CCC_API.Data.Responses.Media;

namespace CCC_API.Data.Responses.Email
{
    public class EmailDistDetails
    {
        public int DistributionId { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public string CreatedDate { get; set; }
        public string SubmittedDate { get; set; }
        public string SendDate { get; set; }
        public string PreviewImage { get; set; }
        public string ScheduleType { get; set; }
        public List<EntityList> MediaContactLists { get; set; }
        public List<EntityList> MediaOutletLists { get; set; }
        public List<EntityList> IndividualLists { get; set; }
        public List<EntityList> OrganizationLists { get; set; }
        public string TimeZoneIdentifier { get; set; }
        public RecipientsSummary RecipientsSummary { get; set; }
        public List<Link> Links { get; set; }
        public bool TrackingEnabled { get; set; }
    }

    public class RecipientsSummary
    {
        public int TotalRecipientCount { get; set; }
        public int KnownOpenedCount { get; set; }
        public int RecipientsWithClickThroughCount { get; set; }
        public int BouncedCount { get; set; }
        public double OpenedRate { get; set; }
        public double ClickThroughRate { get; set; }
        public double BouncedRate { get; set; }
    }

    public class Link
    {
        public string Href { get; set; }
        public string Name { get; set; }
        public int ClickCount { get; set; }

        protected bool Equals(Link other)
        {
            return ClickCount == other.ClickCount 
                && string.Equals(Href, other.Href) 
                && string.Equals(Name, other.Name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Link) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = ClickCount;
                hashCode = (hashCode * 397) ^ (Href?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (Name?.GetHashCode() ?? 0);
                return hashCode;
            }
        }
    }
}
