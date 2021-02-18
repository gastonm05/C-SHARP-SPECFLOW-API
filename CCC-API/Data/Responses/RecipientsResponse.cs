using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CCC_API.Data.Responses
{   
    /// <summary>
    /// Email distribution details response for tracking info for recipients.
    /// Example, 
    /// </summary>
    public class RecipientsResponse
    {
        public int PageSize { get; set; }
        public int Offset { get; set; }
        public int TotalCount { get; set; }
        public int ItemCount { get; set; }
        public List<Item> Items { get; set; }
        public Links Links { get; set; }
        public string DistId { get; set; }
        public string Type { get; set; }
    }

    public class ClickedLink
    {
        public string Href { get; set; }
        public string Name { get; set; }
        public int ClickCount { get; set; }

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            ClickedLink other = obj as ClickedLink;
            return string.Equals(Href, other.Href) && string.Equals(Name, other.Name) && ClickCount == other.ClickCount;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Href != null ? Href.GetHashCode() : 0;
                hashCode = (hashCode * 197) ^ (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 197) ^ ClickCount;
                return hashCode;
            }
        }
    }

    public class Item
    {
        public int EntityId { get; set; }
        public int Type { get; set; }
        public string Name { get; set; }
        public string Info1 { get; set; }
        public string Info2 { get; set; }
        public object Avatar { get; set; }
        public int OpenCount { get; set; }
        public int ClickCount { get; set; }
        public bool HasOpened { get; set; }
        public bool HasClickedLink { get; set; }
        public List<ClickedLink> ClickedLinks { get; set; }
        public string EmailAddress { get; set; }
        public bool HasBounced { get; set; }
        

        protected bool Equals(Item other)
        {
            Func<string, string> cleanUp = s => Regex.Replace(s, @"\s+", " ");
            return EntityId == other.EntityId && Type == other.Type && 
                string.Equals(cleanUp(Name), cleanUp(other.Name));
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Item) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = EntityId;
                hashCode = (hashCode * 397) ^ Type;
                hashCode = (hashCode * 397) ^ (Name != null ? Name.GetHashCode() : 0);
                return hashCode;
            }
        }

        public override string ToString()
        {
            return EntityId + " " + Name + " " + Info1 + " " + Info2;
        }
    }

    public class Links
    {
        public object prev { get; set; }
        public object next { get; set; }
    }
}
