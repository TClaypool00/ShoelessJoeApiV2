using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShoelessJoeWebApi.DataAccess.DataModels
{
    public class State
    {
        [Key]
        public int StateId { get; set; }

        [Required]
        [MaxLength(75)]
        public string StateName { get; set; }

        [Required]
        [MaxLength(2)]
        public string StateAbr { get; set; }

        public List<Address> Addresses { get; set; }
    }
}
