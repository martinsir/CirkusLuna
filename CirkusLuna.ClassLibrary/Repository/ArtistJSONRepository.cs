using CirkusLuna.ClassLibrary.Model;
using System.Text.Json;

namespace CirkusLuna.ClassLibrary.Repository
{
    public class ArtistJSONRepository : IArtistRepository
    {
        // JSON persistence saves artist data between sessions.
        // The file path is provided through the constructor.
        private readonly string _path;

        private List<Artist> _artistList;

        public ArtistJSONRepository(string path)
        {
            _path = path;

            // Make sure the directory for the JSON file exists.
            string? directory = Path.GetDirectoryName(_path);

            if (!string.IsNullOrEmpty(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // Load existing JSON data if the file already exists.
            if (File.Exists(_path))
            {
                string json = File.ReadAllText(_path);

                _artistList =
                    JsonSerializer.Deserialize<List<Artist>>(json)
                    ?? new List<Artist>();
            }
            else
            {
                // Create default artists the first time the JSON file is created.
                _artistList = new List<Artist>
                {
                    new Artist(1, "Mona", "Lisa", "mlisa@cirkusluna.dk", "Akrobat"),
                    new Artist(2, "Hr.", "Skæg", "skæg@cirkusluna.dk", "Klovn"),
                    new Artist(3, "Johnny", "Ace", "ace@cirkusluna.dk", "Strongman"),
                    new Artist(4, "Benny", "Bent", "bent@cirkusluna.dk", "Jonglør"),
                    new Artist(5, "Mette", "Munk", "munk@cirkusluna.dk", "Linedanser")
                };

                SaveToFile();
            }
        }

        // Save artist data to the JSON file.
        private void SaveToFile()
        {
            string json = JsonSerializer.Serialize(
                _artistList,
                new JsonSerializerOptions { WriteIndented = true }
            );

            File.WriteAllText(_path, json);
        }

        // Return all artists.
        public List<Artist> GetAll()
        {
            return _artistList;
        }

        // Find an artist by ID. Returns null if no artist is found.
        public Artist GetById(int id)
        {
            for (int i = 0; i < _artistList.Count; i++)
            {
                if (_artistList[i].Id == id)
                    return _artistList[i];
            }

            return null;
        }

        // Add a new artist and save the changes.
        public void Add(Artist artist)
        {
            _artistList.Add(artist);
            SaveToFile();
        }

        // Update an existing artist and save the changes.
        public void Update(Artist artist)
        {
            for (int i = 0; i < _artistList.Count; i++)
            {
                if (_artistList[i].Id == artist.Id)
                {
                    _artistList[i].FirstName = artist.FirstName;
                    _artistList[i].LastName = artist.LastName;
                    _artistList[i].Email = artist.Email;
                    _artistList[i].Act = artist.Act;
                    break;
                }
            }

            SaveToFile();
        }

        // Delete an artist by ID and save the changes.
        public void Delete(int id)
        {
            _artistList.Remove(GetById(id));
            SaveToFile();
        }
    }
}