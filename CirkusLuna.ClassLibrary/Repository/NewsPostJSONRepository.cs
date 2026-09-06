using System.Text.Json;
using CirkusLuna.ClassLibrary.Model;

namespace CirkusLuna.ClassLibrary.Repository
{
    public class NewsPostJSONRepository : INewsPostRepository
    {

        // JSON persistence saves reservation data between sessions.
        // The file path is provided through the constructor.
        private readonly string _path;
        private List<NewsPost> _newsposts;

        public NewsPostJSONRepository(string path)
        {
            _path = path;
            if (File.Exists(_path))
            {
                string json = File.ReadAllText(_path);
                _newsposts = JsonSerializer.Deserialize<List<NewsPost>>(json) ?? new List<NewsPost>();
            }
            else
            {
                // Filen eksisterer ikke endnu - opret hardcodede nyhedsposter første gang
                _newsposts = new List<NewsPost>
                {
                    new NewsPost(1, "Ny Elefant!", "Vi har fået en ny elefant....", new DateTime(2026, 5, 1)),
                    new NewsPost(2, "10 års Jubilæum", "Benny Blæk har 10 års jubilæum, det fejrer vi med...", new DateTime(2026, 6, 1)),
                    new NewsPost(3, "Sæsonen 3 starter!", "Vi er klar til en ny sæson...", new DateTime(2026, 7, 1)),
                    new NewsPost(4, "Ny Stjerne!", "Kom og oplev vores nyeste artist...", new DateTime(2026, 8, 1))
                };
                SaveToFile();
            }
        }

        //Gemmer data til fil
        private void SaveToFile()
        {
            string json = JsonSerializer.Serialize(_newsposts, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_path, json);
        }

        // Returnerer alle nyhedsposter
        public List<NewsPost> GetAll()
        {
            return _newsposts;
        }

        // Finder og returnerer nyhedspost på ID - returnerer null hvis ikke fundet
        public NewsPost GetById(int id)
        {
            for (int i = 0; i < _newsposts.Count; i++)
            {
                if (_newsposts[i].NewsPostId == id)
                    return _newsposts[i];
            }
            return null;
        }

        // Søger efter nyhedsposter der indeholder søgeordet i titlen
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

        // Finder nyhedsposter på dato
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

        // Tilføjer ny nyhedspost og gemmer til fil
        public void Add(NewsPost newsPost)
        {
            _newsposts.Add(newsPost);
            SaveToFile();
        }

        // Opdaterer eksisterende nyhedspost og gemmer til fil
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

        // Sletter nyhedspost på ID og gemmer til fil
        public void Delete(int id)
        {
            _newsposts.Remove(GetById(id));
            SaveToFile();
        }
    }
}