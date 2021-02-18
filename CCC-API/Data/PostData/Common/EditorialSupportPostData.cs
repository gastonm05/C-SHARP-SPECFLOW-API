
namespace CCC_API.Data.PostData.Common
{
    public class EditorialSupportPostData
    {
        public int EditorialContactDetailsId { get; set; }
        public string FromName { get; set; }
        public string FromEmail { get; set; }
        public string Message { get; set; }        

        public EditorialSupportPostData() { }

        public EditorialSupportPostData(int editorialContactDetailsId, string fromName, string fromEmail, string message)
        {
            this.EditorialContactDetailsId = editorialContactDetailsId;
            this.FromName = fromName;
            this.FromEmail = fromEmail;
            this.Message = message;            
        }
    }
}