using BoDi;
using CCC_API.Data.Responses.Common;
using CCC_API.Data.Responses.Media;
using CCC_API.Services.Media.DMA;
using CCC_API.Steps.Common;
using CCC_Infrastructure.Utils;
using CCC_API.Utils.Assertion;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;
using Is = NUnit.Framework.Is;

namespace CCC_API.Steps.Media.DMA
{
    public class DMASteps : AuthApiSteps
    {
        private readonly DMAService dmaService;
        public const string DMA_RESPONSE = "Dma response";
        public DMASteps(IObjectContainer objectContainer) : base(objectContainer)
        {
            dmaService = new DMAService(SessionKey);
        }

        [When(@"I perform a GET for DMA endpoint sorted by '(.*)'")]
        public void WhenIPerformAGETForDMAEndpointSortedBy(string sort)
        {
            IRestResponse<CollectionResponse<DmaResponse>> response = new DMAService(SessionKey).GetDmaSorted(sort);
            List<DmaResponse> dma = response.Data.Items.ToList();
            PropertyBucket.Remember(DMA_RESPONSE, dma);
        }
        [Then(@"I should see the DMA ranks in the correct order")]
        public void ThenIShouldSeeTheDMARanksInTheCorrectOrder()
        {
            var response = PropertyBucket.GetProperty<List<DmaResponse>>(DMA_RESPONSE);            
            Assert.That(response.Count, Is.GreaterThan(0), Err.Msg("No results returned"));            
            Assert.That(response.Select(c => c.Rank), Is.Ordered.Ascending, Err.Msg("The List is not ordered"));
        }

        [Then(@"I should see the DMA Name in the correct order")]
        public void ThenIShouldSeeTheDMANameInTheCorrectOrder()
        {
            var response = PropertyBucket.GetProperty<List<DmaResponse>>(DMA_RESPONSE);
            Assert.That(response.Count, Is.GreaterThan(0), Err.Msg("No results returned"));
            Assert.That(response.Select(c => c.Name), Is.Ordered.Ascending, Err.Msg("The List is not ordered"));
        }
    }
}
