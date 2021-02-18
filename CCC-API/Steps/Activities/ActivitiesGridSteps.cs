using BoDi;
using CCC_API.Data.PostData.Activities;
using CCC_API.Data.Responses.Activities;
using CCC_API.Data.Responses.Campaigns;
using CCC_API.Data.Responses.Grid;
using CCC_API.Data.TestDataObjects;
using CCC_API.Services.Activities;
using CCC_API.Steps.Campaigns;
using CCC_API.Steps.Common;
using CCC_API.Utils.Assertion;
using CCC_Infrastructure.API.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Does = NUnit.Framework.Does;
using Is = NUnit.Framework.Is;

namespace CCC_API.Steps.Activities
{
    [Binding]
    public class ActivitiesGridSteps : AuthApiSteps
    {
        private MyActivitiesService _service;
        public static readonly string COLUMNS_KEY = "Grid columns";
        public static readonly string EXP_COLUMNS_SETTINGS_KEY = "Grid columns settings";
        public static readonly string ACT_BULK_ADD_CAMPAIGN_KEY = "BulkAddActivities";
        public static readonly string ACT_TYPE_KEY = "Activity Type";
        public static readonly string ACT_BULK_CAMPAIGN_DATA = "BulkData Payload";


        public ActivitiesGridSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
            _service = new MyActivitiesService(SessionKey);
        }

        [When(@"I GET my activities grid columns")]
        public void WhenIgetMyActivitiesGridColumns()
        {
            var columns = _service.GetActivitiesGridColumns();
            PropertyBucket.Remember(COLUMNS_KEY, columns, true);
        }

        [When(@"I POST my activities grid order:")]
        public void WhenIpostMyActivitiesGridOrder(Table table)
        {
            var columns = table.CreateSet<Column>().ToList();

            var request = new ActivitiesGridView
            {
                GridViewId = "activitiesgrid",
                Columns = columns
            };

            var resp = _service.PostActivitiesGridColumns(request);
            PropertyBucket.Remember(COLUMNS_KEY, resp);
            PropertyBucket.Remember(EXP_COLUMNS_SETTINGS_KEY, columns);
        }

        [Then(@"my activities grid view response contains expected settings")]
        public void ThenMyActivitiesGridViewResponseContainsExpectedSettings()
        {
            var expSettings = PropertyBucket.GetProperty<List<Column>>(EXP_COLUMNS_SETTINGS_KEY);
            var actResp = PropertyBucket.GetProperty<ActivitiesGridView>(COLUMNS_KEY);
            var actList = actResp.Columns;

            Assert.That(actList, Is.Not.Empty, "Empty settings list.");
            foreach (var expSetting in expSettings)
            {
                Assert.That(actList, Does.Contain(expSetting).Using(new ColumnComparer()), 
                    $"Column [{expSetting}] has wrong settings.");
            }
        }
        
        [Then(@"my activities default view response is empty")]
        public void ThenMyActivitiesDefaultViewResponseIsEmpty()
        {
            var actColsResp = PropertyBucket.GetProperty<ActivitiesGridView>(COLUMNS_KEY);
            Assert.That(actColsResp.GridViewId, Is.EqualTo("activitiesgrid"), "Wrong activities grid id");
            Assert.That(actColsResp.Columns, Is.Empty, "Activities grid sort order was not default for a user");
        }

        [When(@"I assign '(.*)' activities of type '(.*)' to random campaign")]
        [Then(@"I assign '(.*)' activities of type '(.*)' to random campaign")]
        public void WhenIAssignActivitiesOfTypeToRandomCampaign(int acts, string type)
        {
            var ca = PropertyBucket.GetProperty<Campaign>(CampaignsSteps.Campaign);
            List<PublishActivity> selectedActs = new Poller().TryUntil(() =>
            _service.GetRecentActivities(type).PublishActivities.Take(acts).ToList());
            

            Assert.IsTrue(selectedActs.Count > 0, $"No activities of type: {type} selected");

            List<Delta> deltaFields = new List<Delta>();
            List<int> values = new List<int>();

            foreach (var item in selectedActs)
            {
                deltaFields.Add(new Delta { Type = item.Type, EntityId = item.EntityId.ToString() });
            }

            values.Add(ca.Id);

            BulkAddToCampaignData data = new BulkAddToCampaignData
            {
                allSelected = false,
                delta = deltaFields,
                filter = new filter(),
                Values = values,
                Operation = "APPEND"
            };


            var resp = _service.BulkAddActivitiesToCampaign(data, data.Operation);
            var updatedAct = _service.GetActivitiesByCampaign(ca.Id.ToString());
                
            PropertyBucket.Remember(ACT_BULK_ADD_CAMPAIGN_KEY, updatedAct.PublishActivities);
            PropertyBucket.Remember(ACT_TYPE_KEY, type);
            PropertyBucket.Remember(ACT_BULK_CAMPAIGN_DATA, data);
            Assert.IsTrue(updatedAct.PublishActivities.Count()== 2, "No Activities Were Assigned To A Campaign");
        }

        [Then(@"I remove previous activities from campaign")]
        public void ThenIRemovePreviousActivitiesFromCampaign()
        {
            var type = PropertyBucket.GetProperty<string>(ACT_TYPE_KEY);
            var act = PropertyBucket.GetProperty<List<PublishActivity>>(ACT_BULK_ADD_CAMPAIGN_KEY);
            var data = PropertyBucket.GetProperty<BulkAddToCampaignData>(ACT_BULK_CAMPAIGN_DATA);
            var ca = PropertyBucket.GetProperty<Campaign>(CampaignsSteps.Campaign);
            

            _service.BulkAddActivitiesToCampaign(data, "REMOVE");
            var updated = _service.GetActivitiesByCampaign(ca.Id.ToString()).PublishActivities;
            Assert.Multiple(() => {
                Assert.AreNotEqual(updated, act, "Lists are equals, no campaigns removed from activities");
                Assert.IsEmpty(updated.ToList(), "List is not empty, campaign was not removed");
            });
            
        }

    }
}
