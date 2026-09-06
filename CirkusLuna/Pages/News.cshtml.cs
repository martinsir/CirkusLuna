using CirkusLuna.ClassLibrary.Model;
using CirkusLuna.ClassLibrary.Repository;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CirkusLuna.Pages
{
    public class NewsModel : PageModel
    {
        private INewsPostRepository _newsPostRepository;

        public List<NewsPost> NewsPosts { get; set; } = new List<NewsPost>();
        public string SearchTitle { get; set; } = string.Empty;

        public NewsModel(INewsPostRepository newsPostRepository)
        {
            _newsPostRepository = newsPostRepository;
        }

        //[Bindproperty(SupportsGet = true)]
        //public string SearchTitle { get; set; } = string.Empty;

        public void OnGet()
        {
            SearchTitle = Request.Query["SearchTitle"];

            if (string.IsNullOrEmpty(SearchTitle))
            {
                NewsPosts = _newsPostRepository.GetAll();
            }
            else
            {
                NewsPosts = _newsPostRepository.GetByTitle(SearchTitle);
            }
        }
    }
}