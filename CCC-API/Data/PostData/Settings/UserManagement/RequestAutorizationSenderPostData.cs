

namespace CCC_API.Data.PostData.Settings.UserManagement
{
    public class RequestAutorizationSenderPostData
    {
        public int UserAccountId { get; set; }        
        public RequestAutorizationSenderPostData() { }
        public RequestAutorizationSenderPostData(int userAccountId)
        {
            this.UserAccountId = userAccountId;            
        }

    }
}
