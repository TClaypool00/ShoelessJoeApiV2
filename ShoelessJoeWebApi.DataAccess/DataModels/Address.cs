using System.ComponentModel.DataAnnotations;

namespace ShoelessJoeWebApi.DataAccess.DataModels
{
    public class Address
    {
        [Key]
        public int AddressId { get; set; }

        [Required]
        [MaxLength(150)]
        public string Street { get; set; }

        [Required]
        [MaxLength(100)]
        public string City { get; set; }

        public State State { get; set; }
        public int StateId { get; set; }

        [Required]
        [MaxLength(5)]
        public string ZipCode { get; set; }

        public Manufacter Manufacter { get; set; }
        public School School { get; set; }
    }
}
