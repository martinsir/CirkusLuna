using CirkusLuna.ClassLibrary.Model;

namespace CirkusLuna.ClassLibrary.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employeeList = new List<Employee>();

        //Employee Constructor
        public EmployeeRepository()
        {
            //Oprettelse af medarbejdere
            Employee employee1 = new Employee(1, "Benny", "Blæk", "blæk@cirkusluna.dk", "Direktør", "blæk");
            Employee employee2 = new Employee(2, "Dorte", "Hansen", "hansen@cirkusluna.dk", "Sekretær", "hansen");
            Employee employee3 = new Employee(3, "Manfred", "Manfredi", "manfredi@cirkusluna.dk", "Vært", "manfredi");

            //Tilføjelse af medarbejdere til Employee List
            _employeeList.Add(employee1);
            _employeeList.Add(employee2);
            _employeeList.Add(employee3);
        }

        public List<Employee> GetAll()
        {
            return _employeeList;
        }

        public Employee GetById(int id)
        {
            for (int i = 0; i < _employeeList.Count; i++)
            {
                if (_employeeList[i].Id == id)
                {
                    return _employeeList[i];
                }
            }
            return null;
        }

        public void Add(Employee employee)
        {
            _employeeList.Add(employee);
        }

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
        }

        public void Delete(int id)
        {
            _employeeList.Remove(GetById(id));
        }
    }
}