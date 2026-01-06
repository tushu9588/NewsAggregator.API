using NewsAggregator.API.Models;

namespace NewsAggregator.API.Services
{
    public interface INewsService
    {
        List<News> GetAll();
        List<News> Search(string title);
        News Add(News news);
        News UpdateImpact(int id, string impact);
    }
}
