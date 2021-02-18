using BoDi;
using CCC_API.Data.PostData.Activities;
using CCC_API.Data.PostData.Settings.UserManagement;
using CCC_API.Data.Responses.Activities;
using CCC_API.Data.Responses.Campaigns;
using CCC_API.Data.Responses.Common;
using CCC_API.Data.TestDataObjects;
using CCC_API.Data.TestDataObjects.Activities;
using CCC_API.Services.Activities;
using CCC_API.Services.Activities.DB;
using CCC_API.Services.Common.Support;
using CCC_API.Steps.Campaigns;
using CCC_API.Steps.Common;
using CCC_API.Steps.Settings.FormManagement;
using CCC_API.Utils;
using CCC_Infrastructure.API.Utils;
using CCC_Infrastructure.Utils;
using CCC_API.Utils.Assertion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TechTalk.SpecFlow;
using Does = NUnit.Framework.Does;
using Is = NUnit.Framework.Is;

namespace CCC_API.Steps.Activities
{
    public class PublishActivitySteps : AuthApiSteps
    {
        public const string ACTIVITY_KEY = "activity";
        public const string JOB_KEY = "job";
        public const string REQUEST_KEY = "request";
        public const string EXP_ACTIVITY_KEY = "expected activity";

        private readonly MyActivitiesService _activitiesService;
        public PublishActivitySteps(IObjectContainer objectContainer) : base(objectContainer)
        {
            _activitiesService = new MyActivitiesService(SessionKey);
        }

        [Then(@"I can assign '(.*)' to campaign")]
        public void ThenICanAssignEmailDraftToCampaign(string type)
        {
            var acc = PropertyBucket.GetProperty<PublishActivity>(type);
            var ca  = PropertyBucket.GetProperty<Campaign>(CampaignsSteps.Campaign);
            List<int> ids = new List<int>();
            ids.Add(ca.Id);

            _activitiesService.AssignToACampaign(ids,ca.Id.ToString(), acc.EntityId, acc.Type);
        }

        [When(@"I delete '(.*)' from campaign")]
        [Then(@"I delete '(.*)' from campaign")]
        public void WhenIDeleteFromCampaign(string type)
        {
            var acc = PropertyBucket.GetProperty<PublishActivity>(type);
            var ca = PropertyBucket.GetProperty<Campaign>(CampaignsSteps.Campaign);
            List<int> values = new List<int>();

            _activitiesService.RemoveFromACampaign(values,ca.Id.ToString(), acc.EntityId, acc.Type);
        }

        [Then(@"(can|cannot) find '(.*)' in publish activities by campaign")]
        public void ThenSeeActivityInCampaignActivities(string choise, string type)
        {
            var acc = PropertyBucket.GetProperty<PublishActivity>(type);
            var ca  = PropertyBucket.GetProperty<Campaign>(CampaignsSteps.Campaign);
            var act = _activitiesService.GetActivitiesByCampaign(ca.Id.ToString())
                        .SelectActivity(it => 
                            acc.EntityId == 0 
                            ? it.Title == acc.Title 
                            : it.EntityId == acc.EntityId);

            var exp = choise.Contains("not") ? act == null : act != null;
            Assert.IsTrue(exp, $"Activity '{type}' is " + (choise.Contains("not") ? "not " : "") 
                                + "expected to be present in campaign");
        }

        [Then(@"(can|cannot) find activity of '(.*)' and '(.*)' activity listed among published activities")]
        public void ThenICanFindActivityListedInPublishActivities(string choise, string type, string state)
        {
            var acc  = PropertyBucket.GetProperty<PublishActivity>(type);
            var typeId = PropertyBucket.ContainsKey(FormManagementSteps.CUSTOM_TYPE_ID)
                ? PropertyBucket.GetProperty(FormManagementSteps.CUSTOM_TYPE_ID).ToString()
                : type;

            var acts = _activitiesService.GetRecentActivities(types: typeId, publicationStates: state);
            var act  = acts.SelectActivity(it => it.Title == acc.Title);

            var exp = choise.Contains("not") ? act == null : act != null;
            Assert.IsTrue(exp, $"Activity '{type}' is " + (choise.Contains("not") ? "not " : "")
                                + $"expected to be present among {state} activities");
            PropertyBucket.Remember(type, act, true);
        }
        [Then(@"scheduled '(.*)' activity contains correct publication state, content, date time, owner")]
        public void ThenScheduledActivityContainsCorrectInfo(string type)
        {
            var expDist = PropertyBucket.GetProperty<PublishActivity>(EXP_ACTIVITY_KEY);

            var act = new Poller().TryUntil(() =>
             _activitiesService
             .GetRecentActivities(type, "Scheduled")
             .SelectActivity(it => it.Title == expDist.Title));

            PropertyBucket.Remember(ACTIVITY_KEY, act);
            PropertyBucket.Remember(type, act);

            Assert.AreEqual((int) PublicationState.Scheduled, act.PublicationState, "Publication state is wrong");
            Assert.AreEqual(expDist.Owner, act.Owner, "Activity wrong owner");
            Assert.That(expDist.ContentSnippet, Does.Contain(act.ContentSnippet.Replace("…", "")), "Wrong content snippet");

            var scheduleTime = DateTime.Parse(expDist.PublicationTime);
            var actTime = DateTime.Parse(act.PublicationTime.Replace("Z", ""));
            
            // TODO figure out the Daylight saving time. Not critical atm.
            Assert.That(actTime, Is.EqualTo(scheduleTime).Within(1).Hours, "Wrong scheduled time on Publish activity");
        }

        [When(@"I export activities with default sections")]
        public void WhenIExportActivitiesWithDefaultSections()
        {
            var type   = new ExportField {Key = "Type",   Label = "Type"};
            var status = new ExportField {Key = "Status", Label = "Status"};
            var title  = new ExportField {Key = "Title",  Label = "Title"};
            var time   = new ExportField {Key = "Time",   Label = "Date and time"};
            
            ExportFilterData data = new ExportFilterData
            {
                exportFields = new List<ExportField> {type, status, title, time},
                filter = new Filter()
            };

            var jobResponse = _activitiesService.ExportActivitiesXlxs(data);
            PropertyBucket.Remember(JOB_KEY, jobResponse);
            PropertyBucket.Remember(REQUEST_KEY, data);
        }

        [Then(@"the job created with pending status and link to the report")]
        public void ThenTheJobCreatedWithPendingStatusAndLinkToTheReport()
        {
            var job = PropertyBucket.GetProperty<JobResponse>(JOB_KEY);
            Assert.AreEqual("Pending", job.Status.State, "State");
            Assert.IsNotNull(job._links.self, "Job reference");
            Assert.IsNotNull(job._links.file, "Report link");
        }

        [Then(@"I can download xlxs report by link with correct exported activities")]
        public void ThenICanDownloadXlxsReportByLinkWithCorrectExportedActivities()
        {
            var job = PropertyBucket.GetProperty<JobResponse>(JOB_KEY);
            var request = PropertyBucket.GetProperty<ExportFilterData>(REQUEST_KEY);
            var user = PropertyBucket.GetProperty<DynamicUser>(LoginSteps.USER_KEY);
            var dataGroup = user.DataGroups[0];

            Office2007Reader.DownloadAssertXlsxFile(job._links.file, dataSet =>
            {
                // At least one table "Activities" expected
                Assert.AreEqual(1, dataSet.Tables.Count, "One table expected in activites report");
                var activitiesTable = dataSet.Tables[0];
                Assert.AreEqual(activitiesTable.TableName, "Activities",
                    "Wrong table name in activities report");
                // Default Headers
                var rows = activitiesTable.Rows;
                var headers = request.exportFields.Select(it => it.Label).ToList();
                Assert.AreEquivalent(headers, rows[0].ItemArray, "Headers of the report wrong");
                // Actual exported records (items)
                IList<ExportActivity> actXlsx = activitiesTable.AsEnumerable()
                    .Skip(1) // Skip headers
                    .Select(row => // Convert to ExportActivity
                    {
                        var data = row.ItemArray;
                        var act = new ExportActivity
                        {
                            
                            Type = data[headers.IndexOf("Type")].ToString().Replace(" ", ""),
                            Title    = data[headers.IndexOf("Title")].ToString(),
                            Status   = (int) data[headers.IndexOf("Status")].ToString().Replace(" ", "").ParseEnum<PublicationsStatus>(),
                            DateTime = DateTime.Parse(data[headers.IndexOf("Date and time")].ToString())
                        };
                        return act;
                    })
                    .ToList();

                // Database records
                using (var dbService = new ActivitiesDbService(new CompanyService().GetConnectionToCompanyDbByName(user.CompanyId)))
                {
                    var expActFromDb = dbService.GetAllActivities(dataGroup.id);
                    Assert.IsNotEmpty(expActFromDb, "Activities from the DB empty");

                    // Due to multithreading running nature - some activities might be created during this export job running
                    // So the test may fail. Let's give it a bit of tolerance, but still have a pretty high level of precision
                    // ps. It is still good to have it running on this "live" company with many fresh activities created.
                    var total = expActFromDb.Count;
                    var fault = 5;
                    Assert.That(actXlsx.Count,
                        Is.GreaterThanOrEqualTo(total - fault)
                        & Is.LessThanOrEqualTo(total + fault), "Number of rows is unexpected");
                    // Lets check all the exported activities are similar to the db
                    foreach (var exportedActivity in actXlsx)
                    {
                        Assert.IsTrue(expActFromDb.Contains(exportedActivity),
                            "Not all the activities were exported. " +
                            "Not found in xlxs: " + exportedActivity.Title);
                    }
                }
            });
        }

    }
}