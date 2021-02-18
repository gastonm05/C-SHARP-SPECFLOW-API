using BoDi;
using CCC_API.Data.Responses;
using CCC_API.Steps.Common;
using CCC_Infrastructure.Utils;
using System.Reflection;
using TechTalk.SpecFlow;

namespace CCC_API.Steps.EmailDistribution
{
    public class DistributionTestsSetupSteps : AuthApiSteps
    {
        public const string DISTRIBUTION_RECIPIENTS = "recipients";
        public DistributionTestsSetupSteps(IObjectContainer objectContainer) : base(objectContainer){}
        
        [Given(@"I remember expected data from '(.*)' file")]
        public void GivenIRememberExpectedDataFromFile(string filePath)
        {
            var allRecipients = TestData.DeserializedJson<RecipientsResponse[]>(filePath, Assembly.GetExecutingAssembly());
            PropertyBucket.Remember(DISTRIBUTION_RECIPIENTS, allRecipients);
        }

    }
}
