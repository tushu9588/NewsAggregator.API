using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsAggregator.API.Models;
using NewsAggregator.API.DTOs;
using NewsAggregator.API.Common;
using NewsAggregator.API.Services;

namespace NewsAggregator.API.Controllers
{
    [ApiController]
    [Route("api/news")]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;
        private readonly ILogger<NewsController> _logger;

        // ✅ Constructor Injection
        public NewsController(
            INewsService newsService,
            ILogger<NewsController> logger)
        {
            _newsService = newsService;
            _logger = logger;
        }

        // ============================
        // GET ALL NEWS (PUBLIC)
        // ============================
        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetAll(int page = 1, int pageSize = 5)
        {
            _logger.LogInformation("Fetching all news");

            var allNews = _newsService.GetAll();
            var totalCount = allNews.Count;

            var data = allNews
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return Ok(ApiResponse<object>.SuccessResponse(new
            {
                page,
                pageSize,
                totalCount,
                data
            }));
        }

        // ============================
        // SEARCH + PAGINATION (PUBLIC)
        // ============================
        [AllowAnonymous]
        [HttpGet("search")]
        public IActionResult Search(string title = "", int page = 1, int pageSize = 5)
        {
            _logger.LogInformation("Searching news with title: {title}", title);

            var filtered = _newsService.Search(title);
            var totalCount = filtered.Count;

            var data = filtered
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return Ok(ApiResponse<object>.SuccessResponse(new
            {
                page,
                pageSize,
                totalCount,
                data
            }));
        }

        // ============================
        // UPDATE IMPACT (ADMIN ONLY)
        // ============================
        [Authorize(Roles = "Admin")]
        [HttpPut("impact")]
        public IActionResult UpdateImpact(int id, string impact)
        {
            _logger.LogInformation("Updating impact for newsId={id}", id);

            var updated = _newsService.UpdateImpact(id, impact);

            return Ok(ApiResponse<News>.SuccessResponse(
                updated,
                "News impact updated successfully"
            ));
        }

        // ============================
        // ADD NEWS (ADMIN ONLY)
        // ============================
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AddNews([FromBody] NewsCreateDto dto)
        {
            _logger.LogInformation("Adding new news");

            var news = new News
            {
                Title = dto.Title,
                Url = dto.Url,
                PublicationDate = dto.PublicationDate,
                Source = dto.Source,
                CreatedAt = DateTime.Now
            };

            var created = _newsService.Add(news);

            return Ok(ApiResponse<News>.SuccessResponse(
                created,
                "News added successfully"
            ));
        }
    }
}
