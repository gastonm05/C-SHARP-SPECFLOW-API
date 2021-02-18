namespace CCC_API.Data
{
    public class BulkPatchData
    {
        public string Key { get; set; }
        public bool SelectAll { get; set; }
        public PatchData[] Patch { get; set; }

        public BulkPatchData(string key, bool selectAll, PatchData[] arrayPatchData)
        {
            Key = key;
            SelectAll = selectAll;
            Patch = arrayPatchData;
        }
    }

    public class BulkPatchDataDelta
    {
        public int[] Delta { get; set; }
        public string Key { get; set; }
        public PatchData[] Patch { get; set; }
        public bool SelectAll { get; set; }    
        
        public BulkPatchDataDelta(string key, bool selectAll, PatchData[] patchData, int[] delta)
        {
            Key = key;
            SelectAll = selectAll;
            Patch = patchData;
            Delta = delta;
        }
    }
}
