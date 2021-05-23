using System.Collections.Generic;

namespace ShoelessJoeWebApi.Core.CoreModels
{
    public class CoreSchool
    {
        public int SchoolId { get; set; }
        public string SchoolName { get; set; }

        public CoreAddress Address { get; set; }
        public int AddressId { get; set; }

        public List<CoreUser> Students { get; set; } = new List<CoreUser>();
    }
}
