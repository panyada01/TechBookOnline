using System.ComponentModel.DataAnnotations.Schema;

namespace TechBookOnline.Models
{
    public class UserLike
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int BookID { get; set; }   
        public DateTime LikedAt { get; set; }

        public User User { get; set; }
        public Book Book { get; set; }
    }


}
