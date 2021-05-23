using System.Collections.Generic;

namespace ShoelessJoeWebApi.App.ApiModels
{
    public class ApiStatesModelsManufacter
    {
        public List<ApiState> States { get; set; } = new List<ApiState>();
        public List<ApiManufacter> Manufacters { get; set; } = new List<ApiManufacter>();
        public List<ApiModel> Models { get; set; } = new List<ApiModel>();
    }
}
