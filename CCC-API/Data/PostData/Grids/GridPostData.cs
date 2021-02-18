using CCC_API.Data.Responses.Grid;
using System;


namespace CCC_API.Data.PostData.Grids
{
    public class GridPostData
    {
        public string gridTemplateId { get; set; }
        public Column[] columns { get; set; }
        
        public GridPostData() { }

        public GridPostData(string gridTemplateId, Column[] columns)
        {
            this.gridTemplateId = gridTemplateId;
            this.columns = columns;
        }
    }
}
