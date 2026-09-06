using CirkusLuna.ClassLibrary.Model;
using System.Text.Json;

namespace CirkusLuna.ClassLibrary.Repository
{
    public class NewsPostJSONRepository : INewsPostRepository
    {
        // JSON persistence saves news data between sessions.
        // The file path is provided through the constructor.
        private readonly string _path;

        private List<NewsPost> _newsposts;

        public NewsPostJSONRepository(string path)
        {
            _path = path;

            // Load existing JSON data if the file already exists.
            if (File.Exists(_path))
            {
                string json = File.ReadAllText(_path);

                _newsposts =
                    JsonSerializer.Deserialize<List<NewsPost>>(json)
                    ?? new List<NewsPost>();
            }
            else
            {
                // Create default news posts the first time the JSON file is created.
                _newsposts = new List<NewsPost>
                {
                    new NewsPost(
                        1,
                        "Ny Elefant!",
                        "Vi har fået en ny elefant....",
                        new DateTime(2026, 5, 1)
                    ),

                    new NewsPost(
                        2,
                        "10 års Jubilæum",
                        "Benny Blæk har 10 års jubilæum, det fejrer vi med...",
                        new DateTime(2026, 6, 1)
                    ),

                    new NewsPost(
                        3,
                        "Sæsonen 3 starter!",
                        "Vi er klar til en ny sæson...",
                        new DateTime(2026, 7, 1)
                    ),

                    new NewsPost(
                        4,
                        "Ny Stjerne!",
                        "Kom og oplev vores nyeste artist...",
                        new DateTime(2026, 8, 1)
                    )
                };

                SaveToFile();
            }
        }

        // Save news data to the JSON file.
        private void SaveToFile()
        {
            string json = JsonSerializer.Serialize(
                _newsposts,
                new JsonSerializerOptions { WriteIndented = true }
            );

            File.WriteAllText(_path, json);
        }

        // Return all news posts.
        public List<NewsPost> GetAll()
        {
            return _newsposts;
        }

        // Find a news post by ID. Returns null if no post is found.
        public NewsPost GetById(int id)
        {
            for (int i = 0; i < _newsposts.Count; i++)
            {
                if (_newsposts[i].NewsPostId == id)
                    return _newsposts[i];
            }

            return null;
        }

        // Find news posts containing the search text in the title.
        public List<NewsPost> GetByTitle(string title)
        {
            List<NewsPost> result = new List<NewsPost>();

            for (int i = 0; i < _newsposts.Count; i++)
            {
                if (_newsposts[i].Title.ToLower().Contains(title.ToLower()))
                    result.Add(_newsposts[i]);
            }

            return result;
        }

        // Find news posts by publication date.
        public List<NewsPost> GetByPublishedDate(DateTime dateTime)
        {
            List<NewsPost> result = new List<NewsPost>();

            for (int i = 0; i < _newsposts.Count; i++)
            {
                if (_newsposts[i].PublishedDateTime == dateTime)
                    result.Add(_newsposts[i]);
            }

            return result;
        }

        // Add a new news post and save the changes.
        public void Add(NewsPost newsPost)
        {
            _newsposts.Add(newsPost);
            SaveToFile();
        }

        // Update an existing news post and save the changes.
        public void Update(NewsPost newsPost)
        {
            for (int i = 0; i < _newsposts.Count; i++)
            {
                if (_newsposts[i].NewsPostId == newsPost.NewsPostId)
                {
                    _newsposts[i].Title = newsPost.Title;
                    _newsposts[i].Content = newsPost.Content;
                    _newsposts[i].PublishedDateTime = newsPost.PublishedDateTime;
                    break;
                }
            }

            SaveToFile();
        }

        // Delete a news post by ID and save the changes.
        public void Delete(int id)
        {
            _newsposts.Remove(GetById(id));
            SaveToFile();
        }
    }
}