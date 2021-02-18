using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCC_API.Data.PostData.Common
{
    public class MediaResearchRequest
    {
        private const string FROM_NAME_BASE = "Test Automation";
        private const string FROM_EMAIL = "test@testemail.com";
        private const string MESSAGE = "This is a automation test email message";

        public MediaResearchRequest() { }

        public MediaResearchRequest(int id, string changeType, string entityType)
        {
            Id = id;
            ChangeType = changeType;
            Name = $"fromName{Guid.NewGuid()}";
            EmailAddress = FROM_EMAIL;
            Message = MESSAGE;
            EntityType = entityType;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string EmailAddress { get; set; }

        public string ChangeType { get; set; }

        public string Message { get; set; }

        public string EntityType { get; set; }
    }
}
