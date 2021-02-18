using CCC_API.Data.PostData.Analytics;
using CCC_API.Data.Responses.Analytics;
using CCC_API.Data.Responses.Analytics.Available;
using CCC_Infrastructure.Utils;
using Newtonsoft.Json;
using RestSharp;
using SimpleJson;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace CCC_API.Services.Analytics
{
    public class DashboardsService : AuthApiService
    {
        public const string AnalyticsAvailableWidgetsEndpoint = "analytics/dashboards/availablewidgets";
        public const string AnalyticsLastViewIdEndPoint = "analytics/dashboards/lastviewid";
        public const string AnalyticsViewEndPoint = "analytics/dashboards/view";
        public const string AnalyticsViewsEndPoint = "analytics/dashboards/views";
        public const string AnalyticsSectionTemplateEndpoint = "analytics/dashboard/section/template";
        public const string AnalyticsWidgetTemplateEndpoint = "analytics/dashboard/widget/template";
        public const string AnalyticsSectionEndpoint = "analytics/dashboard/section";

        public DashboardsService(string sessionKey) : base(sessionKey){ }

        public enum AreaId
        {
            Analytics = 1,
            Social = 2
        }

        /// <summary>
        /// GETs the list of available widget configuration available for a given analytics area
        /// </summary>
        /// <param name="areaId">The area identifier.</param>
        /// <returns>
        /// List of AvailableWidget
        /// </returns>
        public List<AvailableWidget> GetAvailableWidgets(AreaId areaId)
            => Request().ToEndPoint(AnalyticsAvailableWidgetsEndpoint).AddUrlQueryParam("AreaId", ((int)areaId).ToString())
                .Get().ExecContentCheck<List<AvailableWidget>>();

        /// <summary>
        /// Sends GET request to the analytics/dashboards/views endpoint.
        /// </summary>
        /// <returns>IRestResponse</returns>
        public IRestResponse<List<ViewsViewResponse>> GetViews()
        {
            return Get<List<ViewsViewResponse>>(AnalyticsViewsEndPoint);
        }

        /// <summary>
        /// Provides all views for a company from analytics/dashboards/views endpoint.
        /// </summary>
        /// <returns></returns>
        public List<ViewsViewResponse> GetAllViews()
            => Request().ToEndPoint(AnalyticsViewsEndPoint).ExecCheck<List<ViewsViewResponse>>();

        /// <summary>
        /// Provides details information about a view by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AvailableViewSectionsWidgets GetView(int id)
            => Request().ToEndPoint($"{AnalyticsViewEndPoint}/{id}")
                .Get().ExecCheck<AvailableViewSectionsWidgets>();

        /// <summary>
        /// Edits view with some data.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public AvailableViewSectionsWidgets PutView(AvailableViewSectionsWidgets data)
            => Request().ToEndPoint(AnalyticsViewEndPoint).Put().Data(data).ExecCheck<AvailableViewSectionsWidgets>();

        /// <summary>
        /// Adds a comment to a widget on an analytics view.
        /// </summary>
        /// <param name="widgetId"></param>
        /// <param name="note"></param>
        /// <param name="searchId"></param>
        /// <returns></returns>
        public IRestResponse PutNote(int widgetId, string note, int searchId = 0)
        {
            var payload = new JsonObject {{"searchId", searchId}, {"notes", note}};
            return Request().ToEndPoint($"{AnalyticsViewEndPoint}/widget/{widgetId}/note").Put().Data(payload).ExecCheck();
        }

        /// <summary>
        /// Creates a view with specified data settings.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public AvailableViewSectionsWidgets PostView(AvailableViewSectionsWidgets data)
            => Request().Post().ToEndPoint(AnalyticsViewEndPoint).Data(data).ExecCheck<AvailableViewSectionsWidgets>();

        /// <summary>
        /// Deletes a view.
        /// </summary>
        /// <param name="viewId"></param>
        /// <returns></returns>
        public IRestResponse DeleteView(int viewId)
            => Request().Delete()
                .ToEndPoint($"{AnalyticsViewEndPoint}/{viewId}")
                .ExecCheck();

        /// <summary>
        /// Creates a view with section.
        /// </summary>
        /// <param name="viewName"></param>
        /// <param name="sectionName"></param>
        /// <returns></returns>
        public AvailableViewSectionsWidgets PostNewView(string viewName, string sectionName = "Auto")
        {
            var data = new AvailableViewSectionsWidgets
            {
                AccessMode = 1,
                IsEditable = true,
                IsOwner = true,
                IsSystem = false,
                Name = viewName,
                Sections = new List<ViewSection>
                {
                    new ViewSection
                    {
                        Name = sectionName,
                        SortOrder = 0,
                        Widgets = new List<Widget>()
                    }
                }
            };
            return PostView(data);
        }

        /// <summary>
        /// GETs the list of sections template for a given analytics area
        /// </summary>
        /// <param name="areaId">The area identifier.</param>
        /// <returns>
        /// List of Sections of template
        /// </returns>
        public List<SectionTemplate> GetSectionTemplate(AreaId areaId)
            => Request().ToEndPoint($"{AnalyticsSectionTemplateEndpoint}/{areaId.GetHashCode()}").Get().ExecContentCheck<List<SectionTemplate>>();


        /// <summary>
        /// GETs the list of widget template for a given analytics area
        /// </summary>
        /// <param name="areaId">The area identifier.</param>
        /// <returns>
        /// List of Widgets of template
        /// </returns>
        public List<WidgetTemplate> GetWidgetTemplate(AreaId areaId)
            => Request().ToEndPoint($"{AnalyticsWidgetTemplateEndpoint}/{areaId.GetHashCode()}").Get().ExecContentCheck<List<WidgetTemplate>>();

        /// <summary>
        /// Creates a view with section and widgets.
        /// </summary>
        /// <param name="viewId"></param>
        /// <param name="widget"></param>
        /// <returns></returns>
        public SectionTemplate PostSectionWithWidget(int viewId, Dictionary<int, string> widgets, string sectionName)
        {
            var widgetId = new List<int>();
            var widgetName = new List<string>();

            var sectionBodyRequest = TestData.DeserializedJson<SectionBody>("AnalyticsSectionBody.json", Assembly.GetExecutingAssembly());
            sectionBodyRequest.ViewId = viewId;
            sectionBodyRequest.Name = sectionName;

            foreach (KeyValuePair<int, string> widget in widgets)
            {
                widgetId.Add(widget.Key);
                widgetName.Add(widget.Value);
            }

            for (int index = 0; index < sectionBodyRequest.Widgets.Count; index++)
            {
                sectionBodyRequest.Widgets[index].AvailableWidgetId = widgetId[index];
                sectionBodyRequest.Widgets[index].Name = widgetName[index];
            }

            return Request().Post().ToEndPoint(AnalyticsSectionEndpoint).Data(sectionBodyRequest).ExecCheck<SectionTemplate>();
        }

        /// <summary>
        /// Sends a POST to create a new Analytics Dashboard/View from a readonly user session
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IRestResponse<ViewsViewResponse> PostDashboardReadOnlyUser(string name)
        {
            var postData = new ViewsViewResponse()
            {
                Name = name,
                AccessMode = 1,
                Id = 0,
                IsEditable = true,
                IsOwner = true,
                IsSystem = false
            };

            return Post<ViewsViewResponse>(AnalyticsViewEndPoint, GetAuthorizationHeader(), postData);
        }   
    }
}
