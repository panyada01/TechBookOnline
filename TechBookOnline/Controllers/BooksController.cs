using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TechBookOnline.Data;
using TechBookOnline.Models;

namespace TechBookOnline.Controllers
{
    public class BooksController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly ApplicationDbContext _context;

        public BooksController(IHttpClientFactory httpClientFactory, ApplicationDbContext context)
        {
            _httpClient = httpClientFactory.CreateClient();
            _context = context;
        }

        //Display book list
        public async Task<IActionResult> Index()
        {
            // If the DB has no data → fetch from API and store
            if (await _context.Books.CountAsync() == 0)
            {
                string apiUrl = "https://api.itbook.store/1.0/search/mysql";
                var response = await _httpClient.GetStringAsync(apiUrl);

                var result = JsonConvert.DeserializeObject<BookResponse>(response);

                if (result?.Books != null)
                {
                    foreach (var item in result.Books)
                    {
                        if (await _context.Books.CountAsync(b => b.ISBN13 == item.ISBN13) == 0)
                        {
                            _context.Books.Add(new Book
                            {
                                ISBN13 = item.ISBN13 ?? "",
                                Title = item.Title ?? "(No Title)",
                                Subtitle = item.Subtitle ?? "",
                                Price = item.Price?.Replace("$", "").Trim() ?? "0",
                                ImageUrl = item.Image ?? "/images/no-image.png",
                                Url = item.Url ?? "",
                                Authors = item.Authors ?? "Unknown",
                                Publisher = item.Publisher ?? "Unknown",
                                Year = item.Year ?? "N/A",
                                Desc = item.Desc ?? ""
                            });
                        }
                    }

                    await _context.SaveChangesAsync();
                }
            }

            // Fetch from DB to display in View
            var books = await _context.Books
                .Select(b => new Book
                {
                    BookId = b.BookId,
                    ISBN13 = b.ISBN13 ?? "",
                    Title = b.Title ?? "(No Title)",
                    Subtitle = b.Subtitle ?? "",
                    Price = b.Price ?? "0",
                    ImageUrl = b.ImageUrl ?? "/images/no-image.png",
                    Url = b.Url ?? "",
                    Authors = b.Authors ?? "Unknown",
                    Publisher = b.Publisher ?? "Unknown",
                    Year = b.Year ?? "N/A",
                    Desc = b.Desc ?? ""
                })
                .OrderBy(b => b.Title)
                .ToListAsync();

            return View(books);
        }

        // Book details
        public async Task<IActionResult> Details(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
                return NotFound();

            return View(book);
        }
    }
}
