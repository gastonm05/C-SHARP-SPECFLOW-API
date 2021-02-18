using System.Collections.Generic;

namespace CCC_API.Data.Responses.Media.Outlet
{
    public class Referrals
    {
        public int ItemCount { get; set; }
        public List<ReferralsItem> Items { get; set; }
        public string Key { get; set; }
    }
}
