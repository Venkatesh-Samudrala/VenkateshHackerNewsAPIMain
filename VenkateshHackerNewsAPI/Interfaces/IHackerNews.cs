using VenkateshHackerNewsAPI.Models;

namespace VenkateshHackerNewsAPI.Interfaces
{
    public interface IHackerNews
    {
        Task<HNResponse> GetNewStoriesFromHackerNewsAPI();
        Task<StoryDTO> GetStoryByID(int id);

    }
}
