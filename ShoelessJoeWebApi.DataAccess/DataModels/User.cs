using ShoelessJoeWebApi.DataAccess.DataModels.BillService;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShoelessJoeWebApi.DataAccess.DataModels
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(60)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(50)]
        public string Email { get; set; }
        [Required]
        [MaxLength(255)]
        public string Password { get; set; }
        public bool? IsAdmin { get; set; } = false;

        public School School { get; set; }
        public int? SchoolId { get; set; } = 0;

        public List<Shoe> Shoes { get; set; }
        public List<Comment> BuyerComments { get; set; }
        public List<Reply> Replies { get; set; }
        public List<Friend> RecieverFriends { get; set; }
        public List<Friend> SenderFriends { get; set; }
        public List<Post> Posts { get; set; }
        public List<CommentAndSeller> CommentAndSellers { get; set; }
        public List<Bill> Bills { get; set; }
    }
}
