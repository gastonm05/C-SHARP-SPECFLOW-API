namespace CCC_API.Data.Responses.News
{
    class SingleTemplate
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Layout { get; set; }
        public string Svg { get; set; }
        public Template_Meta _meta { get; set; }
        public Template_Links _links { get; set; }
        public bool? isPromo { get; set; }
        public string promoLink { get; set; }
    }


}
