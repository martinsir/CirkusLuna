namespace CirkusLuna.ClassLibrary.Model

{
    //Artist class, arver fra Person
    public class Artist : Person
    {
        //Specifikke properties til Artist
        public string Act { get; set; } = string.Empty; //Det de kan, f.eks junglere, akrobat m.m

        //Constructor
        //base() kalder Person's constructor
        public Artist(int id, string firstName, string lastName, string email, string act)
            : base(id, firstName, lastName, email)
        {
            Act = act;
        }
    }
}