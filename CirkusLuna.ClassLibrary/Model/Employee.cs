namespace CirkusLuna.ClassLibrary.Model
{
    public class Employee : Person
    {
        //Specifikke properties til Employee
        public string Role { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        //Constructor
        // base() kalder Person's constructor
        public Employee(int id, string firstName, string lastName, string email, string role, string password) : base(id, firstName, lastName, email)
        {
            Role = role;
            Password = password;
        }
    }
}