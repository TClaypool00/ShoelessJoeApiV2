using System.Collections.Generic;

namespace ShoelessJoeWebApi.Core.CoreModels
{
    public class CoreManufacter
    {
        public int ManufacterId { get; set; }
        public string Name { get; set; }

        public CoreAddress Address { get; set; }
        public int AddressId { get; set; }

        public string ZipCode { get; set; }
        public bool IsApproved { get; set; }

        public List<CoreModel> Models { get; set; } = new List<CoreModel>();
    }
}
