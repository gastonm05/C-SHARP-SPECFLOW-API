
namespace CCC_API.Data.Responses.Messages
{
    public class Counts
    {
        public int Pins { get; set; }
        public int Collaborators { get; set; }
        public int Followers { get; set; }
    }

    public class __invalid_type__60x60
    {
        public string Url { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }

    public class Image
    {
        public __invalid_type__60x60 __invalid_name__60x60 { get; set; }
    }

    public class PinterestBoardResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public string CreatedAt { get; set; }
        public Counts Counts { get; set; }
        public Image Image { get; set; }
    }
}
