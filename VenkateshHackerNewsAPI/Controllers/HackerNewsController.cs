using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using VenkateshHackerNewsAPI.Interfaces;
using VenkateshHackerNewsAPI.Models;

namespace VenkateshHackerNewsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HackerNewsController : ControllerBase
    {
        private IConfiguration _configuration;
        private IHackerNews _hackerNews;
        private IMemoryCache _inMemoryCache;
        public HackerNewsController(IHackerNews hackerNews, IMemoryCache memoryCache, IConfiguration configuration)
        {
            _configuration = configuration;
            _hackerNews = hackerNews;
            _inMemoryCache = memoryCache;
        }
        /// <summary>
        /// To get hacker news stories
        /// </summary>
        /// <param name="searchValue"></param>
        /// <response code="200">Returns hacker news stories</response>
        /// <response code="500">Internal Server Error</response>
        /// <returns></returns>
        [HttpGet]
        [Route("GetHNStories")]
        public async Task<IActionResult> Get(string searchValue = "")
        {
            List<StoryDTO> HNStories = new List<StoryDTO>();
            try
            {
                string cacheKeyName = _configuration.GetSection("CacheKey").Value;
                if (!_inMemoryCache.TryGetValue(cacheKeyName, out HNStories))
                {
                    var stories = await _hackerNews.GetNewStoriesFromHackerNewsAPI();
                    if (stories.Status)
                    {
                        HNStories = ((List<StoryDTO>)stories.Stories).Take(200).ToList();
                        var cacheEntryOptions = new MemoryCacheEntryOptions()

                            .SetAbsoluteExpiration(TimeSpan.FromSeconds(1800))
                            .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                            .SetPriority(CacheItemPriority.Normal)
                            .SetSize(1024);
                        _inMemoryCache.Set(cacheKeyName, HNStories, cacheEntryOptions);
                    }

                }
                if (!string.IsNullOrEmpty(searchValue.Trim()))
                {
                    HNStories = HNStories.Where(x => x.title.ToLower().Contains(searchValue.Trim().ToLower())).ToList();
                }
            }
            catch (Exception ex)
            {

            }

            return Ok(HNStories);

        }
    }
}
