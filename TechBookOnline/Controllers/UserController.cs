using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;
using TechBookOnline.Data;
using TechBookOnline.Models;

public class UsersController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _env;

    public UsersController(ApplicationDbContext context, IWebHostEnvironment env)
    {
        _context = context;
        _env = env;
    }

    public IActionResult Profile(int id)
    {
        var user = _context.Users.FirstOrDefault(u => u.ID == id);
        if (user == null)
        {
            return NotFound();
        }
        return View(user); 
    }
    
    [HttpGet]
    public IActionResult Edit(int id)
    {
        var user = _context.Users.FirstOrDefault(u => u.ID == id);
        if (user == null) return NotFound();
        return View(user);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, User model, IFormFile? AvatarFile)
    {
        var user = _context.Users.FirstOrDefault(u => u.ID == id);
        if (user == null) return NotFound();

        // Update field
        user.Fullname = model.Fullname;
        user.Username = model.Username;
        user.Email = model.Email;

        // Upload Avatar
        if (AvatarFile != null && AvatarFile.Length > 0)
        {
            var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(AvatarFile.FileName)}";
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                AvatarFile.CopyTo(stream);
            }

            user.AvatarUrl = "/uploads/" + fileName;
        }

        _context.SaveChanges();
        return RedirectToAction("Profile", new { id = user.ID });
    }
}
