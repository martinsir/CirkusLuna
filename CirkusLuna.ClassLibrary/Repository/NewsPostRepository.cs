using CirkusLuna.ClassLibrary.Model;

namespace CirkusLuna.ClassLibrary.Repository
{
    public class NewsPostRepository : INewsPostRepository
    {
        private List<NewsPost> _newsposts = new List<NewsPost>();

        //NewsPost Constructor
        public NewsPostRepository()
        {
            //Tom constructor, når nu data håndteres i NewsPostJSONRepository
        }

        public List<NewsPost> GetAll()
        {
            return _newsposts;
        }

        public NewsPost GetById(int id)
        {
            for (int i = 0; i < _newsposts.Count; i++)
            {
                if (_newsposts[i].NewsPostId == id)
                {
                    return _newsposts[i];
                }
            }
            return null;
        }

        //getbytitle
        public List<NewsPost> GetByTitle(string title)
        {
            List<NewsPost> result = new List<NewsPost>();
            for (int i = 0; i < _newsposts.Count; i++)
            {
                if (_newsposts[i].Title.ToLower().Contains(title.ToLower()))
                {
                    result.Add(_newsposts[i]);
                }
            }
            return result;
        }

        public List<NewsPost> GetByPublishedDate(DateTime dateTime)
        {
            List<NewsPost> result = new List<NewsPost>();
            for (int i = 0; i < _newsposts.Count; i++)
            {
                if (_newsposts[i].PublishedDateTime == dateTime)
                {
                    result.Add(_newsposts[i]);
                }
            }
            return result;
        }

        //add
        public void Add(NewsPost newsPost)
        {
            _newsposts.Add(newsPost);
        }

        //update
        public void Update(NewsPost newsPost)
        {
            for (int i = 0; i < _newsposts.Count; i++)
            {
                if (_newsposts[i].NewsPostId == newsPost.NewsPostId)
                {
                    _newsposts[i].Title = newsPost.Title;
                    _newsposts[i].Content = newsPost.Content;
                    _newsposts[i].PublishedDateTime = newsPost.PublishedDateTime;
                    break;
                }
            }
        }

        public void Delete(int id)
        {
            _newsposts.Remove(GetById(id));
        }
    }
}