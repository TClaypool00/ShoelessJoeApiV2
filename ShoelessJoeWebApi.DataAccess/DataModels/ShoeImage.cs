using System.ComponentModel.DataAnnotations;

namespace ShoelessJoeWebApi.DataAccess.DataModels
{
    public class ShoeImage
    {
        [Key]
        public int ImgGroupId { get; set; }

        [Required]
        [MaxLength(255)]
        public string LeftShoeRight { get; set; }
        [Required]
        [MaxLength(255)]
        public string LeftShoeLeft { get; set; }
        [Required]
        [MaxLength(255)]
        public string RightShoeRight { get; set; }
        [Required]
        [MaxLength(255)]
        public string RightShoeLeft { get; set; }

        public int ShoeId { get; set; }
        public Shoe Shoe { get; set; }
    }
}
