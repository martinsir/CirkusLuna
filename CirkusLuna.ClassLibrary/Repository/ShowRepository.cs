using CirkusLuna.ClassLibrary.Model;

namespace CirkusLuna.ClassLibrary.Repository
{
    public class ShowRepository : IShowRepository
    {
        //Private list til at holde data
        private List<Show> _shows = new List<Show>();

        //Constructor
        public ShowRepository()
        {
            //Tom constructor, data håndteres af ShowJSONRepository
        }

        public List<Show> GetAll()
        {
            return _shows;
        }

        public Show GetById(int id)
        {
            foreach (Show show in _shows)
            {
                if (show.Id == id)
                {
                    return show;
                }
            }
            return null;
        }

        //Metode til at søge efter forestilling i bestemt by
        public List<Show> GetByCity(string cityName)
        {
            //Liste til resultat af søgning
            List<Show> result = new List<Show>();

            foreach (Show show in _shows)
            {
                if (show.City.Name.ToLower() == cityName.ToLower())
                {
                    result.Add(show);
                }
            }
            return result;
        }

        public void Add(Show show)
        {
            _shows.Add(show);
        }

        //Funktion til at kunne opdatere Shows
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
                }
            }
        }

        public void Delete(int id)
        {
            _shows.Remove(GetById(id));
        }
    }
}