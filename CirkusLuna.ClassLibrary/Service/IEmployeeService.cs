using CirkusLuna.ClassLibrary.Model;

namespace CirkusLuna.ClassLibrary.Service
{
    public interface IEmployeeService
    {
        List<Employee> GetAll();

        Employee GetById(int id);

        Employee Login(string password);

        Employee AddEmployee(string firstName, string lastName, string email, string role);

        void DeleteEmployee(int id);
    }
}