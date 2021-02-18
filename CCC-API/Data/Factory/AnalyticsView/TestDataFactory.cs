using CCC_API.Data.Responses.Analytics;
using System.Collections.Generic;

namespace CCC_API.Data.Factory.AnalyticsView
{
    public class TestDataFactory
    {
        public const string AUTOMATION_VIEW_NAME = "AutoViaApi";
        public const string AUTOMATION_SECTION_NAME = "AutoSectionViaApi";

        /// <summary>
        /// Convenient way to create generic view data.
        /// </summary>
        /// <param name="viewName">Name of the view.</param>
        /// <param name="widgets">The widgets.</param>
        /// <param name="sectionName">Name of the section.</param>
        /// <returns>A generic view</returns>
        public static AvailableViewSectionsWidgets GenericViewData(List<Widget> widgets = null, string viewName = AUTOMATION_VIEW_NAME, string sectionName = AUTOMATION_SECTION_NAME)
        {
            return new AvailableViewSectionsWidgets
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
                        Widgets = widgets ?? GenericWidgets()
                    }
                }
            };
        }

        /// <summary>
        /// Convenient way to create generic widgets data.
        /// </summary>
        /// <returns></returns>
        public static List<Widget> GenericWidgets()
        {
            return new List<Widget>()
                {
                    new Widget()
                    {
                        Name = "Total Mentions",
                        SortOrder = 0,
                        Size = "md",
                        AvailableWidgetId = 1,
                        WidgetOptionValues = new List<object>(),
                        Data = null,
                        StartDate = null,
                        EndDate = null,
                        SystemId = 0,
                        Notes = null
                    }
                };
        }
    }
}
