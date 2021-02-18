using CCC_Infrastructure.Utils;
using TechTalk.SpecFlow;

namespace CCC_API.Steps
{
    [Binding]
    public class StepHooks
    {
        private Teardown _td;

        public StepHooks(Teardown teardown)
        {
            _td = teardown;
        }

        /// <summary>
        /// Invokes teardowns to cleanup test data.
        /// </summary>
        [AfterScenario]
        public void ExecuteTeardowns()
        {
            _td.GetTeardowns().ForEach(teardown => teardown.Invoke());
        }
    }
}
