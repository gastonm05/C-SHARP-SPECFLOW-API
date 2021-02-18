using CCC_API.Data.PostData.Common;

namespace CCC_API.Data.PostData.Media.Contact
{
    public class ContactExportData : BaseExportPostData
    {
        //All properties are inherited from the base class

        public string[] Fields { get; set; }
    }
}
