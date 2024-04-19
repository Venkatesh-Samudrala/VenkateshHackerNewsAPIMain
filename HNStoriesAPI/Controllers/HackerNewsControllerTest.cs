
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Moq;
using VenkateshHackerNewsAPI.Controllers;
using VenkateshHackerNewsAPI.Interfaces;
using VenkateshHackerNewsAPI.Models;

namespace HackerNews.Tests.Controllers
{
    public class HackerNewsControllerTest
    {
        IConfiguration configuration = null;
        public HackerNewsControllerTest()
        {
            var inMemorySettings = new Dictionary<string, string> {
                                        {"HackerNewsBaseAPIURL", "https://hacker-news.firebaseio.com/"},
                                        {"HackerNewsVersion", "v0"},
                                        {"CacheKey","CacheHNStories" }
};

            configuration = new ConfigurationBuilder()
               .AddInMemoryCollection(inMemorySettings)
               .Build();
        }

        [Fact]
        public async void HackerNewsController_GetNewestStories_ZeroReturn()
        {

            Mock<IHackerNews> _mockHackerNews = new Mock<IHackerNews>();
            var mockMemoryCache = new Mock<IMemoryCache>();
            mockMemoryCache
                            .Setup(x => x.CreateEntry(It.IsAny<object>()))
                            .Returns(Mock.Of<ICacheEntry>);

            HackerNewsController _hackerNewsController = new HackerNewsController(_mockHackerNews.Object, mockMemoryCache.Object, configuration);
            var emptyList = new List<StoryDTO>();
            _mockHackerNews.Setup(x => x.GetNewStoriesFromHackerNewsAPI()).ReturnsAsync(new HNResponse() { Status = true, Stories = emptyList });

            var stories = await _hackerNewsController.Get();
            emptyList = (stories as OkObjectResult).Value as List<StoryDTO>;

            Assert.NotNull(emptyList);

        }

        [Fact]
        public async void HackerNewsController_GetNewestStories_SingleList()
        {
            //Mock<IConfiguration> _configuration = new Mock<IConfiguration>();
            Mock<IHackerNews> _mockHackerNews = new Mock<IHackerNews>();
            var mockMemoryCache = new Mock<IMemoryCache>();
            mockMemoryCache
                            .Setup(x => x.CreateEntry(It.IsAny<object>()))
                            .Returns(Mock.Of<ICacheEntry>);
            HackerNewsController _hackerNewsController = new HackerNewsController(_mockHackerNews.Object, mockMemoryCache.Object, configuration);
            var emptyList = new List<StoryDTO>();
            emptyList.Add(new StoryDTO());
            _mockHackerNews.Setup(x => x.GetNewStoriesFromHackerNewsAPI()).ReturnsAsync(new HNResponse() { Status = true, Stories = emptyList });

            var stories = await _hackerNewsController.Get();
            var actual = (stories as OkObjectResult).Value as List<StoryDTO>;

            Assert.NotNull(actual);
            Assert.Equal(1, actual.Count);

        }

    }
}
