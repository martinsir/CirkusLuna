using CirkusLuna.ClassLibrary.Model;

namespace CirkusLuna.ClassLibrary.Repository
{
    public class ArtistRepository : IArtistRepository
    {
        private List<Artist> _artistList = new List<Artist>();

        public ArtistRepository()
        {
            //Tom constructor, data håndteres af CustomerJSONRepository
        }

        public List<Artist> GetAll()
        {
            return _artistList;
        }

        public Artist GetById(int id)
        {
            for (int i = 0; i < _artistList.Count; i++)
            {
                if (_artistList[i].Id == id)
                {
                    return _artistList[i];
                }
            }
            return null;
        }

        //Add funktion, så nye artister kan oprettes i programmet
        public void Add(Artist artist)
        {
            _artistList.Add(artist);
        }

        //Update funktion, så artister kan ændres i programmet
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
        }

        //Delete funktion, så artister kan slettes i programmet
        public void Delete(int id)
        {
            _artistList.Remove(GetById(id));
        }
    }
}