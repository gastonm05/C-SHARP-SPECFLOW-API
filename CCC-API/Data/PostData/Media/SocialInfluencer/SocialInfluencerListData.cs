using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCC_API.Data.PostData.Media.SocialInfluencer
{
    public class SocialInfluencerListData
    {
        public string Name { get; set; }
        public Select Select { get; set; }
        public Differential Differential { get; set; }

    }

    public class Select
    {
        public string Key { get; set; }        
        public long[] Ids { get; set; }
        public bool IsExclude { get; set; }
        public QueryCriteria QueryCriteria { get; set; }
    }

    public class Differential
    {
        public string Notes { get; set; }
    }

    public class QueryCriteria
    {
        public string keyword { get; set; }
    }


}
