using BoDi;
using CCC_Infrastructure.UserSupport;
using CCC_Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using TechTalk.SpecFlow.Assist.ValueRetrievers;
using Zukini;

namespace CCC_API
{
    [Binding]
    public class ApiHooks
    {
        
        public readonly static string APIUserFile = "APIUsers.json";

        // Property Bucket Keys
        public readonly static string ScenarioUserKey = "ScenarioUser";

        private readonly IObjectContainer _objectContainer;
        private readonly ScenarioContext _scenarioContext;

        public ApiHooks(IObjectContainer container, ScenarioContext scenarioContext)
        {
            _objectContainer = container;
            _scenarioContext = scenarioContext;
        }

        /// <summary>
        /// Method to execute before the suite executes. This will add project users from the User.json file
        /// to the UserList in the infrasturcture project
        /// </summary>
        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            var userList = TestData.DeserializedJson<List<User>>( APIUserFile, Assembly.GetExecutingAssembly() );
            var dupes = userList.GroupBy( i => new { i.CompanyID, i.Username } ).Where( g => g.Count() > 1 ).Select( g => g.Key ).Where( j => j.Username.ToLower() != "manager" );
            if ( dupes.Any() )
            {
                var duplicateCompanyEditions = string.Join( ", ", dupes );
                throw new Exception( $"Duplicate users found in users file. ({duplicateCompanyEditions})" );
            }

            UserList.PopulateUserList(userList);

            // Unregister default enum value retriever & register custom Nullable enum value retriver. 
            var service = Service.Instance;
            var defaultRetriever = service.ValueRetrievers.FirstOrDefault(it => it is EnumValueRetriever);
            if (defaultRetriever != null)
            {
                Service.Instance.UnregisterValueRetriever(defaultRetriever);
            }
            service.RegisterValueRetriever(new NullEnumValueRetriever());
        }

        /// <summary>
        /// Method to execute after each scenario. Releases the scenario user from the UserList [InUse = false].
        /// </summary>
        [AfterScenario]
        public void AfterScenario()
        {
            var propertyBucket = _objectContainer.Resolve<PropertyBucket>();

            if (propertyBucket.ContainsKey(ScenarioUserKey))
            {
                var scenarioUser = propertyBucket.GetProperty<User>(ScenarioUserKey);
                UserList.ReleaseListUser(scenarioUser);
            }
        }

        /// <summary>
        /// Executes after the test suite completes. Releases all users [InUse = false] taken from the database
        /// </summary>
        [AfterTestRun]
        public static void AfterTestRun()
        {
            UserList.ReleaseListDatabaseUsers();
        }
    }
}