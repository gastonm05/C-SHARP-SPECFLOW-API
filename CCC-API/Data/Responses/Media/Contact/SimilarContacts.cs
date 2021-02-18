using System;
namespace CCC_API.Data.Responses.Media.Contact
{
    public class SimilarContacts
    {
        public ContactsItem Entity { get; set; }
        public double SimilarityScore { get; set; }
    }
}
