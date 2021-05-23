using System.Collections.Generic;

namespace ShoelessJoeWebApi.Core.CoreModels
{
    public class CoreState
    {
        public int StateId { get; set; }
        public string StateName { get; set; }
        public string StateAbr { get; set; }

        public List<CoreManufacter> Manufacters { get; set; } = new List<CoreManufacter>();
    }
}
