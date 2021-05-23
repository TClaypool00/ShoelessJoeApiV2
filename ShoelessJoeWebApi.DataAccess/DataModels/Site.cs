using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoelessJoeWebApi.DataAccess.DataModels
{
    [NotMapped]
    public class Site
    {
        [Key]
        public int SiteId { get; set; }
        [Required]
        [MaxLength(100)]
        public string SiteName { get; set; }
    }
}
