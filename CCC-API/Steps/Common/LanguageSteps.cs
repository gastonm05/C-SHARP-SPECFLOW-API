using BoDi;
using CCC_API.Data.Responses.Common;
using CCC_API.Services.Common;
using CCC_Infrastructure.Utils;
using CCC_API.Utils.Assertion;
using RestSharp;
using System.Collections.Generic;
using TechTalk.SpecFlow;
using Is = NUnit.Framework.Is;

namespace CCC_API.Steps.Common
{
    public class LanguageSteps : AuthApiSteps
    {
        public LanguageSteps(IObjectContainer objectContainer) : base(objectContainer) { }

        private const string LANGUAGE_RESPONSE_KEY = "languageresponsekey";

        [When(@"I perform a GET for all languages")]
        public void WhenIPerformAGETForAllLanguages()
        {
            var response = new LanguageService().GetLanguages();
            PropertyBucket.Remember(LANGUAGE_RESPONSE_KEY, response);
        }

        [Then(@"the languages endpoint returns (.*) languages")]
        public void ThenTheLanguagesEndpointReturnsLanguages(int count)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<List<Language>>>(LANGUAGE_RESPONSE_KEY);
            Assert.That(response.Data.Count, Is.EqualTo(count), "Incorrect number of languages were returned");
        }

        [Then(@"the languages response contains the language '(.*)' with an id of '(.*)' a code of '(.*)' and a status of '(.*)'")]
        public void ThenTheLanguagesResponseContainsTheLanguageWithAnIdOfACodeOfAndAStatusOf(string name, int id, string code, bool status)
        {
            var response = PropertyBucket.GetProperty<IRestResponse<List<Language>>>(LANGUAGE_RESPONSE_KEY);
            var lang = response.Data.FirstOrError(l => l.LanguageID.Equals(id), $"Language '{id}' not found in response");
            Assert.Multiple(() =>
            {
                Assert.That(lang.LanguageName, Is.EqualTo(name), $"Incorrect Language name found for '{lang.LanguageName}'");
                Assert.That(lang.LanguageID, Is.EqualTo(id), $"Incorrect Language Id found for '{lang.LanguageName}'");
                Assert.That(lang.IsAvailable, Is.EqualTo(status), $"Incorrect Language status found for '{lang.LanguageName}'");
            });
        }

    }
}
