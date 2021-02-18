using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCC_API.Data.PostData.Media.Contact
{
    public class DuplicateListData
    {
        public int OriginalListId { get; set; }
        public int DestinationDataGroupId { get; set; }
        public string Name { get; set; }
        public string EntityType { get; set; }

    }
}
