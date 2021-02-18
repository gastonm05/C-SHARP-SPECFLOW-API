using CCC_API.Utils.Assertion;
using System.Collections.Generic;
using System.Linq;
using Is = NUnit.Framework.Is;

namespace CCC_API.Data.Responses.Analytics
{
    public class AnalyticsSearch
    {
        public int SearchId { get; set; }
        public string SearchName { get; set; }
        public string SearchTerm { get; set; }
        public string SearchType { get; set; }
        public bool IsScored { get; set; }
        public string[] ToningKeywords { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Color { get; set; }
        public IList<int> GroupIds { get; set; }

        /// <summary>
        /// Sets default values for an analytics search.
        /// </summary>
        /// <param name="name">The name of the analytics search.</param>
        /// <param name="searchType">Type of the search, one of "C", "P", or "M".</param>
        /// <param name="keywords">A string array of toning keywords.</param>
        /// <returns>an initialized analytics search that can be posted to the searches endpoint</returns>
        public AnalyticsSearch Initialize(string name, string searchType, string[] keywords)
        {
            SearchId = 0;
            SearchName = name;
            SearchTerm = name;
            SearchType = searchType;
            IsScored = true;
            ToningKeywords = keywords;
            CategoryId   = searchType.Contains("C") ? -4 : 
                           searchType.Contains("P") ? -3 
                           : -2;
            CategoryName = searchType.Contains("C") ? "Company" : 
                           searchType.Contains("P") ? "Product Search" 
                           : "Message Search";
            Color = string.Empty;
            GroupIds = new List<int>();
            return this;
        }

        /// <summary>
        /// Verifies the analytics search against another search
        /// </summary>
        /// <param name="search">The search to compare against.</param>
        public void VerifyAgainst(AnalyticsSearch search)
        {
            Assert.That(search.SearchId, Is.GreaterThan(0), "SearchId invalid");
            Assert.That(search.SearchName, Is.EqualTo(SearchName), "SearchName did not match");
            Assert.That(search.SearchTerm, Is.EqualTo(SearchTerm), "SearchTerm did not match");
            //SearchType omitted on purpose - bad developers! bad!
            Assert.That(search.IsScored, Is.EqualTo(IsScored), "IsScored did not match");
            Assert.That(string.Join("|", search.ToningKeywords), Is.EqualTo(string.Join("|", ToningKeywords)), "ToningKeywords did not match");
            Assert.That(search.CategoryId, Is.EqualTo(CategoryId), "CategoryId did not match");
            Assert.That(search.CategoryName, Is.EqualTo(CategoryName), "CategoryName did not match");
            Assert.That(search.Color, Is.EqualTo(Color), "Color did not match");
            if (search.GroupIds == null)
            {
                Assert.That(GroupIds.Count(), Is.EqualTo(0), "Search groups were not empty");
            }
            else if(GroupIds == null)
            {
                Assert.That(search.GroupIds, Is.Null, "Search groups were not null");
            }
            else
            {
                Assert.That(search.GroupIds.Count(), Is.EqualTo(GroupIds.Count()), "Search groups count did not match");
                foreach (var groupId in search.GroupIds)
                {
                    Assert.That(GroupIds.Contains(groupId), Is.True, $"Missing group id '{groupId}'");
                }
            }
        }
    }
}
