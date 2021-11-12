using ShoelessJoeWebApi.App.ApiModels.Interfaces;
using ShoelessJoeWebApi.App.ApiModels.PostModels;

namespace ShoelessJoeWebApi.App.ApiModels
{
    public class ApiSchool : PostAddress, IStateInfo
    {
        public int SchoolId { get; set; }
        public string SchoolName { get; set; }
        public string StateName { get; set; }
        public string StateAbr { get; set; }
    }
}
