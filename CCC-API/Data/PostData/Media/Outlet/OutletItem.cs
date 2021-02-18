using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCC_API.Data.PostData.Media.Outlet
{
    public class OutletItem
    {
        public string FullName { get; set; }
        public int CountryId { get; set; }
        public bool IsProprietaryOutlet { get; set; }  
        public string Email { get; set; }
    }
}
