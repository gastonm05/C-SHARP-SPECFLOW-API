using System;
using System.Collections.Generic;

namespace CCC_API.Data.Responses.Analytics
{
    public struct TableColumn
    {
        public string Key { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
    }

    public struct ServerRenderConfig
    {
        public int EndpointId { get; set; }
        public string EndpointParams { get; set; }
        public string HighchartsConfig { get; set; }
        public bool IsCurrencyBased { get; set; }
        public List<TableColumn> TableColumns { get; set; }
    }

    public struct WidgetConfig
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TypeId { get; set; }
        public ServerRenderConfig ServerRenderConfig { get; set; }
    }

    public struct Section
    {
        public string Name { get; set; }
        public List<Widget> Widgets { get; set; }
    }

    public struct SharedServerSideRenderingConfig
    {
        public string CurrencySymbol { get; set; }
        public List<string> NumericSymbols { get; set; }
        public string AngularLocaleConfig { get; set; }
        public string NoDataMessage { get; set; }
        public string CategoryUnavailableMessage { get; set; }
    }

    public struct SharedEmailConfig
    {
        public string From { get; set; }
        public string Subject { get; set; }
        public List<string> Recipients { get; set; }
        public string Message { get; set; }
        public List<Section> Sections { get; set; }
        public bool IncludeLink { get; set; }
        public SharedServerSideRenderingConfig SharedServerSideRenderingConfig { get; set; }
    }

    public struct Schedule
    {
        public int DaysOfWeek { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
        public DateTime? EndDate { get; set; }
    }

    public struct ShareRecurringEmail
    {
        public SharedEmailConfig ReportData { get; set; }
        public Schedule Schedule { get; set; }
    }
}
