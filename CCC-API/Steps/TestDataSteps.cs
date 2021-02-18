using BoDi;
using CCC_Infrastructure.Utils;
using System.Collections.Generic;
using System.Reflection;
using TechTalk.SpecFlow;
using ZukiniWrap;

namespace CCC_API.Steps
{
    [Binding]
    public class TestDataSteps : ApiSteps
    {
        public TestDataSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
        }


        /// <summary>
        /// {D255958A-8513-4226-94B9-080D98F904A1}Assumes test data file is in JSON format. Remembers values in Property Bucket.
        /// {D255958A-8513-4226-94B9-080D98F904A1}Uses property names in JSON file as the keys in Property Bucket
        /// </summary>
        /// <param name="filename">file name and extension of the test data file in json format</param>
        [Given("the API test data '(.*)'")]
        public void GivenIRememberTestData(string filename)
        {
            var expando = TestData.DeserializedJsonKeyValuePairs(filename, Assembly.GetExecutingAssembly(), false);
            foreach (KeyValuePair<string, object> pair in expando)
            {
                PropertyBucket.Remember(pair.Key, pair.Value);
            }
        }

        /// <summary>
        /// Open file as string from universal folder and remembers it.
        /// </summary>
        /// <param name="filename"></param>
        [Given(@"I remember file '(.*)'")]
        public void GivenIRememberDocumentFile(string filename)
        {
            PropertyBucket.Remember(filename, TestData.GetResourceEndingWithFileName(filename, Assembly.GetExecutingAssembly()));
        }
    }
}