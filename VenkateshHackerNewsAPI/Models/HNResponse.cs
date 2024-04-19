namespace VenkateshHackerNewsAPI.Models
{
    public class HNResponse
    {
        /// <summary>
        /// Shows Response Status 
        /// </summary>
        public bool Status { get; set; }
        /// <summary>
        /// Gets Response Message
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Gets stories 
        /// </summary>
        public List<StoryDTO>? Stories { get; set; }
    }
}
