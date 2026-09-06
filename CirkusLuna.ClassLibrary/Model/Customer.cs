namespace CirkusLuna.ClassLibrary.Model
{
    public class Customer : Person //Customer arver fra Person
    {
        //Specifikke properties til customer
        public string PhoneNumber { get; set; } = string.Empty;

        public bool IsVip { get; set; }

        //Constructor
        // base() kalder Person's constructor
        public Customer(int id, string firstName, string lastName, string email, string phoneNumber, bool isVip)
            : base(id, firstName, lastName, email)
        {
            PhoneNumber = phoneNumber;
            IsVip = isVip;
        }
    }
}