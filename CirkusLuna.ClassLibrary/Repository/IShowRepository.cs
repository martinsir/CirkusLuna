using CirkusLuna.ClassLibrary.Model;

namespace CirkusLuna.ClassLibrary.Repository
{
    public interface IShowRepository
    {
        //Beskriver hvad man kan gøre med data
        List<Show> GetAll();

        Show GetById(int id);

        List<Show> GetByCity(string cityName);

        void Add(Show show);

        void Update(Show show);

        void Delete(int id);
    }
}