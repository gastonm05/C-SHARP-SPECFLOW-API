using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCC_API.Data.Responses.ACLS
{
    public class SettingOpsPermissions
    {
        public bool CanEditExpirationDate { get; set; }
        public bool CanEditPermissions { get; set; }
        public bool CanCreate { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
        public bool CanView { get; set; }

        public SettingOpsPermissions() { }
        public SettingOpsPermissions(bool canEditExpirationDate, bool canEditPermissions, bool canCreate, bool canEdit, bool canDelete)
        {
            this.CanEditExpirationDate = canEditExpirationDate;
            this.CanEditPermissions = canEditPermissions;
            this.CanCreate = canCreate;
            this.CanEdit = canEdit;
            this.CanDelete = canDelete;
        }
        public SettingOpsPermissions(bool canCreate, bool canEdit, bool canDelete)
        {
            this.CanCreate = canCreate;
            this.CanEdit = canEdit;
            this.CanDelete = canDelete;
        }
    }
}
