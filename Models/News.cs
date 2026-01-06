using System;

namespace NewsAggregator.API.Models
{
    public class News
    {
        public int Id { get; set; }                // Primary Key

        public string Title { get; set; }           // News title

        public string Url { get; set; }             // News link

        public DateTime PublicationDate { get; set; } // Published date

        public string Source { get; set; }          // ET / TOI etc.

        public string? NewsImpact { get; set; }     // Positive / Negative / Archive

        public DateTime CreatedAt { get; set; }     // Insert date
    }
}
