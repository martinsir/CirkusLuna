namespace CirkusLuna.ClassLibrary.Model
{
    public class City
    {
        //City Properties
        public int Id { get; set; }

        public string Name { get; set; }

        //Constructor
        public City(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}