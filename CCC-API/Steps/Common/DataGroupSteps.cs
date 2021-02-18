using BoDi;
using CCC_API.Data.Responses.Accounts;
using CCC_API.Data.TestDataObjects;
using CCC_API.Services.Common;
using CCC_Infrastructure.Utils;
using CCC_API.Utils.Assertion;
using RestSharp;
using System.Linq;
using TechTalk.SpecFlow;
using Is = NUnit.Framework.Is;

namespace CCC_API.Steps.Common
{
    public class DataGroupSteps : AuthApiSteps
    {
        public DataGroupSteps(IObjectContainer objectContainer) : base(objectContainer) { }

        private const string DATAGROUP_RESPONSE_KEY = "datagroup key";
        
        #region When Steps

        [When(@"I perform a PUT to change the datagroup")]
        public void WhenIPerformAPUTToChangeTheDatagroup()
        {
            var accountService = new AccountInfoService(SessionKey);
            var currentDG = accountService.Me.Profile.Id;
            PropertyBucket.Remember("initial DG", currentDG);
            var newDataGroup = accountService.GetDataGroups().Items.FirstOrError(d => d.Id != currentDG, $"Datagroup '{currentDG}' not found.");
            accountService.ChangeDataGroup(newDataGroup.Name);
            PropertyBucket.Remember("new DG", newDataGroup);
        }

        [When(@"I perform a PUT to change to datagroup '(.*)'")]
        public void WhenIPerformAPUTToChangeToDatagroup(int id)
        {
            var response = new AccountInfoService(SessionKey).ChangeDataGroup(id);
            PropertyBucket.Remember(DATAGROUP_RESPONSE_KEY, response);
        }

        [When(@"I get all datagroups for the user")]
        public void WhenIGetAllDatagroupsForTheUser()
        {
            var response = new AccountInfoService(SessionKey).GetDataGroups();
            PropertyBucket.Remember(DATAGROUP_RESPONSE_KEY, response);
        }

        [When(@"I get all datagroups for the user with company id '(.*)'")]
        public void WhenIGetAllDatagroupsForTheUserWithCompanyId(int id)
        {
            var response = new AccountInfoService(SessionKey).GetDataGroupsById(id);
            PropertyBucket.Remember(DATAGROUP_RESPONSE_KEY, response);
        }

        [Then(@"the active datagroup should be changed")]
        public void ThenTheActiveDatagroupShouldBeChanged()
        {
            var initialDG = PropertyBucket.GetProperty("initial DG");
            var expectedDG = PropertyBucket.GetProperty<Profile>("new DG");
            var currentDG = new AccountInfoService(SessionKey).Me.Profile.Id;
            Assert.That(currentDG, Is.EqualTo(expectedDG.Id), "Data group was not changed");
            Assert.That(currentDG, Is.Not.EqualTo(initialDG), "Data group was not changed");
        }

        #endregion When Steps

        #region Then Steps

        [Then(@"the Accounts endpoint response should be '(.*)'")]
        public void ThenTheAccountsEndpointResponseShouldBe(int code)
        {
            IRestResponse response = PropertyBucket.GetProperty<IRestResponse>(DATAGROUP_RESPONSE_KEY);
            Assert.That(Services.BaseApiService.GetNumericStatusCode(response), Is.EqualTo(code), $"Expected {code} not received");
        }

        [Then(@"the Accounts endpoint content should be '(.*)'")]
        public void ThenTheAccountsEndpointContentShouldBe(string content)
        {
            IRestResponse response = PropertyBucket.GetProperty<IRestResponse>(DATAGROUP_RESPONSE_KEY);
            Assert.That(response.Content, Is.EqualTo(content), "Expected content not found");
        }

        [Then(@"the datagroups should be in alphabetical order")]
        public void ThenTheDatagroupsShouldBeInAlphabeticalOrder()
        {
            ProfilesResponse response = PropertyBucket.GetProperty<ProfilesResponse>(DATAGROUP_RESPONSE_KEY);
            var profiles = response.Items.ToList<Profile>();
            Assert.That(profiles.Count, Is.GreaterThan(1), "Zero or One profiles are available. Cannot determine order");
            for (int i = 0; i < profiles.Count - 1; i++)
            {
                if (profiles[i].Name.CompareTo(profiles[i + 1].Name) > 0)
                {
                    Assert.Fail("Data groups are NOT in alphabetical order");
                }
            }
        }

        [Then(@"the '(.*)' datagroup is not returned in list of datagroups")]
        public void ThenTheDatagroupIsNotReturnedInListOfDatagroups(string name)
        {
            var profiles = new AccountInfoService(SessionKey).GetDataGroups();
            Assert.That(profiles.Items.Any(p => p.Name.ToLower().Equals(name.ToLower())), Is.False, 
                $"'{name}' datagroup returned as a valid datagroup and should not be");
        }

        #endregion Then Steps
    }
}
