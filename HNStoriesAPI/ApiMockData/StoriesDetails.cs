using VenkateshHackerNewsAPI.Models;

namespace HackerNews.Tests.ApiMockData
{
    public static class StoriesDetails
    {
        public static List<int> Stories = new List<int>() { 1 };

        public static StoryDTO StoriesDetailsDto { get; set; }

        public static StoryDTO Get()
        {
            return new StoryDTO()
            {
                id = 1,
                score = 1,
                by = "stevefan1999",
                time = 1704439734,
                title = "Microsoft Announces AppCAT: Simplifying Azure Migration for .NET Apps",
                type = "story",
                url = "https://www.infoq.com/news/2024/01/appcat-azure-dotnet/",
                descendants = 0
            };
        }

        public static HNResponse GetStories()
        {
            return new HNResponse()
            {
                Status = true,
                Stories = new List<StoryDTO>(){ new StoryDTO()
            {
                id = 1,
                score = 1,
                by = "stevefan1999",
                time = 1704439734,
                title = "Microsoft Announces AppCAT: Simplifying Azure Migration for .NET Apps",
                type = "story",
                url = "https://www.infoq.com/news/2024/01/appcat-azure-dotnet/",
                descendants = 0
            }
            }

            };
        }
    }
}
