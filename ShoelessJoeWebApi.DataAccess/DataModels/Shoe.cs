using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShoelessJoeWebApi.DataAccess.DataModels
{
    public class Shoe
    {
        [Key]
        public int ShoeId { get; set; }
        public bool? BothShoes { get; set; } = false;
        public double? RightSize { get; set; } = 0;
        public double? LeftSize { get; set; } = 0;
        public bool IsSold { get; set; } = false;

        public User User { get; set; }
        public int UserId { get; set; }

        public Model Model { get; set; }
        public int ModelId { get; set; }

        public ShoeImage ShoeImage { get; set; }

        public List<Comment> Comments { get; set; }
    }
}
