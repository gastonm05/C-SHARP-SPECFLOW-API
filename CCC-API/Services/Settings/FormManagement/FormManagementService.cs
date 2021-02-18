using CCC_API.Data.Responses.Settings.FormManagement;
using CCC_API.Services.Common;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;

namespace CCC_API.Services.Settings.FormManagement
{
    public class FormManagementService : AuthApiService
    {
        public const string FORMS_ACTIVITIES_URI = "management/activitytypes";

        public FormManagementService(string sessionKey) : base(sessionKey) { }

        public FormsActivity GetFormsActivities(int id = 0) =>
            Request().ToEndPoint(FORMS_ACTIVITIES_URI + "/" + id).ExecCheck<FormsActivity>();

        /// <summary>
        /// Creates new form. 
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        public FormsActivity PutNewForm(FormsActivity payload)
        {
            return Request().ToEndPoint(FORMS_ACTIVITIES_URI).Put().Data(payload).ExecCheck<FormsActivity>();
        }

        /// <summary>
        /// Gets activities forms.
        /// </summary>
        /// <returns></returns>
        public ListFormsActivities GetActivitiesTypes()
        {
            return Request().ToEndPoint(FORMS_ACTIVITIES_URI).ExecCheck<ListFormsActivities>();
        }

        /// <summary>
        /// Deletes activity form.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IRestResponse DeleteFormsActivities(int id)
        {
            return Request().Delete().ToEndPoint(FORMS_ACTIVITIES_URI + "/" + id).Exec();
        }

        /// <summary>
        /// Creates new form if not present.
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="color"></param>
        /// <param name="icon"></param>
        /// <returns>FormsActivity</returns>
        public FormsActivity CreateIfNotPresent(string typeName, string color, string icon)
        {
            var forms = GetActivitiesTypes();
            var form = forms.Items.FirstOrDefault(item => item.Name == typeName);
            if (form == null)
            {
                var existingFields = GetFormsActivities();
                var newField = new FormsActivity
                {
                    Color = color,
                    Icon = icon,
                    Fields = existingFields.Fields,
                    Id = 0,
                    Name = typeName,
                    _meta = existingFields._meta
                };

                return PutNewForm(newField);
            }
            return form;
        }

        /// <summary>
        /// Disable enable fields
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="tbl"></param>
        /// <returns></returns>
        public List<Field> UpdateFields(List<Field> fields, Table tbl, bool isEnabled = false)
        {
            foreach (var item in fields)
            {
                if (tbl.Rows.Any(r => item._meta.Label.Equals(r["Fields"])))
                {
                    item.IsEnabled = isEnabled;
                }

            }
            return fields;
        }
    }
}
