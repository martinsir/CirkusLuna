namespace CirkusLuna.ClassLibrary.Model
{
    public class NewsPost
    {
        //NewsPost Properties
        public int NewsPostId { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime PublishedDateTime { get; set; }

        //Constructor
        public NewsPost(int newsPostId, string title, string content, DateTime publishedDateTime)
        {
            NewsPostId = newsPostId;
            Title = title;
            Content = content;
            PublishedDateTime = publishedDateTime;
        }
    }
}