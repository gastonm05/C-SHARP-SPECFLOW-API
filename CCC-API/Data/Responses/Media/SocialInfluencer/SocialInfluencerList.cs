using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCC_API.Data.Responses.Media.SocialInfluencer
{
    public class SocialInfluencerList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CompanyId { get; set; }
        public int DataGroupId { get; set; }
        public string Notes { get; set; }
    }   
}
