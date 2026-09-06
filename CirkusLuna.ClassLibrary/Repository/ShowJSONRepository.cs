using System.Text.Json;
using CirkusLuna.ClassLibrary.Model;

namespace CirkusLuna.ClassLibrary.Repository
{
    public class ShowJSONRepository : IShowRepository
    {
        // JSON persistence saves reservation data between sessions.
        // The file path is provided through the constructor.
        private readonly string _path;
        private List<Show> _shows;

        public ShowJSONRepository(string path)
        {
            _path = path;
            // Tjekker om JSON filen allerede eksisterer
            if (File.Exists(_path))
            {
                // City og Artist objekter genskabes automatisk fra JSON
                string json = File.ReadAllText(_path);
                _shows = JsonSerializer.Deserialize<List<Show>>(json) ?? new List<Show>();
            }
            else
            {
                // Filen eksisterer ikke endnu - opret hardcodede shows første gang
                // Artister oprettes direkte her da ShowJSONRepository ikke længere modtager IArtistRepository som parameter
                Artist artist1 = new Artist(1, "Mona", "Lisa", "mlisa@cirkusluna.dk", "Akrobat");
                Artist artist2 = new Artist(2, "Hr.", "Skæg", "skæg@cirkusluna.dk", "Klovn");
                Artist artist3 = new Artist(3, "Johnny", "Ace", "ace@cirkusluna.dk", "Strongman");
                Artist artist4 = new Artist(4, "Benny", "Bent", "bent@cirkusluna.dk", "Jonglør");
                Artist artist5 = new Artist(5, "Mette", "Munk", "munk@cirkusluna.dk", "Linedanser");

                // Byer
                City copenhagen = new City(1, "København");
                City roskilde = new City(2, "Roskilde");
                City odense = new City(3, "Odense");
                City aalborg = new City(4, "Aalborg");
                City aarhus = new City(5, "Århus");

                // Shows
                Show show1 = new Show(1, "Cirkus Luna Sjællands-Tourne", new DateOnly(2026, 7, 12), 43, 3, copenhagen);
                show1.Artists.Add(artist1);
                show1.Artists.Add(artist2);
                show1.Artists.Add(artist3);

                Show show2 = new Show(2, "Cirkus Luna Sjællands-Tourne", new DateOnly(2026, 7, 14), 15, 2, roskilde);
                show2.Artists.Add(artist1);
                show2.Artists.Add(artist2);
                show2.Artists.Add(artist4);

                Show show3 = new Show(3, "Cirkus Luna Fyn", new DateOnly(2026, 7, 22), 110, 10, odense);
                show3.Artists.Add(artist1);
                show3.Artists.Add(artist2);
                show3.Artists.Add(artist4);
                show3.Artists.Add(artist5);

                Show show4 = new Show(4, "Cirkus Luna Jyllands-Tourne", new DateOnly(2026, 8, 3), 70, 8, aalborg);
                show4.Artists.Add(artist2);
                show4.Artists.Add(artist3);
                show4.Artists.Add(artist4);

                Show show5 = new Show(5, "Cirkus Luna Jyllands-Tourne", new DateOnly(2026, 8, 6), 37, 6, aarhus);
                show5.Artists.Add(artist2);
                show5.Artists.Add(artist3);
                show5.Artists.Add(artist4);

                _shows = new List<Show> { show1, show2, show3, show4, show5 };

                SaveToFile();
            }
        }

        // Gem data til filen
        // City og Artist objekter skrives som nested objekter i JSON
        private void SaveToFile()
        {
            string json = JsonSerializer.Serialize(_shows, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_path, json);
        }

        // Returnerer alle shows
        public List<Show> GetAll()
        {
            return _shows;
        }

        // Finder og returnerer show på ID - returnerer null hvis ikke fundet
        public Show GetById(int id)
        {
            foreach (Show show in _shows)
            {
                if (show.Id == id)
                    return show;
            }
            return null;
        }

        // Søger efter shows i en bestemt by
        public List<Show> GetByCity(string cityName)
        {
            List<Show> result = new List<Show>();
            foreach (Show show in _shows)
            {
                if (show.City.Name.ToLower() == cityName.ToLower())
                    result.Add(show);
            }
            return result;
        }

        // Tilføjer nyt show og gemmer til fil
        public void Add(Show show)
        {
            _shows.Add(show);
            SaveToFile();
        }

        // Opdaterer eksisterende show og gemmer til fil
        public void Update(Show show)
        {
            for (int i = 0; i < _shows.Count; i++)
            {
                if (_shows[i].Id == show.Id)
                {
                    _shows[i].ShowName = show.ShowName;
                    _shows[i].Date = show.Date;
                    _shows[i].Seats = show.Seats;
                    _shows[i].VipSeats = show.VipSeats;
                    _shows[i].City = show.City;
                    break; // Ingen grund til at fortsætte løkken
                }
            }
            SaveToFile();
        }

        // Sletter show på ID og gemmer til fil
        public void Delete(int id)
        {
            _shows.Remove(GetById(id));
            SaveToFile();
        }
    }
}