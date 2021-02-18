using System;

namespace CCC_API.Data.Responses.Settings.KeywordSearches
{
    public class MediaMonitorUserSearch
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int CompanyId { get; set; }
        public int DataGroupId { get; set; }
        public string[] AndKeywords { get; set; }
        public string[] OrKeywords { get; set; }
        public string[] NotKeywords { get; set; }
        public string[] Languages { get; set; }
        public int[] Countries { get; set; }
        public Boolean UserCreatedSearch { get; set; }
        public Boolean IsAdvancedSearch { get; set; }
        public int MediaMonitorUserSearchId { get; set; }
        public string SearchClause { get; set; }
        public string[] Sources { get; set; }        


        public MediaMonitorUserSearch() { }

        public MediaMonitorUserSearch(string name, int companyId, int dataGroupId, string searchClause, string[] andKeywords, string[] orKeywords, string[] notKeywords, string[] languages, int[] countries, string[] sources, Boolean isAdvancedSearch)
        {
            this.Name = name;
            this.CompanyId = companyId;
            this.DataGroupId = dataGroupId;
            this.SearchClause = searchClause;
            this.AndKeywords = andKeywords;
            this.OrKeywords = orKeywords;
            this.NotKeywords = notKeywords;
            this.Countries = countries;
            this.Languages = languages;
            this.Sources = sources;
            this.IsAdvancedSearch = isAdvancedSearch;
        }
        public MediaMonitorUserSearch(string name, int companyId, int dataGroupId, string searchClause,string[] languages, int[] countries, string[] sources, Boolean isAdvancedSearch)
        {
            this.Name = name;
            this.CompanyId = companyId;
            this.DataGroupId = dataGroupId;
            this.SearchClause = searchClause;            
            this.Countries = countries;
            this.Languages = languages;
            this.Sources = sources;
            this.IsAdvancedSearch = isAdvancedSearch;
        }
    }
}