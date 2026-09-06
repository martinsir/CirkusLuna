using CirkusLuna.ClassLibrary.Model;
using System.Text.Json;

namespace CirkusLuna.ClassLibrary.Repository
{
    public class ShowJSONRepository : IShowRepository
    {
        // JSON persistence saves show data between sessions.
        // The file path is provided through the constructor.
        private readonly string _path;

        private List<Show> _shows;

        public ShowJSONRepository(string path)
        {
            _path = path;

            // Load existing JSON data if the file already exists.
            if (File.Exists(_path))
            {
                // City and Artist objects are reconstructed from the JSON data.
                string json = File.ReadAllText(_path);
                _shows = JsonSerializer.Deserialize<List<Show>>(json) ?? new List<Show>();
            }
            else
            {
                // Create default data the first time the JSON file is created.

                Artist artist1 = new Artist(1, "Mona", "Lisa", "mlisa@cirkusluna.dk", "Akrobat");
                Artist artist2 = new Artist(2, "Hr.", "Skæg", "skæg@cirkusluna.dk", "Klovn");
                Artist artist3 = new Artist(3, "Johnny", "Ace", "ace@cirkusluna.dk", "Strongman");
                Artist artist4 = new Artist(4, "Benny", "Bent", "bent@cirkusluna.dk", "Jonglør");
                Artist artist5 = new Artist(5, "Mette", "Munk", "munk@cirkusluna.dk", "Linedanser");

                // Cities
                City copenhagen = new City(1, "København");
                City roskilde = new City(2, "Roskilde");
                City odense = new City(3, "Odense");
                City aalborg = new City(4, "Aalborg");
                City aarhus = new City(5, "Århus");

                // Shows

                // Shows

                // Past shows kept to demonstrate expired events.
                Show show1 = new Show(
                    1,
                    "Cirkus Luna Sjællands-Tourne",
                    new DateOnly(2026, 7, 12),
                    43,
                    3,
                    copenhagen
                );

                show1.Artists.Add(artist1);
                show1.Artists.Add(artist2);
                show1.Artists.Add(artist3);

                Show show8 = new Show(
                    8,
                    "Cirkus Luna Roskilde",
                    new DateOnly(2025, 5, 17),
                    75,
                    8,
                    roskilde
                );

                show8.Artists.Add(artist1);
                show8.Artists.Add(artist2);
                show8.Artists.Add(artist4);

                Show show9 = new Show(
                    9,
                    "Cirkus Luna Fyn",
                    new DateOnly(2025, 8, 23),
                    95,
                    10,
                    odense
                );

                show9.Artists.Add(artist1);
                show9.Artists.Add(artist3);
                show9.Artists.Add(artist5);

                Show show10 = new Show(
                    10,
                    "Cirkus Luna Jyllands-Tourne",
                    new DateOnly(2026, 4, 11),
                    120,
                    12,
                    aarhus
                );

                show10.Artists.Add(artist2);
                show10.Artists.Add(artist3);
                show10.Artists.Add(artist4);
                show10.Artists.Add(artist5);

                // Upcoming shows
                Show show2 = new Show(
                    2,
                    "Cirkus Luna København",
                    new DateOnly(2026, 10, 18),
                    80,
                    10,
                    copenhagen
                );

                show2.Artists.Add(artist1);
                show2.Artists.Add(artist2);
                show2.Artists.Add(artist4);

                Show show3 = new Show(
                    3,
                    "Cirkus Luna Roskilde",
                    new DateOnly(2027, 3, 20),
                    100,
                    12,
                    roskilde
                );

                show3.Artists.Add(artist1);
                show3.Artists.Add(artist3);
                show3.Artists.Add(artist5);

                Show show4 = new Show(
                    4,
                    "Cirkus Luna Fyn",
                    new DateOnly(2027, 7, 17),
                    110,
                    10,
                    odense
                );

                show4.Artists.Add(artist1);
                show4.Artists.Add(artist2);
                show4.Artists.Add(artist4);
                show4.Artists.Add(artist5);

                Show show5 = new Show(
                    5,
                    "Cirkus Luna Aalborg",
                    new DateOnly(2028, 4, 22),
                    90,
                    8,
                    aalborg
                );

                show5.Artists.Add(artist2);
                show5.Artists.Add(artist3);
                show5.Artists.Add(artist4);

                Show show6 = new Show(
                    6,
                    "Cirkus Luna Aarhus",
                    new DateOnly(2028, 8, 12),
                    120,
                    15,
                    aarhus
                );

                show6.Artists.Add(artist1);
                show6.Artists.Add(artist3);
                show6.Artists.Add(artist5);

                Show show7 = new Show(
                    7,
                    "Cirkus Luna København",
                    new DateOnly(2029, 5, 19),
                    150,
                    20,
                    copenhagen
                );

                show7.Artists.Add(artist1);
                show7.Artists.Add(artist2);
                show7.Artists.Add(artist3);
                show7.Artists.Add(artist4);
                show7.Artists.Add(artist5);

                _shows = new List<Show>
                {
                    show8,
                    show9,
                    show10,
                    show1,
                    show2,
                    show3,
                    show4,
                    show5,
                    show6,
                    show7
                };

                SaveToFile();
            }
        }

        // Save show data to the JSON file.
        private void SaveToFile()
        {
            string json = JsonSerializer.Serialize(
                _shows,
                new JsonSerializerOptions { WriteIndented = true }
            );

            File.WriteAllText(_path, json);
        }

        // Return all shows.
        public List<Show> GetAll()
        {
            return _shows;
        }

        // Find a show by ID. Returns null if no show is found.
        public Show GetById(int id)
        {
            foreach (Show show in _shows)
            {
                if (show.Id == id)
                    return show;
            }

            return null;
        }

        // Find shows in a specific city.
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

        // Add a new show and save the changes.
        public void Add(Show show)
        {
            _shows.Add(show);
            SaveToFile();
        }

        // Update an existing show and save the changes.
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
                    break;
                }
            }

            SaveToFile();
        }

        // Delete a show by ID and save the changes.
        public void Delete(int id)
        {
            _shows.Remove(GetById(id));
            SaveToFile();
        }
    }
}