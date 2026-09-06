using CirkusLuna.ClassLibrary.Model;

namespace CirkusLuna.ClassLibrary.Service
{
    public interface IArtistService
    {
        List<Artist> GetAll();

        Artist GetById(int id);

        Artist AddArtist(string firstName, string lastName, string email, string act);

        void UpdateName(int id, string firstName, string lastName);

        void UpdateEmail(int id, string email);

        void UpdateAct(int id, string act);

        void DeleteArtist(int id);
    }
}