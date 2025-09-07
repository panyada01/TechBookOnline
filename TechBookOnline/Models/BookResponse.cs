using System.Collections.Generic;

namespace TechBookOnline.Models
{
    public class BookResponse
    {
        public string Error { get; set; }
        public string Total { get; set; }
        public string Page { get; set; }
        public List<BookItem> Books { get; set; }
    }
}
