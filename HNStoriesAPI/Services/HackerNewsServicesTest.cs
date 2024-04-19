using HackerNews.Tests.ApiMockData;
using HackerNews.Tests.helper;
using Microsoft.Extensions.Configuration;
using VenkateshHackerNewsAPI.Models;
using VenkateshHackerNewsAPI.Services;

namespace HackerNews.Tests.Services
{
    public class HackerNewsServicesTest
    {
        private IConfiguration _configuration;
        public HackerNewsServicesTest()
        {
            var inMemorySettings = new Dictionary<string, string> {
                                        {"HackerNewsBaseAPIURL", "https://hacker-news.firebaseio.com/"},
                                        {"HackerNewsVersion", "v0"},
                                        {"CacheKey","CacheHNStories" }
            };

            _configuration = new ConfigurationBuilder()
               .AddInMemoryCollection(inMemorySettings)
               .Build();
        }


        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task HackerNewsServices_GetStoryByID_Result(int id)
        {
            var mockData = StoriesDetails.Get();
            var mockHandler = HttpClientHelper.GetResults<StoryDTO>(mockData);
            var httpClient = new HttpClient(mockHandler.Object);
            var hackerNewsServices = new HackerNewsService(httpClient, _configuration);

            var result = await hackerNewsServices.GetStoryByID(id);
            var expectedValue = new StoryDTO()
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
            Assert.NotNull(result);
            Assert.Equivalent(expectedValue, result);

        }

        [Fact]
        public async Task HackerNewsServices_GetNewestStories_Result()
        {
            var _httpClient = new HttpClient();
            var hackerNewsServices = new HackerNewsService(_httpClient, _configuration);
            var actualResult = await hackerNewsServices.GetNewStoriesFromHackerNewsAPI();


            Assert.NotNull(actualResult);
            Assert.NotEmpty((List<StoryDTO>)actualResult.Stories);
            Assert.True(((List<StoryDTO>)actualResult.Stories).Count > 0, "The actualResult was not greater than 0");

        }
    }
}
