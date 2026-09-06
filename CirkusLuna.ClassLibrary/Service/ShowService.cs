using CirkusLuna.ClassLibrary.Model;
using CirkusLuna.ClassLibrary.Repository;

namespace CirkusLuna.ClassLibrary.Service
{
    public class ShowService : IShowService
    {
        private IShowRepository _showRepository;

        public ShowService(IShowRepository showRepository)
        {
            _showRepository = showRepository;
        }

        public List<Show> GetAll()
        {
            return _showRepository.GetAll();
        }

        public List<Show> GetByCity(string cityName)
        {
            List<Show> shows = _showRepository.GetAll();
            List<Show> result = new List<Show>();

            for (int i = 0; i < shows.Count; i++)
            {
                if (shows[i].City.Name.ToLower() == cityName.ToLower())
                {
                    result.Add(shows[i]);
                }
            }
            return result;
        }

        public List<Show> GetByDateOnly(DateOnly date)
        {
            List<Show> shows = _showRepository.GetAll();
            List<Show> result = new List<Show>();

            for (int i = 0; i < shows.Count; i++)
            {
                if (shows[i].Date == date)
                {
                    result.Add(shows[i]);
                }
            }
            return result;
        }

        //bubble sorting
        public List<City> GetSortedCities()
        {
            List<Show> shows = _showRepository.GetAll();
            List<City> result = new List<City>();

            for (int i = 0; i < shows.Count; i++)
            {
                result.Add(shows[i].City);
            }

            for (int i = 0; i < result.Count - 1; i++)
            {
                for (int j = 0; j < result.Count - 1 - i; j++)
                {
                    if (result[j].Name.CompareTo(result[j + 1].Name) > 0)
                    {
                        City temp = result[j];
                        result[j] = result[j + 1];
                        result[j + 1] = temp;
                    }
                }
            }
            return result;
        }

        //Finder og returnerer show på ID
        public Show GetById(int id)
        {
            return _showRepository.GetById(id);
        }

        //Opretter nyt show og tilføjer til repository
        public Show AddShow(string showName, DateOnly date, int seats, int vipSeats, string cityName)
        {
            int newShowId = _showRepository.GetAll().Count + 1;
            int newCityId = newShowId;
            City newCity = new City(newCityId, cityName);
            Show newShow = new Show(newShowId, showName, date, seats, vipSeats, newCity);
            _showRepository.Add(newShow);
            return newShow;
        }

        //Opdater show navn
        public void UpdateShowName(int id, string showName)
        {
            Show show = _showRepository.GetById(id);
            show.ShowName = showName;
            _showRepository.Update(show);
        }

        //Opdater dato på show
        public void UpdateShowDate(int id, DateOnly date)
        {
            Show show = _showRepository.GetById(id);
            show.Date = date;
            _showRepository.Update(show);
        }

        //Opdater antal standard pladser
        public void UpdateShowSeats(int id, int seats)
        {
            Show show = _showRepository.GetById(id);
            show.Seats = seats;
            _showRepository.Update(show);
        }

        //Opdater antal VIP pladser
        public void UpdateShowVipSeats(int id, int vipSeats)
        {
            Show show = _showRepository.GetById(id);
            show.VipSeats = vipSeats;
            _showRepository.Update(show);
        }

        //Opdater show by
        public void UpdateShowCity(int id, string cityName)
        {
            Show show = _showRepository.GetById(id);
            City newCity = new City(show.City.Id, cityName);
            show.City = newCity;
            _showRepository.Update(show);
        }

        //Slet show på ID
        public void DeleteShow(int id)
        {
            _showRepository.Delete(id);
        }
    }
}