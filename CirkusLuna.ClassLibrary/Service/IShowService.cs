using CirkusLuna.ClassLibrary.Model;

namespace CirkusLuna.ClassLibrary.Service
{
    public interface IShowService
    {
        List<Show> GetAll();

        List<Show> GetByCity(string cityName);

        List<Show> GetByDateOnly(DateOnly date);

        List<City> GetSortedCities();

        Show GetById(int id);

        Show AddShow(string showName, DateOnly date, int seats, int vipSeats, string cityName);

        void UpdateShowName(int id, string showName);

        void UpdateShowDate(int id, DateOnly date);

        void UpdateShowSeats(int id, int seats);

        void UpdateShowVipSeats(int id, int vipSeats);

        void UpdateShowCity(int id, string cityName);

        void DeleteShow(int id);
    }
}