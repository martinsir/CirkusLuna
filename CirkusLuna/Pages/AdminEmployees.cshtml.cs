using CirkusLuna.ClassLibrary.Model;
using CirkusLuna.ClassLibrary.Repository;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CirkusLuna.Pages
{
    public class AdminEmployeesModel : PageModel
    {
        private IEmployeeRepository _employeeRepository;

        public List<Employee> Employees { get; set; } = new List<Employee>();

        public AdminEmployeesModel(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public void OnGet()
        {
            Employees = _employeeRepository.GetAll();
        }
    }
}