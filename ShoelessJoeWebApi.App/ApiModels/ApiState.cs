using ShoelessJoeWebApi.App.ApiModels.PartialModels;

namespace ShoelessJoeWebApi.App.ApiModels
{
    public class ApiState : PartialState
    {
        public string StateName { get; set; }
        public string StateAbr { get; set; }
    }
}
