using System.Text.Json;
using CirkusLuna.ClassLibrary.Model;

namespace CirkusLuna.ClassLibrary.Repository
{
    public class ArtistJSONRepository : IArtistRepository
    {
        // JSON persistence saves reservation data between sessions.
        // The file path is provided through the constructor.
        private readonly string _path;
        private List<Artist> _artistList;

        public ArtistJSONRepository(string path)
        {
            _path = path;

            string? directory = Path.GetDirectoryName(_path);

            if (!string.IsNullOrEmpty(directory))
            {
                Directory.CreateDirectory(directory);
            }
            if (File.Exists(_path))
            {
                //Sti til JSON fil - gemmers i programmets output mappe
                string json = File.ReadAllText(_path);
                _artistList = JsonSerializer.Deserialize<List<Artist>>(json) ?? new List<Artist>();
            }
            else
            {
                //Hardcoded artister første gang filen ikke eksisterer og tilføjer dem fremover
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

        //Gemmer data til filen
        private void SaveToFile()
        {
            string json = JsonSerializer.Serialize(_artistList, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_path, json);
        }

        //Returnerer alle artister
        public List<Artist> GetAll()
        {
            return _artistList;
        }

        //Finder og returnerer artist på ID - returnerer null hvis der ikke findes nogen artist
        public Artist GetById(int id)
        {
            for (int i = 0; i < _artistList.Count; i++)
            {
                if (_artistList[i].Id == id)
                    return _artistList[i];
            }
            return null;
        }

        //Tilføjer ny artist og gemmer til fil
        public void Add(Artist artist)
        {
            _artistList.Add(artist);
            SaveToFile();
        }

        //Opdaterer eksisterende artister og gemmer til fil
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

        //Sletter artist på ID og gemmer til fil
        public void Delete(int id)
        {
            _artistList.Remove(GetById(id));
            SaveToFile();
        }
    }
}
