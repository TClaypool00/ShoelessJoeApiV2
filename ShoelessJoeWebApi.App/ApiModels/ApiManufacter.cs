using ShoelessJoeWebApi.App.ApiModels.Interfaces;
using ShoelessJoeWebApi.App.ApiModels.PostModels;

namespace ShoelessJoeWebApi.App.ApiModels
{
    public class ApiManufacter : PostAddress, IStateInfo
    {
        public int ManufacterId { get; set; }
        public string Name { get; set; }
        public bool IsApproved { get; set; }
        public string StateName { get; set; }
        public string StateAbr { get; set ; }
    }
}
