using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoelessJoeWebApi.DataAccess.PartialClassees
{
    public class PartialPost
    {
        [Required]
        [MaxLength(255)]
        public string CommentBody { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime DatePosted { get; set; }
    }
}
