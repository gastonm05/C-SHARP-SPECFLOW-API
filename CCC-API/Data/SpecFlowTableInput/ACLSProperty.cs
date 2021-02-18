using CCC_API.Data.Responses.ACLS;

namespace CCC_API.Data.SpecFlowTableInput
{
    /// <summary>
    /// Used in ACLS feature file to create a set of permission objects from specflow table input.
    /// Also provides a way to get the permission from an ACLSView provided by the ACLS Service.
    /// </summary>
    public class ACLSProperty
    {
        public string Property { get; set; }
        public string SubProperty { get; set; }
        public string SubPropertyOther { get; set; }
        public string Permission { get; set; }
        public string Value { get; set; }

        /// <summary>
        /// Returns a string value for a given ACLS permission from a Settings section
        /// </summary>
        /// <param name="aclsView">ACLS View from the ACLS service response.Data</param>
        /// <param name="section">Section to retrieve the ACLS permission from</param>
        /// <returns>string value of the permission</returns>
        public string GetValue(ACLSView aclsView, string section)
        {
            object permissionObject = aclsView.GetType().GetProperty(section).GetValue(aclsView, null);
            // figure out which permission object we need
            if (!string.IsNullOrEmpty(Property))
            {
                var p = permissionObject.GetType().GetProperty(Property).GetValue(permissionObject, null); // property object
                if (string.IsNullOrEmpty(SubProperty))
                {
                    // the permission is on the property or the subpropertyOther
                    permissionObject = string.IsNullOrEmpty(SubPropertyOther) ?
                        p :
                        permissionObject = p.GetType().GetProperty(SubPropertyOther).GetValue(p, null);
                }
                else
                {
                    var sp = p.GetType().GetProperty(SubProperty).GetValue(p, null); // subproperty object
                    // the permission is on the subproperty or the subpropertyOther
                    permissionObject = string.IsNullOrEmpty(SubPropertyOther) ?
                        sp :
                        permissionObject = sp.GetType().GetProperty(SubPropertyOther).GetValue(sp, null);
                }
            }
            return permissionObject.GetType().GetProperty(Permission).GetValue(permissionObject, null).ToString();
        }
    }
}
