using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShoelessJoeWebApi.DataAccess.DataModels
{
    public class Model
    {
        [Key]
        public int ModelId { get; set; }

        [Required]
        [MaxLength(100)]
        public string ModelName { get; set; }

        public Manufacter Manufacter { get; set; }
        public int ManufacterId { get; set; }

        public List<Shoe> Shoes { get; set; }
    }
}
