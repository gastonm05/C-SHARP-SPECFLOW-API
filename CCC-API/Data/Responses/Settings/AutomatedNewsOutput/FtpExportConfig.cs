

namespace CCC_API.Data.Responses.Settings.AutomatedNewsOutput
{
    public class FtpExportConfig
    {
        public int Frequency { get; set; }
        public int MaxResults { get; set; }
        public bool ExtendedTier { get; set; }
        public bool IncludeDuplicates { get; set; }
        public string FtpServerPath { get; set; }
    }
}
