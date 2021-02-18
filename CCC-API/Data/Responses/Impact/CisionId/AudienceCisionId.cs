
namespace CCC_API.Data.Responses.Impact.CisionId
{
    public class AudienceCisionId : CisionIdBaseImpactResponse
    {
        public CharacteristicsData[] CharacteristicsData { set; get; }
    }

    public class CharacteristicsData
    {
        public int TaxonomyId { set; get; }
        public string Label { set; get; }
        public SubCategory[] Subcategories { set; get; }
    }

    public class SubCategory
    {
        public int TaxonomyId { set; get; }
        public string Label { set; get; }
        public int Views { set; get; }
        public double Percentage { set; get; }
    }
}
