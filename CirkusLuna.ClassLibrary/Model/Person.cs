namespace CirkusLuna.ClassLibrary.Model
{
    //Base class for alle personer
    //Abstract class, fordi Person ikke skal instantieres
    public abstract class Person
    {
        //Fælles properties for alle personer
        public int Id { get; set; }

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        //FullName metode firstName + lastName
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        //Constructor
        public Person(int id, string firstName, string lastName, string email)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }
    }
}