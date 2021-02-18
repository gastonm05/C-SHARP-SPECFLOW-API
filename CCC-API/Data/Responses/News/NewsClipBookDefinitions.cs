using System.Collections.Generic;

namespace CCC_API.Data.Responses.News
{
    public class NewsClipBookDefinitions
    {
        public Title Title { get; set; }
        public NewsIds NewsIds { get; set; }
        public Summary Summary { get; set; }
        public NewsTemplateId NewsTemplateId { get; set; }
        public SortType SortType { get; set; }
        public GroupType GroupType { get; set; }
        public Template Template { get; set; }
    }

    public class Title
    {
        public string Type { get; set; }
        public Attributes Attributes { get; set; }
    }

    public class NewsIds
    {
        public string Type { get; set; }
        public Items Items { get; set; }
    }

    public class Summary
    {
        public string Type { get; set; }
        public Attributes Attributes { get; set; }
    }

    public class NewsTemplateId
    {
        public string Type { get; set; }
    }

    public class Attributes
    {
        public bool Required { get; set; }
        public int MaxLength { get; set; }
        public List<ValidValues> ValidValues { get; set; }
    }

    public class Items
    {
        public string Type { get; set; }
    }

    public class ValidValues
    {
        public int Value { get; set; }
        public string Name { get; set; }
    }

    public class GroupType
    {
        public string Type { get; set; }
        public Attributes Attributes { get; set; }
    }

    public class SortType
    {
        public string Type { get; set; }
        public Attributes Attributes { get; set; }
    }

    public class Template
    {
        public string Type { get; set; }
        public Attributes Attributes { get; set; }
    }
}
