using CirkusLuna.ClassLibrary.Model;

namespace CirkusLuna.ClassLibrary.Service
{
    public interface ICustomerService
    {
        List<Customer> GetAll();

        Customer GetById(int id);

        Customer AddCustomer(string firstName, string lastName, string email, string phoneNumber);

        void UpdateName(int id, string firstName, string lastName);

        void UpdateEmail(int id, string email);

        void UpdatePhoneNumber(int id, string phoneNumber);

        void DeleteCustomer(int id);
    }
}