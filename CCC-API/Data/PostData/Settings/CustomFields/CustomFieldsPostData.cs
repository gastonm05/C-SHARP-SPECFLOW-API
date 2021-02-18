using System.Collections.Generic;
using System.Linq;

namespace CCC_API.Data.PostData.Settings.CustomFields
{
    public class CustomFieldsPostData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string DefaultValue { get; set; }
        public bool MultiSelect { get; set; }
        public List<AllowValue> AllowedValues { get; set; }
        public string EntityType { get; set; }
        public string MaxLength { get; set; }

        /// <summary>
        /// Determines custom field data type.
        /// </summary>
        /// <returns></returns>
        public string EvaluateCustomFieldType()
        {
            var type = Type;
            if (Type != "String") return type;
            if (AllowedValues.Any())
            {
                return MultiSelect ? "MultiSelect" : "SingleSelect";
            }
            return type;
        }
    }
}