
namespace CCC_API.Data.Responses.Media
{
    /// <summary>
    /// Represents media list item - contact, outlet, individual etc.
    /// </summary>
    public interface IMediaListItem
    {
        string Email { get; }
        int Id { get; }
    }
}
