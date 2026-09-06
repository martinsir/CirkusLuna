using CirkusLuna.ClassLibrary.Model;
using CirkusLuna.ClassLibrary.Repository;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CirkusLuna.Pages
{
    public class AdminCustomersModel : PageModel
    {
        private ICustomerRepository _customerRepository;
        public List<Customer> Customers { get; set; } = new List<Customer>();

        public AdminCustomersModel(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public void OnGet()
        {
            Customers = _customerRepository.GetAll();
        }
    }
}