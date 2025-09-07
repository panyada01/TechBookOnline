using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using TechBookOnline.Data;
using TechBookOnline.Models;

namespace TechBookOnline.Controllers
{
    public class UserLikesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserLikesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Add(int bookId)
{
    var user = HttpContext.Session.GetInt32("UserId");
    if (user == null)
    {
        // Store the bookId that the user wants to ❤️ in the session
        HttpContext.Session.SetInt32("PendingLikeBookId", bookId);

        return RedirectToAction("Login", "Auth");
    }

            // If logged in, save immediately
            SaveLike(user.Value, bookId);

    return RedirectToAction("Index", "Books");
}



        public void SaveLike(int userId, int bookId)
        {
            if (bookId <= 0)
            {
                throw new Exception("Invalid BookId.");
            }

            var book = _context.Books.FirstOrDefault(b => b.BookId == bookId);
            if (book == null)
            {
                throw new Exception($"Book with ID {bookId} not found in database.");
            }

            var existingLike = _context.UserLikes
                .FirstOrDefault(l => l.UserID == userId && l.BookID == bookId);

            if (existingLike == null)
            {
                _context.UserLikes.Add(new UserLike
                {
                    UserID = userId,
                    BookID = bookId,
                    LikedAt = DateTime.Now
                });
                _context.SaveChanges();
            }
        }




        // Favorites page
        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            var likes = _context.UserLikes
                                .Where(u => u.UserID == userId)
                                .Include(u => u.Book)   
                                .ToList();

            return View(likes);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFavorite(int bookId)
        {
            var userId = HttpContext.Session.GetInt32("UserId"); 
            if (userId == null)
                return RedirectToAction("Login", "Auth");

            var favorite = await _context.UserLikes
                .FirstOrDefaultAsync(u => u.BookID == bookId && u.UserID == userId);

            if (favorite != null)
            {
                _context.UserLikes.Remove(favorite);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Favorites"); 
        }


    }
}
