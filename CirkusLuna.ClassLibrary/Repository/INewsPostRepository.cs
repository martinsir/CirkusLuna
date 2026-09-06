using CirkusLuna.ClassLibrary.Model;

namespace CirkusLuna.ClassLibrary.Repository
{
    public interface INewsPostRepository
    {
        List<NewsPost> GetAll();

        List<NewsPost> GetByTitle(string titleName);

        List<NewsPost> GetByPublishedDate(DateTime dateTime);

        NewsPost GetById(int id);

        void Delete(int id);

        void Add(NewsPost newsPost);

        void Update(NewsPost newsPost);
    }
}