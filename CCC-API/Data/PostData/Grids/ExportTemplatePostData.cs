using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCC_API.Data.PostData.Grids
{
    public class ExportTemplatePostData
    {
        public int exportTemplateId { get; set; }
        public string name { get; set; }
        public int exportType { get; set; }
        public int companyId { get; set; }
        public int dataGroupId { get; set; }
        public string[] columns { get; set; }

        public ExportTemplatePostData() { }

        public ExportTemplatePostData(int exportTemplateId, string name, int exportType, int companyID, int datagroupId, string[] columns)
        {
            this.exportTemplateId = exportTemplateId;
            this.name = name;
            this.exportType = exportType;
            this.companyId = companyId;
            this.dataGroupId = dataGroupId;
            this.columns = columns;
        }
    }
}
