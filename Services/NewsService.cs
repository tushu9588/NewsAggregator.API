using NewsAggregator.API.Models;
using NewsAggregator.API.Data;

namespace NewsAggregator.API.Services
{
    public class NewsService : INewsService
    {
        private readonly AppDbContext _context;

        public NewsService(AppDbContext context)
        {
            _context = context;
        }

        public List<News> GetAll()
        {
            return _context.News
                .OrderByDescending(n => n.PublicationDate)
                .ToList();
        }

        public List<News> Search(string title)
        {
            return _context.News
                .Where(n => n.Title.Contains(title))
                .OrderByDescending(n => n.PublicationDate)
                .ToList();
        }

        public News Add(News news)
        {
            _context.News.Add(news);
            _context.SaveChanges();
            return news;
        }

        public News UpdateImpact(int id, string impact)
        {
            var news = _context.News.FirstOrDefault(n => n.Id == id);
            if (news == null)
                throw new Exception("News not found");

            news.NewsImpact = impact;
            _context.SaveChanges();
            return news;
        }
    }
}
