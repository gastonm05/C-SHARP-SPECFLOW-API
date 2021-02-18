namespace CCC_API.Data.PostData.News
{
    public class CedromClipPostData
    {
        public int StartSecond { get; set; }
        public int EndSecond { get; set; }
        public bool IsPrimary { get; set; }
    }

    public class CedromClipPutData
    {
        public string Name { get; set; }
        public int StartSecond { get; set; }
        public int EndSecond { get; set; }
        public bool IsPrimary { get; set; }
    }

    public class CedromClipWithSubSecondsPostData
    {
        public double StartSecond { get; set; }
        public double EndSecond { get; set; }
        public bool IsPrimary { get; set; }
    }
}
