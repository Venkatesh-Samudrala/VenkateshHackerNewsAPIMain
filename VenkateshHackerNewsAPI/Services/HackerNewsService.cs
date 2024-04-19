using Newtonsoft.Json;
using VenkateshHackerNewsAPI.Interfaces;
using VenkateshHackerNewsAPI.Models;

namespace VenkateshHackerNewsAPI.Services
{
    public class HackerNewsService : IHackerNews
    {
        private HttpClient _httpClient;
        private IConfiguration _configuration;

        public HackerNewsService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<HNResponse> GetNewStoriesFromHackerNewsAPI()
        {
            HNResponse response = new HNResponse();

            try
            {
                var newestStories = await GetNewestStories();
                if (newestStories != null)
                {
                    List<StoryDTO> stories = new List<StoryDTO>();
                    var task = ((List<int>)(newestStories)).Select(GetStoryByID);
                    stories = (await Task.WhenAll(task)).ToList();
                    //remove those stories which title or URL have null value
                    stories = stories.Where(x => x.url != null && x.url != "" && x.title != null).ToList();
                    if (stories.Count > 0)
                    {
                        response.Status = true;
                        response.Stories = stories;
                    }
                    else
                    {
                        response.Status = false;
                        response.Message = "Not get any hacker news story details";
                    }
                }

            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;

            }


            return response;
        }

        public async Task<StoryDTO> GetStoryByID(int id)
        {
            string URL = _configuration.GetSection("HackerNewsBaseAPIURL").Value + "/" + _configuration.GetSection("HackerNewsVersion").Value;
            StoryDTO story = new StoryDTO();
            try
            {
                var response = await _httpClient.GetAsync(string.Format(URL + "/item/{0}.json?print=pretty", id));
                if (response.IsSuccessStatusCode)
                {
                    var storiesRes = response.Content.ReadAsStringAsync().Result;
                    story = JsonConvert.DeserializeObject<StoryDTO>(storiesRes);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return story;
        }

        private async Task<List<int>> GetNewestStories()
        {
            List<int> stories = new List<int>();
            try
            {

                string URL = _configuration.GetSection("HackerNewsBaseAPIURL").Value + "/" + _configuration.GetSection("HackerNewsVersion").Value;
                var response = await _httpClient.GetAsync(URL + "/newstories.json?print=pretty");
                if (response.IsSuccessStatusCode)
                {
                    var storiesResponse = response.Content.ReadAsStringAsync().Result;
                    stories = JsonConvert.DeserializeObject<List<int>>(storiesResponse);

                }
                else
                {
                    throw new Exception("Problem with Processing Request: " + JsonConvert.SerializeObject(response));
                }
            }
            catch (Exception)
            {
                throw;
            }

            return stories;
        }








    }
}
