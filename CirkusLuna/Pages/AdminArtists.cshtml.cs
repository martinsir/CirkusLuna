using CirkusLuna.ClassLibrary.Model;
using CirkusLuna.ClassLibrary.Repository;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CirkusLuna.Pages
{
    public class AdminArtistsModel : PageModel
    {
        private IArtistRepository _artistRepository;

        public List<Artist> Artists { get; set; } = new List<Artist>();

        public AdminArtistsModel(IArtistRepository artistRepository)
        {
            _artistRepository = artistRepository;
        }

        public void OnGet()
        {
            Artists = _artistRepository.GetAll();
        }
    }
}