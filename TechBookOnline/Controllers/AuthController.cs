using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TechBookOnline.Data;
using TechBookOnline.Models;

namespace TechBookOnline.Controllers
{
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: Login
        [HttpPost("Login")]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginRequest request)
        {
            var user = _context.Users
                .FirstOrDefault(u => u.Username == request.Username && u.Password == request.Password);

            if (user != null)
            {
                HttpContext.Session.SetString("FullName", user.Fullname);
                HttpContext.Session.SetInt32("UserId", user.ID);

                // Check if there is a pending like (not yet saved in the system)
                var pendingBookId = HttpContext.Session.GetInt32("PendingLikeBookId");
                if (pendingBookId != null)
                {
                    // Replaced Any with FirstOrDefault to resolve Error #Dual
                    var book = _context.Books.FirstOrDefault(b => b.BookId == pendingBookId.Value);
                    if (book != null)
                    {
                        var like = _context.UserLikes
                            .FirstOrDefault(u => u.UserID == user.ID && u.BookID == pendingBookId.Value);

                        if (like == null)
                        {
                            _context.UserLikes.Add(new UserLike
                            {
                                UserID = user.ID,
                                BookID = pendingBookId.Value,
                                LikedAt = DateTime.Now
                            });
                            _context.SaveChanges();
                        }
                    }
                    else
                    {
                        // If no book with this Id → clear the value
                        HttpContext.Session.Remove("PendingLikeBookId");
                    }
                }

                // Redirect to Books after login
                return RedirectToAction("Index", "Books");
            }
            else
            {
                ViewBag.Error = "Invalid username or password";
                return View("Login", request);
            }
        }

        // GET: Login
        [HttpGet("Login")]
        public IActionResult Login()
        {
            return View(); // Views/Auth/Login.cshtml
        }

        // GET: Register
        [HttpGet("Register")]
        public IActionResult Register()
        {
            return View("Register"); // Register.cshtml
        }

        // POST: Register
        [HttpPost("Register")]
        [ValidateAntiForgeryToken]
        public IActionResult Register(User model, IFormFile? AvatarFile)
        {
            if (ModelState.IsValid)
            {
                // Check for duplicate username
                var existingUser = _context.Users.FirstOrDefault(u => u.Username == model.Username);
                if (existingUser != null)
                {
                    ModelState.AddModelError("Username", "Username นี้ถูกใช้แล้ว");
                    return View("Register", model);
                }

                // Upload avatar
                if (AvatarFile != null && AvatarFile.Length > 0)
                {
                    var fileName = Path.GetFileName(AvatarFile.FileName);
                    var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");

                    if (!Directory.Exists(uploadPath))
                        Directory.CreateDirectory(uploadPath);

                    var filePath = Path.Combine(uploadPath, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        AvatarFile.CopyTo(stream);
                    }

                    model.AvatarUrl = "/uploads/" + fileName;
                }
                else
                {
                    model.AvatarUrl = "/images/default-avatar.png";
                }

                model.CreatedAt = DateTime.Now;

                _context.Users.Add(model);
                _context.SaveChanges();

                return RedirectToAction("Login", "Auth");
            }

            return View("Register", model);
        }

        //GET: Logout
        [HttpGet("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Books");
        }
    }

    // Model for receiving login data
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
