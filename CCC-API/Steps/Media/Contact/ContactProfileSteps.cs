using BoDi;
using CCC_API.Data;
using CCC_API.Data.Responses.Media.Contact;
using CCC_API.Services.Media.Contact;
using CCC_API.Steps.Common;
using CCC_Infrastructure.Utils;
using CCC_API.Utils.Assertion;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TechTalk.SpecFlow;
using ContactItem = CCC_API.Data.TestDataObjects.Media.Contact;
using Is = NUnit.Framework.Is;

namespace CCC_API.Steps.Media.Contact
{
    public class ContactProfileSteps : AuthApiSteps
    {
        public ContactProfileSteps(IObjectContainer objectContainer) : base(objectContainer) { }

        private const string CONTACT_PROFILE_KEY = "Contact Profile Key";
        public const string CONTACT_ID = "Contact ID";
        private const string CONTACT_RESPONSE_KEY = "Contact Response Key";
        private const string CONTACT_EXPORT_RESPONSE_KEY = "Contact Export Response Key";
        private const string CONTACT_INFLUENCER_RANKING_RESPONSE_KEY = "Contact Influencer Ranking Response Key";
        private const string CONTACT_RELATED_RESPONSE = "Contacts Related Key";
        private const string CONTACT_HISTORY_RESPONSE = "Contacts History Key";

        #region When Steps

        /// <summary>
        /// Executes a patch against a contact and updates the pitching profile with a random value
        /// </summary>
        /// <param name="contact">The contact.</param>
        /// <exception cref="ArgumentNullException"></exception>
        [When(@"I perform a PATCH to edit the Pitching Profile of '(.*)'")]
        public void WhenIPerformAPATCHToEditThePitchingProfileOf(string contact)
        {
            int id;
            var contacts = TestData.DeserializedJson<List<ContactItem>>("Contacts.json", Assembly.GetExecutingAssembly());
            try
            {
                id = contacts.Find(c => c.FullName.ToLower().Equals(contact.ToLower())).Id;
            }
            catch (NullReferenceException)
            {
                throw new ArgumentNullException(Err.Msg($"'{contact}' not found in Contacts.json file."));
            }
            var value = StringUtils.RandomAlphaNumericString(20);
            PropertyBucket.Remember(CONTACT_ID, id);
            PropertyBucket.Remember(CONTACT_PROFILE_KEY, value);
            var list = new List<PatchData>();
            list.Add(new PatchData("update", "/PitchingProfile", value));
            new ContactsService(SessionKey).EditContact(id, list);
        }

        [When(@"I perform a PATCH to edit the Website URL of '(.*)'")]
        public void WhenIPerformAPATCHToEditTheWebsiteURLOf(string contact)
        {
            int id;
            var contacts = TestData.DeserializedJson<List<ContactItem>>("Contacts.json", Assembly.GetExecutingAssembly());
            try
            {
                id = contacts.Find(c => c.FullName.ToLower().Equals(contact.ToLower())).Id;
            }
            catch (NullReferenceException)
            {
                throw new ArgumentNullException(Err.Msg($"'{contact}' not found in Contacts.json file."));
            }
            var value = $"http://www.edited{StringUtils.RandomAlphaNumericString(5)}.com";
            PropertyBucket.Remember(CONTACT_ID, id);
            PropertyBucket.Remember(CONTACT_PROFILE_KEY, value);
            var list = new List<PatchData>();
            list.Add(new PatchData("update", "/ProprietaryData/WebsiteURL", value));
            new ContactsService(SessionKey).EditContact(id, list);
        }

        [When(@"I perform a GET for Contact '(.*)'")]
        public void WhenIPerformAGETForContact(string contact)
        {
            int id;
            var contacts = TestData.DeserializedJson<List<ContactItem>>("Contacts.json", Assembly.GetExecutingAssembly());
            try
            {
                id = contacts.Find(c => c.FullName.ToLower().Equals(contact.ToLower())).Id;
            }
            catch (NullReferenceException)
            {
                throw new ArgumentNullException(Err.Msg($"'{contact}' not found in Contacts.json file."));
            }
            PropertyBucket.Remember(CONTACT_ID, id);
            IRestResponse<ContactsItem> response = new ContactsService(SessionKey).GetSingleContact(id);
            PropertyBucket.Remember(CONTACT_RESPONSE_KEY, response);
        }

        [When(@"I perform a POST for exporting contact '(.*)' without a subject")]
        public void WhenIPerformAPOSTForExportingContactWithoutASubject(int contactId)
        {
            IRestResponse<ContactExportResponse> response = new ContactsService(SessionKey).PostSendContactDetailsViaEmail(contactId);
            PropertyBucket.Remember(CONTACT_EXPORT_RESPONSE_KEY, response.Content);
        }

        [When(@"I perform a GET for her influencer ranking charts")]
        public void WhenIPerformAGETForHerInfluencerRankingCharts()
        {
            int id = PropertyBucket.GetProperty<int>(CONTACT_ID);
            IRestResponse<InfluencerRankingsResponse> response = new ContactsService(SessionKey).GetInfluencerRankings(id);
            PropertyBucket.Remember(CONTACT_INFLUENCER_RANKING_RESPONSE_KEY, response);
        }

        [When(@"I perform a GET for related contacts using the id")]
        public void WhenIPerformAGETForRelatedContactsUsingTheId()
        {
            int id = PropertyBucket.GetProperty<int>(CONTACT_ID);
            IRestResponse<List<ContactsRelated>> response = new ContactsService(SessionKey).GetContactsRelated(id);
            PropertyBucket.Remember(CONTACT_RELATED_RESPONSE, response);
        }

        [When(@"I perform a GET for history contact information using the id")]
        public void WhenIPerformAGETForHistoryContactInformationUsingTheId()
        {
            int id = PropertyBucket.GetProperty<int>(CONTACT_ID);
            IRestResponse<List<ContactHistory>> response = new ContactsService(SessionKey).GetContactHistory(id);
            PropertyBucket.Remember(CONTACT_HISTORY_RESPONSE, response);
        }

        [When(@"I perform a PATCH to add some random notes")]
        public void WhenIPerformAPATCHToAddSomeRandomNotes()
        {
            var note = StringUtils.RandomAlphaNumericString(20);
            int id = PropertyBucket.GetProperty<int>(CONTACT_ID);
            var list = new List<PatchData>();
            list.Add(new PatchData("update", "/ProprietaryData/Notes", note));
            new ContactsService(SessionKey).EditContact(id, list);
            PropertyBucket.Remember("note", note);

        }        
        #endregion When Steps

        #region Then Steps
        [Then(@"the IsProprietaryContact field should be FALSE")]
        public void ThenTheIsProprietaryContactFieldShouldBeFALSE()
        {
            IRestResponse<ContactsItem> response = PropertyBucket.GetProperty<IRestResponse<ContactsItem>>(CONTACT_RESPONSE_KEY);
            Assert.IsFalse(response.Data.IsProprietaryContact, "IsProprietaryContact was true");
        }

        [Then(@"the IsProprietaryContact field should be TRUE")]
        public void ThenTheIsProprietaryContactFieldShouldBeTRUE()
        {
            IRestResponse<ContactsItem> response = PropertyBucket.GetProperty<IRestResponse<ContactsItem>>(CONTACT_RESPONSE_KEY);
            Assert.IsTrue(response.Data.IsProprietaryContact, "IsProprietaryContact was false");
        }

        [Then(@"the Pitching Profile should match the updated value")]
        public void ThenThePitchingProfileShouldMatchTheUpdatedValue()
        {
            var id = PropertyBucket.GetProperty<int>(CONTACT_ID);
            var response = new ContactsService(SessionKey).GetSingleContact(id);
            Assert.That(response.Data.PitchingProfile, Is.EqualTo(PropertyBucket.GetProperty(CONTACT_PROFILE_KEY)),
                "Pitching Profile is not as expected");
        }

        [Then(@"the Website URL should match the updated value")]
        public void ThenTheWebsiteURLShouldMatchTheUpdatedValue()
        {
            var id = PropertyBucket.GetProperty<int>(CONTACT_ID);
            var response = new ContactsService(SessionKey).GetSingleContact(id);
            Assert.That(response.Data.ProprietaryData.WebsiteURL, Is.EqualTo(PropertyBucket.GetProperty(CONTACT_PROFILE_KEY)), 
                "Website URL is not as expected");
        }


        [Then(@"the response should return invalid because '(.*)'")]
        public void ThenTheResponseShouldReturnInvalidBecause(string response)
        {
            string exportResponse = PropertyBucket.GetProperty<string>(CONTACT_EXPORT_RESPONSE_KEY);
            Assert.That(exportResponse.Contains(response), "The wrong error message appeared or this succeded unexpectedly");
        }

        [Then(@"the response's first item should be ranked one and the rest should be ordered incrementally")]
        public void ThenTheResponseSFirstItemShouldBeRankedOneAndTheRestShouldBeOrderedIncrementally()
        {
            IRestResponse<InfluencerRankingsResponse> response = PropertyBucket.GetProperty<IRestResponse<InfluencerRankingsResponse>>(CONTACT_INFLUENCER_RANKING_RESPONSE_KEY);
            Assert.That(response.Data.System.Items, Is.Ordered.By("CurrentRank"));
        }
        [Then(@"I should see the related Contacts for that profile matches with the all coopers data")]
        public void ThenIShouldSeeTheRelatedContactsForThatProfileMatchesWithTheAllCoopersData()
        {
            IRestResponse<List<ContactsRelated>> response = PropertyBucket.GetProperty<IRestResponse<List<ContactsRelated>>>(CONTACT_RELATED_RESPONSE);
            IRestResponse<Contacts> responseSearch = PropertyBucket.GetProperty<IRestResponse<Contacts>>(ContactSearchSteps.GET_CONTACTS_RESPONSE_KEY);
            List<ContactsItem> items = responseSearch.Data.Items;
            foreach (var i in items)
            {
                Assert.IsTrue(response.Data.Any(c => c.ContactId == i.Id), "The id are not the same");
            }            
        }

        [Then(@"all outlets information should match with the data from previous search")]
        public void ThenAllOutletsInformationShouldMatchWithTheDataFromPreviousSearch()
        {
            IRestResponse<List<ContactHistory>> response = PropertyBucket.GetProperty<IRestResponse<List<ContactHistory>>>(CONTACT_HISTORY_RESPONSE);
            IRestResponse<Contacts> responseSearch = PropertyBucket.GetProperty<IRestResponse<Contacts>>(ContactSearchSteps.GET_CONTACTS_RESPONSE_KEY);
            List<ContactsItem> items = responseSearch.Data.Items;
            foreach (var i in items)
            {
                Assert.IsTrue(response.Data.Any(c => c.OutletId == i.OutletId), Err.Msg("The id are not the same"));
                Assert.IsTrue(response.Data.Any(c => c.OutletName == i.OutletName), Err.Msg("The outlet name are not the same"));
                break;
            }
        }

        [Then(@"I should get same notes for all consolidated profile")]
        public void ThenIShouldGetSameNotesForAllConsolidatedProfile()
        {           
            var note = PropertyBucket.GetProperty<string>("note");
            IRestResponse<Contacts> responseSearch = PropertyBucket.GetProperty<IRestResponse<Contacts>>(ContactSearchSteps.GET_CONTACTS_RESPONSE_KEY);
            List<ContactsItem> items = responseSearch.Data.Items;
            Assert.IsNotEmpty(note, "The note are empty");
            Assert.IsTrue(items.All(i => i.ProprietaryData.Notes.Contains(note)), Err.Msg("The notes was not added properly"));
        }

        [Then(@"the WorkingLanguages array is set properly")]
        public void ThenTheWorkingLanguagesArrayIsSetProperly()
        {
            IRestResponse<ContactsItem> response = PropertyBucket.GetProperty<IRestResponse<ContactsItem>>(CONTACT_RESPONSE_KEY);
            Assert.IsNotNull(response.Data.WorkingLanguages, Err.Msg("WorkingLanguages was null"));
            Assert.IsNotEmpty(response.Data.WorkingLanguages, Err.Msg("WorkingLanguages was empty"));
            Assert.That(response.Data.WorkingLanguages[0].Id, Is.GreaterThan(0), Err.Msg("WorkingLanguages.Id was 0"));
            Assert.IsFalse(string.IsNullOrEmpty(response.Data.WorkingLanguages[0].Name), Err.Msg("WorkingLanguages.Name was null or empty"));
        }
        #endregion Then Steps
    }
}
