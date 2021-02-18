using System.Collections.Generic;

namespace CCC_API.Data.Responses.Media
{
    public class UniqueEmailsLists
    {
        public List<EmailEntityList> entityLists { get; set; }
        public List<string> additionalEmailAddresses { get; set; }
    }

    public class EmailEntityList
    {
        public string EntityType { get; set; }
        public int Id { get; set; }
    }
}
