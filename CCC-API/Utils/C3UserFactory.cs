using CCC_API.Data.PostData.Settings.UserManagement;
using CCC_API.Data.Responses.Settings.UserManagement;
using CCC_API.Data.TestDataObjects;
using CCC_API.Services;
using CCC_API.Services.Common;
using CCC_API.Steps.PrNewswire;
using CCC_Infrastructure.API.Utils;
using CCC_Infrastructure.UserSupport;
using CCC_Infrastructure.Utils;
using CCC_API.Utils.Assertion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CCC_API.Utils
{
    /// <summary>
    /// Factory stuff to create users using C3 api and custom endpoint for cleaning users. 
    /// </summary>
    /// <author>Oleh Ilnytskyi</author>
    public class C3UserFactory
    {
        private readonly List<User> _editions;
        private readonly string _cleanUpUri;
        private readonly string _feature;
        private readonly string _scenario;
        private readonly AccountsService _accountsService = new AccountsService();
        public List<Task> RegistrationTasks { get; } = new List<Task>();

        public C3UserFactory(string editionsFile, string cleanUpUri, string featureTitle, string scenarioTitle)
        {
            _editions = TestData.DeserializedJson<List<User>>(editionsFile, Assembly.GetCallingAssembly()); ;
            _cleanUpUri = cleanUpUri;
            _feature = Regex.Replace(featureTitle, "[^a-zA-Z0-9]", string.Empty);
            _scenario = Regex.Replace(scenarioTitle, "[^a-zA-Z0-9]", string.Empty);
        }

        /// <summary>
        /// Creates user with specified settings.
        /// </summary>
        /// <param name="edition"></param>
        /// <param name="toBeCreatedUser"></param>
        /// <param name="otherSettings"></param>
        /// <returns>DynamicUser</returns>
        public DynamicUser CreateUser(
            string edition,
            DynamicUser toBeCreatedUser,
            Func<DynamicUser, DynamicUser> otherSettings = null)
        {
            var user = toBeCreatedUser;
            // Manager creator key
            var manager = _editions.FirstOrDefault(u =>
                u.CompanyEdition.ToLower().Equals(edition.ToLower())
                && u.Username.ToLower().Equals("manager"));
            Assert.IsNotNull(manager, $"Cannot find edition: {edition} in editions file");
            var managerKey = GetCachedManagerKeyByEdition(manager);
            user.CompanyId = manager.CompanyID;
            user.CompanyEdition = edition;
            // PermissionID
            var userManagement = new UserManagementService(managerKey);
            var groups = userManagement.GetUserGroups();
            var permissionString = user.RequestedPermissions.ToString().Replace("_", "").ToLower();
            var group = groups.Find(g => Regex.Replace(g.Name.ToLower(), "[-_ ]", "").Contains(permissionString));
            user.PermissionId = group.UserGroupId;
            // Data Groups
            var accounts = new AccountInfoService(managerKey);
            var userDataGroups = accounts.GetDataGroups();
            var expDataGroups = user.RequestedDataGroupsCsv.Split(',');
            var profileGroups = expDataGroups
                .Select(dg =>
                {
                    var userDg = userDataGroups.Items.FirstOrDefault(udg => dg == udg.Name);
                    Assert.IsNotNull(userDg, $"DataGroup: {dg} was not found in manager profile." +
                                             " Cannot create a user with specified data group.");
                    return userDg;
                })
                .Select(udg => new DataGroupResponse().FromProfile(udg));
            user.DataGroups = profileGroups.ToArray();
            // All other possible settings
            if (otherSettings != null)
                user = otherSettings.Invoke(user);
            // Create user
            userManagement.SaveUserCheck(user);
            // Set session key
            try
            {
                if (!string.IsNullOrEmpty(user.SessionKey))
                {
                    var key = _accountsService.LoginWithRetry(user.AsTestUser());
                    user.SessionKey = key;
                }
            }
            finally
            {
                // Register user for the post deletion
                RegisterForDeletion(userManagement, user, manager);
            }
            return user;
        }

        /// <summary>
        /// Creates user with specified settings.
        /// </summary>
        /// <param name="edition"></param>
        /// <param name="permissionType"></param>
        /// <param name="dataGroupsCsv"></param>
        /// <param name="otherSettings"></param>
        /// <returns>DynamicUser</returns>
        public DynamicUser CreateUser(
            string edition,
            DynamicUser.PermissionType permissionType = DynamicUser.PermissionType.standard,
            string dataGroupsCsv = "(Default)",
            Func<DynamicUser, DynamicUser> otherSettings = null)
        {
            var userSettings = GenerateDefaultUser();
            userSettings.RequestedPermissions = permissionType;
            userSettings.RequestedDataGroupsCsv = dataGroupsCsv;
            return CreateUser(edition, userSettings, otherSettings);
        }

        /// <summary>
        /// Creates logged in user with session key.
        /// </summary>
        /// <param name="edition"></param>
        /// <param name="permissionType"></param>
        /// <param name="dataGroupsCsv"></param>
        /// <param name="otherSettings"></param>
        /// <returns>DynamicUser</returns>
        public DynamicUser CreateUserWithKey(string edition,
            DynamicUser.PermissionType permissionType = DynamicUser.PermissionType.standard,
            string dataGroupsCsv = "(Default)",
            Func<DynamicUser, DynamicUser> otherSettings = null)
        {
            var userSettings = GenerateDefaultUser();
            userSettings.SessionKey = "yes";
            userSettings.RequestedPermissions = permissionType;
            userSettings.RequestedDataGroupsCsv = dataGroupsCsv;

            var user = CreateUser(edition, userSettings, otherSettings);
            return user;
        }

        /// <summary>
        /// Generates user that can be created in the system.
        /// </summary>
        /// <returns>DynamicUser</returns>
        public DynamicUser GenerateDefaultUser()
        {
            var user = new DynamicUser();
            // Fixme > Remove hardcode here.
            user.CountryId = 316;
            var rnd = StringUtils.RandomString(StringUtils.AlphaChars, 8).ToLower();
            var email = $"testuserccc{rnd}@cision.com";
            user.Email = email;

            user.FirstName = UniqueName(_feature, rnd);
            user.FullName = $"Fullname{rnd}";
            user.LastName = UniqueName(_scenario, rnd);

            var login = "testuser" + StringUtils.RandomAlphaNumericString(15).ToLower();
            user.LoginId = login;
            user.LoginName = login;
            user.Password = "1";
            user.PasswordConfirm = "1";
            user.Phone = StringUtils.RandomString(StringUtils.NumericChars, 12);

            var timezone = new TimeZonesResponse { Id = "GMT Standard Time" };
            user.TimeZone = timezone;
            user.ExpirationStamp = DateTime.UtcNow.AddMinutes(60).ToTimeStamp().ToString();
            user.ExpirationDate = DateTime.UtcNow.AddMinutes(60).ToString("s") + "Z";
            user.RequestedPermissions = DynamicUser.PermissionType.standard;
            user.RequestedDataGroupsCsv = "(Default)";
            user.CurrentPassword = "";
            user.DefaultSection = "";

            return user;
        }

        /// <summary>
        /// Generates a unique name that fits within the 50 character limit of user first and last name.
        /// </summary>
        /// <param name="name">The name, usually feature or scenario title.</param>
        /// <param name="random">The random string, usually 6-8 alphanumeric.</param>
        private string UniqueName(string name, string random)
        {
            var nonRandomCharCount = 50 - random.Length;
            return (name.Length <= nonRandomCharCount ? name : name.Substring(0, nonRandomCharCount)) + random;
        }

        /// <summary>
        /// Creates a session key for the manager user.
        /// </summary>
        /// <param name="managerEdition"></param>
        /// <returns>string</returns>
        public string GetCachedManagerKeyByEdition(User managerEdition)
        {
            var managerKey = SessionKeyCache.Instance().GetOrAdd(managerEdition.CompanyEdition, key =>
            {
                var localKey = _accountsService.LoginWithRetry(managerEdition);
                return localKey;
            });
            return managerKey;
        }

        /// <summary>
        /// Registers created user for deletion.
        /// </summary>
        /// <param name="userManagement"></param>
        /// <param name="user"></param>
        /// <param name="manager"></param>
        public void RegisterForDeletion(UserManagementService userManagement, DynamicUser user, User manager)
        {
            var usersForCompany = userManagement.GetUsers();
            var userCreated = usersForCompany.Find(u => u.LoginId == user.LoginId);
            var task = new RestBuilder(new Uri(_cleanUpUri)).Post().ToEndPoint(
                "user/register?" +
                $"company={user.CompanyId}" +
                $"&manager={manager.Username}" +
                (string.IsNullOrEmpty(manager.Password) ? "" : $"&password={manager.Password}") +
                $"&env={TestSettings.GetConfigValue("Environment").ToLower()}" +
                $"&id={userCreated.Id}" +
                (string.IsNullOrEmpty(user.ExpirationStamp) ? "" : $"&expires={user.ExpirationStamp}"))
                .Data(user.SessionKey)
                .ExecAsync()
                .ContinueWith(finished =>
                    {
                        if (finished.Result.StatusCode != System.Net.HttpStatusCode.OK)
                            Console.WriteLine("User registration result: " + finished.Result.StatusCode);
                    }
                );
            // Make to wait for it after your test or otherwise it might be incomplete
            // Nothing bad - just trash user somewhere for you company
            RegistrationTasks.Add(task);
        }
    }
}
