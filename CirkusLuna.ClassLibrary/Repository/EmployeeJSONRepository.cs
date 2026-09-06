using System.Text.Json;
using CirkusLuna.ClassLibrary.Model;

namespace CirkusLuna.ClassLibrary.Repository
{
    public class EmployeeJSONRepository : IEmployeeRepository
    {

        // JSON persistence saves reservation data between sessions.
        // The file path is provided through the constructor.
        private readonly string _path;
        private List<Employee> _employeeList;

        public EmployeeJSONRepository(string path)
        {
            _path = path;
            // Tjekker om JSON filen allerede eksisterer
            if (File.Exists(_path))
            {
                string json = File.ReadAllText(_path);
                _employeeList = JsonSerializer.Deserialize<List<Employee>>(json) ?? new List<Employee>();
            }
            else
            {
                // Filen eksisterer ikke endnu - opret hardcodede medarbejdere første gang
                _employeeList = new List<Employee>
                {
                    new Employee(1, "Benny", "Blæk", "blæk@cirkusluna.dk", "Direktør", "blæk"),
                    new Employee(2, "Dorte", "Hansen", "hansen@cirkusluna.dk", "Sekretær", "hansen"),
                    new Employee(3, "Manfred", "Manfredi", "manfredi@cirkusluna.dk", "Vært", "manfredi")
                };
                SaveToFile();
            }
        }

        //Gem data til fil
        private void SaveToFile()
        {
            string json = JsonSerializer.Serialize(_employeeList, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_path, json);
        }

        // Returnerer alle medarbejdere
        public List<Employee> GetAll()
        {
            return _employeeList;
        }

        // Finder og returnerer medarbejder på ID - returnerer null hvis ikke fundet
        public Employee GetById(int id)
        {
            for (int i = 0; i < _employeeList.Count; i++)
            {
                if (_employeeList[i].Id == id)
                    return _employeeList[i];
            }
            return null;
        }

        // Tilføjer ny kunde og gemmer til fil
        public void Add(Employee employee)
        {
            _employeeList.Add(employee);
            SaveToFile();
        }
        // Opdaterer eksisterende medarbejder og gemmer til fil
        public void Update(Employee employee)
        {
            for (int i = 0; i < _employeeList.Count; i++)
            {
                if (_employeeList[i].Id == employee.Id)
                {
                    _employeeList[i].FirstName = employee.FirstName;
                    _employeeList[i].LastName = employee.LastName;
                    _employeeList[i].Email = employee.Email;
                    _employeeList[i].Password = employee.Password;
                    _employeeList[i].Role = employee.Role;
                    break;
                }
            }
            SaveToFile();
        }
        // Sletter employee på ID og gemmer til fil
        public void Delete(int id)
        {
            _employeeList.Remove(GetById(id));
            SaveToFile();
        }

    }
}