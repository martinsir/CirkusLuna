using System.Text.Json;
using CirkusLuna.ClassLibrary.Model;

namespace CirkusLuna.ClassLibrary.Repository
{
    public class CustomerJSONRepository : ICustomerRepository
    //JSON persistence was implemented to save data between sessions. The file path is currently hardcoded to C:\temp\ due to path resolution challenges in ASP.NET Core. A more robust solution would use IWebHostEnvironment.ContentRootPath."
    {
        // Stien til JSON filen - gemmes i programmets output mappe
        private readonly string _path;
        private List<Customer> _customerList;

        public CustomerJSONRepository(string path)
        {
            _path = path;
            if (File.Exists(_path))
            {
                string json = File.ReadAllText(_path);
                _customerList = JsonSerializer.Deserialize<List<Customer>>(json) ?? new List<Customer>();
            }
            else
            {
                //Hardcoded kunder, tilføjes første gang før filen ikke eksisterer
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

        //Gemmer data til filen
        private void SaveToFile()
        {
            string json = JsonSerializer.Serialize(_customerList, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_path, json);
        }

        // Returnerer alle kunder
        public List<Customer> GetAll()
        {
            return _customerList;
        }

        // Finder og returnerer kunde på ID - returnerer null hvis kunde ikke kan findes
        public Customer GetById(int id)
        {
            for (int i = 0; i < _customerList.Count; i++)
            {
                if (_customerList[i].Id == id)
                    return _customerList[i];
            }
            return null;
        }

        // Tilføjer ny kunde og gemmer til fil
        public void Add(Customer customer)
        {
            _customerList.Add(customer);
            SaveToFile();
        }

        // Opdaterer eksisterende kunde og gemmer til fil
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

        // Sletter kunde på ID og gemmer til fil
        public void Delete(int id)
        {
            _customerList.Remove(GetById(id));
            SaveToFile();
        }
    }
}