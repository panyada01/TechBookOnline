using System.ComponentModel.DataAnnotations;

namespace TechBookOnline.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }

        public string? ISBN13 { get; set; }
        public string? Title { get; set; }
        public string? Subtitle { get; set; }
        public string? Price { get; set; }
        public string? ImageUrl { get; set; }
        public string? Url { get; set; }
        public string? Authors { get; set; }
        public string? Publisher { get; set; }
        public string? Year { get; set; }
        public string? Desc { get; set; }
    }
}
