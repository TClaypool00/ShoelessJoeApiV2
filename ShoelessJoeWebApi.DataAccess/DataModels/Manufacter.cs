using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShoelessJoeWebApi.DataAccess.DataModels
{
    public class Manufacter
    {
        [Key]
        public int ManufacterId { get; set; }
        [MaxLength(75)]
        public string Name { get; set; }

        public Address Address { get; set; }
        public int AddressId { get; set; }

        public bool IsApproved { get; set; } = false;

        public List<Model> Models { get; set; }
    }
}
