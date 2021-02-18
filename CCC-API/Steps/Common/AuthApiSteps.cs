using BoDi;
using CCC_API.Services;
using TechTalk.SpecFlow;
using ZukiniWrap;

namespace CCC_API.Steps.Common
{
    [Binding]
    public class AuthApiSteps : ApiSteps
    {
        protected string SessionKey { get; private set; }

        public AuthApiSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
            SessionKey = ResolveSessionKey();
        }

        protected string ResolveSessionKey()
        {
            return (PropertyBucket.ContainsKey(AccountsService.SessionKey)) ?
                PropertyBucket.GetProperty<string>(AccountsService.SessionKey) :
                null;
        }
    }
}
