namespace VenkateshHackerNewsAPI.Models
{
    public class StoryDTO
    {
        /// <summary>
        /// Shows Story By
        /// </summary>
        public string by { get; set; }
        /// <summary>
        /// Shows Story descendants
        /// </summary>
        public int descendants { get; set; }
        /// <summary>
        /// shows story Id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// show story Kids
        /// </summary>
        public List<int> kids { get; set; }
        /// <summary>
        /// Show story Score
        /// </summary>
        public int score { get; set; }
        /// <summary>
        /// show story Time
        /// </summary>
        public int time { get; set; }
        /// <summary>
        /// shows story Title
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// shows story type
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// shows story URL
        /// </summary>
        public string url { get; set; }

    }
}
