using System;

namespace CCC_API.Data.PostData.Common
{
    /// <summary>
    /// This represents the base class for post data when exporting items. This class should be inherited from a specific type class.
    /// </summary>
    public abstract class BaseExportPostData
    {
        public int[] Delta { get; set; }
        public string Key { get; set; }
        public int PresentationType { get; set; }
        public Boolean SelectAll { get; set; }
    }
}
