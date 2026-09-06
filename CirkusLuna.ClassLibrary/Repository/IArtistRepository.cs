using CirkusLuna.ClassLibrary.Model;

namespace CirkusLuna.ClassLibrary.Repository
{
    public interface IArtistRepository
    {
        List<Artist> GetAll();

        Artist GetById(int id);

        void Add(Artist artist);

        void Update(Artist artist);

        void Delete(int id);
    }
}