using BoDi;
using CCC_API.Data.PostData.Settings.UserManagement;
using CCC_API.Data.Responses.Email;
using CCC_API.Data.Responses.Media;
using CCC_API.Data.TestDataObjects;
using CCC_API.Data.TestDataObjects.Activities;
using CCC_API.Services.Activities;
using CCC_API.Services.Analytics;
using CCC_API.Services.Common.Support;
using CCC_API.Services.EmailDistribution;
using CCC_API.Services.EmailDistribution.DB;
using CCC_API.Services.Media;
using CCC_API.Services.Media.Contact;
using CCC_API.Steps.Activities;
using CCC_API.Steps.Common;
using CCC_Infrastructure.API.Utils;
using CCC_Infrastructure.Utils;
using Newtonsoft.Json;
using CCC_API.Utils.Assertion;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
using TechTalk.SpecFlow;
using Is = NUnit.Framework.Is;

namespace CCC_API.Steps.EmailDistribution
{
    [Binding]
    public class EmailDistributionWizardSteps : AuthApiSteps
    {
        private readonly EmailDistributionService _emailDistributionService;
        private readonly MyActivitiesService _activitiesService;
        private const string Draft = "draft", DefaultDist = "default";
        public const string ADDITIONAL_EMAILS_KEY = "emails";
        public const string DISTRIBUTION_KEY = "distribution";
        public const string DISTRIBUTION_DB_KEY = "distribution db";
        public const string DISTRIBUTION_EMAIL_DB_KEY = "distribution email db";
        public const string SCHEDULE_TIME = "time";
        public const string MERGE_FIELDS_KEY = "merge fields";

        private IObjectContainer _objectContainer;

        public EmailDistributionWizardSteps(IObjectContainer objectContainer)
            : base(objectContainer)
        {
            _emailDistributionService = new EmailDistributionService(SessionKey);
            _activitiesService = new MyActivitiesService(SessionKey);
            _objectContainer = objectContainer;
        }

        [Given(@"I create email '(.*)'")]
        public void GivenICreateEmailDraft(string type)
        {
            var name = StringUtils.RandomAlphaNumericString(8);
            _emailDistributionService
                .SaveFromDefaultDistribution(it => {
                        it.Name = name;
                        return it;
                });

            var draftAc = _activitiesService
                .GetRecentActivities("Email", Draft)
                .GetActivity(it => it.Title == name);

            PropertyBucket.Remember(type, draftAc);
        }
        
        [When(@"I post a word file '(.*)' to the worddoctohtml endpoint")]
        public void WhenIPostFileToTheWorddoctohtmlEndpoint(string fileName)
        {
            var resp = _emailDistributionService.WordDocToHtml(TestData.GetTestFileAbsPath(fileName, Assembly.GetExecutingAssembly()));
            PropertyBucket.Remember("wordFile", resp);
        }
        
        [Then(@"the response should be html code that can be compared to expected '(.*)'")]
        public void ThenTheResponseShouldBeHtmlCodeThatCanBeComparedToExpected(string expFile)
        {
            var resp = PropertyBucket.GetProperty<IRestResponse>("wordFile");
            Assert.AreEqual(HttpStatusCode.OK, resp.StatusCode, $"Wrong response from {EmailDistributionService.WordToHtmlUri}");

            // Lets take expected html
            string expFromFile = TestData.GetResourceEndingWithFileName(expFile, Assembly.GetExecutingAssembly());
            var resObj = resp.Content;

            // Lets ignore images atm, since the resource asset url change everytime (new upload asset created in the system)
            var imgRegexPattern = @"<img.*/>";
            var exp = Regex.Replace(expFromFile,  imgRegexPattern, "");
            var act = Regex.Replace(resObj,       imgRegexPattern, "");

            Assert.AreEqual(exp, act, "Actual response was different");
            
            // Now, lets count img tags
            Assert.AreEqual(Regex.Matches(expFromFile, imgRegexPattern).Count, 
                            Regex.Matches(resObj, imgRegexPattern).Count, "Images were not converted");
        }
        
        [When(@"I post html code '(.*)' to htmltemplates/user endpoint with '(.*)' name")]
        public void WhenIPostHtmlFromFileToHtmltemplatesUserEndpointWithName(string filename, string templatename)
        {
            var tempName = templatename == "some" ? "api " + DateTime.Now : templatename;
            var fileContent = PropertyBucket.GetProperty<string>(filename);

            var htmlTemplate = new HtmlTemplateReq
            {
                name = tempName,
                htmlContent = fileContent
            };

            var resp = _emailDistributionService.SaveHtmlTemplate(htmlTemplate);
            PropertyBucket.Remember("htmltemplates/user", resp);
        }

        [Then(@"html template is saved")]
        public void ThenHtmlTemplateIsSaved()
        {
            var htmlSaveResponse = PropertyBucket.GetProperty<IRestResponse<HtmlTemplate>>("htmltemplates/user");
            Assert.AreEqual(HttpStatusCode.OK, htmlSaveResponse.StatusCode, "Failed to save: EmailDistributionService.HtmlTemplatesUri");
            PropertyBucket.Remember("htmltemplates/user", htmlSaveResponse.Data, true);
        }
        
        [Given(@"I create email html template '(.*)' with name '(\d+)' chars")]
        public void GivenICreateEmailHtmlTemplateWithNameCharsCleanUp(string filename, int nameLength)
        {
            var name = StringUtils.RandomAlphaNumericString(nameLength);
            WhenIPostHtmlFromFileToHtmltemplatesUserEndpointWithName(filename, name);
            ThenHtmlTemplateIsSaved();

            var temp = _emailDistributionService.GetHtmlTemplates().FirstOrDefault(it => it.Name.Equals(name));
            Assert.IsNotNull(temp, "Template was not saved");
            PropertyBucket.Remember("htmltemplates/user", temp, true);
        }

        [When(@"I edit \(put\) html template")]
        public void WhenIEditPutHtmlTemplate()
        {
            var temp = PropertyBucket.GetProperty<HtmlTemplate>("htmltemplates/user");
            Assert.IsNotNull(temp.Id, "Template ID is empty");

            temp.Name = "Edited " + DateTime.Now;
            
            var editedResp = new EmailDistributionService(ResolveSessionKey()).EditHtmlTemplate(temp);
            PropertyBucket.Remember("htmltemplates/user response", editedResp, true);
        }

        [Then(@"the operation '(.*)' is '(\d+)'")]
        public void ThenTheOperationIsNotAllowed(string operation, string expCode)
        {
            var code = HttpStatusCode.OK;
            HttpStatusCode.TryParse(expCode, out code);

            var resp = PropertyBucket.GetProperty<IRestResponse>(operation);
            Assert.AreEqual(code, resp.StatusCode, "Wrong response");
        }

        [Then(@"I can find html template among custom templates '(.*)'")]
        public void ThenICanFindHtmlTemplateAmongCustomTemplates(string htmlFile)
        {
            var template = PropertyBucket.GetProperty<HtmlTemplate>("htmltemplates/user");
            var templates = _emailDistributionService.GetHtmlTemplates();

            var savedTempl = templates.FirstOrDefault(it => it.Name == template.Name);
            Assert.IsNotNull(savedTempl, "Template was not found in company templates");
            
            var code = PropertyBucket.GetProperty<string>(htmlFile);
            var expected = Regex.Replace(code,             @"\s+",            string.Empty);
            var actual   = Regex.Replace(savedTempl.Html, @"\s+|&#xA;|&#xD;", string.Empty);
            Assert.AreEqual(expected, actual, "Html code was not saved");
        }

        [Then(@"I cannot find html template among custom templates")]
        public void ThenICannotFindHtmlTemplateAmongCustomTemplates()
        {
            var template = PropertyBucket.GetProperty<HtmlTemplate>("htmltemplates/user");
            var templates = _emailDistributionService.GetHtmlTemplates();

            var savedTempl = templates.FirstOrDefault(it => it.Name == template.Name);
            Assert.IsNull(savedTempl, "Template was not found in company templates");
        }

        [Then(@"I can see html template thumbnail")]
        public void ThenICanSeeHtmlTemplateThumbnail()
        {
            var template = PropertyBucket.GetProperty<HtmlTemplate>("htmltemplates/user");
            var response = _emailDistributionService.GetHtmlTemplateThumbnail(template);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "Html thumbnail was not saved");
            Assert.IsNotEmpty(response.Content, "Html thumbnail was not saved");
        }

        [When(@"I delete html template ?(.*)?")]
        [Then(@"I delete html template ?(.*)?")]
        public void ThenIDeleteHtmlTemplate(string edition)
        {
            var template = PropertyBucket.GetProperty<HtmlTemplate>("htmltemplates/user");
            var managerKey = string.IsNullOrEmpty(edition)  // Clean up reasons
                ? ResolveSessionKey()
                : new LoginSteps(_objectContainer, FeatureContext, ScenarioContext).GivenILoginAsSharedUser(edition);

            var resp = new EmailDistributionService(managerKey).DeleteHtmlTemplate(template.Name);
            PropertyBucket.Remember("htmltemplates/user response", resp, true);
        }

        [When(@"I create email distribution with additional recipients")]
        public void WhenICreateEmailDistributionWithAdditionalRecipients()
        {
            var emails = PropertyBucket.GetProperty<List<string>>(ADDITIONAL_EMAILS_KEY);
            var dist = _emailDistributionService.CreateDefaultDistribution(settings: it =>
            {
                it.MediaOutlets = new List<BaseMedia>();
                it.MediaContacts = new List<BaseMedia>();
                it.AdditionalEmailAddresses = emails;
                return it;
            });
            PropertyBucket.Remember("distribution", dist);
        }

        [When(@"I create email distribution with additional recipients with schedule type '(now|in future)'")]
        public void WhenICreateEmailDistributionWithAdditionalRecipientsWithScheduleType(string type)
        {
            var emails = PropertyBucket.GetProperty<List<string>>(ADDITIONAL_EMAILS_KEY);
            var dist = _emailDistributionService.CreateDefaultDistribution(scheduleType: type, settings: it =>
            {
                it.MediaOutlets = new List<BaseMedia>();
                it.MediaContacts = new List<BaseMedia>();
                it.AdditionalEmailAddresses = emails;
                return it;
            });
            PropertyBucket.Remember("distribution", dist);
        }

        [When(@"I create email distribution to '(.*)' '(.*)' with additional recipients with schedule type '(.*)'")]
        public EmailDist WhenICreateEmailDistributionToWithAdditionalRecipientsWithScheduleType(string contacts, string outlets, string type)
        {
            var emails = PropertyBucket.GetProperty<List<string>>("emails");

            var dist = _emailDistributionService.CreateDefaultDistribution(scheduleType: type, settings: it =>
            {
                it.MediaOutlets  = string.IsNullOrEmpty(contacts) ? new List<BaseMedia>() : it.MediaOutlets.TakeExactly(1).ToList();
                it.MediaContacts = string.IsNullOrEmpty(outlets)  ? new List<BaseMedia>() : it.MediaContacts.TakeExactly(1).ToList();
                it.AdditionalEmailAddresses = emails;
                return it;
            });

            PropertyBucket.Remember(DISTRIBUTION_KEY, dist);
            return dist;
        }

        [When(@"I POST email distribution with '(.*)', tracking type: '(.*)', '(.*)'")]
        public EmailDist WhenIPostEmailDistributionWithTrackingType(string scheduleType, string trackingType, string parameters)
        {
            var dist = _emailDistributionService.CreateDefaultDistribution(scheduleType: scheduleType, settings: it =>
            {
                it.TrackingParameters = parameters;
                it.TrackingType = trackingType;
                it.HTMLBody = "<p>Email distribution with a link <a href=\"https://en.wikipedia.org/wiki/Main_Page\">https://en.wikipedia.org/wiki/Main_Page</a></p>";
                return it;
            });

            PropertyBucket.Remember(DISTRIBUTION_KEY, dist);
            return dist;
        }

        [When(@"I POST email distribution with '(.*)', 'true|false', 'true|false', tracking type: '(.*)', '(.*)'")]
        public EmailDist WhenIPostEmailDistributionWithTrueTrueTrackingType(
            string scheduleType, bool sendCopy, bool overrideAddress, string trackingType, string parameters)
        {
            var dist = _emailDistributionService.CreateDefaultDistribution(
                scheduleType: scheduleType, 
                settings: it =>
            {
                it.TrackingParameters = parameters;
                it.TrackingType = trackingType;
                it.HTMLBody =
                    "<p>Email distribution with a link <a href=\"https://en.wikipedia.org/wiki/Main_Page\">https://en.wikipedia.org/wiki/Main_Page</a></p>";
                it.SendEmailCopy = sendCopy;
                it.OverrideOptOutAddress = overrideAddress;
                if (overrideAddress)
                {
                    var rnd = StringUtils.RandomAlphaNumericString(44);
                    it.OptOutAddressLine1 = "Line 1" + rnd;
                    it.OptOutAddressLine2 = "Line 2" + rnd;
                    it.OptOutCity = "Austin";
                    it.OptOutState = "Texas";
                    it.OptOutZip = "73344";
                }
                return it;
            });

            PropertyBucket.Remember(DISTRIBUTION_KEY, dist);
            return dist;
        }
        
        [Then(@"distribution has tracking type: '(.*)' and parameters: '(.*)' in DB")]
        public void ThenDistributionHasTrackingTypeAndParametersInDb(string type, string trackingParameters)
        {
            var actDistEmail = PropertyBucket.GetProperty<DistributionEmail>(DISTRIBUTION_EMAIL_DB_KEY);

            Assert.AreEqual(trackingParameters, actDistEmail.TrackingParameters, "Tracking parameters were not saved in system");
            Assert.AreEqual((int) type.ParseEnum<AnalyticsTracking>(), actDistEmail.TrackingType, "Tracking type was wrong in the DB");
        }

        [When(@"I retrieve distribution infromation from tables Distribution, DistributionEmail")]
        public void ThenIRetrieveDistributionInfromationFromTablesDistributionDistributionEmail()
        {
            var userInfo = PropertyBucket.GetProperty<DynamicUser>(LoginSteps.USER_KEY);
            var dist = PropertyBucket.GetProperty<EmailDist>(DISTRIBUTION_KEY);
            using (var service = new EmailDistributionDbService(new CompanyService().GetConnectionToCompanyDbByName(userInfo.CompanyId)))
            {
                var distFromDb = service.GetDistributionByField("Name", dist.Name);
                var distEmail = service.GetDistributionEmail(distFromDb.DistributionID);

                PropertyBucket.Remember(DISTRIBUTION_DB_KEY, distFromDb);
                PropertyBucket.Remember(DISTRIBUTION_EMAIL_DB_KEY, distEmail);
            }
        }

        [Then(@"distribution has other parameters saved in DB to tables Distribution, DistributionEmail")]
        public void ThenDistributionHasOtherParametersSavedInDb()
        {
            var actDist = PropertyBucket.GetProperty<Distribution>(DISTRIBUTION_DB_KEY);
            var actDistEmail = PropertyBucket.GetProperty<DistributionEmail>(DISTRIBUTION_EMAIL_DB_KEY);
            var expDist = PropertyBucket.GetProperty<EmailDist>(DISTRIBUTION_KEY);

            Assert.Multiple(() =>
            {
                // Distribution object
                Assert.AreEqual(expDist.Name, actDist.Name, "Name");
                Assert.AreEqual(actDist.TrackReaderInteractions, 1, "Tracking");
                Assert.That(actDist.EmailJobID, Is.GreaterThan(0), "Email Job");
                Assert.AreEqual(expDist.DataGroupId, actDist.DataGroupId, "Data group id");
                Assert.AreEqual(expDist.CompanyId, actDist.CompanyID, "Distribution company ID");

                // Distribution email object
                Assert.AreEqual(expDist.SendEmailCopy, actDistEmail.CarbonCopy, "Send copy");
                Assert.AreEqual(expDist.Subject, actDistEmail.Subject, "Subject");

                Assert.AreEqual(expDist.HTMLBody,              actDistEmail.HTMLBody, "Body");
                Assert.AreEqual(expDist.OverrideOptOutAddress, actDistEmail.OverrideOptOutAddress, "Override address");
                Assert.AreEqual(expDist.ScheduleType,          actDistEmail.PriorityType, "Send type");

                var expDate = expDist.ScheduleType == (int) EmailDist.ScheduleTypes.Now
                    ? DateTime.MinValue
                    : expDist.GetSendTime();

                Assert.AreEqual(expDate,                         actDistEmail.PriorityDate, "Send date time");
                Assert.AreEqual(expDist.EmailSenderName,         actDistEmail.DisplayName, "Sender name");
                Assert.AreEqual(expDist.EmailSenderAddress,      actDistEmail.ReplyToAddress, "Sender address");
                Assert.AreEqual(expDist.TimeZoneIdentifier,      actDistEmail.TimeZoneName, "Timezone");
                Assert.AreEqual(expDist.OptOutWorkingLanguageID, actDistEmail.OptOutMessageLanguageID, "Opt out language ID");
                Assert.AreEqual(expDist.CompanyId,               actDistEmail.CompanyID, "Distribution Email Company ID");
                Assert.AreEqual(expDist.OptOutName,              actDistEmail.OptOutName, "Opt out name");
                Assert.AreEqual(expDist.OptOutAddressLine1,      actDistEmail.OptOutAddressLine1, "Opt out Address line 1");
                Assert.AreEqual(expDist.OptOutAddressLine2,      actDistEmail.OptOutAddressLine2, "Opt out Address line 2");
                Assert.AreEqual(expDist.OptOutCity,              actDistEmail.OptOutCity, "Opt out City");
                Assert.AreEqual(expDist.OptOutZip,               actDistEmail.OptOutZip, "Opt out Zip");
                Assert.AreEqual(expDist.OptOutCountryId,         actDistEmail.OptOutCountryID, "Opt out Country Id");
            });
        }

        [Given(@"I remember distribution links")]
        public List<Link> GivenIParseLinks()
        {
            var links = JsonConvert.DeserializeObject<List<Link>>
                (JsonConvert.SerializeObject(PropertyBucket.GetProperty(EmailDetailsSteps.LINKS_KEY)));
            PropertyBucket.Remember(EmailDetailsSteps.LINKS_KEY, links, true);
            return links;
        }

        [When(@"I create sent email distribution to '(.*)' with links")]
        [Given(@"I create sent email distribution to '(.*)' with links")]
        public void GivenICreateEmailSentDistributionToWithLinks(string recipientsType)
        {
            var links = PropertyBucket.GetProperty<List<Link>>(EmailDetailsSteps.LINKS_KEY);
            var linkTags = links.Select(l => $@"<p><a href=""{l.Href}"">{l.Name}</a></p>");
            var html = string.Join("\n", linkTags);

            var d = _emailDistributionService.CreateDefaultDistribution(settings: dist =>
            {
                dist.Name = "Dist links " + DateTime.Now;
                // By default some contacts & outlets set.
                if (! recipientsType.Contains("contact")) dist.MediaContacts = new List<BaseMedia>();
                if (! recipientsType.Contains("outlet"))  dist.MediaOutlets  = new List<BaseMedia>();

                var randBody = StringUtils.RandomSentence(100);
                dist.HTMLBody =
                "<p><img src=\"https://goo.gl/y9AVGG\" alt=\"David Gerstein\" align=\"middle\"></p>" +
                $"<p><b>London, {DateTime.Now}</b>. - {randBody}</p>" 
                + html; 
                return dist;
            });

            var act = new Poller(TimeSpan.FromSeconds(180)).TryUntil(() => 
             _activitiesService
             .GetRecentActivities("Email", "sent")
             .SelectActivity(it => it.Title == d.Name));

            PropertyBucket.Remember(DISTRIBUTION_KEY, d);
            PropertyBucket.Remember(PublishActivitySteps.ACTIVITY_KEY, act);
        }

        [Given(@"a few common email addresses:")]
        public void GivenAFewCommonEmailAddresses(Table table)
        {
            var list = table.Rows.Select(it => it.Values.ElementAt(0)).ToList();
            PropertyBucket.Remember(ADDITIONAL_EMAILS_KEY, list);
        }

        [Given(@"I remember avaliable email credits for the company")]
        public void GivenIRememberAvaliableEmailCreditsForTheCompany()
        {
            var emailSettings = _emailDistributionService.GetEmailSettings;
            Assert.IsFalse(emailSettings.HasUnlimitedEmails, "Wrong company for testing. Unlimited emails must be false");
            Assert.IsTrue(emailSettings.HasAdditionalEmailAddresses, "Wrong company for testing. Additional emails must be enabled");
            var credits = emailSettings.AvailableSubscriptions;
            PropertyBucket.Remember("credits", credits);
        }

        [When(@"company credits are charged for unique emails")]
        public void WhenCompanyCreditsAreChargedForEachEmail()
        {
            var dist = PropertyBucket.GetProperty<EmailDist>("distribution");
            var uniquePayload = new UniqueEmailsLists();
            
            var entities = new List<EmailEntityList>();
            entities.AddRange(dist.MediaContacts.Select(it => new EmailEntityList {EntityType = "MediaContact", Id = it.Id}));
            entities.AddRange(dist.MediaOutlets.Select(it  => new EmailEntityList {EntityType = "MediaOutlet",  Id = it.Id}));

            uniquePayload.entityLists = entities;
            uniquePayload.additionalEmailAddresses = dist.AdditionalEmailAddresses;

            var unique = _emailDistributionService.UniqueEmailCount(uniquePayload);
            var emailSettings = _emailDistributionService.GetEmailSettings;

            var expected = PropertyBucket.GetProperty<int>("credits") - unique;
            Assert.AreEqual(expected, emailSettings.AvailableSubscriptions, "Company is not charged properly");
        }

        [When(@"I perform POST to email/distribution/uniqueemailcount with '(.*)'")]
        public void WhenIPerformPostToEmailDistributionUniqueemailcountWith(string types)
        {
            if (! types.Contains("additional")) 
                PropertyBucket.Remember(ADDITIONAL_EMAILS_KEY, new List<string>(), true);

            var emails = PropertyBucket.GetProperty<List<string>>(ADDITIONAL_EMAILS_KEY);

            var listService = new EntityListService(SessionKey);
            var uniquePayload = new UniqueEmailsLists();
            uniquePayload.additionalEmailAddresses = new List<string>(emails);
            uniquePayload.entityLists = new List<EmailEntityList>();

            if (types.Contains("contact"))
            {
                var contactLists = listService.GetAvaliableMediaContactsLists().TakeExactly(1);
                uniquePayload.entityLists.AddRange(contactLists
                    .Select(it => new EmailEntityList { EntityType = "MediaContact", Id = it.Id }));

                var service = new ContactsService(SessionKey);
                var contacts = contactLists
                    .Select(list => list.Name)
                    .SelectMany(name => service.FindContacts(ContactsService.ContactsSearchCriteria.List_Name, name).Items);

                emails.AddRange(contacts.Where(it => !string.IsNullOrEmpty(it.Email)).Select(it => it.Email));
            }

            if (types.Contains("outlet"))
            {
                var outletLists = listService.GetAvaliableMediaOutletsLists().TakeExactly(1);
                uniquePayload.entityLists.AddRange(outletLists
                    .Select(it => new EmailEntityList { EntityType = "MediaOutlet", Id = it.Id }));
                
                var service = new ArticlesAndOutletsService(SessionKey);
                var outlets = outletLists
                    .Select(list => list.Name)
                    .SelectMany(name => service.GetOutlets($"listname={name}").Items);

                emails.AddRange(outlets.Where(it => ! string.IsNullOrEmpty(it.Email)).Select(it => it.Email));
            }

            var unique = _emailDistributionService.UniqueEmailCount(uniquePayload);
            PropertyBucket.Remember("emails count", unique);
        }

        [Then(@"I see a number of unique emails")]
        public void ThenISeeANumberOfUniqueEmails()
        {
            var unique = PropertyBucket.GetProperty<int>("emails count");
            var emails = PropertyBucket.GetProperty<List<string>>(ADDITIONAL_EMAILS_KEY);
            var expCount = new HashSet<string>(emails).Count;
            Assert.AreEqual(expCount, unique, "Email count is wrong");
        }

        [When(@"I schedule \(POST\) email distribution with: (.*), (.*)")]
        public EmailDist WhenIscheduleEmailDistributionWithTimeTimezone(string timezone, string recipientsType)
        {
            // To remember - this scheduletime object during the debug looks like local
            // Actual time on the server will be converted into the timezone specified
            var scheduletime = DateTime.Now.ToUniversalTime().AddDays(1).AddHours(1);
            var d = _emailDistributionService.CreateDefaultDistribution(settings: dist =>
            {
                dist.Name = "Dist scheduled " + scheduletime;
                if (! recipientsType.Contains("contact")) dist.MediaContacts = new List<BaseMedia>();
                if (! recipientsType.Contains("outlet"))  dist.MediaOutlets  = new List<BaseMedia>();

                var randBody = StringUtils.RandomSentence(200);
                dist.HTMLBody = $"<p>{randBody}</p>";

                dist.SetScheduleType(EmailDist.ScheduleTypes.InFuture);
                dist.SetSendTime(scheduletime);
                dist.TimeZoneIdentifier = timezone;

                return dist;
            });

            PropertyBucket.Remember(DISTRIBUTION_KEY, d);
            PropertyBucket.Remember(SCHEDULE_TIME, scheduletime);

            // TODO figure out the Daylight saving time. Not critical atm.
            // This converts time that was scheduled, including Timezone to the utc.
            var offset = - TimeZoneInfo.FindSystemTimeZoneById(timezone).BaseUtcOffset;
            var time = DateTime.Parse(d.SendTime).Add(offset);

            // Testing expect objects to store set properties
            var expAct = new PublishActivity
            {
                Title = d.Name,
                ContentSnippet = d.HTMLBody,
                PublicationState = (int) PublicationsStatus.Scheduled,
                PublicationTime = time.ToString(DateFormat.ISO_8601),
                Owner = PropertyBucket.GetProperty<DynamicUser>(LoginSteps.USER_KEY).LoginName
            };

            PropertyBucket.Remember(PublishActivitySteps.EXP_ACTIVITY_KEY, expAct);
            return d;
        }
        
        [When(@"I unschedule \(POST\) email distribution")]
        public void WhenIunschedulePostEmailDistribution()
        {
            var dist = PropertyBucket.GetProperty<PublishActivity>(PublishActivitySteps.ACTIVITY_KEY);
            _emailDistributionService.UnscheduleEmailDistribution(dist.EntityId);
        }

        [When(@"I GET email merge fields for '(.*)'")]
        public void WhenIgetEmailMergeFieldsFor(string fieldsType)
        {
            var fields = _emailDistributionService.GetMergeFields(fieldsType);
            PropertyBucket.Remember(MERGE_FIELDS_KEY, fields);
        }

        [Then(@"response contains merge fields from file '(.*)'")]
        public void ThenResponseContainsMergeFieldsFromFile(string filename)
        {
            var fileContent    = PropertyBucket.GetProperty<string>(filename);
            var mergeFieldsExp = JsonConvert.DeserializeObject<List<MergeField>>(fileContent);
            var mergeFieldsAct = PropertyBucket.GetProperty<List<MergeField>>(MERGE_FIELDS_KEY);
            
            Assert.That(mergeFieldsAct, Is.EquivalentTo(mergeFieldsExp), Err.Line("Unexpected content for mergefield, comparing with file " + filename));
        }
    }
}