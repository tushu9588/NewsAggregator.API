using System.ComponentModel.DataAnnotations;

namespace NewsAggregator.API.DTOs
{
    public class NewsCreateDto
    {
        [Required]
        [MinLength(5)]
        public string Title { get; set; }

        [Required]
        [Url]
        public string Url { get; set; }

        [Required]
        public DateTime PublicationDate { get; set; }

        [Required]
        public string Source { get; set; }
    }
}
