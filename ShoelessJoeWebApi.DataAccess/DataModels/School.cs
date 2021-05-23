using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShoelessJoeWebApi.DataAccess.DataModels
{
    public class School
    {
        [Key]
        public int SchoolId { get; set; }
        [Required]
        [MaxLength(75)]
        public string SchoolName { get; set; }

        public Address Address { get; set; }
        public int AddressId { get; set; }

        public List<User> Students { get; set; }
    }
}
