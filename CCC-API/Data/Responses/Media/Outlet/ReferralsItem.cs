using System.Collections.Generic;

namespace CCC_API.Data.Responses.Media.Outlet
{
  public  class ReferralsItem
    {
        public int MediaOutletId { get; set; }
        public string MediaOutletSortName { get; set; }
        public string MediaOutletDomain { get; set; }
        public int Conversions { get; set; }
        public int UniqueVisitors { get; set; }
        public string CurrencySymbol { get; set; }
        public int PageViews { get; set; }
        public double TotalValue { get; set; }
        public double AverageSessionSeconds { get; set; }
        public int Bounces { get; set; }
        public int NewUsers { get; set; }
        public double AverageSecondsOnPage { get; set; }
        public int Checkouts { get; set; }
        public double OrderRevenue { get; set; }
        public double Orders { get; set; }
    }
}
