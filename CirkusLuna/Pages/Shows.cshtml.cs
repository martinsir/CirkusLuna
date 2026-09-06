using CirkusLuna.ClassLibrary.Model;
using CirkusLuna.ClassLibrary.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CirkusLuna.Pages
{
    public class ShowsModel : PageModel
    {
        //service injected via dependency injection
        private IShowService _showService;

        //list of shows that page will display
        public List<Show> Shows { get; set; } = new List<Show>();

        //constructor
        public ShowsModel(IShowService showService)
        {
            _showService = showService;
        }

        //BindProperty is new for me so ill include it and paste an alternative
        [BindProperty(SupportsGet = true)]
        public string SearchCity { get; set; } = string.Empty;

        //If i was to use something else than bind property,
        //it would be as as below
        //SearchCity = Request.Query["SearchCity"]
        [BindProperty(SupportsGet = true)]
        public string SearchDate { get; set; } = string.Empty;

        [BindProperty(SupportsGet = true)]
        public bool OnlyAvailable { get; set; } = false;

        public void OnGet()
        {
            if (string.IsNullOrEmpty(SearchCity))
            {
                //all shows
                Shows = _showService.GetAll();
            }
            //filter by city
            if (!string.IsNullOrEmpty(SearchCity))
            {
                Shows = _showService.GetByCity(SearchCity);
            }

            //filter by date
            if (!string.IsNullOrEmpty(SearchDate))
            {
                DateOnly date = DateOnly.Parse(SearchDate);
                List<Show> dateResult = new List<Show>();
                foreach (Show show in Shows)
                {
                    if (show.Date == date)
                    {
                        dateResult.Add(show);
                    }
                }
                Shows = dateResult;
            }

            //filter by availability
            if (OnlyAvailable)
            {
                List<Show> availableResult = new List<Show>();
                foreach (Show show in Shows)
                {
                    if (show.Seats > 0)
                    {
                        availableResult.Add(show);
                    }
                }
                Shows = availableResult;
            }
        }
    }
}