using ShoelessJoeWebApi.DataAccess.PartialClassees;
using System.ComponentModel.DataAnnotations;

namespace ShoelessJoeWebApi.DataAccess.DataModels
{
    public class Post : PartialPost
    {
        [Key]
        public int PostId { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
