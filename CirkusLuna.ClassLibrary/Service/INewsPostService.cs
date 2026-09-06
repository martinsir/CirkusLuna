using CirkusLuna.ClassLibrary.Model;

namespace CirkusLuna.ClassLibrary.Service
{
    public interface INewsPostService
    {
        List<NewsPost> GetAll();

        NewsPost GetById(int id);

        List<NewsPost> GetByTitle(string title);

        List<NewsPost> GetByPublishedDate(DateTime dateTime);

        NewsPost AddNewsPost(string title, string content);

        void DeleteNewsPost(int id);
    }
}