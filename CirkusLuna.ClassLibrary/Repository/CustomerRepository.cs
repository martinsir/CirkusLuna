using CirkusLuna.ClassLibrary.Model;

namespace CirkusLuna.ClassLibrary.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private List<Customer> _customerList = new List<Customer>();

        public CustomerRepository()
        {
            //Tom constructor, data håndteres af CustomerJSONRepository
        }

        public List<Customer> GetAll()
        {
            return _customerList;
        }

        public Customer GetById(int id)
        {
            for (int i = 0; i < _customerList.Count; i++)
            {
                if (_customerList[i].Id == id)
                {
                    return _customerList[i];
                }
            }
            return null;
        }

        public void Add(Customer customer)
        {
            _customerList.Add(customer);
        }

        public void Update(Customer customer)
        {
            for (int i = 0; i < _customerList.Count; i++)
            {
                if (_customerList[i].Id == customer.Id)
                {
                    _customerList[i].FirstName = customer.FirstName;
                    _customerList[i].LastName = customer.LastName;
                    _customerList[i].Email = customer.Email;
                    _customerList[i].PhoneNumber = customer.PhoneNumber;
                    _customerList[i].IsVip = customer.IsVip;
                    break; //no point continuing the loop
                }
            }
        }

        public void Delete(int id)
        {
            _customerList.Remove(GetById(id));
        }
    }
}