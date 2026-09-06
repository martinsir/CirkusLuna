using CirkusLuna.ClassLibrary.Model;
using System.Text.Json;

namespace CirkusLuna.ClassLibrary.Repository
{
    public class CustomerJSONRepository : ICustomerRepository
    {
        // JSON persistence saves customer data between sessions.
        // The file path is provided through the constructor.
        private readonly string _path;

        private List<Customer> _customerList;

        public CustomerJSONRepository(string path)
        {
            _path = path;

            // Load existing JSON data if the file already exists.
            if (File.Exists(_path))
            {
                string json = File.ReadAllText(_path);

                _customerList =
                    JsonSerializer.Deserialize<List<Customer>>(json)
                    ?? new List<Customer>();
            }
            else
            {
                // Create default customers the first time the JSON file is created.
                _customerList = new List<Customer>
                {
                    new Customer(1, "Gunner", "Gunnersen", "gumhmail@mail.com", "56345678", false),
                    new Customer(2, "Åge", "Ågesen", "åmhmail@mail.com", "35345678", false),
                    new Customer(3, "Viggo", "Viggosen", "vigmhmail@mail.com", "20345678", false),
                    new Customer(4, "Maja", "Majasen", "majmhmail@mail.com", "89345678", false),
                    new Customer(5, "Shen", "Hana", "shemhmail@mail.com", "12995678", true)
                };

                SaveToFile();
            }
        }

        // Save customer data to the JSON file.
        private void SaveToFile()
        {
            string json = JsonSerializer.Serialize(
                _customerList,
                new JsonSerializerOptions { WriteIndented = true }
            );

            File.WriteAllText(_path, json);
        }

        // Return all customers.
        public List<Customer> GetAll()
        {
            return _customerList;
        }

        // Find a customer by ID. Returns null if no customer is found.
        public Customer GetById(int id)
        {
            for (int i = 0; i < _customerList.Count; i++)
            {
                if (_customerList[i].Id == id)
                    return _customerList[i];
            }

            return null;
        }

        // Add a new customer and save the changes.
        public void Add(Customer customer)
        {
            _customerList.Add(customer);
            SaveToFile();
        }

        // Update an existing customer and save the changes.
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
                    break;
                }
            }

            SaveToFile();
        }

        // Delete a customer by ID and save the changes.
        public void Delete(int id)
        {
            _customerList.Remove(GetById(id));
            SaveToFile();
        }
    }
}