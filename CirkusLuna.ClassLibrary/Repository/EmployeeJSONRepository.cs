using CirkusLuna.ClassLibrary.Model;
using System.Text.Json;

namespace CirkusLuna.ClassLibrary.Repository
{
    public class EmployeeJSONRepository : IEmployeeRepository
    {
        // JSON persistence saves employee data between sessions.
        // The file path is provided through the constructor.
        private readonly string _path;

        private List<Employee> _employeeList;

        public EmployeeJSONRepository(string path)
        {
            _path = path;

            // Load existing JSON data if the file already exists.
            if (File.Exists(_path))
            {
                string json = File.ReadAllText(_path);

                _employeeList =
                    JsonSerializer.Deserialize<List<Employee>>(json)
                    ?? new List<Employee>();
            }
            else
            {
                // Create default employees the first time the JSON file is created.
                _employeeList = new List<Employee>
                {
                    new Employee(
                        1,
                        "Benny",
                        "Blæk",
                        "blæk@cirkusluna.dk",
                        "Direktør",
                        "blæk"
                    ),

                    new Employee(
                        2,
                        "Dorte",
                        "Hansen",
                        "hansen@cirkusluna.dk",
                        "Sekretær",
                        "hansen"
                    ),

                    new Employee(
                        3,
                        "Manfred",
                        "Manfredi",
                        "manfredi@cirkusluna.dk",
                        "Vært",
                        "manfredi"
                    )
                };

                SaveToFile();
            }
        }

        // Save employee data to the JSON file.
        private void SaveToFile()
        {
            string json = JsonSerializer.Serialize(
                _employeeList,
                new JsonSerializerOptions { WriteIndented = true }
            );

            File.WriteAllText(_path, json);
        }

        // Return all employees.
        public List<Employee> GetAll()
        {
            return _employeeList;
        }

        // Find an employee by ID. Returns null if no employee is found.
        public Employee GetById(int id)
        {
            for (int i = 0; i < _employeeList.Count; i++)
            {
                if (_employeeList[i].Id == id)
                    return _employeeList[i];
            }

            return null;
        }

        // Add a new employee and save the changes.
        public void Add(Employee employee)
        {
            _employeeList.Add(employee);
            SaveToFile();
        }

        // Update an existing employee and save the changes.
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

        // Delete an employee by ID and save the changes.
        public void Delete(int id)
        {
            _employeeList.Remove(GetById(id));
            SaveToFile();
        }
    }
}