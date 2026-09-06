namespace CirkusLuna.ClassLibrary.Model
{
    public class ShowArtist
    {
        public Artist? Artist { get; set; }
        public Show? Show { get; set; }
        public int ShowArtistId { get; set; }
        public int PerformOrder { get; set; }
        public string RoleInShow { get; set; } = string.Empty;

        public ShowArtist(Artist artist, Show show, int showArtistId, int performOder, string roleInShow)
        {
            Artist = artist;
            Show = show;
            ShowArtistId = showArtistId;
            PerformOrder = performOder;
            RoleInShow = roleInShow;
        }
    }
}