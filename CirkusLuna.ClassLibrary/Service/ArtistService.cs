using CirkusLuna.ClassLibrary.Model;
using CirkusLuna.ClassLibrary.Repository;

namespace CirkusLuna.ClassLibrary.Service
{
    public class ArtistService : IArtistService
    {
        //Gemmer reference til IArtistRepository
        private IArtistRepository _artistRepository;

        public ArtistService(IArtistRepository artistRepository)
        {
            _artistRepository = artistRepository;
        }

        //Returner alle artister
        public List<Artist> GetAll()
        {
            return _artistRepository.GetAll();
        }

        //Find artist på ID
        public Artist GetById(int id)
        {
            return _artistRepository.GetById(id);
        }

        //Opret ny artist og tilføj til repository
        public Artist AddArtist(string firstName, string lastName, string email, string act)
        {
            int artistId = _artistRepository.GetAll().Count + 1;
            Artist newArtist = new Artist(artistId, firstName, lastName, email, act);
            _artistRepository.Add(newArtist);
            return newArtist;
        }

        //Opdater navn på artist
        public void UpdateName(int id, string firstName, string lastName)
        {
            Artist artist = _artistRepository.GetById(id);
            artist.FirstName = firstName;
            artist.LastName = lastName;
            _artistRepository.Update(artist);
        }

        //Opdater email på artist
        public void UpdateEmail(int id, string email)
        {
            Artist artist = _artistRepository.GetById(id);
            artist.Email = email;
            _artistRepository.Update(artist);
        }

        //Opdater act på artist
        public void UpdateAct(int id, string act)
        {
            Artist artist = _artistRepository.GetById(id);
            artist.Act = act;
            _artistRepository.Update(artist);
        }

        //Sletter artist på ID
        public void DeleteArtist(int id)
        {
            _artistRepository.Delete(id);
        }
    }
}