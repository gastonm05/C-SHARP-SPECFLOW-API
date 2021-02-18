
namespace CCC_API.Data.PostData.Settings.UserManagement
{
    public class ResetPasswordPostData
    {
        public int CompanyId { get; set; }
        public string PasswordResetGuid { get; set; }
        public string NewPassword { get; set; }
        public ResetPasswordPostData() { }
        public ResetPasswordPostData(int companyId, string passwordResetGuid, string newPassword )
        {
            this.CompanyId = companyId;
            this.PasswordResetGuid = passwordResetGuid;
            this.NewPassword = newPassword;
        }
    }
}