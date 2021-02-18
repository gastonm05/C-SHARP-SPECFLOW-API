using BoDi;
using CCC_API.Data.Factory.AnalyticsView;
using CCC_API.Data.Responses.Analytics;
using CCC_API.Data.Responses.Analytics.Available;
using CCC_API.Data.Responses.Common;
using CCC_API.Services.Analytics;
using CCC_API.Steps.Common;
using CCC_API.Utils;
using CCC_Infrastructure.Utils;
using Newtonsoft.Json;
using CCC_API.Utils.Assertion;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using TechTalk.SpecFlow;
using static CCC_API.Services.Analytics.DashboardsService;
using Is = NUnit.Framework.Is;
using CCC_API.Data.PostData.Analytics;

namespace CCC_API.Steps
{
    [Binding]
    public class AnalyticsDashboardsEndPointSteps : AuthApiSteps
    {
        public const string GET_RESPONSE_KEY = "GetResponse";
        public const string VIEW_KEY = "View";
        public const string NOTE_KEY = "Note";
        public const string AVAILABLE_WIDGETS_KEY = "AvailableWidgets";
        public const string SECTION_TEMPLATE_KEY = "SectionsTemplate";
        public const string WIDGET_TEMPLATE_KEY = "WidgetsTemplate";
        public const string SECTION_KEY = "Section";
        public const string SECTION_NAME = "Section created from an automated test";
        public const string ANALYTICS_DASHBOARD_READONLY = "AnalyticsDashBoardReadOnly";

        private DashboardsService _dashboardsService;

        public AnalyticsDashboardsEndPointSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
            _dashboardsService = new DashboardsService(SessionKey);
        }

        [Given(@"I take '(.*)' analytics view information")]
        public void GivenITakeViewId(string viewName)
        {
            var views = _dashboardsService.GetAllViews();
            var view = views
                .Where(_ => _.Name == viewName)
                .FirstOrError("Cannot find view " + viewName);
            PropertyBucket.Remember(VIEW_KEY, view);
        }

        [When(@"I perform a GET for all views")]
        public void WhenIPerformAGETForAllViews()
        {
            var response = _dashboardsService.GetViews();
            PropertyBucket.Remember(GET_RESPONSE_KEY, response);
        }

        [When(@"I perform a sessionless GET for all views")]
        public void WhenIPerformASessionlessGETForAllViews()
        {
            var response = Get(ApiEndpoints.UriBaseDomain, DashboardsService.AnalyticsViewsEndPoint);
            PropertyBucket.Remember(GET_RESPONSE_KEY, response);
        }

        [When(@"I perform a sessionless GET for available widgets")]
        public void WhenIPerformAGETForAvailableWidgets()
        {
            var response = Get(ApiEndpoints.UriBaseDomain, DashboardsService.AnalyticsAvailableWidgetsEndpoint);
            PropertyBucket.Remember(GET_RESPONSE_KEY, response);
        }

        [When(@"I perform a sessionless GET for last view id")]
        public void WhenIPerformAGETForLastViewId()
        {
            var response = Get(ApiEndpoints.UriBaseDomain, DashboardsService.AnalyticsLastViewIdEndPoint);
            PropertyBucket.Remember(GET_RESPONSE_KEY, response);
        }

        [When(@"I perform a sessionless GET for a view")]
        public void WhenIPerformAGETForViewId()
        {
            const int VIEW_ID = 1;
            var response = Get(ApiEndpoints.UriBaseDomain, $"{DashboardsService.AnalyticsViewEndPoint}\\{VIEW_ID}");
            PropertyBucket.Remember(GET_RESPONSE_KEY, response);
        }

        [When(@"I perform a GET for '(.*)' available widgets")]
        public void WhenIPerformAGETForAvailableWidgets(AreaId areaId)
        {
            var availableWidgets = _dashboardsService.GetAvailableWidgets(areaId);
            PropertyBucket.Remember(AVAILABLE_WIDGETS_KEY, availableWidgets);
        }

        [When(@"I use a generic automation view")]
        public AvailableViewSectionsWidgets WhenIUseTheAutomationView()
        {
            AvailableViewSectionsWidgets info;
            // reuse the automation view if available
            var views = _dashboardsService.GetAllViews();
            var viewId = views.FirstOrDefault(_ =>
                _.Name == TestDataFactory.AUTOMATION_VIEW_NAME &&
                _.IsEditable == true
                )?.Id ?? 0;
            if (viewId == 0) // create a new automation view
            {
                info = _dashboardsService.PostView(TestDataFactory.GenericViewData());
            }
            else // reuse the automation view
            {
                var view = TestDataFactory.GenericViewData();
                view.Id = viewId; // overwrite existing view;
                info = _dashboardsService.PutView(view);
            }
            PropertyBucket.Remember(VIEW_KEY, info, true);
            return info;
        }

        [When(@"I take \(GET\) available view data")]
        public AvailableViewSectionsWidgets GivenITakeLastViewId()
        {
            var viewId = PropertyBucket.GetProperty<ViewsViewResponse>(VIEW_KEY).Id;
            var info = _dashboardsService.GetView(Convert.ToInt32(viewId));
            PropertyBucket.Remember(VIEW_KEY, info, true);
            return info;
        }

        [When(@"I add \(PUT\) comment to a widget")]
        public void WhenIAddPutCommentToWidgets()
        {
            var info = PropertyBucket.GetProperty<AvailableViewSectionsWidgets>(VIEW_KEY);
            var widget = info.Sections
                .Where(_ => _.Widgets.Any())
                .RandomFirst("No section present")
                .Widgets
                .RandomFirst("No widgets to comment");

            var rnd = StringUtils.RandomAlphaNumericString(8);
            var comment = "<p><strong>API super test comment</strong> " +
                      $"<em>{StringUtils.RandomSentence(5)}</em> " +
                      $"<u>{StringUtils.RandomEmail(5)}</u> <s>{rnd}</s> {DateTime.Now}</p>\n" +
                      $"<p style=\"text - align: center; \">{rnd}</p>\n" +
                      $"<p style=\"text - align: right; \">{rnd}</p>\n" +
                      $"<p style=\"text - align: justify; \">{StringUtils.RandomSpecialString(8)}</p>\n" +
                      "<ol><li><strong>one</strong></li><li><strong>two</strong></li><li><strong>three</strong></li></ol>" +
                      "<ul><li>one</li><li>two</li><li>three</li></ul>";

            widget.Notes = comment;
            _dashboardsService.PutNote(widget.Id, comment, searchId: 0);
            PropertyBucket.Remember(NOTE_KEY, widget);
        }

        [When(@"I perform a GET for '(.*)' section templates endpoint")]
        public void WhenIPerformAGETForSectionTemplatesEndpoint(AreaId areaId)
        {
            var availableSectionTemplate = _dashboardsService.GetSectionTemplate(areaId);
            PropertyBucket.Remember(SECTION_TEMPLATE_KEY, availableSectionTemplate);
        }

        [When(@"I perform a GET for '(.*)' widget templates endpoint")]
        public void WhenIPerformAGETForWidgetTemplatesEndpoint(AreaId areaId)
        {
            var availableWidgetTemplate = _dashboardsService.GetWidgetTemplate(areaId);
            PropertyBucket.Remember(WIDGET_TEMPLATE_KEY, availableWidgetTemplate);
        }

        [When(@"I perform a POST for section endpoint with the following widgets")]
        public void WhenIPerformAPOSTForSectionEndpointWithTheFollowingWidgets(Table table)
        {
            var widgets = new Dictionary<int, string>();

            table.Rows.ToList().ForEach(r =>
            {
                widgets.Add(Int32.Parse(r["widgetId"]), r["widgetName"]);
            });

            var info = PropertyBucket.GetProperty<AvailableViewSectionsWidgets>(VIEW_KEY);
            var section = _dashboardsService.PostSectionWithWidget(info.Id, widgets, SECTION_NAME);
            PropertyBucket.Remember(SECTION_KEY, section);
        }

        [When(@"I perform a POST to create a new analytics dashboard '(.*)'")]
        public void WhenIPerformAPOSTToCreateANewAnalyticsDashboard(string name)
        {
            var response = _dashboardsService.PostDashboardReadOnlyUser(name);
            PropertyBucket.Remember(ANALYTICS_DASHBOARD_READONLY, response);
        }

        [Then(@"the endpoint is unauthorized")]
        public void ThenTheEndpointIsUnauthorized()
        {
            var response = PropertyBucket.GetProperty<RestResponse<Dictionary<string, string>>>(GET_RESPONSE_KEY);
            var incident = JsonConvert.DeserializeObject<Incident>(response.Content);
            Assert.AreEqual(Incident.UNAUTHORIZED_OPERATION, incident, $"Expected unauthorized incident error response, not '{response.Content}'");
        }

        [Then(@"the endpoint denies authorization")]
        public void ThenTheEndpointDeniesAuthorized()
        {
            var response = PropertyBucket.GetProperty<RestResponse<Dictionary<string, string>>>(GET_RESPONSE_KEY);
            var incident = JsonConvert.DeserializeObject<Incident>(response.Content);
            Assert.AreEqual(Incident.AUTHORIZATION_DENIED, incident, $"Expected denied authorization incident error response, not '{response.Content}'");
        }

        [Then(@"the views endpoint has the correct response")]
        public void ThenTheViewsEndpointHasTheCorrectResponse()
        {
            IRestResponse<List<ViewsViewResponse>> response = PropertyBucket.GetProperty<IRestResponse<List<ViewsViewResponse>>>(GET_RESPONSE_KEY);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, response.Content);
            IList<ViewsViewResponse> views = response.Data;
            Assert.That(views.Count, Is.GreaterThan(0), "Not views found");
            foreach (ViewsViewResponse view in views)
            {
                Assert.Multiple(() =>
                {
                    Assert.IsNotNull(view.Id, "View id is null");
                    Assert.IsNotNull(view.Name, "View name is null");
                    Assert.IsNotNull(view.IsEditable, "IsEditable is null");
                    Assert.IsNotNull(view.IsOwner, "IsOwner is null");
                    Assert.IsNotNull(view.IsSystem, "IsSystem is null");
                });
            }
        }

        [Then(@"widget note is saved")]
        public void ThenWidgetNoteIsSaved()
        {
            var exp = PropertyBucket.GetProperty<Widget>(NOTE_KEY);
            var act = GivenITakeLastViewId();
            var actWidget = act.Sections
                .SelectMany(_ => _.Widgets)
                .Where(_ => _.Id == exp.Id)
                .FirstOrError("Cannot find widget");
            Assert.That(actWidget.Notes, Is.EqualTo(exp.Notes).And.Not.Empty, "Notes not saved");
        }

        [Then(@"the available widget endpoint has the correct response")]
        public void ThenTheAvailableWidgetEndpointHasTheCorrectResponse()
        {
            var availableWidgets = PropertyBucket.GetProperty<List<AvailableWidget>>(AVAILABLE_WIDGETS_KEY);
            Assert.That(availableWidgets.Count, Is.GreaterThan(0), "No available widgets found.");
            foreach (var widget in availableWidgets)
            {
                Assert.That(widget.Id, Is.GreaterThan(0), $"Available widget id '{widget.Id}' invalid");
                Assert.That(widget.LanguageKey, Is.Not.Null.Or.Empty, $"Available widget language key '{widget.LanguageKey}' invalid");
                Assert.That(widget.TooltipLanguageKey, Is.Not.Null.Or.Empty, $"Available widget tooltip invalid for '{widget.LanguageKey}'");
                Assert.That(widget.AxisNames, Is.Not.Null, $"Available widget axis names was null for '{widget.LanguageKey}'"); // can be empty
                Assert.That(widget.AxisNames.Any(a => string.IsNullOrWhiteSpace(a)), Is.False, $"One of the axis names invalid for '{widget.LanguageKey}'");
                Assert.That(widget.AvailableDataSet, Is.Not.Null, $"Available data set was null for '{widget.LanguageKey}'"); // can be empty
                Assert.That(widget.AvailableDataSet.Id, Is.GreaterThan(0), $"Available data set id invalid for '{widget.LanguageKey}'");
                Assert.That(widget.AvailableDataSet.Name, Is.Not.Null.Or.Empty, $"Available data set language key invalid for '{widget.LanguageKey}'");
                Assert.That(widget.AvailableDataSet.Endpoint, Is.Not.Null, $"Available data set endpoint was null for '{widget.LanguageKey}'"); // can be empty
                Assert.That(widget.AvailableDataSet.AvailableDataSeries, Is.Not.Null, $"Available data series was null for '{widget.LanguageKey}'"); // can be empty
                Assert.That(widget.AvailableDataSet.AvailableDataSeries.All(a => a.IsValid()), Is.True, $"One of the data series was invalid for '{widget.LanguageKey}'");
                Assert.That(widget.AvailableWidgetType, Is.Not.Null, $"Available widget type invalid for '{widget.LanguageKey}'"); // can be empty
                Assert.That(widget.AvailableWidgetType.Id, Is.GreaterThan(0), $"Available widget type id invalid for '{widget.LanguageKey}'");
                Assert.That(widget.AvailableWidgetType.LanguageKey, Is.Not.Null.Or.Empty, $"Available widget type language key invalid for '{widget.LanguageKey}'");
                Assert.That(widget.AnalyticsArea, Is.Not.Null.Or.Empty, $"Available area invalid for '{widget.LanguageKey}'");
                Assert.That(widget.AnalyticsArea.Id, Is.GreaterThan(0), $"Available area id invalid for '{widget.LanguageKey}'");
                Assert.That(widget.AnalyticsArea.LanguageKey, Is.Not.Null.Or.Empty, $"Available area language key invalid for '{widget.LanguageKey}'");
                Assert.That(widget.AvailableWidgetOptions, Is.Not.Null, $"Available widget options was null for '{widget.LanguageKey}'"); // can be empty
                Assert.That(widget.AvailableWidgetOptions.All(a => a.IsValid()), Is.True, $"One of the analytics widget options was invalid for '{widget.LanguageKey}'");
            }
        }

        [Then(@"the custom categories have all groups")]
        public void ThenTheCustomCategoriesHaveAllGroups()
        {
            var availableWidgets = PropertyBucket.GetProperty<List<AvailableWidget>>(AVAILABLE_WIDGETS_KEY);
            Assert.That(availableWidgets.Count, Is.GreaterThan(0), "No available widgets found");
            var customCategoryWidgets = availableWidgets.Where(w => w.LanguageKey.StartsWith("COMP_ANALYTICS_CHART_NAME_AA_"));
            Assert.That(customCategoryWidgets.Count(), Is.EqualTo(5), "Missing some custom category widgets");
            foreach (var widget in customCategoryWidgets)
            {
                var option = widget.AvailableWidgetOptions.Where(o => o.Name == "Category / Group");
                Assert.That(option.Count(), Is.EqualTo(1), $"Too many AvailableWidgetOptions found'{option.Count()}'");

                // verify all group options
                var allGroupsOptionValues = option.ElementAt(0).AvailableWidgetOptionValues.Where(o => o.Name.StartsWith("All Groups in "));
                var groupsCount = option.ElementAt(0).AvailableWidgetOptionValueGroups.Count();
                Assert.That(allGroupsOptionValues.Count(), Is.GreaterThan(0), "No All Groups options found");
                Assert.That(allGroupsOptionValues.Count(), Is.EqualTo(groupsCount), "Group count did not match count of All Groups options");
                foreach (var allGroup in allGroupsOptionValues)
                {
                    Assert.That(allGroup.Value == allGroup.OptionValueGroupId.ToString(), $"All Group '{allGroup.OptionValueGroupId}' did not reference itself");
                }

                // verify non all group options
                var groupOptionValues = option.ElementAt(0).AvailableWidgetOptionValues.Where(o => !o.Name.StartsWith("All Groups in "));
                Assert.That(groupOptionValues.Count(), Is.GreaterThan(0), "No groups found");
                foreach (var group in groupOptionValues)
                {
                    var delimitedValue = group.Value.Split('|');
                    // groups should have a reference to the all group option
                    Assert.That(delimitedValue.Count(), Is.GreaterThan(1), $"Group '{group.Name}' was missing a reference to a group or all group option");
                    // the reference should point back to a valid all group option
                    Assert.That(allGroupsOptionValues.Any(allGroup => allGroup.Value == delimitedValue[0]), Is.True, $"Could not find all group for id '{delimitedValue[0]}'");
                }
            }
        }

        [Then(@"the section templates endpoint has the correct response")]
        public void ThenTheSectionTemplatesEndpointHasTheCorrectResponse()
        {
            var sectionsTemplate = PropertyBucket.GetProperty<List<SectionTemplate>>(SECTION_TEMPLATE_KEY);

            Assert.That(sectionsTemplate.Count, Is.GreaterThan(0), "No available section template found");

            foreach (var sectionTemplate in sectionsTemplate)
            {
                Assert.That(sectionTemplate.Id, Is.GreaterThan(0), $"Available section template id '{sectionTemplate.Id}' invalid");
                Assert.That(sectionTemplate.Name, Is.Not.Null.Or.Empty, $"Available section template Name '{sectionTemplate.Name}' invalid");
                Assert.IsTrue(sectionTemplate.IsNameTranslatable, $"Available section template IsNameTranslatable '{sectionTemplate.IsNameTranslatable}' invalid");
                Assert.That(sectionTemplate.SortOrder >= 0, $"Available section template SortOrder '{sectionTemplate.SortOrder}' invalid");
                Assert.That(sectionTemplate.SystemId, Is.Not.Null.Or.Empty, $"Available section template SystemId '{sectionTemplate.SystemId}' invalid");
                Assert.That(sectionTemplate.Widgets.All(w => w.Id > 0), $"The Id widget is not valid");
                Assert.True(sectionTemplate.Widgets.All(w => !string.IsNullOrEmpty(w.Name)), "The Name widget is not valid");
                Assert.True(sectionTemplate.Widgets.All(w => w.IsNameTranslatable), "The IsNameTranslatable widget is not valid");
                Assert.That(sectionTemplate.Widgets.All(w => w.SortOrder >= 0), "The SortOrder widget is not valid");
                Assert.That(sectionTemplate.Widgets.All(w => w.AvailableWidgetId > 0), "The AvailableWidgetId widget is not valid");
                Assert.That(sectionTemplate.Widgets.All(w => w.SystemId > 0), "The SystemId widget is not valid");
                Assert.That(sectionTemplate.Widgets.All(w => !string.IsNullOrEmpty(w.Icon)), "The Icon widget is not valid");
            }
        }

        [Then(@"the widget templates endpoint has the correct response")]
        public void ThenTheWidgetTemplatesEndpointHasTheCorrectResponse()
        {
            var widgetsTemplate = PropertyBucket.GetProperty<List<WidgetTemplate>>(WIDGET_TEMPLATE_KEY);

            Assert.That(widgetsTemplate.Count, Is.GreaterThan(0), "No available widget template found");

            foreach (var widgetTemplate in widgetsTemplate)
            {
                Assert.That(widgetTemplate.Category.Id, Is.GreaterThan(0), $"Available widget template id '{widgetTemplate.Category.Id}' invalid");
                Assert.That(widgetTemplate.Category.Name, Is.Not.Null.Or.Empty, $"Available widget template Name '{widgetTemplate.Category.Name}' invalid");
                Assert.That(widgetTemplate.Widgets.All(w => w.Id > 0), "The Id widget is not valid");
                Assert.True(widgetTemplate.Widgets.All(w => !string.IsNullOrEmpty(w.Name)), "The Name widget is not valid");
                Assert.True(widgetTemplate.Widgets.All(w => w.IsNameTranslatable), "The IsNameTranslatable widget is not valid");
                Assert.That(widgetTemplate.Widgets.All(w => w.SortOrder >= 0), "The SortOrder widget is not valid");
                Assert.That(widgetTemplate.Widgets.All(w => w.AvailableWidgetId > 0), "The AvailableWidgetId widget is not valid");
                Assert.That(widgetTemplate.Widgets.All(w => w.SystemId > 0), "The SystemId widget is not valid");
                Assert.True(widgetTemplate.Widgets.All(w => !string.IsNullOrEmpty(w.Icon)), "The Icon widget is not valid");
            }
        }

        [Then(@"the section endpoint has the correct response")]
        public void ThenTheSectionEndpointHasTheCorrectResponse()
        {
            var info = PropertyBucket.GetProperty<AvailableViewSectionsWidgets>(VIEW_KEY);
            var viewSection = PropertyBucket.GetProperty<SectionTemplate>(SECTION_KEY);

            Assert.That(info.Id == viewSection.ViewId, $"Section id '{viewSection.ViewId}' is invalid");
            Assert.That(SECTION_NAME.Equals(viewSection.Name), $"Section name '{viewSection.Name}' is invalid");
        }

        [Then(@"the following widgets have been created")]
        public void ThenTheFollowingWidgetsHaveBeenCreated(Table table)
        {
            var expectedWidgets = new Dictionary<int, string>();

            table.Rows.ToList().ForEach(r =>
            {
                expectedWidgets.Add(Int32.Parse(r["widgetId"]), r["widgetName"]);
            });

            var viewSection = PropertyBucket.GetProperty<ViewSection>(SECTION_KEY);

            foreach (var widget in viewSection.Widgets)
            {
                int actualId = (int) widget.AvailableWidgetId;
                var value = expectedWidgets[actualId];
                Assert.That(value, Is.Not.Null.Or.Empty, $"The widget with id '{widget.AvailableWidgetId}' is invalid");
                Assert.That(widget.Name == widget.Name, $"The widget name '{widget.Name}' is invalid");
            }
        }

        [Then(@"the news analytics dashboard endpoint should respond with unauthorized access")]
        public void ThenTheNewsAnalyticsDashboardEndpointShouldRespondWithUnauthorizedAccess()
        {
            var response = PropertyBucket.GetProperty<IRestResponse<ViewsViewResponse>>(ANALYTICS_DASHBOARD_READONLY);
            Assert.AreEqual(403, CCC_Infrastructure.API.Services.BaseApiService.GetNumericStatusCode(response), response.Content);
        }
    }
}
