using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCC_API.Data.PostData.Media.SocialInfluencer
{
    public class SocialInfluencerRemoveItem
    {
        public string Name { get; set; }
        public bool SelectAll { get; set; }
        public long[] Delta { get; set; }
    }
}
