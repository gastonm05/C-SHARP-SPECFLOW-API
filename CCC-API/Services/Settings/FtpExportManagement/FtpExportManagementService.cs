using CCC_API.Data.Responses.Settings.AutomatedNewsOutput;
using RestSharp;


namespace CCC_API.Services.Settings.FtpExportManagement
{
    public class FtpExportManagementService : AuthApiService
    {
        public FtpExportManagementService(string sessionKey) : base(sessionKey) { }
        public static string ExportFTPConfigEndpoint = "management/newsftpexport";

        /// <summary>
        /// Gets Automated News Output Configuarion
        /// </summary>
        /// <returns></returns>
        public IRestResponse<FtpExportConfig> GetFtpExportConfig()
        {
            return Get<FtpExportConfig>(ExportFTPConfigEndpoint);
        }

        /// <summary>
        /// Edit Automated News Output Configuarion
        /// </summary>
        /// <param name="newConfig"></param>
        /// <returns></returns>
        public IRestResponse EditFtpExportConfig(FtpExportConfig newConfig)
        {
            return Put<FtpExportConfig>(ExportFTPConfigEndpoint, newConfig);
        }
    }
}
