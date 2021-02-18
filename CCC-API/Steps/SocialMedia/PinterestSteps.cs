using BoDi;
using CCC_API.Data.Responses.SocialMedia;
using CCC_API.Services.SocialMedia;
using CCC_API.Steps.Common;
using CCC_Infrastructure.Utils;
using CCC_API.Utils.Assertion;
using RestSharp;
using System.Collections.Generic;
using TechTalk.SpecFlow;
using Is = NUnit.Framework.Is;

namespace CCC_API.Steps.SocialMedia
{
    public class PinterestSteps : AuthApiSteps
    {
        public PinterestSteps(IObjectContainer objectContainer) : base(objectContainer) {  }

        private const string PINTEREST_BOARDS_KEY = "pinterest boards key";

        [When(@"I get all pinterest boards")]
        public void WhenIGetAllPinterestBoards()
        {
            var response = new SocialMediaService(SessionKey).GetSocialMediaAccounts();
            var id = response.Data.
                FirstOrError(a => a.Type.Equals("Pinterest"), "Pinterest Account not found, ensure Pinterest is authorized on the account").ExternalApplicationId;
            var boardsResponse = new SocialMediaService(SessionKey).GetPinterestBoards(id);
            PropertyBucket.Remember(PINTEREST_BOARDS_KEY, boardsResponse);
        }

        [Then(@"the response returns pinterest boards")]
        public void ThenTheResponseReturnsPinterestBoards()
        {
            var boards = PropertyBucket.GetProperty<IRestResponse<List<PinterestBoards>>>(PINTEREST_BOARDS_KEY);
            Assert.That(boards.Data.Count, Is.GreaterThan(0), "No Pinterest boards were returned");
        }

    }
}
