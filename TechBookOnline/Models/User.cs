namespace TechBookOnline.Models
{
    public class User
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? AvatarUrl { get; set; }
    }
}
