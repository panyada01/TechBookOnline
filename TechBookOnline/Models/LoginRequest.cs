using Microsoft.AspNetCore.Mvc;

namespace TechBookOnline.Models
{
    public class LoginRequest : Controller
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
